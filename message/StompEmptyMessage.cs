using System.Collections.Generic;

namespace zion.message
{
    public partial class StompEmptyMessage : StompMessage
    {
        public StompEmptyMessage(string command, object body, Dictionary<string, string> headers) : base(command, body, headers)
        {
        }
    }
}