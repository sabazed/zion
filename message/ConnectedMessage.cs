using System.Collections.Generic;
using zion.message;

public class ConnectedMessage : StompMessage
{
    public ConnectedMessage(string message, Dictionary<string, string> headers) : base(StompCommand.CONNECTED, message, headers)
    {
    }
}