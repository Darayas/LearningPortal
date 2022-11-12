using Serilog;
using Serilog.Events;
using System;
using ILogger = LearningPortal.Framework.Contracts.ILogger;

namespace LearningPortal.Infrastructure.Serilog
{
    public class Serilogger : ILogger
    {
        public Serilogger()
        {
            Log.Logger = new SerilogConfig().ConfigForSQLServer(LogEventLevel.Warning).CreateLogger();
        }
        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Debug(string message, params object[] parameters)
        {
            Log.Debug(message, parameters);
        }

        public void Debug(Exception exception)
        {
            Log.Debug(exception, exception.Message);
        }

        public void Debug(Exception exception, string message)
        {
            Log.Debug(exception, message);
        }

        public void Debug(Exception exception, string message, params object[] parameters)
        {
            Log.Debug(exception, message, parameters);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(string message, params object[] parameters)
        {
            Log.Error(message, parameters);
        }

        public void Error(Exception exception)
        {
            Log.Error(exception, exception.Message);
        }

        public void Error(Exception exception, string message)
        {
            Log.Error(exception, message);
        }

        public void Error(Exception exception, string message, params object[] parameters)
        {
            Log.Error(exception.Message, message, parameters);
        }

        public void Fatal(string message)
        {
            Log.Fatal(message);
        }

        public void Fatal(string message, params object[] parameters)
        {
            Log.Fatal(message, parameters);
        }

        public void Fatal(Exception exception)
        {
            Log.Fatal(exception, exception.Message);
        }

        public void Fatal(Exception exception, string message)
        {
            Log.Fatal(exception, message);
        }

        public void Fatal(Exception exception, string message, params object[] parameters)
        {
            Log.Fatal(exception, message, parameters);
        }

        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Information(string message, params object[] parameters)
        {
            Log.Information(message, parameters);
        }

        public void Information(Exception exception)
        {
            Log.Information(exception, exception.Message);
        }

        public void Information(Exception exception, string message)
        {
            Log.Information(exception, message);
        }

        public void Information(Exception exception, string message, params object[] parameters)
        {
            Log.Information(exception, message, parameters);
        }

        public void Warning(string message)
        {
            Log.Warning(message);
        }

        public void Warning(string message, params object[] parameters)
        {
            Log.Warning(message, parameters);
        }

        public void Warning(Exception exception)
        {
            Log.Warning(exception, exception.Message);
        }

        public void Warning(Exception exception, string message)
        {
            Log.Warning(exception, message);
        }

        public void Warning(Exception exception, string message, params object[] parameters)
        {
            Log.Warning(exception, message, parameters);
        }
    }
}
