using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Controls
{
    public class DataGridViewSelectColumn<TDataObject> : DataGridViewColumn, ISelectColumn
    {

        #region Properties

        public virtual ObjectQuery<TDataObject> SearchQuery
        {
            get { return null; }
        }

        public virtual Type SelectFormType {
            get { return null; }
        }

        public virtual string DisplayMember
        {
            get { return null; }
        }

        public virtual string ValueMember
        {
            get { return null; }
        }

        public virtual string SearchMember
        {
            get { return null; }
        }

        #endregion

        public DataGridViewSelectColumn()
        {
            this.CellTemplate = new DataGridViewSelectCell<TDataObject>();
        }

        #region ISelectColumn Implementation

        bool ISelectColumn.FormatedValueIsValid(object formatedValueIsValid)
        {
            if (formatedValueIsValid is string)
            {
                return !string.IsNullOrEmpty((string)formatedValueIsValid);
            }
            else
            {
                return formatedValueIsValid != null;
            }
        }

        #endregion
    }
}
