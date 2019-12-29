using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Core.Controls
{
    public partial class HistoriaDokumentuControl : BAL.Forms.Controls.BALControl
    {
        #region Fields

        private List<Enova.Business.Old.DB.Web.HistoriaDokumentu> historia;
        private Enova.Business.Old.DB.Web.HistoriaDokumentu lastHistoria;
        private bool fireChanged;

        #endregion

        #region Properties

        /*
        protected DokumentZHistoria Dokument
        {
            get
            {
                if (this.DataContext != null)
                    return this.DataContext.GetData() as DokumentZHistoria;
                return null;
            }
        }
         */

        protected Enova.Business.Old.DB.Web.IDokumentZHistoria Dokument
        {
            get
            {
                if (this.DataContext != null)
                    return this.DataContext.GetData() as Enova.Business.Old.DB.Web.IDokumentZHistoria;
                return null;
            }
        }


        #endregion

        #region Methods

        public HistoriaDokumentuControl()
        {
            InitializeComponent();
        }

        private void HistoriaDokumentuList_Load(object sender, EventArgs e)
        {

        }

        protected override void OnAfterBinding(EventArgs e)
        {
            if (Dokument != null)
            {
                this.historia = Dokument.Historia.ToList();
                this.lastHistoria = Dokument.OstatniaHistoriaDokumentu;
                initStatusy();
            }
           
           base.OnAfterBinding(e);
        }

        private void initStatusy()
        {
            this.fireChanged = false;

            if (this.DataContext != null && this.DataContext is Enova.Business.Old.Core.IContexable)
            {
                var dc = ((Enova.Business.Old.Core.IContexable)this.DataContext).DbContext as Enova.Business.Old.DB.Web.WebContext;
                if (dc != null)
                {
                    List<Enova.Business.Old.DB.Web.StatusDokumentu> statusy = new List<Enova.Business.Old.DB.Web.StatusDokumentu>();
                    //var t = CoreModule.GetInstance(DataContext).StatusyDokumentow.WgKategorii[Dokument.Table.TableName];
                    var t = dc.StatusyDokumentow.Where(r => r.Kategoria == Dokument.Table.TableName).ToList();
                    if (lastHistoria != null)
                    {

                        statusy.Add(lastHistoria.Status);
                        statusy.AddRange(lastHistoria.Status.Podrzedne.ToArray());
                        var flag = (int)OpcjeStatusuDokumentu.Niezalezny;
                        //var niezalezne = t.WgOpcji[OpcjeStatusuDokumentu.Niezalezny].ToList();
                        var niezalezne = t.Where(r => (r.OpcjeInt & flag) == flag).ToList();
                        if (niezalezne.Count > 0)
                            statusy.AddRange(niezalezne);
                    }
                    else
                    {
                        statusy.Add(new Enova.Business.Old.DB.Web.StatusDokumentu() { Nazwa = "Brak" });
                        var status = t.ToList().Where(r => (r.Opcje & OpcjeStatusuDokumentu.Domyslny) == OpcjeStatusuDokumentu.Domyslny).FirstOrDefault();
                        if (status != null)
                            statusy.Add(status);
                    }
                    statusyComboBox.Items.Clear();
                    statusyComboBox.Items.AddRange(statusy.ToArray());
                    statusyComboBox.SelectedIndex = 0;
                }
            }
            this.fireChanged = true;
        }

        private void zmienStatus(Enova.Business.Old.DB.Web.StatusDokumentu status)
        {
            if (fireChanged && status.ID != 0 && (lastHistoria == null || lastHistoria.Status.ID != status.ID))
            {
                var hist = Dokument.ZmienStatus(status);
                historia.Add(hist);
                lastHistoria = hist;
                initStatusy();
            }
        }

        private void historiaButton_Click(object sender, EventArgs e)
        {
            new HistoriaDokumentuListForm() { Dokument = this.Dokument }.ShowDialog();

        }

        private void statusyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            zmienStatus((Enova.Business.Old.DB.Web.StatusDokumentu)statusyComboBox.SelectedItem);
        }


        #endregion


    }
}
