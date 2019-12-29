using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Core;
using DBWeb = Enova.Business.Old.DB.Web;

[assembly: DataContext(typeof(DBWeb.ZamowienieView), typeof(AbakTools.Zamowienia.Forms.ZamowienieViewContext))]
[assembly: DataContext(typeof(DBWeb.Zamowienie), typeof(AbakTools.Zamowienia.Forms.ZamowienieViewContext))]

namespace AbakTools.Zamowienia.Forms
{
    public class ZamowienieViewContext : DataContext
    {

        public DBWeb.Zamowienie Zamowienie
        {
            get
            {
                return this[typeof(DBWeb.Zamowienie)] as DBWeb.Zamowienie;
            }
        }

        public ZamowienieViewContext(object row)
            : base(null, true)
        {
            DBWeb.ZamowienieView view = row as DBWeb.ZamowienieView;
            if (view != null)
            {
                this.SetData(typeof(DBWeb.Zamowienie), view.Zamowienie);
                this.AddData(typeof(DBWeb.ZamowienieView), view);
            }
            else
                this.SetData(row.GetType(), row);
        }

        public ZamowienieViewContext() : base(null, true) { }

        public override void SetData(Type dataType, object data)
        {
            if (data is DBWeb.ZamowienieView)
            {
                base.SetData(((DBWeb.ZamowienieView)data).Zamowienie);
                AddData(data);
            }
            else
                base.SetData(dataType, data);
        }

        private void refreshView()
        {
            DBWeb.ZamowienieView view = this[typeof(DBWeb.ZamowienieView)] as DBWeb.ZamowienieView;
            if (view != null)
                ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, view);
        }

        public override void SaveChanges()
        {
            if (this.Zamowienie != null)
                this.Zamowienie.SaveChanges();
            this.refreshView();
        }

        public override void CancelEdit()
        {
            if (this.Zamowienie != null)
                this.Zamowienie.UndoChanges();
            base.CancelEdit();
        }

        protected override void OnBeforeEdit(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBeforeEdit(e);
            ZamowieniaView.DisableReload = true;
            if (this.Zamowienie != null)
            {
                if (this.Zamowienie.EntityState == System.Data.EntityState.Unchanged)
                {
                    ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this.Zamowienie);
                    if (this.Zamowienie.Blokada != null && this.Zamowienie.Blokada.Value)
                    {
                        if (!FormManager.Confirm("Zamówienie jest zablokowane do edycji przez innego operatora\r\nCzy chcesz kontynułować?"))
                        {
                            e.Cancel = true;
                            return;
                        }
                        this.Zamowienie.BlokadaEdycji = true;
                    }
                    else
                    {
                        this.Zamowienie.Blokada = true;
                        this.SaveChanges();
                        this.Reset();
                    }
                }
            }
        }

        protected override void OnAfterEdit(EventArgs e)
        {
            if (this.Zamowienie != null)
            {
                if (this.Zamowienie.EntityState == System.Data.EntityState.Modified || this.Zamowienie.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (Zamowienie.BlokadaEdycji)
                    {
                        Zamowienie.BlokadaEdycji = false;
                    }
                    else
                    {
                        Zamowienie.Blokada = false;
                    }
                }

                this.SaveChanges();
                this.Reset();
            }
            ZamowieniaView.DisableReload = false;
            base.OnAfterEdit(e);
        }
    }
}
