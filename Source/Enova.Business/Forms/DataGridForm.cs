using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.Forms
{
    public partial class DataGridForm : Enova.Business.Old.Forms.DataBaseForm, Enova.Business.Old.Core.IProcessEnterKey, Enova.Business.Old.Controls.ISelectForm
    {
        #region Fields

        private string sortColumn = null;
        private string sortOrder = null;

        #endregion

        #region Properties
        [Browsable(false)]
        public object DataSource
        {
            get { return DataGridBindingSource.DataSource; }
            set {
                
                //DataGridBindingSource.DataSource = value;
                setDataSource(value);
            }
        }

        private delegate void setDataSourceDelegate(object ds);
        private void setDataSource(object ds)
        {
            if (DataGrid.InvokeRequired)
            {
                var d = new setDataSourceDelegate(setDataSource);
                DataGrid.Invoke(d, new object[] { ds });
            }
            else
            {
                DataGridBindingSource.DataSource = ds;
            }
        }

        [Browsable(true)]
        public string Filter
        {
            get { return DataGridBindingSource.Filter; }
            set { DataGridBindingSource.Filter = value; }
        }

        [Browsable(true)]
        public string ConfigFile { get; set; }


        [Browsable(true), Category("Panels")]
        public SplitterPanel LeftPanel
        {
            get { return LeftSplitContainer.Panel1; }
        }

        [Browsable(true), Category("Panels")]
        public SplitterPanel RightPanel
        {
            get { return RightSplitContainer.Panel2; }
        }

        [Browsable(true), Category("Panels")]
        public SplitterPanel TopPanel
        {
            get { return TopSplitContainer.Panel1; }
        }

        [Browsable(true), Category("Panels")]
        public SplitterPanel BottomPanel
        {
            get { return BottomSplitContainer.Panel2; }
        }

        [Browsable(true),Category("Panels collapsed")]
        public bool LeftPanelCollapsed
        {
            get { return LeftSplitContainer.Panel1Collapsed; }
            set { LeftSplitContainer.Panel1Collapsed = value; }
        }

        [Browsable(true), Category("Panels collapsed")]
        public bool RightPanelCollapsed
        {
            get { return RightSplitContainer.Panel2Collapsed; }
            set { RightSplitContainer.Panel2Collapsed = value; }
        }

        [Browsable(true), Category("Panels collapsed")]
        public bool TopPanelCollapsed
        {
            get { return TopSplitContainer.Panel1Collapsed; }
            set { TopSplitContainer.Panel1Collapsed = value; }
        }

        [Browsable(true), Category("Panels collapsed")]
        public bool BottomPanelCollapsed
        {
            get { return BottomSplitContainer.Panel2Collapsed; }
            set { BottomSplitContainer.Panel2Collapsed = value; }
        }

        #endregion

        #region Constructors

        public DataGridForm()
        {
            InitializeComponent();
            DataGrid.ParentForm = this;
        }

        #endregion

        #region Methods

        protected virtual void LoadData() { }

        private Type getEditFormType(Type type)
        {
            if (DataSource != null)
            {
                var attributes = type.GetCustomAttributes(typeof(Enova.Business.Old.Core.DataEditFormAttribute), false);
                foreach (var a in attributes)
                {
                    //return Type.GetType(((Enova.Business.Core.DataEditFormAttribute)attributes[0]).TypeName);
                    return Type.GetType(((Enova.Business.Old.Core.DataEditFormAttribute)a).TypeName);
                }
            }
            return null;
        }

        protected virtual DialogResult EditRecord(object record)
        {
            object dataBound = (record is Enova.Business.Old.Core.IProxyObject) ? ((Enova.Business.Old.Core.IProxyObject)record).ProxyData : record;
            Type type = getEditFormType(dataBound.GetType());
            if (type != null)
            {
                var form = Activator.CreateInstance(type);
                if (form != null && form is Enova.Business.Old.Core.IDataSource)
                {
                    ((Enova.Business.Old.Core.IDataSource)form).DataSource = dataBound;
                    return ((Form)form).ShowDialog();
                }
            }

            return DialogResult.None;
        }

        protected virtual void EditCurrentRow()
        {
            DataGridViewRow row = DataGrid.CurrentRow;
            if (row != null)
            {
                DialogResult result;
                if (row.DataBoundItem != null)
                {
                    object record = row.DataBoundItem;
                    BeforeEditRowEventArgs e = new BeforeEditRowEventArgs(record);
                    OnBeforeEditRow(e);
                    if (!e.Cancel)
                    {
                        result = EditRecord(record);
                        OnAfterEditRow(new AfterEditRowEventArgs(record));
                    }
                    else
                    {
                        result = DialogResult.Cancel;
                    }

                }
                else
                {
                    result = EditRecord(row);
                }

                if (result == DialogResult.OK)
                {
                    DataGrid.Refresh();
                }
            }
        }

        protected virtual void CloneCurrentRow()
        {
            DataGridViewRow row = DataGrid.CurrentRow;
            if (row != null)
            {
                DialogResult result = System.Windows.Forms.DialogResult.None;
                if (row.DataBoundItem != null && row.DataBoundItem is ICloneable)
                {
                    var record = ((ICloneable)row.DataBoundItem).Clone();
                    BeforeEditRowEventArgs e = new BeforeEditRowEventArgs(record);
                    OnBeforeEditRow(e);
                    if (!e.Cancel)
                    {
                        result = EditRecord(record);
                        OnAfterEditRow(new AfterEditRowEventArgs(record));
                    }
                    else
                    {
                        result = DialogResult.Cancel;
                    }

                }

                if (result == DialogResult.OK)
                    DataGrid.Refresh();
            }
        }

        protected virtual void AddNewRow()
        {
            if (DataGridBindingSource.DataSource is Enova.Business.Old.Core.IElementType)
            {
                Type rowType = ((Enova.Business.Old.Core.IElementType)DataGridBindingSource.DataSource).GetElementType();
                Type type = getEditFormType(rowType);
                if (type != null)
                {
                    var form = Activator.CreateInstance(type);
                    if (form != null)
                    {
                        if (form is Enova.Business.Old.Core.IDataSource)
                        {
                            if (DataGridBindingSource.DataSource is Enova.Business.Old.Core.ICreateNewRecord)
                                ((Enova.Business.Old.Core.IDataSource)form).DataSource = ((Enova.Business.Old.Core.ICreateNewRecord)DataGridBindingSource.DataSource).CreateNewRecord();
                            else
                                ((Enova.Business.Old.Core.IDataSource)form).DataSource = Activator.CreateInstance(rowType);
                        }
                        DialogResult result = ((Form)form).ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            bindingNavigatorRefreshItem_Click(null, null);
                            if (form is Enova.Business.Old.Core.IDataSource)
                                OnAfterAddNewRow(new AfterEditRowEventArgs(((Enova.Business.Old.Core.IDataSource)form).DataSource));
                        }

                    }
                }
            }
        }

        protected virtual void DeleteCurrentRow()
        {
            if (DataGrid.CurrentRow != null && DataGrid.CurrentRow.DataBoundItem != null)
            {
                object row = DataGrid.CurrentRow.DataBoundItem;
                if (row is Enova.Business.Old.Core.IProxyObject)
                {
                    row = ((Enova.Business.Old.Core.IProxyObject)row).ProxyData;
                }
                if (row != null && row is Enova.Business.Old.Core.IDeleteRecord)
                {
                    DialogResult result = MessageBox.Show("Chy napewno chcesz usunąć rekord?", "EnovaTools", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        ((Enova.Business.Old.Core.IDeleteRecord)row).DeleteRecord();
                        bindingNavigatorRefreshItem_Click(null, null);
                        OnAfterDeleteRow(new AfterEditRowEventArgs(row));
                    }
                }
            }
        }


        private string getConfigFullPath()
        {
            string fullPath;
            if (Path.IsPathRooted(ConfigFile))
            {
                fullPath = ConfigFile;
            }
            else
            {
                fullPath = Directory.GetCurrentDirectory() + "\\" + ConfigFile;
            }
            return fullPath;
        }

        public void LoadConfig()
        {
            if (!string.IsNullOrEmpty(ConfigFile))
            {
                string fullPath = getConfigFullPath();


                if (File.Exists(fullPath))
                {
                    XmlTextReader xml = new XmlTextReader(fullPath);
                    xml.Read();
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element)
                        {
                            if (xml.Name.ToLower() == "gridconfig")
                                continue;

                            if (xml.Name.ToLower() == "sort")
                                loadSort(xml);

                            if (xml.Name.ToLower() == "columns")
                                loadColumns(xml);

                            if (xml.Name.ToLower() == "panels")
                                loadPanels(xml);


                        }
                    }
                }
                else
                    SaveConfig();

            }
        }

        private void loadSort(XmlTextReader xml)
        {
            while (xml.Read())
            {
                if (xml.NodeType == XmlNodeType.Element)
                {
                    if (xml.Name.ToLower() == "column")
                    {
                        sortColumn = XT(xml);
                    }
                    else if (xml.Name.ToLower() == "order")
                    {
                        sortOrder = XT(xml);
                    }
                }
                else if (xml.NodeType == XmlNodeType.EndElement)
                    break;
            }

            /*
             */

        }

        private void loadColumns(XmlTextReader xml)
        {
            DataGrid.Columns.Clear();

            while (xml.Read())
            {
                if (xml.NodeType == XmlNodeType.Element)
                {
                    if (xml.Name == "Column")
                        loadColum(xml);
                }
                else if (xml.NodeType == XmlNodeType.EndElement)
                    break;
            }
        }

        private void loadColum(XmlTextReader xml)
        {
            DataGridViewColumn column = new DataGridViewColumn();

            while (xml.Read())
            {
                if (xml.NodeType == XmlNodeType.Element)
                {
                    switch (xml.Name.ToLower())
                    {
                        case "name":
                            column.Name = XT(xml);
                            break;
                        case "headertext":
                            column.HeaderText = XT(xml);
                            break;
                        case "datapropertyname":
                            column.DataPropertyName = XT(xml);
                            break;
                        case "celltemplate":
                            Type type = Type.GetType(XT(xml));
                            if (type != null)
                            {
                                column.CellTemplate = (DataGridViewCell)Activator.CreateInstance(type);
                            }
                            break;
                        case "defaultcellstyle":
                            column.DefaultCellStyle = loadCellStyle(xml);
                            break;
                        case "sortmode":
                            string value = XT(xml);
                            column.SortMode =
                                (DataGridViewColumnSortMode)Enum.Parse(typeof(DataGridViewColumnSortMode), value);
                            break;
                        case "width":
                            column.Width = int.Parse(XT(xml));
                            break;
                    }
                }
                else if (xml.NodeType == XmlNodeType.EndElement)
                    break;
            }
            if (column.CellTemplate == null)
                column.CellTemplate = new DataGridViewTextBoxCell();
            DataGrid.Columns.Add(column);
        }

        private DataGridViewCellStyle loadCellStyle(XmlTextReader xml)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            while (xml.Read())
            {
                if (xml.NodeType == XmlNodeType.Element)
                {
                    switch (xml.Name.ToLower())
                    {
                        case "aligment":
                            string value = XT(xml);
                            if (value.ToLower() != "notset")
                            {
                                style.Alignment =
                                    (DataGridViewContentAlignment)Enum.Parse(typeof(DataGridViewContentAlignment), value);
                            }
                            break;
                    }
                }
                else if (xml.NodeType == XmlNodeType.EndElement)
                    break;
            }

            return style;
        }

        private void loadPanels(XmlTextReader xml)
        {
            while (xml.Read())
            {
                if (xml.NodeType == XmlNodeType.Element)
                {
                    string name = xml.Name.ToLower().Replace("panel", "");
                    loadPanel(xml, name);
                }
                else if (xml.NodeType == XmlNodeType.EndElement)
                    break;
            }
        }

        private void loadPanel(XmlTextReader xml, string name)
        {
            SplitContainer split = null;
            switch (name)
            {
                case "left":
                    split = LeftSplitContainer;
                    break;
                case "right":
                    split = RightSplitContainer;
                    break;
                case "top":
                    split = TopSplitContainer;
                    break;
                case "bottom":
                    split = BottomSplitContainer;
                    break;
            }

            while (xml.Read())
            {
                if (xml.NodeType == XmlNodeType.Element)
                {
                    if (split != null)
                    {
                        int value = int.Parse(XT(xml));
                        split.SplitterDistance = value;
                    }
                }
                else if (xml.NodeType == XmlNodeType.EndElement)
                    break;
            }
        }

        private string XT(XmlTextReader xml)
        {
            string text = null;
            while (xml.Read())
            {
                if (xml.NodeType == XmlNodeType.Text)
                {
                    text = xml.Value;
                }
                else if (xml.NodeType == XmlNodeType.EndElement)
                    break;

            }

            return text;
        }

        public void SaveConfig()
        {
            if (!string.IsNullOrEmpty(ConfigFile))
            {
                string fullPath = getConfigFullPath();
                string dir = Path.GetDirectoryName(fullPath);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                XmlTextWriter xml = new XmlTextWriter(fullPath, null);

                xml.WriteStartDocument();
                xml.WriteStartElement("GridConfig");
                if (DataGrid.SortedColumn != null)
                {
                    xml.WriteStartElement("Sort");
                    xml.WriteStartElement("Column");
                    xml.WriteString(DataGrid.SortedColumn.Name);
                    xml.WriteEndElement();
                    xml.WriteStartElement("Order");
                    xml.WriteString(DataGrid.SortOrder.ToString());
                    xml.WriteEndElement();
                    xml.WriteEndElement();
                }

                savePanels(xml);

                //Save columns config
                xml.WriteStartElement("Columns");
                foreach (DataGridViewColumn c in DataGrid.Columns)
                {
                    xml.WriteStartElement("Column");

                    xml.WriteStartElement("Name");
                    xml.WriteString(c.Name);
                    xml.WriteEndElement();

                    xml.WriteStartElement("HeaderText");
                    xml.WriteString(c.HeaderText);
                    xml.WriteEndElement();

                    xml.WriteStartElement("DataPropertyName");
                    xml.WriteString(c.DataPropertyName);
                    xml.WriteEndElement();

                    xml.WriteStartElement("CellTemplate");
                    xml.WriteString(c.CellTemplate.GetType().AssemblyQualifiedName);
                    xml.WriteEndElement();

                    xml.WriteStartElement("SortMode");
                    xml.WriteString(c.SortMode.ToString());
                    xml.WriteEndElement();

                    saveCellStyle(xml, c.DefaultCellStyle);

                    xml.WriteStartElement("Width");
                    xml.WriteString(c.Width.ToString());
                    xml.WriteEndElement();

                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
                xml.WriteEndElement();
                xml.WriteEndDocument();
                xml.Close();
            }
        }

        private void saveCellStyle(XmlTextWriter xml, DataGridViewCellStyle style)
        {
            xml.WriteStartElement("DefaultCellStyle");
            xml.WriteStartElement("Aligment");
            xml.WriteString(style.Alignment.ToString());
            xml.WriteEndElement();
            //xml.WriteStartElement("BackColor");
            //xml.WriteString(style.BackColor.ToArgb().ToString());
            //xml.WriteEndElement();
            //xml.WriteStartElement("ForeColor");
            //xml.WriteString(style.ForeColor.ToArgb().ToString());
            //xml.WriteEndElement();
            xml.WriteEndElement();

        }

        private void savePanels(XmlTextWriter xml)
        {
            xml.WriteStartElement("Panels");

            xml.WriteStartElement("LeftPanel");
            xml.WriteStartElement("SplitterDistance");
            xml.WriteString(LeftSplitContainer.SplitterDistance.ToString());
            xml.WriteEndElement();
            xml.WriteEndElement();

            xml.WriteStartElement("RightPanel");
            xml.WriteStartElement("SplitterDistance");
            xml.WriteString(RightSplitContainer.SplitterDistance.ToString());
            xml.WriteEndElement();
            xml.WriteEndElement();

            xml.WriteStartElement("BottomPanel");
            xml.WriteStartElement("SplitterDistance");
            xml.WriteString(BottomSplitContainer.SplitterDistance.ToString());
            xml.WriteEndElement();
            xml.WriteEndElement();

            xml.WriteStartElement("TopPanel");
            xml.WriteStartElement("SplitterDistance");
            xml.WriteString(TopSplitContainer.SplitterDistance.ToString());
            xml.WriteEndElement();
            xml.WriteEndElement();


            xml.WriteEndElement();
        }

        public void Reload()
        {
            if (this.InvokeRequired)
            {
                var d = new MethodInvoker(Reload);
                this.Invoke(d);
            }
            else
            {
                this.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                LoadData();

                this.Cursor = Cursors.Default;
                this.Enabled = true;
            }
        }

        


        public virtual bool ProcessEnterKey(Keys keyData)
        {
            if (SelectMode)
            {
                SelectCurrentRow();
            }
            else
            {
                EditCurrentRow();
            }
            return true;
        }

        protected virtual void SelectCurrentRow()
        {
            if (DataGrid.CurrentRow != null)
            {
                object record = DataGrid.CurrentRow.DataBoundItem;
                this.selectedItem = (record is Enova.Business.Old.Core.IProxyObject) ? ((Enova.Business.Old.Core.IProxyObject)record).ProxyData : record;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


        #endregion

        #region Events

        [Browsable(true)]
        public event EventHandler PrintItemClick
        {
            add
            {
                bindingNavigatorPrintItem.Click += value;
            }
            remove
            {
                bindingNavigatorPrintItem.Click -= value;
            }
        }

        [Browsable(true)]
        public event EventHandler EmailItemClick
        {
            add
            {
                bindingNavigatorEditItem.Click += value;
            }
            remove
            {
                bindingNavigatorEditItem.Click -= value;
            }
        }

        [Bindable(true)]
        public event EventHandler RefreshItemClick;

        [Browsable(true)]
        public event EventHandler SaveItemClick
        {
            add
            {
                bindingNavigatorSavelItem.Click += value;
            }
            remove
            {
                bindingNavigatorSavelItem.Click -= value;
            }
        }

        [Browsable(true)]
        public event BeforeEditRowEventHandler BeforeEditRow;

        [Browsable(true)]
        public event AfterEditRowEventHandler AfterEditRow;

        [Browsable(true)]
        public event AfterEditRowEventHandler AfterAddNewRow;

        [Browsable(true)]
        public event AfterEditRowEventHandler AfterDeleteRow;

        #endregion

        #region Events Methods

        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                LoadConfig();

                this.Invoke(new MethodInvoker(LoadData));

                if (DataSource != null)
                {
                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortOrder)
                        && (DataSource is IBindingList) && (DataSource is Enova.Business.Old.Core.IElementType)
                        && ((IBindingList)DataSource).SupportsSorting)
                    {
                        DataGridViewColumn column = DataGrid.Columns[sortColumn];
                        SortOrder order = (SortOrder)Enum.Parse(typeof(SortOrder), sortOrder);
                        if (column != null && order != SortOrder.None)
                        {
                            Type type = ((Enova.Business.Old.Core.IElementType)DataSource).GetElementType();
                            PropertyDescriptor property = TypeDescriptor.GetProperties(type)[column.DataPropertyName];
                            if (property != null)
                            {
                                ((IBindingList)DataSource).ApplySort(property,
                                    order == SortOrder.Descending ? ListSortDirection.Descending :
                                    ListSortDirection.Ascending);
                                column.HeaderCell.SortGlyphDirection = order;
                            }
                        }
                    }

                    if (DataSource is Enova.Business.Old.Core.IReadOnly && ((Enova.Business.Old.Core.IReadOnly)DataSource).IsReadOnly)
                    {
                        DataGridBindingNavigator.Items.Remove(DataGridBindingNavigator.Items["bindingNavigatorAddNewItem"]);
                        DataGridBindingNavigator.Items.Remove(DataGridBindingNavigator.Items["bindingNavigatorDeleteItem"]);
                    }

                    DataGridBindingSource.DataSource = DataSource;
                }
                base.OnLoad(e);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BottomSplitContainer.Focus();
            BottomSplitContainer.ActiveControl = DataGrid;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (this.HideOnSelect)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        protected virtual void OnBeforeEditRow(BeforeEditRowEventArgs e)
        {
            if (BeforeEditRow != null)
                BeforeEditRow(this, e);
        }

        protected virtual void OnAfterEditRow(AfterEditRowEventArgs e)
        {
            if (AfterEditRow != null)
                AfterEditRow(this, e);
        }

        protected virtual void OnAfterAddNewRow(AfterEditRowEventArgs e)
        {
            if (AfterAddNewRow != null)
                AfterAddNewRow(this, e);
        }

        protected virtual void OnAfterDeleteRow(AfterEditRowEventArgs e)
        {
            if (AfterDeleteRow != null)
                AfterDeleteRow(this, e);
        }

        #endregion

        #region Events Handlers

        private void zapiszKonfiguracjęTabeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ConfigFile))
            {
                SaveConfig();
            }

        }

        private void bindingNavigatorEditItem_Click(object sender, EventArgs e)
        {
            EditCurrentRow();
        }

        private void DataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.SelectMode)
            {
                SelectCurrentRow();
            }
            else
            {
                EditCurrentRow();
            }

        }

        private void bindingNavigatorRefreshItem_Click(object sender, EventArgs e)
        {
            if (this.RefreshItemClick != null)
                this.RefreshItemClick(sender, e);

            this.Reload();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteCurrentRow();
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.KeyCode == Keys.Enter)
            {
                if (this.SelectMode)
                {
                    SelectCurrentRow();
                }
                else
                {
                    EditCurrentRow();
                }
                e.Handled = true;
            }
        }

        private void DataGridForm_Shown(object sender, EventArgs e)
        {
            BottomSplitContainer.Focus();
            BottomSplitContainer.Panel1.Focus();
            DataGrid.Focus();
        }

        private void DataGridForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.Alt == false && e.Shift == false)
            {
                bool handled = true;
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        AddNewRow();
                        break;
                    case Keys.Delete:
                        DeleteCurrentRow();
                        break;
                    case Keys.F2:
                        EditCurrentRow();
                        break;
                    case Keys.F5:
                        Reload();
                        break;
                    default:
                        handled = false;
                        break;
                }
                e.Handled = handled;
            }
        }

        #endregion

        #region ISelectForm Implementation

        private object selectedItem = null;
        public virtual object SelectedItem
        {
            get { return selectedItem; }
            set { this.selectedItem = value; }
        }

        private bool selectMode = false;
        public virtual bool SelectMode
        {
            get { return selectMode; }
            set { selectMode = value; }
        }

        public virtual bool HideOnSelect
        {
            get { return false; }
        }

        #endregion

        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void fileOpentoolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileAction();
        }

        protected virtual void OpenFileAction() { }

     }
}
