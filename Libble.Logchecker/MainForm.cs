﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Libble.Logchecker.Core;

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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Log Files (*.log)|*.log|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.ReadLog(openFileDialog.FileName);
            }
        }

        private void ReadLog(string fileName)
        {
            string text;
            try
            {
                text = File.ReadAllText(fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error reading log: " + e.Message);
                return;
            }
            LogcheckerWrapper logchecker;
            try
            {
                logchecker = new LogcheckerWrapper(text);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("php4ts"))
                {
                    MessageBox.Show("Couldn't load php4ts.dll. Are you missing VS2010 redistributable? Download it here http://www.microsoft.com/en-us/download/details.aspx?id=26999");
                    return;
                }
                throw e;
            }

            this.labelScore.Text = "Score: " + logchecker.Score;
            webBrowser.DocumentText = this.FormatHtml(logchecker.Log);

            if (logchecker.Bad.Length == 0)
            {
                this.textBad.Text = "- None";
            }
            else
            {
                this.textBad.Text = string.Join(Environment.NewLine, logchecker.Bad.Select(s => "- " + s));
            }
        }

        private string FormatHtml(string log)
        {
            log = log.Replace("\r", "").Replace("\n", "<br />" + Environment.NewLine);

            StringBuilder html = new StringBuilder();
            html.Append(@"<!DOCTYPE html>
<html>
<head>
<title>Logcheck</title>
<style type=""text/css"">
body {
    font-family: monospace;
}
.good {
    background: #cfc;
}
.bad {
    background: #fcc;
}
</style>
</head>
<body>");
            html.Append(log);
            html.Append(
@"</body>
</html>");
            return html.ToString();
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    this.ReadLog(files[0]);
                }
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }
    }
}
