using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using BAL.Types;
using BAL.Business;

namespace BAL.Forms
{
    public partial class ColumnSelectForm : BAL.Forms.FormBase
    {
        public Column SelectedColumn = null;
        public static ICollection<Type> ExcludedTypes = new List<Type>();
        private Type dataType = null;

        public ColumnSelectForm(Type dataType)
        {
            InitializeComponent();
            this.dataType = dataType;
            this.initColumnsTree();
        }

        private void initColumnsTree(IEnumerable<Column> columns)
        {
            this.treeView.Nodes.Clear();
            foreach (var column in columns)
            {
                this.treeView.Nodes.Add(new ColumnTreeNode(column, this));
            }
        }

        private void initColumnsTree()
        {
            this.treeView.Nodes.Clear();

            SortedDictionary<string, PropertyDescriptor> list = new SortedDictionary<string, PropertyDescriptor>();

            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(dataType))
            {
                if (pd.Name == "Item" || pd.Attributes[typeof(HiddenAttribute)] != null || ExcludedTypes.Contains(pd.PropertyType))
                    continue;
                if (!list.ContainsKey(pd.Name))
                    list.Add(pd.Name, pd);
            }

            if (dataType.IsInterface)
            {
                foreach (var it in dataType.GetInterfaces())
                {
                    if (it.GetCustomAttributes(typeof(HiddenAttribute), true).Length > 0 || ExcludedTypes.Contains(it))
                        continue;
                    foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(it))
                    {
                        if (pd.Name == "Item" || pd.Attributes[typeof(HiddenAttribute)] != null)
                            continue;
                        if (!list.ContainsKey(pd.Name))
                            list.Add(pd.Name, pd);
                    }
                }
            }

            foreach (var pd in list.Values)
                addProperty(this, null, pd);
        }

        private static void addProperty(ColumnSelectForm form, ColumnTreeNode parent, PropertyDescriptor pinfo)
        {
            var nodes = parent != null ? parent.Nodes : form.treeView.Nodes;
            Column c = new Column();
            c.HeaderText = pinfo.Name;
            c.Visible = true;
            c.Width = 100;
            if (parent == null)
            {
                c.PropertyDescriptorPath = new PropertyDescriptorPath(pinfo);
            }
            else
            {
                c.PropertyDescriptorPath = new PropertyDescriptorPath(form.dataType, parent.Column.PropertyDescriptorPath.ToString() + "." + pinfo.Name);
            }
            ColumnTreeNode node = new ColumnTreeNode(c, form);
            if ((pinfo.PropertyType.IsClass || pinfo.PropertyType.IsInterface)
                && !pinfo.PropertyType.IsStandard())
            {
                node.Nodes.Add(new GetPropertiesTreeNode());
            }

            nodes.Add(node);
        }


        private bool isEnumerable(Type type)
        {
            var interfaces = type.GetInterfaces();
            foreach (var i in interfaces)
                if (typeof(IEnumerable).IsAssignableFrom(i))
                    return true;
            return false;
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if ( e.Node is ColumnTreeNode && e.Node.FirstNode is GetPropertiesTreeNode)
            {
                ((ColumnTreeNode)e.Node).LoadChildProperties();
            }
        }

        private void treeView_DoubleClick(object sender, EventArgs e)
        {
            this.SelectedColumn = ((ColumnTreeNode)this.treeView.SelectedNode).Column;
            this.Close();
        }
     

        #region Nested Types

        public class ColumnTreeNode : TreeNode
        {
            #region Fields

            private Column column;
            private ColumnSelectForm form;

            #endregion

            #region Properties

            public Column Column
            {
                get { return this.column; }
            }

            #endregion

            #region Methods

            public ColumnTreeNode(Column column, ColumnSelectForm form)
                : base(column.PropertyName)
            {
                this.column = column;
                this.form = form;
            }

            public void LoadChildProperties()
            {
                this.Nodes.Clear();
                var properties = TypeDescriptor.GetProperties(this.column.PropertyDescriptorPath.Last.PropertyType).Cast<PropertyDescriptor>().ToList();
                properties.AddRange(TypeDescriptor.GetProperties(this.column.PropertyDescriptorPath.Last.PropertyType,
                    this.form.dataType.GetCustomAttributes(true).Cast<Attribute>().ToArray()).Cast<PropertyDescriptor>());
                SortedDictionary<string, PropertyDescriptor> list = new SortedDictionary<string, PropertyDescriptor>();
                foreach (PropertyDescriptor pd in properties)
                {
                    if (pd.Name == "Item" || pd.Attributes[typeof(HiddenAttribute)] != null)
                        continue;
                    list[pd.Name] = pd;
                }
                foreach (var pd in list.Values)
                    ColumnSelectForm.addProperty(this.form, this, pd);
            }

            #endregion
        }

        public class GetPropertiesTreeNode : TreeNode
        {
        }

        #endregion
    }
}
