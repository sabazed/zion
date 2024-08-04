using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocket4Net;
using zion.message;

public class ZionClient
{
    
    public Action OnOpen { get; set; } = () => { };
    public Action<Exception> OnError { get; set; } = _ => { };
    public Action<StompMessage> OnMessage { get; set; } = _ => { };

    private readonly WebSocket _client;
    private readonly MessageConverter _converter;

    public ZionClient(string uri, IMessageTypeResolver messageTypeResolver, List<KeyValuePair<string, string>> headers = default)
    {
        _converter = new(messageTypeResolver);
        _client = new WebSocket(uri, customHeaderItems: headers);
        _client.Error += (sender, e) => OnError.Invoke(e.Exception);
        _client.Opened += (sender, e) =>
        {
            SendStompEmptyMessage(new ConnectMessage());
            OnOpen.Invoke();
        };
        _client.MessageReceived += (sender, e) =>
        {
            var stompMessage = _converter.Deserialize(e.Message);
            OnMessage.Invoke(stompMessage);
        };
    }

    public async Task Connect()
    {
        await _client.OpenAsync();
    }

    public async Task Disconnect()
    {
        if (_client.State == WebSocketState.Open)
        {
            SendStompEmptyMessage(new DisconnectMessage());
        }
        await _client.CloseAsync();
    }

    public string Subscribe(string destination)
    {
        var subId = Guid.NewGuid().ToString();
        SendStompEmptyMessage(new SubscribeMessage(destination, subId));
        return subId;
    }

    public void SendMessage(object message, string destination)
    {
        SendStompMessage(new SendMessage(message, destination));
    }

    private void SendStompMessage(StompMessage message)
    {
        var serializedMessage = _converter.Serialize(message);
        _client.Send(serializedMessage);
    }

    private void SendStompEmptyMessage(StompEmptyMessage message)
    {
        var serializedMessage = _converter.Serialize(message, true);
        _client.Send(serializedMessage);
    }
    
}