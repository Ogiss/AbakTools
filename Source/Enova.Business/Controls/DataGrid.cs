using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Enova.Business.Old.Controls
{
    [Designer(typeof(System.Windows.Forms.Design.ControlDesigner))]
    [ToolboxItem(false)]
    public class DataGrid : DataGridView
    {
        private Form parentForm = null;
        public Form ParentForm
        {
            get
            {
                return this.parentForm;
            }
            set
            {
                this.parentForm = value;
            }
        }

        [Browsable(true), Category("Design")]
        public new bool AutoGenerateColumns
        {
            get { return base.AutoGenerateColumns; }
            set { base.AutoGenerateColumns = value; }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if (key == Keys.Enter && this.parentForm != null && this.parentForm is Enova.Business.Old.Core.IProcessEnterKey)
                return ((Enova.Business.Old.Core.IProcessEnterKey)this.parentForm).ProcessEnterKey(keyData);
            return base.ProcessDialogKey(keyData);
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.parentForm != null && this.parentForm is Enova.Business.Old.Core.IProcessEnterKey)
                return ((Enova.Business.Old.Core.IProcessEnterKey)this.parentForm).ProcessEnterKey(e.KeyData);
            return base.ProcessDataGridViewKey(e);
        }


    }
}
