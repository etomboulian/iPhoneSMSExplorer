using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoneMessageExplorer.Common
{
    class SMSMessage : IComparable<SMSMessage>
    {
        // the message guid value
        public string Guid { get; set; }
        // the message text value
        public string Text { get; set; }
        // the message handle_ID (identifies the chat the message belongs to)
        public int Handle_ID { get; set; }
        // the service that the message was carried on (ie SMS or iMessage)
        public string Service { get; set; }
        // the phone number associated with the message
        public string Account { get; set; }
        // a guid that is unique to the phone number associated with the message
        public string AccountGuid { get; set; }
        // The datestamp associated with the message
        public DateTime DateStamp { get; set; }
        // Indicates whether the message was sent or received
        public bool FromMe { get; set; }
        // Indicates whether the message had an attachment 
        public bool HasAttachment { get; set; }

        public int CompareTo(SMSMessage other)
        {
            if(other == null)
            {
                return 1;
            }

            return (DateStamp.CompareTo(other.DateStamp) * -1);
        }

        // Override to string to output a csv of the main properties of the message object
        public override string ToString()
        {
            return $"{Guid}, {Text}, {Handle_ID}, {Service}, {Account}, {AccountGuid}, {DateStamp}, {FromMe}, {HasAttachment}";
        }
    }
}
