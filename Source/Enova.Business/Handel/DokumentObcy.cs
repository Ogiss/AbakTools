using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;

namespace Enova.Old.Handel
{
    public class DokumentObcy : SubRow
    {
        #region Properties
        //[AttributeInheritance, Caption("Numer obcy")]
        public string Numer
        {
            get
            {
                return base.GetFieldValue<string>("Numer");
            }
            set
            {
                throw new NotFiniteNumberException("Enova.Handel.DokumentObcy.Numer.set");
                /*
                string numer = this.Numer;
                base.Numer = Transform.Numer(this, value);
                if (numer != this.Numer)
                {
                    if (this.Numer != "")
                    {
                        base.Session.Verifiers.Add(new NumerVerifier(this));
                        base.Session.Verifiers.Add(new NumerWypełnionyVerifier(this));
                        base.Session.Verifiers.Add(new NumerLengthVerifier(this));
                    }
                    if (base.Parent is INumerDokumentuHost)
                    {
                        ((INumerDokumentuHost)base.Parent).Update();
                    }
                    foreach (RelacjaHandlowa handlowa in this.Dokument.PodrzedneRelacje)
                    {
                        if ((handlowa.Typ == TypRelacjiHandlowej.HandlowoMagazynowa) && !handlowa.Podrzedny.IsReadOnly())
                        {
                            handlowa.Podrzedny.Obcy.Numer = value;
                        }
                    }
                }
                 */
            }
        }



        #endregion
    }
}
