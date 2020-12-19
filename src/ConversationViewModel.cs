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

        #endregion

        #region CalculatedProperties

        public int ConversationCount => Conversations.Count;
        public int SelectedConversationMessageCount => SelectedConversation.Messages.Count;

        #endregion

        #region SearchMessageProperties
        
        public MatchCollection SearchMatches { get; set; }
        public int SearchPosition { get; set; }
        
        #endregion

        // Conversation ViewModel Constructor
        public ConversationViewModel()
        {
            // populate the conversation list and setup the BindingSource
            conversations = SMSRepository.GetConversations();
            Conversations = new BindingSource();
            Conversations.DataSource = conversations;
            // set the selected conversation to an empty object on start
            SelectedConversation = new SMSConversation();
        }
    }
}
