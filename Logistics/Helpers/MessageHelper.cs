namespace Logistics.Helpers
{
    public enum MessageType
    {
        Info,
        Success,
        Warning,
        Error,
        None
    }

    public class MessageHelper
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public MessageHelper()
        {
        }

        public MessageHelper(bool result, string message)
        {
            MessageType tmp = result ? MessageType.Success : MessageType.Error;
            this.Type = tmp.ToString().ToLower();
            this.Title = tmp.ToString();
            this.Message = message;
        }
    }
}