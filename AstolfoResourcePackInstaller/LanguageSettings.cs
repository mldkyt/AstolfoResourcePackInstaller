using System;
using System.Windows.Forms;

namespace AstolfoResourcePackInstaller
{
    public partial class LanguageSettings : Form
    {
        public event EventHandler<LanguageData> OKClicked;
        
        public LanguageSettings(LanguageData data)
        {
            InitializeComponent();

            if (data == null) return;
            checkBox1.Checked = data.MenuButtons;
            checkBox2.Checked = data.CherryToFemboy;
            checkBox3.Checked = data.GameTitle;
        }

        private void button1Click(object sender, EventArgs e)
        {
            OKClicked?.Invoke(this, new LanguageData()
            {
                GameTitle = checkBox3.Checked,
                CherryToFemboy = checkBox2.Checked,
                MenuButtons = checkBox1.Checked
            });
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }

    public class LanguageData
    {
        public bool MenuButtons { get; set; }
        public bool CherryToFemboy { get; set; }
        public bool GameTitle { get; set; }
    }
}