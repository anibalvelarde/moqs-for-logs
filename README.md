# moq-for-logs

If you are a fan of mocking dependencies with [`MOQ`](https://github.com/moq/moq4#moq) and some of your "Subjects Under Test" use the `Microsoft.Extensions.Logging.ILogger` extension methods... You know how irritating is to get the "Moq does not do extension methods" error.

Well, this library, should help you add some syntactic sugar to ease up your extension method mocking & verfication pains.

## Example:

Say you have a class, called `MyClass` that uses `ILogger` extension method functionality as in this example below.
Further imagine that you are using `ILogger` to add some debug traces in your code like so:

```cs
    public class MyClass
    {
        private readonly ILogger<MyClass> _logger;

        public MyClass(ILogger<MyClass> logger)
        {
            _logger = logger;
        }

        public void DoSomething()
        {
            // some code
            ...

            // more code
            ...
            _logger.Debug("some debug message there...");

            // more code
            _logger.Debug("before calling serivce X");
            CallServiceX()
            _logger.Debug("after calling service X");
        }
    }
```

When you put this class under test you could write a unit test like the following:

```cs
        [Fact]
        public void Should_Verify_When_Logging_For_Info_Was_Called()
        {
            // arrange
            var expValue = 1;
            var mockLogger = new Mock<ILogger<MyClass>>(MockBehavior.Strict);
            mockLogger
               .Setup(_ => _.Debug(It.IsAny<string>()))          //  <--- Moq does not support Extension Methods!
               .Setup(_ => _.Information(It.IsAny<string>()));   //  <--- Moq does not support Extension Methods!
            var sut = new MyClass(mockLogger);

            // act
            sut.DoSomethingAndLogInfo(expValue);

            // assert
            mockLogger.VerifyAll();
        }
```

Then you sadly find out that `Moq` does not support Extension Methods, because [_reasons_](https://github.com/Moq/moq4/issues/189).

If you spend some time on-line you'll find ways to work around this problem. Another way, is to use Moq-For-Logs and keep on coding, like so:

```cs
        [Fact]
        public void Should_Verify_When_Logging_For_Info_Was_Called()
        {
            // arrange
            var expValue = 1;
            var fakeLogger = Mock.Of<ILogger<MyClass>>();
            var sut = new MyClass(mockLogger);

            // act
            sut.DoSomethingAndLogInfo(expValue);

            // assert
            var mockLogger = Mock.Get<ILogger<MyClass>>(fakeLogger);
            mockLogger.VerifyOnlyDebugLogsWereCalled();
        }
```

For more examples of how to use it, check out the [`Unit Tests`](./tests/MockLoggerForExtensionTests.cs) section in this repo.
I hope you try it. I hope you like using it!
