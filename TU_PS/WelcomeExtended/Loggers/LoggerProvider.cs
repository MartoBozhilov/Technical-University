using Microsoft.Extensions.Logging;

namespace WelcomeExtended.Loggers
{
    class LoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new HashLogger(categoryName);
        }

        public void Dispose()
        {

        }
    }
}
