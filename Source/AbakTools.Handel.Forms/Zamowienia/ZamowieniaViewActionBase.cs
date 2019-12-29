using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;

namespace AbakTools.Zamowienia.Forms
{
    public class ZamowieniaViewActionBase
    {
        #region Fields

        private List<TypStatusuZamowienia> dostepneStatusy;
        private List<RodzajTransportu> dostepneRodzajeTransportu;
        private Control control;

        #endregion

        #region Properties

        public ActionTarget Target
        {
            get { return ActionTarget.GridHeader; }
        }

        public Control Control
        {
            get { return this.control; }
            set
            {
                this.control = value;
                if (this.ZmienWidocznosc && !this.ZawszeWidoczne)
                    this.control.Visible = false;
                if (this.ZmienDostepnosc)
                    this.control.Enabled = false;
            }
        }

        public bool WaitCursorEnable
        {
            get { return true; }
        }

        public ActionInvoker FireReload { get; set; }
        public ActionInvoker FireRefresh { get; set; }
        public ActionInvoker FireSelectionChanged { get; set; }

        public ZamowieniaViewStatusAction StatusAction
        {
            get
            {
                return ZamowieniaViewStatusAction.Instance;
            }
        }

        public virtual RodzajTransportu? DostepnyRodzajTransportu
        {
            get { return null; }
        }

        public virtual bool TylkoAdmin
        {
            get { return false; }
        }

        public virtual bool TylkoSuperAdmin
        {
            get { return false; }
        }

        public virtual bool TylkoMagazynier
        {
            get { return false; }
        }

        protected virtual bool ZawszeWidoczne
        {
            get { return false; }
        }

        protected virtual bool TylkoPojedynczyWiersz
        {
            get { return true; }
        }

        protected virtual bool SprawdzPrzedstawiciela
        {
            get { return false; }
        }

        protected virtual bool ZmienWidocznosc
        {
            get { return true; }
        }

        protected virtual bool ZmienDostepnosc
        {
            get { return false; }
        }

        #endregion

        #region Methods

        public ZamowieniaViewActionBase()
        {
            this.dostepneStatusy = new List<TypStatusuZamowienia>();
        }

        public virtual void OnSelectionChanged()
        {
            var inst = ZamowieniaViewStatusAction.Instance;
            TypStatusuZamowienia status = inst.StatusZamowienia == null ? TypStatusuZamowienia.Nieznany : inst.StatusZamowienia.Value;
            RodzajTransportu transport = StatusAction.Transport;
            int iloscWierszy = StatusAction.SelectedRows != null ? StatusAction.SelectedRows.Count : (StatusAction.CurrentRow != null ? 1 : 0);
            if (this.ZawszeWidoczne || this.CheckAnd() && this.dostepneStatusy.Contains(status) &&
                this.checkTransport() &&
                (!this.TylkoAdmin || StatusAction.IsAdmin) &&
                (!this.TylkoSuperAdmin || StatusAction.IsSuperAdmin) &&
                (!this.TylkoMagazynier || StatusAction.IsMagazynier) &&
                (!this.TylkoPojedynczyWiersz || iloscWierszy == 1) &&
                (!this.SprawdzPrzedstawiciela || StatusAction.IsAdmin || User.LoginedUser.Login == StatusAction.Przedstawiciel)
                 || this.CheckOr()
                )
            {
                if (this.ZmienWidocznosc)
                    this.control.Visible = true;
                if (this.ZmienDostepnosc)
                    this.control.Enabled = true;
            }
            else
            {
                if (this.ZmienWidocznosc)
                    this.control.Visible = false;
                if (this.ZmienDostepnosc)
                    this.control.Enabled = false;
            }
        }

        protected virtual bool CheckAnd()
        {
            return true;
        }

        protected virtual bool CheckOr()
        {
            return false;
        }

        protected void AddStatus(TypStatusuZamowienia status)
        {
            if (!this.dostepneStatusy.Contains(status))
                this.dostepneStatusy.Add(status);
        }

        protected void AddTransport(RodzajTransportu transport)
        {
            if (this.dostepneRodzajeTransportu == null)
                this.dostepneRodzajeTransportu = new List<RodzajTransportu>();
            if (!this.dostepneRodzajeTransportu.Contains(transport))
                this.dostepneRodzajeTransportu.Add(transport);
        }

        private bool checkTransport()
        {
            RodzajTransportu transport = StatusAction.Transport;
            if (this.dostepneRodzajeTransportu != null)
                return this.dostepneRodzajeTransportu.Contains(transport);
            else if (this.DostepnyRodzajTransportu != null)
                return this.DostepnyRodzajTransportu.Value == transport;
            return true;
        }

        public void Reload()
        {
            if (this.FireReload != null)
                this.FireReload(this);
        }

        public void SaveChanges()
        {
            Enova.Business.Old.Core.ContextManager.WebContext.OptimisticSaveChanges();
        }

        public void RefreshSelectedRows()
        {
            if (StatusAction.SelectedRows != null && StatusAction.SelectedRows.Count > 0)
                Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, StatusAction.SelectedRows);
        }

        public void Refresh(bool fireSelectionChanged = true)
        {
            if (this.FireRefresh != null)
                this.FireRefresh(this);
            if (fireSelectionChanged)
                this.SelectionChanged();
        }

        public void SelectionChanged()
        {
            if (this.FireSelectionChanged != null)
                this.FireSelectionChanged(this);
        }

        #endregion
    }
}
