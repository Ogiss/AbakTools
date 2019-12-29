using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Enova.Business.Old.DB;
using Enova.Business.Old.Core;

namespace Enova.Business.Old.Types
{
    public class Podmiot : Enova.Business.Old.Core.IPodmiot, Enova.Business.Old.Core.IRozrachunkiQuery
    {
        public object Row { get; set; }
        public int ID
        {
            get
            {
                if (Row != null && Row is IPodmiot)
                {
                    return ((IPodmiot)Row).ID;
                }
                return 0;
            }
        }
        
        public string Kod
        {
            get
            {
                if (Row != null && Row is IPodmiot)
                {
                    return ((IPodmiot)Row).Kod;
                }
                return null;
            }
        }

        public string Nazwa
        {
            get
            {
                if (Row != null && Row is IPodmiot)
                {
                    return ((IPodmiot)Row).Nazwa;
                }
                return null;
            }
        }

        public ObjectQuery<RozrachunekIdx> RozrachunkiQuery
        {
            get
            {
                if (Row != null && Row is IRozrachunkiQuery)
                {
                    return ((IRozrachunkiQuery)Row).RozrachunkiQuery;
                }
                return null;
            }
        }
    }
}
