using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using BAL.Types;

namespace BAL.Business
{
    public class ViewSerializer : IDataContextSerializer
    {
        private XmlDocument doc;
        private XmlNode root;
        private View context;

        public void Serialize(System.IO.Stream stream, DataContext context)
        {
            doc = new XmlDocument();
            root = doc.CreateElement("grid");
            doc.AppendChild(root);
            this.context = (View)context;
            appendKey();
            appendSort();
            appendProperties();
            appendColumns();
            doc.Save(stream);
            stream.Close();
        }

        private void appendKey()
        {
            var el = doc.CreateElement("key");
            el.InnerText = context.Key;
            root.AppendChild(el);
        }

        private void appendSort()
        {
            if (this.context.SupportsSorting && this.context.IsSorted)
            {
                var el = doc.CreateElement("sort");
                addAttribute(el, "property", this.context.SortProperty.Name);
                addAttribute(el, "direction", this.context.SortDirection.ToString());
                root.AppendChild(el);
            }
        }

        private void appendProperties()
        {
            var properties = this.context.GetStoredProperties();
            if (properties != null)
            {
                var el = doc.CreateElement("properties");
                foreach (var kvp in properties)
                {
                    var add = doc.CreateElement("add");
                    addAttribute(add, "name", kvp.Key);
                    addAttribute(add, "value", kvp.Value.ToString());
                    addAttribute(add, "type", kvp.Value.GetType().FullName);
                    el.AppendChild(add);
                }
                root.AppendChild(el);
            }
        }

        private void appendColumns()
        {
            var columns = doc.CreateElement("columns");
            foreach (var col in this.context.VisibleColumns.GetVisible().OrderBy(c=>c.Order))
            {
                this.appendColumn(columns, col);
            }
            this.root.AppendChild(columns);
        }

        private void appendColumn(XmlElement columns, object column)
        {
            XmlElement add = this.doc.CreateElement("add");
            if (column is string)
            {
                string s = (string)column;
                addAttribute(add, "headertext", s);
                addAttribute(add, "headertextalign", "0");
                addAttribute(add, "width", "100");
                addAttribute(add, "textalign", "0");
                addAttribute(add, "propertypath", s);
                addAttribute(add, "readonly", "1");
            }
            else if (column is Column)
            {
                Column c = (Column)column;
                addAttribute(add, "headertext", c.HeaderText);
                addAttribute(add, "headertextalign", ((int)c.HeaderTextAlign).ToString());
                addAttribute(add, "width", c.Width.ToString());
                addAttribute(add, "textalign", ((int)c.TextAlign).ToString());
                addAttribute(add, "format", c.Format);
                addAttribute(add, "propertypath", c.PropertyDescriptorPath!= null ? c.PropertyDescriptorPath.ToString() : c.PropertyPath.ToString());
                addAttribute(add, "readonly", c.ReadOnly.ToString());
            }
            columns.AppendChild(add);
        }

        public void Deserialize(System.IO.Stream stream, DataContext context)
        {
            this.doc = new XmlDocument();
            this.doc.Load(stream);
            stream.Close();
            this.root = this.doc.SelectSingleNode("//grid");
            this.context = (View)context;
            CoreTools.Xml.ParseChildNodes(this.root, (node) => {
                switch (node.Name.ToLower())
                {
                    case "sort":
                        this.parseSort(node);
                        break;
                    case "properties":
                        this.parseProperties(node);
                        break;
                    case "columns":
                        int i = 0;
                        CoreTools.Xml.ParseChildNodes(node, (child) => {
                            var c = this.createColumn(child);
                            c.Order = i++;
                            c.Visible = true;
                            context.VisibleColumns.Add(c);
                        }, "add");
                        break;
                }
            });

        }

