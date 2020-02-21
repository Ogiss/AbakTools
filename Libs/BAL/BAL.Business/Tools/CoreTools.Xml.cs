using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BAL.Business
{
    public static partial class CoreTools
    {
        public static partial class Xml
        {
            public delegate void XmlNodeParser(XmlNode node);
            public delegate void XmlAttributeParser(string name, string value);

            public static void ParseChildNodes(XmlNode root, XmlNodeParser parser, string nodeName = null)
            {
                if (root != null && root.HasChildNodes)
                {
                    root = root.FirstChild;
                    do
                    {
                        if (root.NodeType == XmlNodeType.Element && (nodeName == null || nodeName.ToLower() == root.Name.ToLower()))
                            parser(root.Clone());
                        root = root.NextSibling;
                    } while (root != null);
                }
            }

            public static void ParseNodeAttributes(XmlNode node, XmlAttributeParser parser, string attributeName = null)
            {
                if (node != null)
                {
                    foreach (XmlAttribute attr in node.Attributes)
                    {
                        if (attributeName == null || attributeName.ToLower() == attr.Name.ToLower())
                            parser(attr.Name, attr.Value);
                    }
                }

            }
        }
    }
}
