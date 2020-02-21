using System;

namespace AbakTools.Forms
{
    public class DataContextBase : BAL.Business.DataContext, Enova.Business.Old.Core.IContexable
    {
        #region Fields

        private Enova.Business.Old.DB.EnovaContext enovaContext;

        #endregion

        #region Properties

        public Enova.Business.Old.DB.EnovaContext EnovaContext
        {
            get
            {
                if (enovaContext == null)
                    enovaContext = new Enova.Business.Old.DB.EnovaContext();
                return enovaContext;
            }
        }

        public System.Data.Objects.ObjectContext DbContext
        {
            get
            {
                if (Parent != null && Parent is Enova.Business.Old.Core.IContexable)
                    return ((Enova.Business.Old.Core.IContexable)Parent).DbContext;
                return null;
            }
        }

        public int ID
        {
            get
            {
                try
                {
                    return (int)GetValue("ID");
                }
                catch
                {
                    return 0;
                }
            }
        }

        public System.Data.EntityState EntityState
        {
            get
            {
                try
                {
                    return ((System.Data.Objects.DataClasses.EntityObject)this.Current).EntityState;
                }
                catch
                {
                    return System.Data.EntityState.Detached;
                }
            }
        }

        #endregion

        #region Methods

        public DataContextBase() : base(null, true) { }

        public override void EndEdit()
        {

            if (this.DbContext != null && this.Current is Enova.Business.Old.Core.IContextSaveChanges)
                ((Enova.Business.Old.Core.IContextSaveChanges)this.Current).SaveChanges(this.DbContext);
            else if (this.Current is Enova.Business.Old.Core.ISaveChanges)
                ((Enova.Business.Old.Core.ISaveChanges)this.Current).SaveChanges();

            base.EndEdit();
        }

        public override void CancelEdit()
        {
            if (this.Current is Enova.Business.Old.Core.IUndoChanges)
                ((Enova.Business.Old.Core.IUndoChanges)this.Current).UndoChanges();
            base.CancelEdit();
        }

        protected override void OnDisposed(EventArgs e)
        {
            base.OnDisposed(e);
            if (this.enovaContext != null)
            {
                this.enovaContext.Dispose();
                this.enovaContext = null;
            }
        }

        public void Refresh()
        {
            if (this.DbContext != null && this.Current != null && this.EntityState != System.Data.EntityState.Added && this.EntityState!= System.Data.EntityState.Detached)
            {
                this.DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this.Current);
            }
        }

        #endregion
    }
}