        private void parseSort(XmlNode node)
        {
            if (this.context.SupportsSorting)
            {
                string propertyName = null;
                string directionName = null;
                CoreTools.Xml.ParseNodeAttributes(node, (attrName, attrValue) =>
                {
                    switch (attrName.ToLower())
                    {
                        case "property":
                            propertyName = attrValue;
                            break;
                        case "direction":
                            directionName = attrValue;
                            break;
                    }
                });

                if (!string.IsNullOrEmpty(propertyName))
                {
                    System.ComponentModel.ListSortDirection direction = System.ComponentModel.ListSortDirection.Ascending;
                    if (!string.IsNullOrEmpty(directionName))
                        Enum.TryParse<System.ComponentModel.ListSortDirection>(directionName, out direction);

                    var pd = TypeDescriptor.GetProperties(this.context.GetDataType())[propertyName];
                    if (pd != null)
                        this.context.ApplySort(pd, direction);
                    /*
                    if (!string.IsNullOrEmpty(propertyName))
                        this.context.ApplySort(propertyName, direction);
                     */
                }
            }
        }

        private void parseProperties(XmlNode node)
        {
            var properties = new Dictionary<string, object>();

            CoreTools.Xml.ParseChildNodes(node, (child) => {
                string name = null;
                string valueStr = null;
                string typeStr = null;

                CoreTools.Xml.ParseNodeAttributes(child, (attrName, attrValue) => {
                    switch (attrName.ToLower())
                    {
                        case "name":
                            name = attrValue;
                            break;
                        case "value":
                            valueStr = attrValue;
                            break;
                        case "type":
                            typeStr = attrValue;
                            break;
                    }
                });
                if (!string.IsNullOrEmpty(name))
                {
                    object value = valueStr;
                    if (!string.IsNullOrEmpty(typeStr) && !string.IsNullOrEmpty(valueStr))
                    {
                        try
                        {
                            switch (typeStr)
                            {
                                case "System.Int32":
                                    value = int.Parse(valueStr);
                                    break;
                                case "System.Boolean":
                                    value = CoreTools.ParseBool(valueStr);
                                    break;
                            }
                        }
                        catch { }
                    }
                    properties.Add(name, value);
                }
            }, "add");
            this.context.SetStoredProperties(properties);
        }

        private Column createColumn(XmlNode node)
        {
            string header = null;
            string headerTextAlign = null;
            string headerheight = null;
            string widthStr = null;
            string textAling = null;
            string format = null;
            string path = null;
            string readOnly = null;

            CoreTools.Xml.ParseNodeAttributes(node, (attrName, attrValue) => {
                switch (attrName.ToLower())
                {
                    case "headertext":
                        header = attrValue;
                        break;
                    case "headertextalign":
                        headerTextAlign = attrValue;
                        break;
                    case "headerheight":
                        headerheight = attrValue;
                        break;
                    case "width":
                        widthStr = attrValue;
                        break;
                    case "textalign":
                        textAling = attrValue;
                        break;
                    case "format":
                        format = attrValue;
                        break;
                    case "propertypath":
                        path = attrValue;
                        break;
                    case "readonly":
                        readOnly = attrValue;
                        break;
                }
            });

            if (!string.IsNullOrEmpty(path))
            {
                Column column = new Column();
                //column.PropertyPath = new PropertyPath(this.context.GetDataType(), path);
                //column.HeaderText = string.IsNullOrEmpty(header) ? column.PropertyPath.Last.Name : header;
                column.PropertyDescriptorPath = new PropertyDescriptorPath(this.context.GetDataType(), path);
                column.HeaderText = string.IsNullOrEmpty(header) ? column.PropertyDescriptorPath.Last.Name : header;
                int i;

                if (int.TryParse(headerTextAlign, out i))
                    column.HeaderTextAlign = (TextAlign)i;

                if (int.TryParse(widthStr, out i))
                    column.Width = i;
                else
                    column.Width = 100;

                if (int.TryParse(textAling, out i))
                    column.TextAlign = (TextAlign)i;
                else
                    column.TextAlign = TextAlign.None;
                column.Format = format;

                if (!string.IsNullOrEmpty(readOnly))
                    column.ReadOnly = CoreTools.ParseBool(readOnly);

                return column;
            }
            return null;
        }

        private void addAttribute(XmlElement node, string name, string value)
        {
            XmlAttribute attribute = this.doc.CreateAttribute(name);
            attribute.Value = value;
            node.Attributes.Append(attribute);
        }
    }
}
