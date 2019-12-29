using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Enova.Business.Old.Controls.Design
{
    public partial class ExtDataGridColumnCollectionDialog : Form
    {
        #region Constans

        private const int LISTBOXITEMHEIGHT = 0x11;
        private const int OWNERDRAWHORIZONTALBUFFER = 3;
        private const int OWNERDRAWITEMIMAGEBUFFER = 2;
        private const int OWNERDRAWVERTICALBUFFER = 4;


        #endregion

        #region Fields

        private static ColorMap[] colorMap = new ColorMap[] { new ColorMap() };
        private static Type iComponentChangeServiceType = typeof(IComponentChangeService);
        private static Type iHelpServiceType = typeof(IHelpService);
        private static Type iTypeDiscoveryServiceType = typeof(ITypeDiscoveryService);
        private static Type iTypeResolutionServiceType = typeof(ITypeResolutionService);
        private static Type iUIServiceType = typeof(IUIService);
        private static Type toolboxBitmapAttributeType = typeof(ToolboxBitmapAttribute);

        //private static Bitmap selectedColumnsItemBitmap;


        private ExtDataGridAddColumnDialog addColumnDialog;
        private bool columnCollectionChanging;
        private Hashtable columnsNames;
        private DataGridViewColumnCollection columnsPrivateCopy;
        private IComponentChangeService compChangeService;
        private DataGridView dataGridViewPrivateCopy;
        private bool formIsDirty;
        private ExtDataGrid liveDataGridView;
        private Hashtable userAddedColumns;


        #endregion

        #region Methods

        public ExtDataGridColumnCollectionDialog()
        {
            InitializeComponent();
            this.dataGridViewPrivateCopy = new DataGridView();
            this.columnsPrivateCopy = this.dataGridViewPrivateCopy.Columns;
            this.columnsPrivateCopy.CollectionChanged += new CollectionChangeEventHandler(this.columnsPrivateCopy_CollectionChanged);
        }

        private void PopulateSelectedColumns()
        {
            int selectedIndex = this.selectedColumns.SelectedIndex;
            for (int i = 0; i < this.selectedColumns.Items.Count; i++)
            {
                ListBoxItem item = this.selectedColumns.Items[i] as ListBoxItem;
                if (item.DataGridViewColumnDesigner != null)
                {
                    TypeDescriptor.RemoveAssociation(item.DataGridViewColumn, item.DataGridViewColumnDesigner);
                }
            }
            this.selectedColumns.Items.Clear();
            ITypeResolutionService tr = (ITypeResolutionService)this.liveDataGridView.Site.GetService(iTypeResolutionServiceType);
            for (int j = 0; j < this.columnsPrivateCopy.Count; j++)
            {
                ComponentDesigner componentDesignerForType = ExtDataGridAddColumnDialog.GetComponentDesignerForType(tr, this.columnsPrivateCopy[j].GetType());
                //this.selectedColumns.Items.Add(new ListBoxItem(this.columnsPrivateCopy[j], this, componentDesignerForType));
            }
            this.selectedColumns.SelectedIndex = Math.Min(selectedIndex, this.selectedColumns.Items.Count - 1);
            //this.SetSelectedColumnsHorizontalExtent();
            if (this.selectedColumns.Items.Count == 0)
            {
                //this.propertyGridLabel.Text = SR.GetString("DataGridViewProperties");
            }
        }

        #endregion

        #region Event handlers

        private void columnsPrivateCopy_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            if (!this.columnCollectionChanging)
            {
                this.PopulateSelectedColumns();
                if (e.Action == CollectionChangeAction.Add)
                {
                    this.selectedColumns.SelectedIndex = this.columnsPrivateCopy.IndexOf((DataGridViewColumn)e.Element);
                    ListBoxItem selectedItem = this.selectedColumns.SelectedItem as ListBoxItem;
                    this.userAddedColumns[selectedItem.DataGridViewColumn] = true;
                    this.columnsNames[selectedItem.DataGridViewColumn] = selectedItem.DataGridViewColumn.Name;
                }
                this.formIsDirty = true;
            }
        }

        #endregion

        #region Nested Types

        private class ColumnTypePropertyDescriptor : PropertyDescriptor
        {
            // Methods
            public ColumnTypePropertyDescriptor()
                : base("ColumnType", null)
            {
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override object GetValue(object component)
            {
                if (component == null)
                {
                    return null;
                }
                ExtDataGridColumnCollectionDialog.ListBoxItem item = (ExtDataGridColumnCollectionDialog.ListBoxItem)component;
                return item.DataGridViewColumn.GetType().Name;
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
                ExtDataGridColumnCollectionDialog.ListBoxItem item = (ExtDataGridColumnCollectionDialog.ListBoxItem)component;
                Type newType = value as Type;
                if (item.DataGridViewColumn.GetType() != newType)
                {
                    //item.Owner.ColumnTypeChanged(item, newType);
                    this.OnValueChanged(component, EventArgs.Empty);
                }
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }

            // Properties
            public override AttributeCollection Attributes
            {
                get
                {
                    EditorAttribute attribute = 
                        new EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnTypeEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor));
                    DescriptionAttribute attribute2 = new DescriptionAttribute("DataGridViewColumnTypePropertyDescription"/*SR.GetString("DataGridViewColumnTypePropertyDescription")*/);
                    CategoryAttribute design = CategoryAttribute.Design;
                    return new AttributeCollection(new Attribute[] { attribute, attribute2, design });
                }
            }

            public override Type ComponentType
            {
                get
                {
                    return typeof(ExtDataGridColumnCollectionDialog.ListBoxItem);
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public override Type PropertyType
            {
                get
                {
                    return typeof(Type);
                }
            }
        }


        internal class ListBoxItem : ICustomTypeDescriptor, IComponent, IDisposable
        {
            // Fields
            private DataGridViewColumn column;
            private ComponentDesigner compDesigner;
            private ExtDataGridColumnCollectionDialog owner;
            private Image toolboxBitmap;

            // Events
            event EventHandler IComponent.Disposed
            {
                add
                {
                }
                remove
                {
                }
            }

            // Methods
            public ListBoxItem(DataGridViewColumn column, ExtDataGridColumnCollectionDialog owner, ComponentDesigner compDesigner)
            {
                this.column = column;
                this.owner = owner;
                this.compDesigner = compDesigner;
                if (this.compDesigner != null)
                {
                    this.compDesigner.Initialize(column);
                    TypeDescriptor.CreateAssociation(this.column, this.compDesigner);
                }
                ToolboxBitmapAttribute attribute = TypeDescriptor.GetAttributes(column)[ExtDataGridColumnCollectionDialog.toolboxBitmapAttributeType] as ToolboxBitmapAttribute;
                if (attribute != null)
                {
                    this.toolboxBitmap = attribute.GetImage(column, false);
                }
                else
                {
                    //this.toolboxBitmap = this.owner.SelectedColumnsItemBitmap;
                }
                DataGridViewColumnDesigner designer = compDesigner as DataGridViewColumnDesigner;
                if (designer != null)
                {
                    //designer.LiveDataGridView = this.owner.liveDataGridView;
                }
            }

            AttributeCollection ICustomTypeDescriptor.GetAttributes()
            {
                return TypeDescriptor.GetAttributes(this.column);
            }

            string ICustomTypeDescriptor.GetClassName()
            {
                return TypeDescriptor.GetClassName(this.column);
            }

            string ICustomTypeDescriptor.GetComponentName()
            {
                return TypeDescriptor.GetComponentName(this.column);
            }

            TypeConverter ICustomTypeDescriptor.GetConverter()
            {
                return TypeDescriptor.GetConverter(this.column);
            }

            EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent(this.column);
            }

            PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty(this.column);
            }

            object ICustomTypeDescriptor.GetEditor(Type type)
            {
                return TypeDescriptor.GetEditor(this.column, type);
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
            {
                return TypeDescriptor.GetEvents(this.column);
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
            {
                return TypeDescriptor.GetEvents(this.column, attrs);
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
            {
                return ((ICustomTypeDescriptor)this).GetProperties(null);
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attrs)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.column);
                PropertyDescriptor[] array = null;
                if (this.compDesigner != null)
                {
                    Hashtable hashtable = new Hashtable();
                    for (int i = 0; i < properties.Count; i++)
                    {
                        hashtable.Add(properties[i].Name, properties[i]);
                    }
                    ((IDesignerFilter)this.compDesigner).PreFilterProperties(hashtable);
                    array = new PropertyDescriptor[hashtable.Count + 1];
                    hashtable.Values.CopyTo(array, 0);
                }
                else
                {
                    array = new PropertyDescriptor[properties.Count + 1];
                    properties.CopyTo(array, 0);
                }
                array[array.Length - 1] = new ExtDataGridColumnCollectionDialog.ColumnTypePropertyDescriptor();
                return new PropertyDescriptorCollection(array);
            }

            object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
            {
                if ((pd != null) && (pd is ExtDataGridColumnCollectionDialog.ColumnTypePropertyDescriptor))
                {
                    return this;
                }
                return this.column;
            }

            void IDisposable.Dispose()
            {
            }

            public override string ToString()
            {
                return this.column.HeaderText;
            }

            // Properties
            public DataGridViewColumn DataGridViewColumn
            {
                get
                {
                    return this.column;
                }
            }

            public ComponentDesigner DataGridViewColumnDesigner
            {
                get
                {
                    return this.compDesigner;
                }
            }

            public ExtDataGridColumnCollectionDialog Owner
            {
                get
                {
                    return this.owner;
                }
            }

            ISite IComponent.Site
            {
                get
                {
                    return this.owner.liveDataGridView.Site;
                }
                set
                {
                }
            }

            public Image ToolboxBitmap
            {
                get
                {
                    return this.toolboxBitmap;
                }
            }
        }

        #endregion

    }
}
