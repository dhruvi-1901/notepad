using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNotepad
{
    public partial class FindReplaceForm : Form
    {
        private TextBox editor;
        public FindReplaceForm(TextBox tb)
        {
            InitializeComponent();
            editor = tb;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int start = editor.SelectionStart + editor.SelectionLength;
            int index = editor.Text.IndexOf(txtFind.Text, start,
                StringComparison.CurrentCultureIgnoreCase);

            if (index != -1)
            {
                editor.Select(index, txtFind.Text.Length);
                editor.ScrollToCaret();
            }
            else
            {
                MessageBox.Show("Text not found");
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (editor.SelectedText.Equals(txtFind.Text,
                StringComparison.CurrentCultureIgnoreCase))
            {
                editor.SelectedText = txtReplace.Text;
            }
            btnNext.PerformClick();
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            editor.Text = editor.Text.Replace(
                txtFind.Text, txtReplace.Text);
        }
    }
}
