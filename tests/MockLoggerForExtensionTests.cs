using System;
using Microsoft.Extensions.Logging;
using Moq;
using moqforlogs;
using Xunit;

namespace moqforlogs.tests
{
    public class MockLoggerForExtensionTests
    {
        [Fact]
        public void Should_Verify_When_Logging_For_Info_Was_Called()
        {
            // arrange
            var expValue = 1;
            var fakeLogger = Mock.Of<ILogger<LogConsumer>>();
            var sut = new LogConsumer(fakeLogger);

            // act
            sut.DoSomethingAndLogInfo(expValue);

            // assert
            var mockLogger = Mock.Get<ILogger<LogConsumer>>(fakeLogger);
            mockLogger.VerifyInfoWasCalledWith($"Before operation with Value: {expValue}");
            mockLogger.VerifyInfoWasCalledWith($"After operation with Value: {expValue}");
        }

        [Fact]
        public void Should_Verify_When_Logging_For_Debug_Was_Called()
        {
            // arrange
            var expValue = 1;
            var fakeLogger = Mock.Of<ILogger<LogConsumer>>();
            var sut = new LogConsumer(fakeLogger);

            // act
            sut.DoSomethingAndLogDebug(expValue);

            // assert
            var mockLogger = Mock.Get<ILogger<LogConsumer>>(fakeLogger);
            mockLogger.VerifyDebugWasCalledWith($"Before operation with Value: {expValue}");
            mockLogger.VerifyDebugWasCalledWith($"After operation with Value: {expValue}");
        }

        [Fact]
        public void Should_Verify_When_Logging_For_Error_Was_Called()
        {
            // arrange
            var expValue = 1;
            var fakeLogger = Mock.Of<ILogger<LogConsumer>>();
            var sut = new LogConsumer(fakeLogger);

            // act
            sut.DoSomethingAndLogError(expValue);

            // assert
            var mockLogger = Mock.Get<ILogger<LogConsumer>>(fakeLogger);
            mockLogger.VerifyErrorWasCalledWith($"Before operation with Value: {expValue}");
            mockLogger.VerifyErrorWasCalledWith($"After operation with Value: {expValue}");
        }

        [Theory]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Error)]
        public void Should_Verify_When_Logging_Multiple_Log_Kinds(LogLevel testLevel)
        {
            // arrange
            var fakeLogger = Mock.Of<ILogger<LogConsumer>>();
            var sut = new LogConsumer(fakeLogger);

            // act
            sut.DoManyLogTypesOfCertainLogLevelKind(testLevel);

            // assert
            var mockLogger = Mock.Get<ILogger<LogConsumer>>(fakeLogger);
            mockLogger.VerifyLogLevelMeetsCallCount(testLevel, 1);
        }

        [Fact]
        public void Should_Verify_When_Logging_Multiple_Log_Kinds_Multiple_Times()
        {
            // arrange
            var expLogCount = 3;
            var fakeLogger = Mock.Of<ILogger<LogConsumer>>();
            var sut = new LogConsumer(fakeLogger);

            // act
            sut.DoManyLogTypes(expLogCount);

            // assert
            var mockLogger = Mock.Get<ILogger<LogConsumer>>(fakeLogger);
            mockLogger.VerifyLogLevelMeetsCallCount(LogLevel.Information, expLogCount);
            mockLogger.VerifyLogLevelMeetsCallCount(LogLevel.Debug, expLogCount);
            mockLogger.VerifyLogLevelMeetsCallCount(LogLevel.Error, expLogCount);
        }

        [Fact]
        public void Should_Verify_No_Logs_Were_Called()
        {
            // arrange
            var expLogCount = 0;
            var fakeLogger = Mock.Of<ILogger<LogConsumer>>();
            var sut = new LogConsumer(fakeLogger);

            // act
            sut.DoManyLogTypes(expLogCount);

            // assert
            var mockLogger = Mock.Get<ILogger<LogConsumer>>(fakeLogger);
            mockLogger.VerifyNoLogsWereCalled();
        }

        [Fact]
        public void Should_Verify_Only_DebugLogs_Were_Called()
        {
            // arrange
            var expLogCount = 5;
            var fakeLogger = Mock.Of<ILogger<LogConsumer>>();
            var sut = new LogConsumer(fakeLogger);

            // act
            sut.DoManyLogTypesOfCertainLogLevelKind(LogLevel.Debug, expLogCount);

            // assert
            var mockLogger = Mock.Get<ILogger<LogConsumer>>(fakeLogger);
            mockLogger.VerifyOnlyDebugLogsWereCalled();
        }

        [Fact]
        public void Should_Verify_Only_InfoLogs_Were_Called()
        {
            // arrange
            var expLogCount = 5;
            var fakeLogger = Mock.Of<ILogger<LogConsumer>>();
            var sut = new LogConsumer(fakeLogger);

            // act
            sut.DoManyLogTypesOfCertainLogLevelKind(LogLevel.Information, expLogCount);

            // assert
            var mockLogger = Mock.Get<ILogger<LogConsumer>>(fakeLogger);
            mockLogger.VerifyOnlyInfoLogsWereCalled();
        }

        [Fact]
        public void Should_Verify_Only_ErrorLogs_Were_Called()
        {
            // arrange
            var expLogCount = 5;
            var fakeLogger = Mock.Of<ILogger<LogConsumer>>();
            var sut = new LogConsumer(fakeLogger);

            // act
            sut.DoManyLogTypesOfCertainLogLevelKind(LogLevel.Error, expLogCount);

            // assert
            var mockLogger = Mock.Get<ILogger<LogConsumer>>(fakeLogger);
            mockLogger.VerifyOnlyErrorLogsWereCalled();
        }
    }
}
