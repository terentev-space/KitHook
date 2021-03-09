using System.Runtime.Serialization;

namespace KitHook.Mediators.QueueSenderMediators.Interfaces
{
    public interface IQueueHttpMessageContent
    {
        public ContentType Type { get; }

        public enum ContentType
        {
            [EnumMember(Value = "none")] None,
            [EnumMember(Value = "form")] Form,
            [EnumMember(Value = "text")] Text,
            [EnumMember(Value = "json")] Json,
            [EnumMember(Value = "byte")] Byte,
            [EnumMember(Value = "else")] Else,
        }
    }
}