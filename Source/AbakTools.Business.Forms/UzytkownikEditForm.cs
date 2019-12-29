using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;

namespace AbakTools.Business.Forms
{
    public partial class UzytkownikEditForm : Enova.Business.Old.Forms.DataEditForm
    {
        bool canClose = true;
        private Dictionary<string, Enova.Business.Old.DB.Web.SynchProfileItem> profiles = null;

        public UzytkownikEditForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.Text != repasswordTextBox.Text)
            {
                MessageBox.Show("Hasło i powtórzone hasło nie zgadzają się", "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                passwordTextBox.Focus();
                passwordTextBox.SelectAll();
                canClose = false;
                
            }
            else
            {
                WebContext dc = Enova.Business.Old.Core.ContextManager.WebContext;
                Enova.Business.Old.DB.Web.User user = (Enova.Business.Old.DB.Web.User)this.DataSource;

                ((Enova.Business.Old.DB.Web.User)DataSource).Password = passwordTextBox.Text;
                ((Enova.Business.Old.DB.Web.User)DataSource).Encrypt();

                if (user.EntityState != System.Data.EntityState.Added && user.EntityState != System.Data.EntityState.Detached)
                {
                    if (!profiles.ContainsKey("SYNCH_AGENT_KODE"))
                    {
                        var prof = new SynchProfileItem()
                        {
                            TableName = "Users",
                            Parent = user.ID,
                            Type = "bool",
                            Key = "SYNCH_AGENT_KODE"
                        };
                        dc.AddToSynchProfileItems(prof);
                        profiles.Add("SYNCH_AGENT_KODE", prof);
                    }
                    profiles["SYNCH_AGENT_KODE"].Value = synchAgentKodeTextBox.Text;

                    if (!profiles.ContainsKey("SYNCH_KONTRAH_ENABLED"))
                    {
                        var prof = new SynchProfileItem()
                        {
                            TableName = "Users",
                            Parent = user.ID,
                            Type = "bool",
                            Key = "SYNCH_KONTRAH_ENABLED"
                        };
                        dc.AddToSynchProfileItems(prof);
                        profiles.Add("SYNCH_KONTRAH_ENABLED", prof);
                    }
                    profiles["SYNCH_KONTRAH_ENABLED"].Value = synchKontrahEnabledCheckBox.Checked.ToString();


                    if (!profiles.ContainsKey("SYNCH_KONTRAH_FILTR_AGENT"))
                    {
                        var prof = new SynchProfileItem()
                        {
                            TableName = "Users",
                            Parent = user.ID,
                            Type = "bool",
                            Key = "SYNCH_KONTRAH_FILTR_AGENT"
                        };
                        dc.AddToSynchProfileItems(prof);
                        profiles.Add("SYNCH_KONTRAH_FILTR_AGENT", prof);
                    }
                    profiles["SYNCH_KONTRAH_FILTR_AGENT"].Value = synchKontrahFiltrAgentCheckBox.Checked.ToString();

                    if (!profiles.ContainsKey("SYNCH_TOWARY_ENABLED"))
                    {
                        var prof = new SynchProfileItem()
                        {
                            TableName = "Users",
                            Parent = user.ID,
                            Type = "bool",
                            Key = "SYNCH_TOWARY_ENABLED"
                        };
                        dc.AddToSynchProfileItems(prof);
                        profiles.Add("SYNCH_TOWARY_ENABLED", prof);
                    }
                    profiles["SYNCH_TOWARY_ENABLED"].Value = synchTowaryEnabledCheckBox.Checked.ToString();

                    if (!profiles.ContainsKey("SYNCH_ZAMOWIENIA_ENABLED"))
                    {
                        var prof = new SynchProfileItem()
                        {
                            TableName = "Users",
                            Parent = user.ID,
                            Type = "bool",
                            Key = "SYNCH_ZAMOWIENIA_ENABLED"
                        };
                        dc.AddToSynchProfileItems(prof);
                        profiles.Add("SYNCH_ZAMOWIENIA_ENABLED", prof);
                    }
                    profiles["SYNCH_ZAMOWIENIA_ENABLED"].Value = synchZamowEnabledCheckBox.Checked.ToString();

                    if (!profiles.ContainsKey("SYNCH_ZAMOWIENIA_FILTR_AGENT"))
                    {
                        var prof = new SynchProfileItem()
                        {
                            TableName = "Users",
                            Parent = user.ID,
                            Type = "bool",
                            Key = "SYNCH_ZAMOWIENIA_FILTR_AGENT"
                        };
                        dc.AddToSynchProfileItems(prof);
                        profiles.Add("SYNCH_ZAMOWIENIA_FILTR_AGENT", prof);
                    }
                    profiles["SYNCH_ZAMOWIENIA_FILTR_AGENT"].Value = synchZamFiltrAgentCheckBox.Checked.ToString();


                }


                canClose = true;
                Close();
            }
        }

        private void UzytkownikEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
                canClose = true;
            }
        }

        private void UzytkownikEditForm_Load(object sender, EventArgs e)
        {
            Enova.Business.Old.DB.Web.User user = (Enova.Business.Old.DB.Web.User)this.DataSource;
            if (user.EntityState != System.Data.EntityState.Added && user.EntityState != System.Data.EntityState.Detached)
            {
                Enova.Business.Old.DB.Web.WebContext dc = Enova.Business.Old.Core.ContextManager.WebContext;
                profiles = dc.SynchProfileItems.Where(s => s.TableName == "Users" && s.Parent == user.ID).ToDictionary(p => p.Key);
                if (profiles.ContainsKey("SYNCH_AGENT_KODE"))
                   synchAgentKodeTextBox.Text = profiles["SYNCH_AGENT_KODE"].Value;
                

                bool b = false;
                if (profiles.ContainsKey("SYNCH_KONTRAH_ENABLED"))
                    bool.TryParse(profiles["SYNCH_KONTRAH_ENABLED"].Value, out b);
                synchKontrahEnabledCheckBox.Checked = b;

                b = false;
                if (profiles.ContainsKey("SYNCH_KONTRAH_FILTR_AGENT"))
                    bool.TryParse(profiles["SYNCH_KONTRAH_FILTR_AGENT"].Value, out b);
                synchKontrahFiltrAgentCheckBox.Checked = b;

                b = false;
                if (profiles.ContainsKey("SYNCH_TOWARY_ENABLED"))
                    bool.TryParse(profiles["SYNCH_TOWARY_ENABLED"].Value, out b);
                synchTowaryEnabledCheckBox.Checked = b;

                b = false;
                if (profiles.ContainsKey("SYNCH_ZAMOWIENIA_ENABLED"))
                    bool.TryParse(profiles["SYNCH_ZAMOWIENIA_ENABLED"].Value, out b);
                synchZamowEnabledCheckBox.Checked = b;

                b = false;
                if (profiles.ContainsKey("SYNCH_ZAMOWIENIA_FILTR_AGENT"))
                    bool.TryParse(profiles["SYNCH_ZAMOWIENIA_FILTR_AGENT"].Value, out b);
                synchZamFiltrAgentCheckBox.Checked = b;
            }
        }
    }
}
