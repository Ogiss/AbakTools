using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
    public interface IFeatureCollection
    {
        object this[string name] { get; set; }
    }
}
