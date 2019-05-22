using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TinyLogger.Enums;
using TinyLogger.Interfaces;

namespace TinyLogger
{
    //a central container for logging channels.
    //This is what the program actually calls to log and it
    //uses its available to channels to log the message
    public class TinyLogger : ITinyLogger
    {
        public string AppName { get; set; }
        public LogLevel AppLogLevel { get; set; }
        private IEnumerable<ITinyLoggerChannel> Channels { get; set; }

        public TinyLogger(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            var logConfig = configuration.GetSection("Logging");

            AppName = logConfig?.GetSection("AppName").Value;
            AppLogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), logConfig.GetSection("LogLevel").GetSection("Default").Value);

            Channels = serviceProvider.GetServices<ITinyLoggerChannel>();
        }

        public void LogError(string message, Exception exception)
        {
            Log(message, LogLevel.Error, Guid.NewGuid(), null, exception);
        }

        public void LogError(string message, Guid referenceId, string statusCode, Exception exception)
        {
            Log(message, LogLevel.Error, referenceId, statusCode, exception);
        }

        public void LogInformation(string message)
        {
            Log(message, LogLevel.Info, Guid.NewGuid());
        }

        public void LogWarning(string message)
        {
            Log(message, LogLevel.Warning, Guid.NewGuid());
        }

        //TODO We should look into async or some kind of queue if this starts taking to much time.
        private void Log(string message, LogLevel level, Guid referenceId, string statusCode = null, Exception exception = null)
        {
            //short curcuit so we can skip logging if it doesnt matter
            if (level != LogLevel.Error && AppLogLevel != LogLevel.Debug && AppLogLevel != level)
            {
                return;
            }
            var payload = new LogPayload(message, level, referenceId, statusCode, exception);

            foreach (var Channel in Channels)
            {
                //this empty try catch is just for safety incase a channel blows up.  We cant really do anything since
                //if the logger is exceptioning there is no more hope
                try
                {
                    Channel.Log(payload);
                }
                catch { }
            }
        }
    }
}
