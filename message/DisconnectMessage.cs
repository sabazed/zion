using System.Collections.Generic;
using zion.message;

public class DisconnectMessage : StompEmptyMessage
{
    public DisconnectMessage() : base(StompCommand.DISCONNECT, string.Empty, new Dictionary<string, string>())
    {
    }
}