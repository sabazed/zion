public class StompCommand
{
    //Client Commands
    public const string CONNECT = "CONNECT";
    public const string DISCONNECT = "DISCONNECT";        
    public const string SEND = "SEND";
    public const string SUBSCRIBE = "SUBSCRIBE";

    //Server Responses
    public const string CONNECTED = "CONNECTED";
    public const string MESSAGE = "MESSAGE";
    public const string ERROR = "ERROR";
}