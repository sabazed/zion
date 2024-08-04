using System.Collections.Generic;

public class SendMessage : StompMessage
{
    public SendMessage(object message, string destination) 
        : base(StompCommand.SEND, message, new Dictionary<string, string>())
    {
        this["destination"] = destination;
        this["content-length"] = Body.Length.ToString();
    }
}