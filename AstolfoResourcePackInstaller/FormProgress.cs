using System;
using System.Windows.Forms;

namespace AstolfoResourcePackInstaller
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        public void SetText(string text)
        {
            if (label1.Text != text)
            {
                label1.Text = text;

                textBox1.AppendText(text + Environment.NewLine);
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.Update();
            }

            label1.Update();
        }
    }
}