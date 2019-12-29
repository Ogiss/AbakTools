using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.MenuAction("Admin\\Zmień date dokumentu", typeof(Enova.Forms.Handel.ZmienDateForm), Priority = 1200, MenuAction = BAL.Forms.MenuActionsType.OpenFormModal)]

namespace Enova.Forms.Handel
{
    public partial class ZmienDateForm : Form
    {
        public ZmienDateForm()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (Enova.Business.Old.DB.Web.User.LoginedUser.IsSuperAdmin == null || !Enova.Business.Old.DB.Web.User.LoginedUser.IsSuperAdmin.Value)
            {
                MessageBox.Show("Nie posiadasz uprawnień do wykonania tej operacji");
                return;
            }

            try
            {
                this.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                using (var session = Enova.API.EnovaService.Instance.CreateSession())
                {
                    int id = int.Parse(this.idTextBox.Text);
                    //session.GetModule<API.Handel.HandelModule>().ZmienDateDokumentu(id, dateTimePicker.Value.Date);
                    var dh = (Enova.API.Handel.DokumentHandlowy)session.GetModule<Enova.API.Handel.HandelModule>().DokHandlowe[id];
                    if(dh!=null)
                    {
                        using(var t = session.CreateTransaction())
                        {
                            var save = dh.Stan;
                            dh.Stan = API.Handel.StanDokumentuHandlowego.Bufor;
                            if (dh.Obcy != null)
                                dh.Obcy.DataOtrzymania = dateTimePicker.Value.Date;
                            dh.Data = dateTimePicker.Value.Date;
                            dh.Stan = save;
                            t.Commit();
                        }
                        session.Save();
                    }
                }
                MessageBox.Show("Zmieniono date");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Enabled = true;
            }
            
        }
    }
}
