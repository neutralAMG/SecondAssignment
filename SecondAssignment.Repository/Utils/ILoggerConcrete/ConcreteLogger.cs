using Microsoft.Extensions.Logging;

namespace SecondAssignment.Infraestructure.Utils.ILoggerConcrete
{
    public class ConcreteLogger<T> : IConcreteLogger
        where T : class
    {
        private readonly ILogger<T> _logger;

        public ConcreteLogger(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void LogCritical(string message)
        {
           _logger.LogCritical(message);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWargning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
