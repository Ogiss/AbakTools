using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Magazyny
{
    internal class StanMagazynuWorker : API.Types.ObjectBase, API.Magazyny.StanMagazynuWorker
    {
        public API.Towary.Towar Towar
        {
            get
            {
                return EnovaHelper.FromEnova<API.Towary.Towar>(GetValue("Towar"));
            }
            set
            {
                SetValue("Towar", value.EnovaObject);
            }
        }

        public API.Towary.Quantity Stan
        {
            get { return EnovaHelper.FromEnova<API.Towary.Quantity>(GetValue("Stan")); }
        }

        public API.Towary.Quantity StanMagazynu
        {
            get { return EnovaHelper.FromEnova<API.Towary.Quantity>(GetValue("StanMagazynu")); }
        }

        public API.Magazyny.Magazyn Magazyn
        {
            get
            {
                return EnovaHelper.FromEnova<API.Magazyny.Magazyn>(GetValue("Magazyn"));
            }
            set
            {
                SetValue("Magazyn", value.EnovaObject);
            }
        }

        public API.Magazyny.Magazyn[] Magazyny
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


    }
}
