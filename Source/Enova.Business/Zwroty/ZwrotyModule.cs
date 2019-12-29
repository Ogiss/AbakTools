using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.DB.Web;

namespace Enova.Business.Old.Zwroty
{
    public class ZwrotyModule : Module
    {
        #region Fields

        private Zwroty zwrotyTable;

        #endregion

        #region Properties

        public Zwroty Zwroty
        {
            get { return this.zwrotyTable; }
        }

        #endregion

        #region Methods

        public ZwrotyModule(WebContext dc)
            : base(dc, "Zwroty")
        {
            this.zwrotyTable = new Zwroty(dc);
        }

        public static ZwrotyModule GetInstance(WebContext dc)
        {
            var module = Module.GetModule("Zwroty");
            if (module == null)
            {
                module = new ZwrotyModule(dc);
                Module.AddModule(module);
            }
            return (ZwrotyModule)module;
        }

        #endregion
    }
}
