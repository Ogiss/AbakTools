using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public class ZwrotAnalizaDokHandlowyOld : IEquatable<ZwrotAnalizaDokHandlowyOld>
    {
        #region Fields

        private DokumentHandlowy dokumentHandlowy;
        private Dictionary<PozycjaZwrotuAnalizaOld, double> pozycjeKorygowane;
        private PozycjaZwrotuAnalizaCallectionOld pozycje;

        #endregion

        #region Properties

        public DokumentHandlowy DokumentHandlowy
        {
            get { return this.dokumentHandlowy; }
        }
        public PozycjaZwrotuAnalizaCallectionOld Pozycje
        {
            get
            {
                if (this.pozycje == null)
                    this.pozycje = new PozycjaZwrotuAnalizaCallectionOld();
                return this.pozycje;
            }
        }
            
        public DateTime Data
        {
            get { return this.DokumentHandlowy.Data; }
        }
        public string NumerPelny
        {
            get { return this.DokumentHandlowy.NumerPelny; }
        }
        public PozycjaZwrotuAnalizaOld this[int ident]
        {
            get
            {
                foreach (var p in this.Pozycje)
                    if (((PozycjaZwrotuAnalizaOld)p).Ident == ident)
                        return (PozycjaZwrotuAnalizaOld)p;
                return null;
            }
        }
        public int IloscPozycjiZwrotu
        {
            get
            {
                return this.Pozycje.Count;
            }
        }

        #endregion

        public ZwrotAnalizaDokHandlowyOld(DokumentHandlowy dokumentHandlowy)
        {
            this.dokumentHandlowy = dokumentHandlowy;
        }

        public bool Contains(PozycjaZwrotuAnalizaOld pozycja)
        {
            return this.Pozycje.Contains(pozycja.Ident);
        }

        public bool Equals(ZwrotAnalizaDokHandlowyOld obj)
        {
            return obj.DokumentHandlowy.Guid == this.DokumentHandlowy.Guid;
        }

        public override bool Equals(object obj)
        {
            return this.Equals((ZwrotAnalizaDokHandlowyOld)obj);
        }

        public override int GetHashCode()
        {
            return this.DokumentHandlowy.Guid.GetHashCode();
        }

        public override string ToString()
        {
            return this.DokumentHandlowy.NumerPelny;
        }
    }
}
