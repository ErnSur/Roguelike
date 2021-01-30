using LDF.EventSystem;

namespace LDF.Roglik.UI
{
    public static class Events
    {
        public class LogMessage : Event<LogMessage>
        {
            public readonly string Message;

            public LogMessage(string message) => Message = message;

            public static implicit operator LogMessage(string message)
            {
                return new LogMessage(message);
            }

            public static implicit operator string(LogMessage message)
            {
                return message.Message;
            }
        }
    }
}