using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;
using Enova.Business.Old.DB;
using Enova.Old.Magazyny;

namespace Enova.Old.Handel
{
    public class InicjalizatorWalutyInfo : SubRow
    {
        #region Properties

        [Description("Wskazuje na źr\x00f3dło pozyskania waluty dla podsumowania/płatności dokumentu podrzędnego.")]
        public ZrodloWaluty WalutaPlatnosci
        {
            get
            {
                return this.GetFieldValue<ZrodloWaluty>("WalutaPlatnosci");
            }
            set
            {
                this.SetFieldValue("WalutaPlatnosci", (int)value);
            }
        }

        [Description("Wskazuje na źr\x00f3dło pozyskania waluty dla wartości pozycji dokumentu podrzędnego.")]
        public ZrodloWaluty WalutaPozycji
        {
            get
            {
                return this.GetFieldValue<ZrodloWaluty>("WalutaPozycji");
            }
            set
            {
                this.SetFieldValue("WalutaPozycji", (int)value);
            }
        }

        internal bool KursZNadrzednego
        {
            get
            {
                throw new NotImplementedException("InicjalizatorwalutyInfo.KursZNadrzednego");
                /*
                DefRelacjiHandlowej parent = SubRow.GetParent<DefRelacjiHandlowej>(this);
                if (this.WalutaPlatnosci == ZrodloWaluty.ZNadrzednego)
                {
                    return ((parent != null) && (parent is DefRelacjiKorekta));
                }
                return (((parent != null) && (parent is DefRelacjiMagazynowa)) && (parent.DefinicjaNadrzednego.TypPartiiMagazynowej == TypPartii.Brak));
                 */
            }
        }

        #endregion

        #region Methods

        public InicjalizatorWalutyInfo(IRow parent, string name)
            : base(parent, name)
        {
        }

        public InicjalizatorWalutyInfo() : this(null, null) { }

        #endregion

    }
}
