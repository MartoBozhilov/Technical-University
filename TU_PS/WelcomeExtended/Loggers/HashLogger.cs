using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace WelcomeExtended.Loggers
{
    class HashLogger : ILogger
    {
        private readonly ConcurrentDictionary<int, string> _logMessages;
        private readonly string _name;

        public HashLogger(string name)
        {
            _name = name;
            _logMessages = new ConcurrentDictionary<int, string>();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);
            switch (logLevel)
            {
                case LogLevel.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine("- LOGGER -");
            var messageToBeLogged = new StringBuilder();
            messageToBeLogged.Append($"[{logLevel}]");
            messageToBeLogged.AppendFormat("[{0}]",_name);
            Console.WriteLine(messageToBeLogged);
            Console.WriteLine($" {formatter(state, exception)}");
            Console.WriteLine("- LOGGER -");
            Console.ResetColor();
            _logMessages[eventId.Id] = message;
        }
    
        public void LogAll()
        {
            foreach (var eventId in _logMessages.Keys)
            {
                Console.WriteLine("- LOGGER -");
                var messageToBeLogged = new StringBuilder();
                messageToBeLogged.Append($"[{eventId}]");
                messageToBeLogged.AppendLine($"[{_logMessages[eventId]}]");
                Console.WriteLine(messageToBeLogged);
                Console.WriteLine("- LOGGER -");
            }
        }

        public string LogAllToString()
        {
            var messageToBeLogged = new StringBuilder();
            foreach (var eventId in _logMessages.Keys)
            {
                messageToBeLogged.AppendLine("- LOGGER -")
                                 .AppendLine($"[{eventId}]")
                                 .AppendLine($"[{_logMessages[eventId]}]")
                                 .AppendLine("- LOGGER -");
            }
            return messageToBeLogged.ToString().TrimEnd();
        }

        public void LogElement(EventId eventId)
        {
                Console.WriteLine("- LOGGER -");
                var messageToBeLogged = new StringBuilder();
                messageToBeLogged.Append($"[{eventId.Id}]");
                messageToBeLogged.AppendLine($"[{_logMessages[eventId.Id]}]");
                Console.WriteLine(messageToBeLogged);
                Console.WriteLine("- LOGGER -");
        }

        public void LogElement(int eventId)
        {
                Console.WriteLine("- LOGGER -");
                var messageToBeLogged = new StringBuilder();
                messageToBeLogged.Append($"[{eventId}]");
                messageToBeLogged.AppendLine($"[{_logMessages[eventId]}]");
                Console.WriteLine(messageToBeLogged);
                Console.WriteLine("- LOGGER -");
        }

        public void LogDeleteElement(EventId eventId)
        {
            _logMessages.Remove(eventId.Id,out string? message);
            
            if (message == null)
                Console.WriteLine("No such element!");
            else
                Console.WriteLine($"The element with id : [{eventId.Id}] and message : [{message}] was removed!");
        }

        public void LogDeleteElement(int eventId)
        {
            _logMessages.Remove(eventId, out string? message);

            if (message == null)
                Console.WriteLine("No such element!");
            else
                Console.WriteLine($"The element with id : [{eventId}] and message : [{message}] was removed!");
        }
    }
}
