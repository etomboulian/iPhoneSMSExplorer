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
                conversationVM.SetSelectedConversation(index);
            }

            // set the conversation pane content to the messages for the selected conversation
            string messageText = conversationVM.getCurrentConversationMessages();
            textBoxMessages.Text = messageText;
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

            // Search for the string in the message text, focus on it if found
            if(conversationVM.SearchMessages(searchText, textBoxMessages.Text))
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
                conversationVM.ClearSearch();
                setSearchButtonsVisible(false);
            }
        }

        private void buttonExportMessages_Click(object sender, EventArgs e)
        {
            if(!(conversationVM.SelectedConversation.Messages is null))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV File|*.csv";
                saveFileDialog.Title = "Save Messages";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    // Write out, but show an error message if the write failed
                    if (!conversationVM.ExportCurrentConversationMessages(saveFileDialog.FileName))
                    {
                        MessageBox.Show("Unable to save file.");
                    }
                }
                else 
                {
                    MessageBox.Show("Unable to get the filePath from the saveFileDialog");
                }
            }
            else
            {
                MessageBox.Show("No Messages to Export");
            }
        }

        #endregion

        #region OpenFolderMenuAction
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            // show the open folder dialog
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // get the selected folder path from the open folder dialog
                folderPath = folderBrowserDialog.SelectedPath;
            }
            
            // try to set the selected folder path and show an error if it fails
            if(!conversationVM.SetSelectedFolderPath(folderPath))
            {
                MessageBox.Show("Unable to open the selected folder as it does not validate.");
            }
            // maybe do more after this to trigger data loading?
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

        #endregion

    }
}
