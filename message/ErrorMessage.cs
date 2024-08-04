using System.Collections.Generic;

public class ErrorMessage : StompMessage
{
    public ErrorMessage(string error, Dictionary<string, string> headers) : base(StompCommand.ERROR, error, headers)
    {
    }
}