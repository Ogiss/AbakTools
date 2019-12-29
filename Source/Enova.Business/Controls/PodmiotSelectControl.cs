using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class PodmiotSelectControl : UserControl
    {
        public PodmiotSelectControl()
        {
            InitializeComponent();
        }

        public PodmiotType? TypPodmiotu
        {
            get
            {
                if (podmiotTypeComboBox.SelectedIndex != -1)
                    return (PodmiotType)podmiotTypeComboBox.SelectedIndex;
                return null;
            }
            set
            {
                if (value != null)
                    podmiotComboBox.SelectedIndex = (int)value;
            }
        }

        public object Podmiot
        {
            get
            {
                return podmiotComboBox.SelectedItem;
            }
        }

        private void PodmiotSelectControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (!(Business.Old.DB.Web.User.LoginedUser.IsAdmin.Value || Business.Old.DB.Web.User.LoginedUser.IsSuperAdmin.Value) || Business.Old.DB.Web.User.LoginedUser.Login == "ŁD")
                {
                    podmiotTypeComboBox.Items.Add("Kontrahenci");
                    podmiotTypeComboBox.Items.Add("Pracownicy");
                }
                else
                {
                    podmiotTypeComboBox.Items.Add("Kontrahenci");
                    podmiotTypeComboBox.Items.Add("Pracownicy");
                    podmiotTypeComboBox.Items.Add("Banki");
                    podmiotTypeComboBox.Items.Add("Urzędy Skarbowe");
                    podmiotTypeComboBox.Items.Add("ZUSY");
                }
                podmiotTypeComboBox.SelectedIndex = 0;
                loadData();
            }
        }

        private void loadData()
        {
            if (!this.DesignMode && TypPodmiotu != null)
            {
                EnovaContext dc = ContextManager.DataContext;
                switch (TypPodmiotu.Value)
                {
                    case PodmiotType.Kontahent:
                        podmiotBindingSource.DataSource = (from k in dc.Kontrahenci orderby k.Kod select new Podmiot() { Row = k }).ToList();
                        break;
                    case PodmiotType.Pracownik:
                        if (!(Business.Old.DB.Web.User.LoginedUser.IsAdmin.Value || Business.Old.DB.Web.User.LoginedUser.IsSuperAdmin.Value) || Business.Old.DB.Web.User.LoginedUser.Login == "ŁD")
                            podmiotBindingSource.DataSource = (from p in dc.Pracownicy where p.Kod == Business.Old.DB.Web.User.LoginedUser.Login select new Podmiot() { Row = p }).ToList();
                        else
                            podmiotBindingSource.DataSource = (from p in dc.Pracownicy orderby p.Kod select new Podmiot() { Row = p }).ToList();
                        break;
                    case PodmiotType.Bank:
                        podmiotBindingSource.DataSource = (from b in dc.Banki orderby b.Kod select new Podmiot() { Row = b }).ToList();
                        break;
                    case PodmiotType.UrządSkarbowy:
                        podmiotBindingSource.DataSource = (from u in dc.UrzędySkarbowe orderby u.Kod select new Podmiot() { Row = u }).ToList();
                        break;
                    case PodmiotType.ZUS:
                        podmiotBindingSource.DataSource = (from z in dc.ZUSY orderby z.Kod select new Podmiot() { Row = z }).ToList();
                        break;
                }
            }
        }

        private void podmiotTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
