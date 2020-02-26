using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;
using Enova.API.Connector;

[assembly: TypeMap("Soneta.Types.Date, Soneta.Types", null, typeof(Enova.API.Types.Date))]

namespace Enova.API.Types
{
    public class Date : API.Types.ObjectBase, IComparable, IComparable<Date>
    {
        #region Properties

        #endregion

        #region Methods

        public static implicit operator DateTime(Date date)
        {
            return EnovaService.FromEnova<DateTime>(date);
        }

        public static implicit operator Date(DateTime dateTime)
        {
            return EnovaService.Instance.CreateObject<Date>(null, new object[] { dateTime });
        }

        public override string ToString()
        {
            return (string)CallMethod("ToString");
        }

        public int CompareTo(Date other)
        {
            return ((DateTime)this).CompareTo(other);
        }

        public int CompareTo(object obj)
        {
            return this.CompareTo((Date)obj);
        }

        #endregion

    }
}
