using System;
using System.Collections.Generic;
using System.Text;
using TinyLogger.Enums;

namespace TinyLogger
{
    public class LogPayload
    {
        public string Message { get; }
        public Exception ExceptionItem { get; }
        public LogLevel Level { get; }
        public string LevelName => Enum.GetName(typeof(LogLevel), Level);
        public DateTime LogDateTime { get; }
        public Guid ReferenceId { get; }
        public string StatusCode { get; }

        public LogPayload(string message, LogLevel level, Guid referenceId, string statusCode = null, Exception exception = null)
        {
            Message = message;
            Level = level;
            ExceptionItem = exception;
            LogDateTime = DateTime.Now;
            StatusCode = statusCode;
            ReferenceId = referenceId;
        }
    }
}
