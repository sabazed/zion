using System.Collections.Generic;
using System.IO;
using System.Text;

public class MessageConverter
{
    private readonly IMessageTypeResolver _messageTypeResolver;

    public MessageConverter(IMessageTypeResolver messageTypeResolver)
    {
        _messageTypeResolver = messageTypeResolver;
    }

    public string Serialize(StompMessage message, bool isEmpty = false)
    {
        var buffer = new StringBuilder();
        buffer.Append(message.Command + "\n");

        foreach (var header in message.Headers)
        {
            buffer.Append(header.Key + ":" + header.Value + "\n");
        }
        buffer.Append('\n');
        
        if (!isEmpty) buffer.Append(message.Body);
        buffer.Append('\0');
        
        return buffer.ToString();
    }

    public StompMessage Deserialize(string message)
    {
        var reader = new StringReader(message);
        var command = reader.ReadLine();
        if (command == null)
            throw new InvalidDataException($"Corrupted payload received: ${message}");
        

        var headers = new Dictionary<string, string>();
        var header = reader.ReadLine();
        var messageType = typeof(string);
        while (!string.IsNullOrEmpty(header))
        {
            var split = header.Split(':');
            if (split.Length == 2)
            {
                var headerName = split[0].Trim();
                var headerVal = split[1].Trim();
                headers[headerName] = headerVal;
                if (headerName.Equals("message-type"))
                    messageType = _messageTypeResolver.ResolveMessageType(headerVal) ?? messageType;
            }
            header = reader.ReadLine() ?? string.Empty;
        }

        var body = reader.ReadToEnd() ?? string.Empty;
        body = body.TrimEnd('\r', '\n', '\0');

        StompMessage result = null;
        if (command.Equals(StompCommand.MESSAGE))
            result = new ReceiveMessage(body, messageType, headers);
        else if (command.Equals(StompCommand.ERROR))
            result = new ErrorMessage(body, headers);
        else if (command.Equals(StompCommand.CONNECTED))
            result = new ConnectedMessage(body, headers);
        
        if (result == null)
            throw new InvalidDataException($"Unable to construct object from the payload: ${message}");
        
        return result;
    }
}