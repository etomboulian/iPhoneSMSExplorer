using iPhoneMessageExplorer.Common;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace iPhoneMessageExplorer
{
    public partial class MainForm : Form
    {
        private readonly ConversationViewModel conversationVM;
        public MainForm()
        {
            InitializeComponent();
            conversationVM = new ConversationViewModel();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            setBindings();
            setSearchButtonsVisible(false);
        }

        private void setBindings()
        {
            // set bindings for the conversations listbox
            listBoxConversations.DataSource = conversationVM.Conversations;
            listBoxConversations.DisplayMember = "ChatIdentifier";

            // Bindings for data labels
            labelCountData.DataBindings.Add("Text", conversationVM, "ConversationCount");
            labelMsgCountData.DataBindings.Add("Text", conversationVM, "SelectedConversationMessageCount", true, DataSourceUpdateMode.OnPropertyChanged, "N0");
        }

        // On change the selected item in the listbox, update the value in selectedConversation to the new selection
        private void listBoxConversations_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxConversations.SelectedIndex;
            // if the selected index value is a valid one (ie not -1)
            if (index >= 0)
            {
                conversationVM.SelectedConversation = ((SMSConversationList)conversationVM.Conversations.DataSource)[index];
            }
            displayCurrentConversationMessages();
        }

        #region BUTTON_ACTIONS
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text;

            // Exit if we clicked search with nothing
            if(String.IsNullOrEmpty(searchText))
            {
                return;
            }

            // Get the list of matches
            MatchCollection matches = Regex.Matches(textBoxMessages.Text, searchText);

            // Set the match list in the ViewModel and current position
            conversationVM.SearchMatches = matches;
            conversationVM.SearchPosition = 0;
            if (conversationVM.SearchMatches.Count > 0)
            {
                // Highlight the match at the current position
                Match match = conversationVM.SearchMatches[conversationVM.SearchPosition];
                textBoxMessages.SelectionStart = match.Index;
                textBoxMessages.SelectionLength = searchText.Length;
                textBoxMessages.Focus();
                textBoxMessages.ScrollToCaret();
                setSearchButtonsVisible(true);
            }
            else
            {
                MessageBox.Show($"No Results found for {searchText}");
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if(conversationVM.SearchMatches.Count > 0 )
            {
                ++conversationVM.SearchPosition;
                if (conversationVM.SearchPosition < conversationVM.SearchMatches.Count)
                {
                    Match match = conversationVM.SearchMatches[conversationVM.SearchPosition];
                    textBoxMessages.SelectionStart = match.Index;
                    textBoxMessages.SelectionLength = match.Length;
                    textBoxMessages.Focus();
                    textBoxMessages.ScrollToCaret();
                }
                else 
                {
                    MessageBox.Show("Reached the end of the search results");
                }
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (conversationVM.SearchMatches.Count > 0)
            {
                --conversationVM.SearchPosition;
                if (conversationVM.SearchPosition >= 0)
                {
                    Match match = conversationVM.SearchMatches[conversationVM.SearchPosition];
                    textBoxMessages.SelectionStart = match.Index;
                    textBoxMessages.SelectionLength = match.Length;
                    textBoxMessages.Focus();
                    textBoxMessages.ScrollToCaret();
                }
                else
                {
                    MessageBox.Show("Reached the beginning of the search results");
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (conversationVM.SearchMatches.Count > 0)
            {
                conversationVM.SearchMatches = null;
                conversationVM.SearchPosition = 0;
                setSearchButtonsVisible(false);
            }
            //textBoxSearch.Text = "";
        }

        #endregion

        #region OPEN_FOLDER_ACTION
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog.SelectedPath;
            }
            validateFolderPath(folderPath);
        }
        #endregion

        #region HELPER_FUNCTIONS

        private void setSearchButtonsVisible(bool visible)
        {
            buttonNext.Enabled = visible;
            buttonPrevious.Enabled = visible;
            buttonClear.Enabled = visible;
            buttonSearch.Enabled = !visible;
            textBoxSearch.Enabled = !visible;
        }

        private bool validateFolderPath(string folderPath)
        {
            if (!File.Exists(folderPath))
            {
                return false;
            }
            return true;
        }

        // Builds a string out of the messages in the message list for the current selected conversation
        private void displayCurrentConversationMessages()
        {
            if (conversationVM.SelectedConversation.MessagesString is null)
            {
                // buffer to hold the messages text
                StringBuilder displayMessages = new StringBuilder();

                // if there exist some messages in the list, then build the message text
                if (!(conversationVM.SelectedConversation.Messages is null))
                {
                    foreach (var item in conversationVM.SelectedConversation.Messages)
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
                    conversationVM.SelectedConversation.MessagesString = messagesString;
                    // replace the text of the textbox with the generated message text
                    textBoxMessages.Text = messagesString;
                }
                // if no messages exist indicate so with an appropriate message
                else
                {
                    textBoxMessages.Text = "No messages to display";
                }
            }
            else
            {
                textBoxMessages.Text = conversationVM.SelectedConversation.MessagesString;
            }
        }

        #endregion
    }
}
