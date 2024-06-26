﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Tools.Logging
{
    public class LoggerEx<T> : ILogger<T>
    {
        private readonly ILogger _logger;

        public LoggerEx(ILoggerFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            _logger = factory.CreateLogger(typeof(T).Assembly.GetName().Name!);
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state)!;
        }

        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter!);
        }
    }
}
