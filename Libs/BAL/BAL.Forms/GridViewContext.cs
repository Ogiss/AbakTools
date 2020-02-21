using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;

namespace BAL.Forms
{
    public class GridViewContext : BAL.Business.View
    {
        #region Fields

        private Form parentForm;

        #endregion

        #region Properties

        public virtual bool ExtraPanelAvailable
        {
            get { return false; }
        }

        public virtual bool ExtraPanelVisible
        {
            get { return false; }
        }

        public Form ParentForm
        {
            get { return this.parentForm; }
            internal set
            {
                this.parentForm = value;
                this.OnParentFormChanged(new EventArgs());
            }
        }

        #endregion

        #region Methods

        public GridViewContext(ITable table, string key = null)
            : base(table, key)
        {
        }

        public GridViewContext(Session session, string key = null) : base(session, key) { }

        public GridViewContext() : this((Session)null,null) { }

        public override bool EditRow(object row)
        {
            var form = this.GetEditForm();
            if (form != null)
            {
                //if (this.Session != null)
                //{
                var context = this.CreateContext(row);

                this.BeginEdit();

                if (this.IsEditing)
                {
                    if (form is IDataContexable)
                        ((IDataContexable)form).DataContext = context;

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        using (new BAL.Forms.WaitCursor(this.ParentForm))
                        {
                            this.EndEdit();
                            return true;
                        }
                    }
                    else
                        using (new BAL.Forms.WaitCursor(this.ParentForm))
                        {
                            this.CancelEdit();
                            //return false;
                        }
                    //}
                    //return true;
                }
                else
                    this.CancelEdit();
            }
            return false;
        }

        protected virtual Form GetEditForm()
        {
            var type = FormManager.FormService.GetDataEditFormType(this.GetDataType());
            if (type != null)
            {
                var form = (Form)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                form.StartPosition = FormStartPosition.CenterParent;
                return form;
            }
            return null;
        }

        public virtual void InitExtraPanel(Panel panel) { }

        public virtual void InitGridColumn(DataGridViewColumn column) { }

        #endregion

        #region Events

        public event EventHandler ParentFormChanged;

        #endregion

        #region Events Handlers

        protected virtual void OnParentFormChanged(EventArgs e)
        {
            if (this.ParentFormChanged != null)
                this.ParentFormChanged(this, e);
        }

        #endregion
    }
}
