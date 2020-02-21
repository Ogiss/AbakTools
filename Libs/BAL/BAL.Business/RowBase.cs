using System;
using System.Dynamic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BAL.Types;

namespace BAL.Business
{
    public abstract class RowBase : MarshalByRefObject, ICloneable, IRowInvoker
    {

        #region Fields

        #endregion


        #region Properties

        public virtual bool NewApi
        {
            get { return false; }
        }

        #endregion

        #region Methods



        public virtual object Clone()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnDeleting(CancelEventArgs e)
        {
            if (this.Deleting != null)
                this.Deleting(this, e);
        }

        protected virtual void OnDeleted(EventArgs e)
        {
            if (this.Deleted != null)
                this.Deleted(this, e);
        }

        protected virtual void OnAdded(EventArgs e)
        {
            if (this.Added != null)
                this.Added(this, e);
        }

        protected virtual void OnLoaded(EventArgs e)
        {
            if (this.Loaded != null)
                this.Loaded(this, e);
        }

        protected virtual void OnEdited(EventArgs e)
        {
            if (this.Edited != null)
                this.Edited(this, e);
        }

        protected virtual void OnSaved(EventArgs e)
        {
            if (this.Saved != null)
                this.Saved(this, e);
        }

        public void Invoke(RowInvokeType type, EventArgs e)
        {
            switch (type)
            {
                case RowInvokeType.Added:
                    this.OnAdded(e);
                    break;
                case RowInvokeType.Deleted:
                    this.OnDeleted(e);
                    break;
                case RowInvokeType.Deleting:
                    this.OnDeleting((CancelEventArgs)e);
                    break;
                case RowInvokeType.Edited:
                    this.OnEdited(e);
                    break;
                case RowInvokeType.Loaded:
                    this.OnLoaded(e);
                    break;
                case RowInvokeType.Saved:
                    this.OnSaved(e);
                    break;
            }

            RowInvokerAttribute.Invoke(type, this, e);
        }

        #endregion

        #region Events

        public event EventHandler<CancelEventArgs> Deleting;
        public event EventHandler Deleted;
        public event EventHandler Added;
        public event EventHandler Loaded;
        public event EventHandler Edited;
        public event EventHandler Saved;

        #endregion

        #region Nested Types

        public class RowBaseConfiguration<R> : EntityTypeConfiguration<R>
            where R : RowBase
        {
            public RowBaseConfiguration()
            {
            }
        }

        #endregion

    }
}
