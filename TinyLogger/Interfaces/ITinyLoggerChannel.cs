using System;
using System.Collections.Generic;
using System.Text;

namespace TinyLogger.Interfaces
{
    public interface ITinyLoggerChannel
    {
        void Log(LogPayload payload);
    }
}
