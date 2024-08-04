using System;
using System.Collections.Generic;

public class ReceiveMessage : StompMessage
{
    public ReceiveMessage(string message, Type messageType, Dictionary<string, string> headers) : base(StompCommand.MESSAGE, message, headers)
    {
        MessageType = messageType;
    }

    public Type MessageType { get; }
}