using System;
using NLog;
using RedWood.Interface.Debug;

namespace RedWood.Implementation.Debug
{   
    public class Logger : ILogger
    {
        readonly Autofac.Extras.NLog.ILogger _logger;

        public Logger(Autofac.Extras.NLog.ILogger logger)
        {
            _logger = logger;
        }

        public void Debug(string message, params object[] args)
        {
           _logger.Debug(message, args);
        }

        public void DebugException(string message, Exception exception)
        {
            _logger.LogException(LogLevel.Debug, message, exception);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void ErrorException(string message, Exception exception)
        {
            _logger.LogException(LogLevel.Error, message, exception);
        }

        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void InfoException(string message, Exception exception)
        {
            _logger.LogException(LogLevel.Info, message, exception);
        }

        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public void WarnException(string message, Exception exception)
        {
            _logger.LogException(LogLevel.Warn, message, exception);
        }

        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public void FatalException(string message, Exception exception)
        {
            _logger.LogException(LogLevel.Fatal, message, exception);
        }

        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public void TraceException(string message, Exception exception)
        {
            _logger.LogException(LogLevel.Trace, message, exception);
        }
    }
}