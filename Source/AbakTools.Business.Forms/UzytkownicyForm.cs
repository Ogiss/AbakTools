using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.MenuAction("Konfiguracja\\Użytkownicy", typeof(AbakTools.Business.Forms.UzytkownicyForm), MenuAction = BAL.Forms.MenuActionsType.OpenFormModal, Priority = 1110)]

namespace AbakTools.Business.Forms
{
    public partial class UzytkownicyForm : Enova.Business.Old.Forms.DataGridForm
    {
        public UzytkownicyForm()
        {
            InitializeComponent();
        }

        private void UzytkownicyForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            //this.DataSource = new Enova.Business.Core.Users();
            this.DataSource = Enova.Business.Old.Core.ContextManager.WebContext.Users.OrderBy(u => u.Login);
        }

        protected override void AddNewRow()
        {
            var form = new UzytkownikEditForm()
            {
                DataSource = new Enova.Business.Old.DB.Web.User()
                {
                    IsAdmin = false,
                    IsAgent = false,
                    IsWarehouseman = false
                }
            };

            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Enova.Business.Old.DB.Web.User login = (Enova.Business.Old.DB.Web.User)form.DataSource;

                Enova.Business.Old.Core.ContextManager.WebContext.AddToUsers(login);
                Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
                loadData();
            }
        }

        protected override void EditCurrentRow()
        {
            if (DataGrid.CurrentRow != null)
            {
                var form = new UzytkownikEditForm()
                {
                    DataSource = DataGrid.CurrentRow.DataBoundItem
                };

                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();
                    loadData();
                }
                else
                {
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, DataGrid.CurrentRow.DataBoundItem);
                }
            }
        }


        protected override void DeleteCurrentRow()
        {
            DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć użytkownika", "EnovaTools", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                Enova.Business.Old.DB.Web.User login = (Enova.Business.Old.DB.Web.User)DataGrid.CurrentRow.DataBoundItem;

                Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(login);
                Enova.Business.Old.Core.ContextManager.WebContext.SaveChanges();

                loadData();
            }
        }

    }
}
