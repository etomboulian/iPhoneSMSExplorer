using iPhoneMessageExplorer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iPhoneMessageExplorer.Common;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.IO;

namespace iPhoneMessageExplorer
{
    class ConversationViewModel : INotifyPropertyChanged
    {
        #region ImplementINotifyPropertyChanged

        // Implement the INofityPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region SelectedConversationProperty

        // Create the conversations list and the corresponding binding source
        private readonly SMSConversationList conversations;
        public BindingSource Conversations { get; set; }

        // Create the selected conversation property and methods
        private SMSConversation selectedConversation;
        public SMSConversation SelectedConversation
        {
            get
            {
                return selectedConversation;
            }
            set
            {
                // if there are no messages, get the messages for the new selectedConversation
                if (value.Messages is null)
                {
                    Task.Run(()=>SMSRepository.GetMessages(value)).Wait();
                }
                value.Messages.Sort();
                selectedConversation = value;
                OnPropertyChanged();
                OnPropertyChanged("SelectedConversationMessageCount");
            }
        }

        public void SetSelectedConversation(int index)
        {
            SelectedConversation = ((SMSConversationList)Conversations.DataSource)[index];
        }

        public string getCurrentConversationMessages()
        {
            if (SelectedConversation.MessagesString is null)
            {
                // buffer to hold the messages text
                StringBuilder displayMessages = new StringBuilder();

                // if there exist some messages in the list, then build the message text
                if (!(SelectedConversation.Messages is null))
                {
                    foreach (var item in SelectedConversation.Messages)
                    {

                        if (item.FromMe)
                        {
                            displayMessages.Append("> Sent | ");
                        }
                        else
                        {
                            displayMessages.Append("> Received | ");
                        }
                        displayMessages.Append(item.DateStamp.ToShortDateString());
                        displayMessages.Append(" | ");
                        displayMessages.AppendLine(item.DateStamp.ToShortTimeString());
                        displayMessages.AppendLine(item.Text);
                        displayMessages.AppendLine("\n");
                    }
                    string messagesString = displayMessages.ToString();
                    SelectedConversation.MessagesString = messagesString;
                    // replace the text of the textbox with the generated message text
                    return messagesString;
                }
                // if no messages exist indicate so with an appropriate message
                else
                {
                    return "No messages to display";
                }
            }
            else
            {
                return SelectedConversation.MessagesString;
            }
        }

        #endregion

        #region CalculatedConversationProperties

        public int ConversationCount => Conversations.Count;
        public int SelectedConversationMessageCount => SelectedConversation.Messages.Count;

        #endregion

        #region SearchMessage
        
        public MatchCollection SearchMatches { get; set; }
        public int SearchPosition { get; set; }

        public bool SearchMessages(string searchText, string searchSpace)
        {
            // Get the list of matches
            MatchCollection matches = Regex.Matches(searchSpace, searchText);

            // Set the match list in the ViewModel and current position
            SearchMatches = matches;
            SearchPosition = 0;
            return SearchMatches.Count > 0;
        }

        public void ClearSearch()
        {
            SearchMatches = null;
            SearchPosition = 0;
        }

        #endregion

        #region ExportMessages
        // put the export messages core code in here (move out of mainform)
        public bool ExportCurrentConversationMessages(string outFilePath)
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Date,Time,Status,Message"); // header row
            foreach (var message in SelectedConversation.Messages)
            {
                string status = (message.FromMe) ? "Sent" : "Received";
                string messageText = message.Text.Replace('\n', ' ');
                messageText = "\"" + messageText + "\"";
                csvData.AppendLine($"{message.DateStamp.ToShortDateString()}," +
                                    $"{message.DateStamp.ToLongTimeString()}," +
                                    $"{status}," +
                                    $"{messageText}");
            }

            try
            {
                File.WriteAllText(outFilePath, csvData.ToString());
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        #endregion

        #region OpenFileOrFolder
        // put the open file code and properties in here
        public string SelectedFolderPath { get; private set; }

        public bool SetSelectedFolderPath(string folderPath) 
        {
            bool success = false;
            if(validateFolderPath(folderPath))
            {
                SelectedFolderPath = folderPath;
                success = true;
            }
            return success;
        }

        #endregion

        // Conversation ViewModel Constructor
        public ConversationViewModel()
        {
            // populate the conversation list and setup the BindingSource
            conversations = SMSRepository.GetConversations();

            // create a BindingSource and point it at the retrieved conversation list
            Conversations = new BindingSource();
            Conversations.DataSource = conversations;

            // set the selected conversation to an empty object on start
            SelectedConversation = new SMSConversation();

            // set the current db file to an empty object or null on start
        }

        #region HelperFunctions

        private bool validateFolderPath(string folderPath)
        {
            if (!File.Exists(folderPath))
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
