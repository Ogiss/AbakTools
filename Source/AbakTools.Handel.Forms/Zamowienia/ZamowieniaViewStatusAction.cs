using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Types;
using Enova.Business.Old.DB.Web;

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewStatusAction), DataContextKey = "ZamowieniaView")]

namespace AbakTools.Zamowienia.Forms
{
    [Priority(0)]
    public class ZamowieniaViewStatusAction
    {
        #region Fields

        private static ZamowieniaViewStatusAction instance;
        private TypStatusuZamowienia? status = null;
        private bool tensamStatus = false;
        private bool statusPracIsNull = false;
        private RodzajTransportu transport = RodzajTransportu.NieWybrano;
        private bool tensamKontr = false;
        private int? kontrah = null;
        private string przedstawiciel;
        private bool wystawionoFakture;

        #endregion

        #region Properties

        public static ZamowieniaViewStatusAction Instance
        {
            get { return instance; }
        }

        public IList SelectedRows { get; set; }

        public ZamowienieView CurrentRow { get; set; }

        public TypStatusuZamowienia? StatusZamowienia
        {
            get { return this.status; }
        }

        public bool TenSamStatus
        {
            get { return this.tensamStatus; }
        }

        public bool TenSamKontrahent
        {
            get { return this.tensamKontr; }
        }

        public bool PracownikIsNull
        {
            get { return this.statusPracIsNull; }
        }

        public RodzajTransportu Transport
        {
            get { return this.transport; }
        }

        public int? KontrahentID
        {
            get { return this.kontrah; }
        }

        public bool WystawionoFakture
        {
            get { return this.wystawionoFakture; }
        }

        public bool IsAdmin
        {
            get
            {
                return User.LoginedUser.IsAdmin != null && User.LoginedUser.IsAdmin.Value ||
                User.LoginedUser.IsSuperAdmin != null && User.LoginedUser.IsSuperAdmin.Value;
            }
        }

        public bool IsSuperAdmin
        {
            get { return User.LoginedUser.IsSuperAdmin != null && User.LoginedUser.IsSuperAdmin.Value; }
        }
        public bool IsMagazynier
        {
            get { return User.LoginedUser.IsWarehouseman != null && User.LoginedUser.IsWarehouseman.Value || this.IsAdmin; }
        }

        public string Przedstawiciel
        {
            get { return this.przedstawiciel; }
        }
    

        #endregion

        #region Methods

        public ZamowieniaViewStatusAction()
        {
            instance = this;
        }

        public void OnSelectionChanged()
        {
            this.tensamKontr = false;
            this.kontrah = null;
            this.status = null;
            this.tensamStatus = false;
            this.statusPracIsNull = false;
            this.przedstawiciel = null;
            bool flag3 = false;
            this.wystawionoFakture = false;

            if (this.SelectedRows != null)
            {
                foreach (ZamowienieView zam in this.SelectedRows)
                {
                    if (kontrah == null || tensamKontr)
                    {
                        if (kontrah == null)
                        {
                            kontrah = zam.KontrahentID;
                            tensamKontr = true;
                        }
                        else if (zam.KontrahentID != kontrah)
                        {
                            this.tensamKontr = false;
                        }
                    }

                    if (this.status == null || this.tensamStatus)
                    {

                        if (this.status == null)
                        {
                            status = zam.StatusTyp == null ? (TypStatusuZamowienia?)null : zam.TypStatusu;
                            transport = zam.RodzajTransportu;
                            tensamStatus = true;
                        }
                        else if (status != zam.TypStatusu || zam.TypStatusu == TypStatusuZamowienia.NoweZamowienie && transport != zam.RodzajTransportu)
                        {
                            tensamStatus = false;
                        }
                    }

                    if (string.IsNullOrEmpty(this.przedstawiciel) || flag3)
                    {
                        if (string.IsNullOrEmpty(this.przedstawiciel))
                        {
                            this.przedstawiciel = zam.PrzedstawicielKod;
                            flag3 = true;
                        }
                        else if (this.przedstawiciel != zam.PrzedstawicielKod)
                        {
                            flag3 = false;
                        }
                    }

                    if (!this.statusPracIsNull && zam.StatusPrID == null)
                        this.statusPracIsNull = true;

                    if (zam.FakturaGuid != null)
                        this.wystawionoFakture = true;
                }

            }
        }

        #endregion
    }
}
