using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface Table : IEnumerable, ISessionable
    {
        string TableName { get; }
        string Filter { get; set; }
        IFeatureDefinitions FeatureDefinitions { get; }
        void AddRow(Row row);
        Row this[int id] { get; }
        View CreateView();
    }
}
