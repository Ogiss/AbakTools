﻿using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Enova.Business.Old.Controls.Design
{
    public partial class ExtDataGridAddColumnDialog : Form
    {
        #region Fields

        private static Type[] columnTypes = new Type[] { 
            typeof(DataGridViewButtonColumn),
            typeof(DataGridViewCheckBoxColumn),
            typeof(DataGridViewComboBoxColumn),
            typeof(DataGridViewImageColumn),
            typeof(DataGridViewLinkColumn),
            typeof(DataGridViewTextBoxColumn)
        };

        private static Type dataGridViewColumnDesignTimeVisibleAttributeType = typeof(DataGridViewColumnDesignTimeVisibleAttribute);
        private static Type dataGridViewColumnType = typeof(DataGridViewColumn);
        private static Type iComponentChangeServiceType = typeof(IComponentChangeService);
        private static Type iDesignerHostType = typeof(IDesignerHost);
        private static Type iDesignerType = typeof(IDesigner);
        private static Type iHelpServiceType = typeof(IHelpService);
        private static Type iNameCreationServiceType = typeof(INameCreationService);
        private static Type iTypeDiscoveryServiceType = typeof(ITypeDiscoveryService);
        private static Type iTypeResolutionServiceType = typeof(ITypeResolutionService);
        private static Type iUIServiceType = typeof(IUIService);

        private ExtDataGrid liveDataGridView;
        private DataGridViewColumnCollection dataGridViewColumns;
        private int initialDataGridViewColumnsCount;
        private int insertAtPosition;
        private bool persistChangesToDesigner;



        #endregion

        #region Methods

        public ExtDataGridAddColumnDialog(DataGridViewColumnCollection dataGridViewColumns, ExtDataGrid liveDataGridView)
        {
            this.insertAtPosition = -1;
            this.initialDataGridViewColumnsCount = -1;
            this.dataGridViewColumns = dataGridViewColumns;
            this.liveDataGridView = liveDataGridView;
            Font defaultFont = Control.DefaultFont;
            IUIService service = (IUIService)this.liveDataGridView.DataGridControl.Site.GetService(iUIServiceType);
            if (service != null)
            {
                defaultFont = (Font)service.Styles["DialogFont"];
            }
            this.Font = defaultFont;
            this.InitializeComponent();
            this.EnableDataBoundSection();
        }

        private void AddColumn()
        {
            DataGridViewColumn dataGridViewColumn = Activator.CreateInstance(((ComboBoxItem)this.columnTypesCombo.SelectedItem).ColumnType) as DataGridViewColumn;
            bool flag = (this.dataGridViewColumns.Count > this.insertAtPosition) && this.dataGridViewColumns[this.insertAtPosition].Frozen;
            dataGridViewColumn.Frozen = flag;
            if (!this.persistChangesToDesigner)
            {
                dataGridViewColumn.HeaderText = this.headerTextBox.Text;
                dataGridViewColumn.Name = this.nameTextBox.Text;
                dataGridViewColumn.DisplayIndex = -1;
                this.dataGridViewColumns.Insert(this.insertAtPosition, dataGridViewColumn);
                this.insertAtPosition++;
            }
            dataGridViewColumn.HeaderText = this.headerTextBox.Text;
            dataGridViewColumn.Name = this.nameTextBox.Text;
            dataGridViewColumn.Visible = this.visibleCheckBox.Checked;
            dataGridViewColumn.Frozen = this.frozenCheckBox.Checked || flag;
            dataGridViewColumn.ReadOnly = this.readOnlyCheckBox.Checked;
            if (this.dataBoundColumnRadioButton.Checked && (this.dataColumns.SelectedIndex > -1))
            {
                dataGridViewColumn.DataPropertyName = ((ListBoxItem)this.dataColumns.SelectedItem).PropertyName;
            }
            if (this.persistChangesToDesigner)
            {
                try
                {
                    dataGridViewColumn.DisplayIndex = -1;
                    this.dataGridViewColumns.Insert(this.insertAtPosition, dataGridViewColumn);
                    this.insertAtPosition++;
                    this.liveDataGridView.DataGridControl.Site.Container.Add(dataGridViewColumn, dataGridViewColumn.Name);
                }
                catch (InvalidOperationException exception)
                {
                    IUIService uiService = (IUIService)this.liveDataGridView.DataGridControl.Site.GetService(typeof(IUIService));
                    ExtDataGridDesigner.ShowErrorDialog(uiService, exception, this.liveDataGridView);
                    return;
                }
            }
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(dataGridViewColumn)["UserAddedColumn"];
            if (descriptor != null)
            {
                descriptor.SetValue(dataGridViewColumn, true);
            }
            this.nameTextBox.Text = this.headerTextBox.Text = this.AssignName();
            this.nameTextBox.Focus();
        }

        private string AssignName()
        {
            int num = 1;
            string name = "Column" + num.ToString(CultureInfo.InvariantCulture);
            IDesignerHost service = null;
            IContainer container = null;
            service = this.liveDataGridView.DataGridControl.Site.GetService(iDesignerHostType) as IDesignerHost;
            if (service != null)
            {
                container = service.Container;
            }
            while (!ValidName(name, this.dataGridViewColumns, container, null, this.liveDataGridView.Columns, !this.persistChangesToDesigner))
            {
                num++;
                name = "Column" + num.ToString(CultureInfo.InvariantCulture);
            }
            return name;
        }

        private void EnableDataBoundSection()
        {
            if (this.dataColumns.Items.Count > 0)
            {
                this.dataBoundColumnRadioButton.Enabled = true;
                this.dataBoundColumnRadioButton.Checked = true;
                this.dataBoundColumnRadioButton.Focus();
                this.headerTextBox.Text = this.nameTextBox.Text = this.AssignName();
            }
            else
            {
                this.dataBoundColumnRadioButton.Enabled = false;
                this.unboundColumnRadioButton.Checked = true;
                this.nameTextBox.Focus();
                this.headerTextBox.Text = this.nameTextBox.Text = this.AssignName();
            }
        }

        public static bool ValidName(string name, DataGridViewColumnCollection columns, IContainer container, INameCreationService nameCreationService, DataGridViewColumnCollection liveColumns, bool allowDuplicateNameInLiveColumnCollection)
        {
            if (columns.Contains(name))
            {
                return false;
            }
            if (((container != null) && (container.Components[name] != null)) && ((!allowDuplicateNameInLiveColumnCollection || (liveColumns == null)) || !liveColumns.Contains(name)))
            {
                return false;
            }
            return (((nameCreationService == null) || nameCreationService.IsValidName(name)) || ((allowDuplicateNameInLiveColumnCollection && (liveColumns != null)) && liveColumns.Contains(name)));
        }

        public static bool ValidName(string name, DataGridViewColumnCollection columns, IContainer container, INameCreationService nameCreationService, DataGridViewColumnCollection liveColumns, bool allowDuplicateNameInLiveColumnCollection, out string errorString)
        {
            if (columns.Contains(name))
            {
                errorString = "DataGridViewDuplicateColumnName " + name;// SR.GetString("DataGridViewDuplicateColumnName", new object[] { name });
                return false;
            }
            if (((container != null) && (container.Components[name] != null)) && ((!allowDuplicateNameInLiveColumnCollection || (liveColumns == null)) || !liveColumns.Contains(name)))
            {
                errorString = "DesignerHostDuplicateName " + name;//SR.GetString("DesignerHostDuplicateName", new object[] { name });
                return false;
            }
            if (((nameCreationService != null) && !nameCreationService.IsValidName(name)) && ((!allowDuplicateNameInLiveColumnCollection || (liveColumns == null)) || !liveColumns.Contains(name)))
            {
                errorString = "CodeDomDesignerLoaderInvalidIdentifier " + name; // SR.GetString("CodeDomDesignerLoaderInvalidIdentifier", new object[] { name });
                return false;
            }
            errorString = string.Empty;
            return true;
        }

        public static ComponentDesigner GetComponentDesignerForType(ITypeResolutionService tr, Type type)
        {
            ComponentDesigner designer = null;
            DesignerAttribute attribute = null;
            AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
            for (int i = 0; i < attributes.Count; i++)
            {
                DesignerAttribute attribute2 = attributes[i] as DesignerAttribute;
                if (attribute2 != null)
                {
                    Type type2 = Type.GetType(attribute2.DesignerBaseTypeName);
                    if ((type2 != null) && (type2 == iDesignerType))
                    {
                        attribute = attribute2;
                        break;
                    }
                }
            }
            if (attribute != null)
            {
                Type c = null;
                if (tr != null)
                {
                    c = tr.GetType(attribute.DesignerTypeName);
                }
                else
                {
                    c = Type.GetType(attribute.DesignerTypeName);
                }
                if ((c != null) && typeof(ComponentDesigner).IsAssignableFrom(c))
                {
                    designer = (ComponentDesigner)Activator.CreateInstance(c);
                }
            }
            return designer;
        }

        private void PopulateColumnTypesCombo()
        {
            this.columnTypesCombo.Items.Clear();
            IDesignerHost host = (IDesignerHost)this.liveDataGridView.DataGridControl.Site.GetService(iDesignerHostType);
            if (host != null)
            {
                ITypeDiscoveryService service = (ITypeDiscoveryService)host.GetService(iTypeDiscoveryServiceType);
                if (service != null)
                {
                    foreach (Type type in DesignerUtils.FilterGenericTypes(service.GetTypes(dataGridViewColumnType, false)))
                    {
                        if (((type != dataGridViewColumnType) && !type.IsAbstract) && (type.IsPublic || type.IsNestedPublic))
                        {
                            DataGridViewColumnDesignTimeVisibleAttribute attribute = TypeDescriptor.GetAttributes(type)[dataGridViewColumnDesignTimeVisibleAttributeType] as DataGridViewColumnDesignTimeVisibleAttribute;
                            if ((attribute == null) || attribute.Visible)
                            {
                                this.columnTypesCombo.Items.Add(new ComboBoxItem(type));
                            }
                        }
                    }
                    this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(DataGridViewTextBoxColumn));
                }
            }
        }

        private void PopulateDataColumns()
        {
            int selectedIndex = this.dataColumns.SelectedIndex;
            this.dataColumns.SelectedIndex = -1;
            this.dataColumns.Items.Clear();
            if (this.liveDataGridView.DataSource != null)
            {
                CurrencyManager manager = null;
                try
                {
                    manager = this.BindingContext[this.liveDataGridView.DataSource, this.liveDataGridView.DataMember] as CurrencyManager;
                }
                catch (ArgumentException)
                {
                    manager = null;
                }
                PropertyDescriptorCollection descriptors = (manager != null) ? manager.GetItemProperties() : null;
                if (descriptors != null)
                {
                    for (int i = 0; i < descriptors.Count; i++)
                    {
                        if (!typeof(IList).IsAssignableFrom(descriptors[i].PropertyType) || TypeDescriptor.GetConverter(typeof(Image)).CanConvertFrom(descriptors[i].PropertyType))
                        {
                            this.dataColumns.Items.Add(new ListBoxItem(descriptors[i].PropertyType, descriptors[i].Name));
                        }
                    }
                }
            }
            if ((selectedIndex != -1) && (selectedIndex < this.dataColumns.Items.Count))
            {
                this.dataColumns.SelectedIndex = selectedIndex;
            }
            else
            {
                this.dataColumns.SelectedIndex = (this.dataColumns.Items.Count > 0) ? 0 : -1;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((keyData & ~Keys.KeyCode) == Keys.None)
            {
                Keys keys = keyData & Keys.KeyCode;
                if (keys == Keys.Return)
                {
                    IDesignerHost service = null;
                    INameCreationService nameCreationService = null;
                    IContainer container = null;
                    service = this.liveDataGridView.Site.GetService(iDesignerHostType) as IDesignerHost;
                    if (service != null)
                    {
                        container = service.Container;
                    }
                    nameCreationService = this.liveDataGridView.DataGridControl.Site.GetService(iNameCreationServiceType) as INameCreationService;
                    string errorString = string.Empty;
                    if (ValidName(this.nameTextBox.Text, this.dataGridViewColumns, container, nameCreationService, this.liveDataGridView.Columns, !this.persistChangesToDesigner, out errorString))
                    {
                        this.AddColumn();
                        base.Close();
                    }
                    else
                    {
                        IUIService uiService = (IUIService)this.liveDataGridView.DataGridControl.Site.GetService(iUIServiceType);
                        ExtDataGridDesigner.ShowErrorDialog(uiService, errorString, this.liveDataGridView);
                    }
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void SetDefaultDataGridViewColumnType(Type type)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Image));
            if ((type == typeof(bool)) || (type == typeof(CheckState)))
            {
                this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(DataGridViewCheckBoxColumn));
            }
            else if (typeof(Image).IsAssignableFrom(type) || converter.CanConvertFrom(type))
            {
                this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(DataGridViewImageColumn));
            }
            else
            {
                this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(DataGridViewTextBoxColumn));
            }
        }

        private int TypeToSelectedIndex(Type type)
        {
            for (int i = 0; i < this.columnTypesCombo.Items.Count; i++)
            {
                if (type == ((ComboBoxItem)this.columnTypesCombo.Items[i]).ColumnType)
                {
                    return i;
                }
            }
            return -1;
        }

        internal void Start(int insertAtPosition, bool persistChangesToDesigner)
        {
            this.insertAtPosition = insertAtPosition;
            this.persistChangesToDesigner = persistChangesToDesigner;
            if (this.persistChangesToDesigner)
            {
                this.initialDataGridViewColumnsCount = this.liveDataGridView.Columns.Count;
            }
            else
            {
                this.initialDataGridViewColumnsCount = -1;
            }
        }

        #endregion

        #region Nested Types

        private class ComboBoxItem
        {
            // Fields
            private Type columnType;

            // Methods
            public ComboBoxItem(Type columnType)
            {
                this.columnType = columnType;
            }

            public override string ToString()
            {
                return this.columnType.Name;
            }

            // Properties
            public Type ColumnType
            {
                get
                {
                    return this.columnType;
                }
            }
        }

        private class ListBoxItem
        {
            // Fields
            private string propertyName;
            private Type propertyType;

            // Methods
            public ListBoxItem(Type propertyType, string propertyName)
            {
                this.propertyType = propertyType;
                this.propertyName = propertyName;
            }

            public override string ToString()
            {
                return this.propertyName;
            }

            // Properties
            public string PropertyName
            {
                get
                {
                    return this.propertyName;
                }
            }

            public Type PropertyType
            {
                get
                {
                    return this.propertyType;
                }
            }
        }

        #endregion

        private void addButton_Click(object sender, EventArgs e)
        {
            this.cancelButton.Text = "Zamknij"; // SR.GetString("DataGridView_Close");
            this.AddColumn();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void ExtDataGridAddColumnDialog_Load(object sender, EventArgs e)
        {
            if (this.dataBoundColumnRadioButton.Checked)
            {
                this.headerTextBox.Text = this.nameTextBox.Text = this.AssignName();
            }
            else
            {
                string str = this.AssignName();
                this.headerTextBox.Text = this.nameTextBox.Text = str;
            }
            this.PopulateColumnTypesCombo();
            this.PopulateDataColumns();
            this.EnableDataBoundSection();
            this.cancelButton.Text = "Anuluj"; // SR.GetString("DataGridView_Cancel");
        }

        private void ExtDataGridAddColumnDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.persistChangesToDesigner)
            {
                try
                {
                    IComponentChangeService service = (IComponentChangeService)this.liveDataGridView.Site.GetService(iComponentChangeServiceType);
                    if (service == null)
                    {
                        return;
                    }
                    DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[this.liveDataGridView.Columns.Count - this.initialDataGridViewColumnsCount];
                    for (int i = this.initialDataGridViewColumnsCount; i < this.liveDataGridView.Columns.Count; i++)
                    {
                        dataGridViewColumns[i - this.initialDataGridViewColumnsCount] = this.liveDataGridView.Columns[i];
                    }
                    int initialDataGridViewColumnsCount = this.initialDataGridViewColumnsCount;
                    while (initialDataGridViewColumnsCount < this.liveDataGridView.Columns.Count)
                    {
                        this.liveDataGridView.Columns.RemoveAt(this.initialDataGridViewColumnsCount);
                    }
                    PropertyDescriptor member = TypeDescriptor.GetProperties(this.liveDataGridView)["Columns"];
                    service.OnComponentChanging(this.liveDataGridView, member);
                    for (int j = 0; j < dataGridViewColumns.Length; j++)
                    {
                        dataGridViewColumns[j].DisplayIndex = -1;
                    }
                    this.liveDataGridView.Columns.AddRange(dataGridViewColumns);
                    service.OnComponentChanged(this.liveDataGridView, member, null, null);
                }
                catch (InvalidOperationException)
                {
                }
            }
            base.DialogResult = DialogResult.OK;
        }

        private void ExtDataGridAddColumnDialog_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible && base.IsHandleCreated)
            {
                if (this.dataBoundColumnRadioButton.Checked)
                {
                    this.dataColumns.Select();
                }
                else
                {
                    this.nameTextBox.Select();
                }
            }
        }

        private void dataBoundColumnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //this.columnInDataSourceLabel.Enabled = this.dataBoundColumnRadioButton.Checked;
            this.dataColumns.Enabled = this.dataBoundColumnRadioButton.Checked;
            this.dataColumns_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void dataColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dataColumns.SelectedItem != null)
            {
                this.headerTextBox.Text = this.nameTextBox.Text = ((ListBoxItem)this.dataColumns.SelectedItem).PropertyName;
                this.SetDefaultDataGridViewColumnType(((ListBoxItem)this.dataColumns.SelectedItem).PropertyType);
            }
        }

        private void unboundColumnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.unboundColumnRadioButton.Checked)
            {
                this.nameTextBox.Text = this.headerTextBox.Text = this.AssignName();
                this.nameTextBox.Focus();
            }
        }

        private void nameTextBox_Validating(object sender, CancelEventArgs e)
        {
            IDesignerHost service = null;
            INameCreationService nameCreationService = null;
            IContainer container = null;
            service = this.liveDataGridView.Site.GetService(iDesignerHostType) as IDesignerHost;
            if (service != null)
            {
                container = service.Container;
            }
            nameCreationService = this.liveDataGridView.Site.GetService(iNameCreationServiceType) as INameCreationService;
            string errorString = string.Empty;
            if (!ValidName(this.nameTextBox.Text, this.dataGridViewColumns, container, nameCreationService, this.liveDataGridView.Columns, !this.persistChangesToDesigner, out errorString))
            {
                IUIService uiService = (IUIService)this.liveDataGridView.Site.GetService(iUIServiceType);
                ExtDataGridDesigner.ShowErrorDialog(uiService, errorString, this.liveDataGridView);
                e.Cancel = true;
            }
        }

    }
}
