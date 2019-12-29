using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Forms
{
    public partial class DataBaseForm : Form, Enova.Business.Old.Core.IContexable
    {
        [Browsable(false)]
        System.Data.Objects.ObjectContext Enova.Business.Old.Core.IContexable.DbContext
        {
            get
            {
                return Enova.Business.Old.Core.ContextManager.DataContext;
            }
        }

        public Enova.Business.Old.DB.EnovaContext DataContext
        {
            get
            {
                return (Enova.Business.Old.DB.EnovaContext)((Enova.Business.Old.Core.IContexable)this).DbContext;
            }
        }

        private bool isLoaded = false;
        [Browsable(false)]
        public bool IsLoaded
        {
            get { return this.isLoaded; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            isLoaded = true;
        }


        public DataBaseForm()
        {
            InitializeComponent();
        }
    }
}
