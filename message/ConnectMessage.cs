using System.Collections.Generic;
using zion.message;

public class ConnectMessage : StompEmptyMessage
{
    public ConnectMessage()
        : base(StompCommand.CONNECT, string.Empty, new Dictionary<string, string>())
    {
        this["accept-version"] = "1.2";
    }
}