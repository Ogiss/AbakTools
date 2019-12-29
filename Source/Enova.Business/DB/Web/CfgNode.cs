using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using BusinessApp.Business;
using AbakTools.Business;

namespace Enova.Business.Old.DB.Web
{
    /*
    public partial class CfgNode : ISessionable
    {

        #region Fields
        
        public static Guid RootGuid = new Guid("00000000-0002-0001-0001-000000000000");
        internal static CfgNode root = null;

        #endregion

        #region Properties

        public Session Session
        {
            get { return this.GetSession(); }
        }

        public BusinessModule BusinessModule
        {
            get { return (BusinessModule)this.Session.Modules["Business"]; }
        }

        public static CfgNode Root
        {
            get
            {
                return root;
            }
        }


        #endregion

        #region Methods

        public CfgNode AddSubNode(Guid guid, string name)
        {
            CfgNode node = new CfgNode()
            {
                Guid = guid,
                Name = name,
                Parent = this
            };

            BusinessModule.CfgNodes.AddRow(node);

            return node;
        }

        public CfgAttribute AddAttribute(string name, object value)
        {
            CfgAttribute attribute = new CfgAttribute()
            {
                Guid = Guid.NewGuid(),
                Node = this,
                Name = name,
                Value = value.ToString()
            };

            BusinessModule.CfgAttributes.AddRow(attribute);
            return attribute;
        }

        public CfgNode FindSubNode(string name)
        {
            return this.SubNodes.Where(sn => sn.Name == name).FirstOrDefault();
        }

        public CfgAttribute FindSingleAttribute(string name)
        {
            return this.Attributes.Where(a => a.Name == name).FirstOrDefault();
        }

        public IEnumerable<CfgAttribute> FindMultiAttribute(string name)
        {
            return this.Attributes.Where(a => a.Name == name).AsEnumerable();
        }

        public string GetValue(string attributeName)
        {
            return this.Attributes.Where(a => a.Name == attributeName).Select(a => a.Value).FirstOrDefault();
        }

        public string[] GetValues(string attributeName)
        {
            return this.Attributes.Where(a => a.Name == attributeName).Select(a => a.Value).ToArray();
        }

        #endregion
    }
     */
}
