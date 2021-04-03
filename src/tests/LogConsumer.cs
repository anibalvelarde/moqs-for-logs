using System;
using Microsoft.Extensions.Logging;

namespace aev.moqforlogs.tests
{
    public class LogConsumer
    {
        private readonly ILogger<LogConsumer> _logger;

        public LogConsumer(ILogger<LogConsumer> logger)
        {
            _logger = logger;
        }

        public void DoSomethingAndLogInfo(int value)
        {
            _logger.LogInformation($"Before operation with Value: {value}");
            var x = value + 2;
            _logger.LogInformation($"After operation with Value: {value}");
        }

        public void DoSomethingAndLogDebug(int value)
        {
            _logger.LogDebug($"Before operation with Value: {value}");
            var x = value + 2;
            _logger.LogDebug($"After operation with Value: {value}");
        }

        public void DoSomethingAndLogError(int value)
        {
            _logger.LogError($"Before operation with Value: {value}");
            var x = value + 2;
            _logger.LogError($"After operation with Value: {value}");
        }

        public void DoManyLogTypes()
        {
            _logger.LogInformation("Logging some information");
            _logger.LogDebug("Logging some debug");
            _logger.LogError("Logging some error");
        }

        public void DoManyLogTypes(int logItThisManyTimes = 0)
        {
            for (int i = 0; i < logItThisManyTimes; i++)
            {
                _logger.LogInformation("Logging some information");
            }

            for (int i = 0; i < logItThisManyTimes; i++)
            {
                _logger.LogDebug("Logging some debug");
            }

            for (int i = 0; i < logItThisManyTimes; i++)
            {
                _logger.LogError("Logging some error");
            }
        }

        public void DoManyLogTypesOfCertainLogLevelKind(LogLevel expLogLevel, int logItThisManyTimes = 1)
        {
            switch (expLogLevel)
            {
                case LogLevel.Information:
                    for (int i = 0; i < logItThisManyTimes; i++)
                    {
                        _logger.LogInformation("Logging some information");
                    }
                    break;

                case LogLevel.Debug:
                    for (int i = 0; i < logItThisManyTimes; i++)
                    {
                        _logger.LogDebug("Logging some debug");
                    }
                    break;

                case LogLevel.Error:
                    for (int i = 0; i < logItThisManyTimes; i++)
                    {
                        _logger.LogError("Logging some error");
                    }
                    break;
                default:
                    throw new NotSupportedException($"{expLogLevel} is not supported.");
            }
        }
    }
}