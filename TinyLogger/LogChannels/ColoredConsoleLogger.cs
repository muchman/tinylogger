using System;
using System.Collections.Generic;
using System.Text;
using TinyLogger.Enums;
using TinyLogger.Interfaces;

namespace TinyLogger.LogChannels
{
    //this thing just prints colored text to the console based on the loglevel
    public class ColoredConsoleLogChannel : ITinyLoggerChannel
    {
        public void Log(LogPayload payload)
        {
            var currentColor = Console.ForegroundColor;
            var message = new StringBuilder();

            Console.ForegroundColor = GetConsoleColor(payload.Level);

            message.Append($"{payload.LevelName}: RefId: {payload.ReferenceId} - ");

            if (!string.IsNullOrWhiteSpace(payload.StatusCode))
            {
                message.Append($"HTTPCode: {payload.StatusCode} - ");
            }

            message.Append($"{payload.Message} - {Environment.NewLine}");

            if (payload.ExceptionItem != null)
            {
                message.Append($"{payload.LevelName} - {payload.ExceptionItem.ToString()}");
            }

            Console.WriteLine(message.ToString());

            Console.ForegroundColor = currentColor;
        }

        private ConsoleColor GetConsoleColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error: return ConsoleColor.Red;
                case LogLevel.Info: return ConsoleColor.Green;
                case LogLevel.Warning: return ConsoleColor.Yellow;
                default: return ConsoleColor.Blue;
            }
        }
    }
}
