using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public interface IRow : ISessionable
    {
        int ID { get; }
        Row Root { get; }
        ITable Table { get; }
        DBContextBase DBContext { get; }
        RowState State { get; }
    }
}
