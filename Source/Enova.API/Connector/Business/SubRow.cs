using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class SubRow : Enova.API.Types.ObjectBase, Enova.API.Business.SubRow
    {
        public API.Business.Row Parent
        {
            get { return FromEnova<API.Business.Row>("Parent"); }
        }

        public API.Business.Row Root
        {
            get { return FromEnova<API.Business.Row>("Root"); }
        }
    }
}
