using System;

public interface IMessageTypeResolver
{
    
    /// <summary>
    /// Resolves the message type from the given message type string.
    /// </summary>
    /// <param name="messageType">The message type string.</param>
    /// <returns>The Type corresponding to the message type string.</returns>
    Type? ResolveMessageType(string messageType);
    
}