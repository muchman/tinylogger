using System;
using System.Collections.Generic;
using System.Text;

namespace TinyLogger.Interfaces
{
    public interface ITinyLogger
    {
        string AppName { get; set; }

        void LogWarning(string message);

        void LogInformation(string message);

        void LogError(string message, Exception exception);

        void LogError(string message, Guid errorId, string statusCode, Exception exception);
    }
}
