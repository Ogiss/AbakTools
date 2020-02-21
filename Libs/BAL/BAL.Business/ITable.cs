using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BAL.Business
{
    public interface ITable : IEnumerable, ISessionable
    {
        Type GetRowType();
        IRowCollection Rows { get; }
        Module Module { get; }
        string TableName { get; }
        IRow GetByID(int id);
        View CreateView();
        IQueryable Query { get; }
        int Count { get; }
        void AddRow(IRow row);
        ITable RootTable { get; }
        //IRow CreateRow();

        //IRow CreateRow();
        //void AcceptChanges();
        //void RemoveRow(IRow row);
    }
}
