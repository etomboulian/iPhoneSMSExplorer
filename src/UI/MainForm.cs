using iPhoneMessageExplorer.Common;
using iPhoneMessageExplorer.UI;
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
        private static ConversationViewModel conversationVM;
        private static bool isBindingSet{ get; set; }
        public MainForm()
        {
            InitializeComponent();
            //conversationVM = new ConversationViewModel();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //SetBindings();
            SetSearchButtonsVisible(false);
        }

        private void SetBindings()
        {
            
            // set bindings for the conversations listbox
            listBoxConversations.DataSource = conversationVM.Conversations;
            listBoxConversations.DisplayMember = "ChatIdentifier";

            // Bindings for data labels
            if (!isBindingSet)
            {
                labelCountData.DataBindings.Add("Text", conversationVM, "ConversationCount");
                labelMsgCountData.DataBindings.Add("Text", conversationVM, "SelectedConversationMessageCount", true, DataSourceUpdateMode.OnPropertyChanged, "N0");
                isBindingSet = true;
            }
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
            textBoxMessages.Text = conversationVM.getCurrentConversationMessages();
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
                SetSearchButtonsVisible(true);
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
                SetSearchButtonsVisible(false);
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

        #region ToolStripMenuAction
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
            if(!conversationVM.SetSelectedDBFilePath(folderPath))
            {
                MessageBox.Show("Unable to open the selected folder as it does not validate.");
            }
            // maybe do more after this to trigger data loading?
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutSMSExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            dialog.ShowDialog(this);
            // show the about dialog
        }
        #endregion

        #region HELPER_FUNCTIONS

        private void SetSearchButtonsVisible(bool visible)
        {
            buttonNext.Enabled = visible;
            buttonPrevious.Enabled = visible;
            buttonClear.Enabled = visible;
            buttonSearch.Enabled = !visible;
            textBoxSearch.Enabled = !visible;
        }



        #endregion

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "database files (*.db)|*.db|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    conversationVM = new ConversationViewModel(filePath);
                    SetBindings();
                }
            }
        }
    }
}
