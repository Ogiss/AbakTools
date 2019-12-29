using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.Core;

namespace AbakTools.Business.Forms
{
    public partial class SerwerConfigForm : Form
    {
        public SerwerConfigForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Configuration.SetSetting("ServerAddress",adresSerweraTextBox.Text);
            Configuration.SetSetting("ServerPort", portTextBox.Text);
            Configuration.SetSetting("ServerUserName", loginTextBox.Text);
            Configuration.SetSetting("ServerPassword", hasloTextBox.Text);
            Configuration.SetSetting("ServerAutoConnect", autoConnectCheckBox.Checked.ToString());
            Configuration.SetSetting("ServerAutoConnectInterval", intervalTextBox.Text);
            Configuration.SaveConfiguration();
        }

        private void SerwerConfigForm_Load(object sender, EventArgs e)
        {
            adresSerweraTextBox.Text = Configuration.GetSetting("ServerAddress");
            portTextBox.Text = Configuration.GetSetting("ServerPort");
            loginTextBox.Text = Configuration.GetSetting("ServerUserName");
            hasloTextBox.Text = Configuration.GetSetting("ServerPassword");
            string autoConnectStr = Configuration.GetSetting("ServerAutoConnect");
            if (!string.IsNullOrEmpty(autoConnectStr))
            {
                bool b;
                if (bool.TryParse(autoConnectStr, out b))
                    autoConnectCheckBox.Checked = b;
            }
            intervalTextBox.Text = Configuration.GetSetting("ServerAutoConnectInterval");
        }
    }
}
