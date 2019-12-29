using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Business
{
    public class DictionaryView : GridViewWithEnovaApi<API.Business.DictionaryItem>
    {
        #region Fields

        private string category;

        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return "DictionaryEnovaView" + (SelectionMode ? "Select" : "");
            }
        }

        #endregion

        #region Methods

        public DictionaryView(string category)
        {
            this.category = category;
        }

        protected override API.Business.Table<API.Business.DictionaryItem> CreateTable(API.Business.Session session)
        {
            var table = session.GetModule<API.Business.BusinessModule>().Dictionary;
            table.Filter = @"Category = '" + category + "'";
            return table;
        }

        #endregion
    }
}
