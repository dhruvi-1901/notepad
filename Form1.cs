using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
namespace MyNotepad
{
    public partial class Form1 : Form
    {
        string fname="";
        bool isModified = false;
        //PrintDocument printDoc;
        PageSettings pageSettings;
        
        public Form1()
        {
            InitializeComponent();
            statusStrip1.Visible = false;
            statusStrip1.Items.Add("Welcome...");
            //printDoc = new PrintDocument();
            this.Text = "Untitled";
            pageSettings = new PageSettings();
        }

        // New File
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.Clear();
            this.Text = "Untitled";
        }

        // Open File
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtEditor.Text = File.ReadAllText(ofd.FileName);
                fname = ofd.FileName;
                this.Text = fname.Substring(ofd.FileName.IndexOf('\\')+1);
            }
            
            // saveFlag = 1;
        }

        // Save File
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            if (isModified==true && fname!="")
                File.WriteAllText(fname, txtEditor.Text);
            else
                SaveAs();
            
        }
        public void SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, txtEditor.Text);
                fname = sfd.FileName;
                isModified = false;
                this.Text = fname.Substring(sfd.FileName.IndexOf('\\') + 1);
            }
            
            //saveFlag = 1;
        }
        private bool CheckUnsavedChanges()
        {
            if (!isModified)
                return true;

            DialogResult result = MessageBox.Show(
                "Do you want to save changes?",
                "Notepad",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                if (fname=="")
                    SaveAs();
                else
                    File.WriteAllText(fname, txtEditor.Text);
                return !isModified; // exit only if save succeeded
            }
            else if (result == DialogResult.No)
            {
                return true;
            }

            return false; // Cancel
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PageSetupDialog psd = new PageSetupDialog();

            psd.Document = printDoc;

            psd.PageSettings = pageSettings;
            psd.AllowMargins = true;
            psd.AllowOrientation = true;
            psd.AllowPaper = true;

            if (psd.ShowDialog() == DialogResult.OK)
            {
                pageSettings = psd.PageSettings;
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog preview = new PrintPreviewDialog();

            
            printDoc.DocumentName = fname;
            MessageBox.Show(printDoc.DocumentName);

            preview.Document = printDoc;

            // Reset before preview
            preview.Width = 800;
            preview.Height = 600;

            preview.ShowDialog();
        }
        // Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckUnsavedChanges())
                Application.Exit();
        }
        // Edit Menu
        //Undo
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtEditor.Undo();
        }
        // Cut
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.Cut();
        }

        // Copy
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.Copy();
        }

        // Paste
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.Paste();
        }

        private void clearFormattingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.Font = new Font(txtEditor.Font.FontFamily, 11, FontStyle.Regular);
            txtEditor.ForeColor = Color.Black;
        }

        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.SelectedText = "";
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.ShowColor = true;
            fd.ShowDialog();
            txtEditor.Font = fd.Font;
            txtEditor.ForeColor = fd.Color;
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindReplaceForm frm = new FindReplaceForm(txtEditor);
            frm.Show(this);
        }
        //View Menu
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int size = ((int)txtEditor.Font.Size);
            size++;
            txtEditor.Font = new Font(txtEditor.Font.FontFamily, size);
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int size = ((int)txtEditor.Font.Size);
            size--;
            txtEditor.Font = new Font(txtEditor.Font.FontFamily, size);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.Font = new Font(txtEditor.Font.FontFamily, 11);
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.Checked == true)
                statusBarToolStripMenuItem.Checked = false;
            else
                statusBarToolStripMenuItem.Checked = true;
            if (statusBarToolStripMenuItem.Checked==true)
                statusStrip1.Visible = true;
            else
                statusStrip1.Visible = false;
        }

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordToolStripMenuItem.Checked == true)
                wordToolStripMenuItem.Checked = false;
            else
                wordToolStripMenuItem.Checked =true;
            if (wordToolStripMenuItem.Checked==true)
            
                txtEditor.WordWrap = true;
           
            else
                txtEditor.WordWrap = false;
        }

        private void txtEditor_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exitToolStripMenuItem_Click(sender, e);
        }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.SelectAll();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindReplaceForm frm = new FindReplaceForm(txtEditor);
            frm.Show(this);
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
         
        }
    }
}
