using System.Collections.Generic;
using zion.message;

public class SubscribeMessage : StompEmptyMessage
{
    public SubscribeMessage(string destination, string subId) 
        : base(StompCommand.SUBSCRIBE, string.Empty, new Dictionary<string, string>())
    {
        this["destination"] = destination;
        this["id"] = subId;
    }
}