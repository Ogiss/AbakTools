using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

[assembly: BAL.Forms.MenuAction("Konfiguracja\\Enova", typeof(AbakTools.Business.Forms.KonfiguracjaEnovaForm), MenuAction = BAL.Forms.MenuActionsType.OpenFormModal, Priority = 1140)]

namespace AbakTools.Business.Forms
{
    public partial class KonfiguracjaEnovaForm : Form
    {
        public KonfiguracjaEnovaForm()
        {
            InitializeComponent();
        }

        private void enovaPathButton_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.enovaPathTextBox.Text = this.folderBrowserDialog.SelectedPath;
            }
        }

        private void KonfiguracjaEnovaForm_Load(object sender, EventArgs e)
        {
            this.enovaPathTextBox.Text = Enova.Business.Old.Core.Configuration.GetSetting("EnovaPath");
            this.enovaDatabaseTextBox.Text = Enova.Business.Old.Core.Configuration.GetSetting("EnovaDatabase");
            this.fvRaportTextBox.Text = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFVReport");
            this.fkRaportTextBox.Text = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFKReport");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.enovaPathTextBox.Text))
                Enova.Business.Old.Core.Configuration.SetSetting("EnovaPath", this.enovaPathTextBox.Text);

            if (!string.IsNullOrEmpty(this.enovaDatabaseTextBox.Text))
                Enova.Business.Old.Core.Configuration.SetSetting("EnovaDatabase", this.enovaDatabaseTextBox.Text);

            if (!string.IsNullOrEmpty(fvRaportTextBox.Text))
                Enova.Business.Old.Core.Configuration.SetSetting("EnovaFVReport", fvRaportTextBox.Text);

            if (!string.IsNullOrEmpty(fkRaportTextBox.Text))
                Enova.Business.Old.Core.Configuration.SetSetting("EnovaFKReport", fkRaportTextBox.Text);

            Enova.Business.Old.Core.Configuration.SaveConfiguration();
        }

        private void fvButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + "\\Aspx";
            if (this.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var file = this.openFileDialog.FileName;
                this.fvRaportTextBox.Text = file.Substring(file.IndexOf("\\Aspx\\") + 6);
            }
        }

        private void fkButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + "\\Aspx";
            if (this.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var file = this.openFileDialog.FileName;
                this.fkRaportTextBox.Text = file.Substring(file.IndexOf("\\Aspx\\") + 6);
            }
        }
    }
}
