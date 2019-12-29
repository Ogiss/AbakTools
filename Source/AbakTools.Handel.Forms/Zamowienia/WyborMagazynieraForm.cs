using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Zamowienia.Forms
{
    public partial class WyborMagazynieraForm : Form
    {
        public Enova.Business.Old.DB.Web.User User = null;

        public WyborMagazynieraForm()
        {
            InitializeComponent();
        }

        private void WyborMagazynieraForm_Load(object sender, EventArgs e)
        {
            loadData();
            magazynierComboBox.SelectedItem = User;
        }

        private void loadData()
        {
            bindingSource.DataSource = Enova.Business.Old.Core.ContextManager.WebContext.Users
                .Where(u => u.IsPakowacz == true).OrderBy(u => u.Login).ToList();
        }

        private void magazynierComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.User = (Enova.Business.Old.DB.Web.User)magazynierComboBox.SelectedItem;
        }
    }
}
