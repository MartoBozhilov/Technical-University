using Microsoft.Extensions.Logging;

namespace WelcomeExtended.Loggers
{
    class LoggerToFileProvider : ILoggerProvider
    {
        private HashLogger logger;

        public ILogger CreateLogger(string categoryName)
        {
            logger = new HashLogger(categoryName);
            return logger;
        }

        public void Dispose()
        {
            File.WriteAllText("./logs.txt",logger.LogAllToString());
        }
    }
}
