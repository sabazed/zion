# Zion
Zion is a websocket client build onto Websocket4NET to support STOMP protocol. It includes basic operations like connecting, subscribing, sending message and disconnecting. The project was created for Unity applications, but with slight modifications can be used in a different context as well.

## How It Works
`ZionClient` - main client class that gets uri, headers and a message resolver as parameters. Message resolver is used to be able to distinguish received payload type, based on a provided custom header `message-type`. If you do not wish to use this header, you can modify the source code.
For the current implementation, you need to have a concrete class for IMessageTypeResolver, that would have a single method returning a type based on the string input.

## Example usage
```c#
private void Awake()
{
    var headers = new List<KeyValuePair<string, string>> { new("key", "value") };
    client = new ZionClient("ws://localhost:8080", new MessageTypeResolver(), headers);

    client.OnOpen = () => Debug.Log("Connected to the WebSocket server.");

    client.OnError = ex => Debug.Log($"Error: {ex.Message}");

    client.OnMessage = message =>
    {
        Debug.Log($"Received {message.Command} message: {message.Body}, headers: {message.Headers.Count}");
        if (message.GetType() == typeof(ReceiveMessage))
        {
            var receiveMessage = (ReceiveMessage)message;
            var obj = JsonConvert.DeserializeObject(receiveMessage.Body, receiveMessage.MessageType);
            Debug.Log(obj);
        }
    };

    var request = new GetMarksRequest();
    client.SendMessage(request, MARK_GET_ENDPOINT);
}
```

## Important Notes

- As the client is not yet advanced in certain ways, I suggest to not issue any subscribe requests before a connected message is received. This will be improved in further releases.

## Disclaimer

This product has not been thoroughly tested. Use it at your own risk. Always back up your data before performing any essential operations. 
**Use at your own risk**.
