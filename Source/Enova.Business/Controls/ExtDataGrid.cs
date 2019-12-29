using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace Enova.Business.Old.Controls
{
    [Designer(typeof(Enova.Business.Old.Controls.Design.ExtDataGridDesigner))]
    [ToolboxItem(false)]
    public partial class ExtDataGrid : UserControl, ISupportInitialize
    {
        #region Fields

        private bool sumBarVisible = false;

        #endregion

        #region Properties

        [Browsable(true), Category("Controls")]
        public DataGrid DataGrid
        {
            get { return DataGridControl; }
        }

        [Browsable(true), Category("Data")]
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        public object DataSource
        {
            get { return DataGridControl.DataSource; }
            set { DataGridControl.DataSource = value; }
        }

        [Browsable(true), Category("Data")]
        public string DataMember
        {
            get { return DataGridControl.DataMember; }
            set { DataGridControl.DataMember = value; }
        }

        [Browsable(true)]
        //[Editor(typeof(System.Windows.Forms.Design
        [Editor(typeof(Enova.Business.Old.Controls.Design.ExtDataGridColumnsEditor), typeof(UITypeEditor)), MergableProperty(false)]
        public DataGridViewColumnCollection Columns
        {
            get
            {
                return DataGridControl.Columns;
            }
        }

        [Browsable(true), Category("Appearance")]
        public bool SumBarVisible
        {
            get { return sumBarVisible; }
            set
            { 
                sumBarVisible = value;
                if (value)
                {
                    DataGridControl.Size = new Size(this.Size.Width, this.Size.Height - 20);
                }
                else
                {
                    DataGridControl.Size = new Size(this.Size.Width, this.Size.Height);
                }
            }
        }


        #endregion

        #region Constructors

        public ExtDataGrid()
        {
            InitializeComponent();
        }

        #endregion

        #region Operators

        public static explicit operator DataGridView(ExtDataGrid grid)
        {
            return (DataGridView)grid.DataGridControl;
        }

        #endregion

        #region ISupportInitialize Implementation

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        #endregion
    }
}
