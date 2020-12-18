using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iPhoneMessageExplorer.Common;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace iPhoneMessageExplorer.Data
{
    class SMSRepository
    {
        // Set the database filename and connection string values
        private static string fileName = "3d0d7e5fb2ce288813306e4d4636395e047a3d28";
        private static string connString = $"Data Source=C:\\Users\\etomb\\source\\repos\\iPhoneMessageExplorer\\src\\{fileName}";

        /// <summary>
        /// Gets the full list of SMS conversations from the iPhone SMS db file
        /// </summary>
        /// <returns>A populated list of the SMS Conversations in the SMS DB file</returns>
        public static SMSConversationList GetConversations()
        {
            // setup the conversation list and sql
            SMSConversationList conversations = new SMSConversationList();

            string sql = @"select 
                        c.ROWID, 
                        c.guid, 
                        c.chat_identifier, 
                        c.service_name, 
						chj.handle_id,
                        count(m.ROWID) as totalMessages, 
                        max(datetime(m.date / 1000000000 + strftime('%s', '2001-01-01'), 'unixepoch', 'localtime')) as date
                        from chat c
                        left join chat_message_join cmj on c.ROWID = cmj.chat_id
						left join chat_handle_join chj on c.ROWID = chj.chat_id
                        left join message m on cmj.message_id = m.ROWID
                        group by c.ROWID, c.guid, c.chat_identifier, c.service_name";

            //string sql = @"select distinct c.ROWID, c.guid, c.chat_identifier, c.service_name, max(date) from chat c
            //                join message m on c.ROWID = m.handle_id
            //                group by c.ROWID, c.guid, c.chat_identifier, c.service_name";

            // Create and open the connection to the sqlite db
            using (var conn = new SqliteConnection(connString))
            {   
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw new SqliteException("Unable to open connection to db file", 1);
                }

                // Create the sql command and assign the command text
                SqliteCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                // Execute the command and read the results into new SMSConversation objects
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    // create variables to hold the return results of each row
                    int rowId;
                    string guid;
                    string chatIdentifier;
                    string serviceName;
                    int handleId;
                    int totalMessages;
                    DateTime lastMessageDate;
                    SMSMessageList messages = null;

                    // for each row returned by the sql reader
                    while (reader.Read())
                    {
                        // get the data pieces if the values exist in the database
                        rowId = (!reader.IsDBNull(0)) ? reader.GetInt32(0) : 0;
                        guid = (!reader.IsDBNull(reader.GetOrdinal("guid"))) ? reader["guid"] as string : null;
                        chatIdentifier = (!reader.IsDBNull(reader.GetOrdinal("chat_identifier"))) ? reader["chat_identifier"] as string : null;
                        serviceName = (!reader.IsDBNull(reader.GetOrdinal("service_name"))) ? reader["service_name"] as string : null;
                        handleId = (!reader.IsDBNull(reader.GetOrdinal("handle_id"))) ? reader.GetInt32(reader.GetOrdinal("handle_id")) : 0;
                        totalMessages = (!reader.IsDBNull(reader.GetOrdinal("totalMessages"))) ? reader.GetInt32(reader.GetOrdinal("totalMessages")) : 0;

                        // crate a new conversation object
                        SMSConversation conversation = new SMSConversation
                        {
                            RowId = rowId,
                            Guid = guid,
                            ChatIdentifier = chatIdentifier,
                            ServiceName = serviceName,
                            HandleId = handleId,
                            Messages = messages,
                            TotalMessages = totalMessages
                        };

                        if (conversation.TotalMessages > 0)
                        {
                            // add the new conversation to the list of conversations
                            conversations.Add(conversation);
                        }

                        // reset the temp value variables to null
                        rowId = 0;
                        guid = null;
                        chatIdentifier = null;
                        serviceName = null;
                        handleId = 0;
                    }
                }
            }

            // Return back the populated list of conversations
            return conversations;
        }

        /// <summary>
        /// Gets the full list of messages for the given conversation
        /// </summary>
        /// <param name="conversation">The conversation for which to fetch the message history</param>
        public static void GetMessages(SMSConversation conversation)
        {
            conversation.Messages = new SMSMessageList();
            string sql = @"select m.guid, m.text, m.handle_id, m.service, m.account, m.account_guid,
                            datetime(m.date/1000000000 + strftime('%s', '2001-01-01') ,'unixepoch','localtime') as date,
                            m.is_from_me, m.cache_has_attachments
                            from message m
                            where m.handle_id = $id";

            using(var conn = new SqliteConnection(connString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw new SqliteException("Unable to open connection to db file", 1);
                }

                SqliteCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("$id", conversation.HandleId);

                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    string guid;
                    string text;
                    int handle_ID;
                    string service;
                    string account;
                    string accountGuid;
                    string dateString;
                    DateTime dateStamp;
                    bool fromMe;
                    bool hasAttachment;

                    while(reader.Read())
                    {
                        guid = (!reader.IsDBNull(reader.GetOrdinal("guid"))) ? reader["guid"] as string : null;
                        text = (!reader.IsDBNull(reader.GetOrdinal("text"))) ? reader["text"] as string : null;
                        handle_ID = (!reader.IsDBNull(reader.GetOrdinal("handle_id"))) ? reader.GetInt32(reader.GetOrdinal("handle_id")) : 0;
                        service = (!reader.IsDBNull(reader.GetOrdinal("service"))) ? reader["service"] as string : null;
                        account = (!reader.IsDBNull(reader.GetOrdinal("account"))) ? reader["account"] as string : null;
                        accountGuid = (!reader.IsDBNull(reader.GetOrdinal("account_guid"))) ? reader["account_guid"] as string : null;
                        dateString = (!reader.IsDBNull(reader.GetOrdinal("date"))) ? reader["date"] as string : null;
                        fromMe = (!reader.IsDBNull(reader.GetOrdinal("is_from_me"))) ? reader.GetBoolean(reader.GetOrdinal("is_from_me")) : false;
                        hasAttachment = (!reader.IsDBNull(reader.GetOrdinal("cache_has_attachments"))) ? reader.GetBoolean(reader.GetOrdinal("cache_has_attachments")) : false;

                        string[] dateTimeArray = dateString.Split(' ');
                        string[] dateArray = dateTimeArray[0].Split('-');
                        string[] timeArray = dateTimeArray[1].Split(':');

                        int year = int.Parse(dateArray[0]); int month = int.Parse(dateArray[1]); int day = int.Parse(dateArray[2]);
                        int hour = int.Parse(timeArray[0]); int min = int.Parse(timeArray[1]); int sec = int.Parse(timeArray[2]);
                        dateStamp = new DateTime(year, month, day, hour, min, sec);

                        SMSMessage message = new SMSMessage
                        {
                            Guid = guid,
                            Text = text,
                            Handle_ID = handle_ID,
                            Service = service,
                            Account = account,
                            AccountGuid = accountGuid,
                            DateStamp = dateStamp,
                            FromMe = fromMe,
                            HasAttachment = hasAttachment
                        };

                        conversation.Messages.Add(message);
                        
                        // reset the variables to default values
                        guid = null;
                        text = null;
                        handle_ID = 0;
                        service = null;
                        account = null;
                        accountGuid = null;
                        dateStamp = DateTime.MinValue;
                        fromMe = false;
                        hasAttachment = false;
                    }
                }
            }
        }
    }
}
