using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoneMessageExplorer.Common
{
    class SMSConversation
    {
        // The conversation rowid value
        public int RowId { get; set; }
        // The conversation guid value
        public string Guid { get; set; }
        // The phone number associated with the conversation
        public string ChatIdentifier { get; set; }
        // The service that the conversation belongs to (SMS, iMessage)
        public string ServiceName { get; set; }
        // The handle id for the selected chat conversation
        public int HandleId { get; set; }
        // The list of messages that belong to this conversation
        public int TotalMessages { get; set; }
        public DateTime LastMessageDate { get; set; }
        public SMSMessageList Messages { get; set; }
        public string MessagesString { get; set; }

        // Override toString to return a csv of the main properties of the conversation
        public override string ToString()
        {
            return $"{RowId},{Guid},{ChatIdentifier},{ServiceName}";
        }
    }
}
