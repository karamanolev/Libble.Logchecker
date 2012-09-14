using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Libble.Logchecker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/karamanolev/Libble.Logchecker");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
        }
    }
}
