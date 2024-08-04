using System.Collections.Generic;
using Newtonsoft.Json;

public abstract class StompMessage
{

    public string Command { get; private set; }
    
    public string Body { get; private set; }

    public Dictionary<string, string> Headers { get; }

    public string this[string header]
    {
        get => Headers.ContainsKey(header) ? Headers[header] : string.Empty;
        set => Headers[header] = value;
    }

    protected StompMessage(string command, object body, Dictionary<string, string> headers)
    {
        Command = command;
        Body = JsonConvert.SerializeObject(body);
        Headers = headers;
    }
    
}