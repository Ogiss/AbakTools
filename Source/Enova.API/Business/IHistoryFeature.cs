using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface IHistoryFeature
    {
        DateTime[] Dates { get; }
        object this[DateTime date] { get; set; }
    }

}
