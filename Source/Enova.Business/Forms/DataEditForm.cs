using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Forms
{
    public partial class DataEditForm : Enova.Business.Old.Forms.DataSourceForm, Enova.Business.Old.Core.ISaveChanges, Enova.Business.Old.Core.IUndoChanges
    {

        protected bool CancelClose = false;
        protected bool UndoChangesOnClose = true;

        public bool AllowSaveChanges = true;
        


        public DataEditForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsReadOnly)
            {
                anulujButton.Visible = false;
            }
        }

        [Browsable(true)]
        public event EventHandler BeforeSaveChanges;
        protected virtual void OnBeforeSaveChanges(EventArgs e)
        {
            if (BeforeSaveChanges != null)
                BeforeSaveChanges(this, e);
        }

        [Browsable(true)]
        public event EventHandler AfterSaveChanges;
        protected virtual void OnAfterSaveChanges(EventArgs e)
        {
            if (AfterSaveChanges != null)
                AfterSaveChanges(this, e);
        }

        #region ISaveChanges Implementation

        public virtual bool SaveChanges()
        {
            OnBeforeSaveChanges(new EventArgs());
            bool ret = false;
            if (!IsReadOnly && DataSource != null)
            {
                if (DataSource is Enova.Business.Old.Core.IValidation)
                {
                    if (!((Enova.Business.Old.Core.IValidation)DataSource).IsValid)
                    {
                        if (!string.IsNullOrEmpty(((Enova.Business.Old.Core.IValidation)DataSource).ValidationError))
                            MessageBox.Show(((Enova.Business.Old.Core.IValidation)DataSource).ValidationError, "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.CancelClose = true;
                        return false;
                    }
                }

                if (DataSource is Enova.Business.Old.Core.IContextSaveChanges)
                {
                    ret = ((Enova.Business.Old.Core.IContextSaveChanges)DataSource).SaveChanges(DataContext);
                }
                else if (DataSource is Enova.Business.Old.Core.ISaveChanges)
                {
                    ret = ((Enova.Business.Old.Core.ISaveChanges)DataSource).SaveChanges();
                }
                else if (DataContext != null && DataSource.GetType().IsAssignableFrom(typeof(System.Data.Objects.DataClasses.EntityObject)))
                {
                    DataContext.SaveChanges();
                    ret = true;
                }
            }
            if (ret)
                OnAfterSaveChanges(new EventArgs());
            UndoChangesOnClose = false;
            return ret;
        }

        #endregion

        #region IUndoChanges Implementation

        public virtual bool UndoChanges()
        {
            UndoChangesOnClose = false;
            if (!IsReadOnly && DataSource != null)
            {
                if (DataSource is Enova.Business.Old.Core.IContextUndoChanges)
                {
                    return ((Enova.Business.Old.Core.IContextUndoChanges)DataSource).UndoChanges(DataContext);
                }
                else if (DataSource is Enova.Business.Old.Core.IUndoChanges)
                {
                    return ((Enova.Business.Old.Core.IUndoChanges)DataSource).UndoChanges();
                }
                else if (DataContext != null && DataSource.GetType().IsAssignableFrom(typeof(System.Data.Objects.DataClasses.EntityObject)))
                {
                    DataContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, DataSource);
                    return true;
                }
            }
            
            return false;
        }

        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            /*
            if (!IsReadOnly)
                SaveChanges();
             */
            ProgressOk();
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            /*
            if (!IsReadOnly)
                UndoChanges();
             */
            ProgressCancel();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = this.CancelClose;
            this.CancelClose = false;

            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (UndoChangesOnClose)
                UndoChanges();
            base.OnClosed(e);
        }

        protected virtual bool OkValid()
        {
            return true;
        }

        public void ProgressOk()
        {
            if (OkValid())
            {
                if (!IsReadOnly && AllowSaveChanges)
                    SaveChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                CancelClose = true;
        }

        protected virtual bool CancelValid()
        {
            return true;
        }

        public void ProgressCancel()
        {
            if (CancelValid())
            {
                if (!IsReadOnly)
                    UndoChanges();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
                CancelClose = true;
        }

        private void DataEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Enter)
            {
                ProgressOk();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ProgressCancel();
                e.Handled = true;
            }
        }

        private void zatwierdźButton_Click(object sender, EventArgs e)
        {
            if (OkValid() && !IsReadOnly)
                this.SaveChanges();
        }
    }
}
