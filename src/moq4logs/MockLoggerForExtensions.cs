using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace aev.moqforlogs
{
    public static class MockLoggerForExtensions
    {
        private static readonly Func<object, string, bool> state = (v, s) =>
        {
            return v.ToString().Contains(s);
        };

        public static Mock<ILogger<T>> VerifyInfoWasCalledWith<T>(this Mock<ILogger<T>> logger, string expectedLogMessageFragment, int callCount = 1)
        {
            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(lvl => lvl == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, expectedLogMessageFragment)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Exactly(callCount));

            return logger;
        }

        public static Mock<ILogger<T>> VerifyDebugWasCalledWith<T>(this Mock<ILogger<T>> logger, string expectedLogMessageFragment, int callCount = 1)
        {
            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(lvl => lvl == LogLevel.Debug),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, expectedLogMessageFragment)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Exactly(callCount));

            return logger;
        }

        public static Mock<ILogger<T>> VerifyErrorWasCalledWith<T>(this Mock<ILogger<T>> logger, string expectedLogMessageFragment, int callCount = 1)
        {
            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(lvl => lvl == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, expectedLogMessageFragment)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Exactly(callCount));

            return logger;
        }

        public static Mock<ILogger<T>> VerifyLogLevelMeetsCallCount<T>(this Mock<ILogger<T>> logger, LogLevel expectedLogLevel, int callCount)
        {
            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(lvl => lvl == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Exactly(callCount));

            return logger;
        }

        public static Mock<ILogger<T>> VerifyNoLogsWereCalled<T>(this Mock<ILogger<T>> logger)
        {
            logger.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Never);

            return logger;
        }

        public static Mock<ILogger<T>> VerifyOnlySingleLogLevelWasCalled<T>(this Mock<ILogger<T>> logger, LogLevel expectedLogLevel)
        {
            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(lvl => lvl == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.AtLeastOnce);

            return logger;
        }

        public static Mock<ILogger<T>> VerifyOnlyDebugLogsWereCalled<T>(this Mock<ILogger<T>> logger)
        {
            return logger.VerifyOnlySingleLogLevelWasCalled(LogLevel.Debug);
        }

        public static Mock<ILogger<T>> VerifyOnlyInfoLogsWereCalled<T>(this Mock<ILogger<T>> logger)
        {
            return logger.VerifyOnlySingleLogLevelWasCalled(LogLevel.Information);
        }

        public static Mock<ILogger<T>> VerifyOnlyErrorLogsWereCalled<T>(this Mock<ILogger<T>> logger)
        {
            return logger.VerifyOnlySingleLogLevelWasCalled(LogLevel.Error);
        }
    }
}
