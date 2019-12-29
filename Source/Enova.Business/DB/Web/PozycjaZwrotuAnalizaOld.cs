using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Old.Handel;

namespace Enova.Business.Old.DB.Web
{
    public class PozycjaZwrotuAnalizaOld : IEquatable<PozycjaZwrotuAnalizaOld>
    {
        public int Ident { get; set; }
        public Towar Towar { get; set; }
        public double Ilosc { get; set; }
        public PozycjeDokHan PozycjeDokHan { get; set; }
        public IEnumerable<DokumentHandlowy> DokHandlowe { get; set; }
        public ZwrotAnalizaDokHandlowyCollectionOld DokHandloweAnaliza { get; set; }


        public string TowarKod
        {
            get { return this.Towar.Kod; }
        }
        public string TowarNazwa
        {
            get
            { 
                return this.Towar.Nazwa; 
            }
        }
        public string TowarKodNazwa
        {
            get { return Towar.Kod + " - " + Towar.Nazwa; }
        }
        public string DokumentyFV
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var dh in this.DokHandlowe)
                {
                    sb.Append(dh.NumerPelny+"; ");
                }
                return sb.ToString();
            }
        }

        public DokumentHandlowy Dokument0 { get; set; }
        public DokumentHandlowy Dokument1 { get; set; }
        public DokumentHandlowy Dokument2 { get; set; }
        public DokumentHandlowy Dokument3 { get; set; }
        public DokumentHandlowy Dokument4 { get; set; }
        public DokumentHandlowy Dokument5 { get; set; }
        public DokumentHandlowy Dokument6 { get; set; }
        public DokumentHandlowy Dokument7 { get; set; }
        public DokumentHandlowy Dokument8 { get; set; }
        public DokumentHandlowy Dokument9 { get; set; }

        public string Dokument0Str
        {
            get
            {
                return Dokument0 != null ? Dokument0.NumerPelny : string.Empty;
            }
        }
        public string Dokument1Str
        {
            get
            {
                return Dokument1 != null ? Dokument1.NumerPelny : string.Empty;
            }
        }
        public string Dokument2Str
        {
            get
            {
                return Dokument2 != null ? Dokument2.NumerPelny : string.Empty;
            }
        }
        public string Dokument3Str
        {
            get
            {
                return Dokument3 != null ? Dokument3.NumerPelny : string.Empty;
            }
        }
        public string Dokument4Str
        {
            get
            {
                return Dokument4 != null ? Dokument4.NumerPelny : string.Empty;
            }
        }
        public string Dokument5Str
        {
            get
            {
                return Dokument5 != null ? Dokument5.NumerPelny : string.Empty;
            }
        }
        public string Dokument6Str
        {
            get
            {
                return Dokument6 != null ? Dokument6.NumerPelny : string.Empty;
            }
        }
        public string Dokument7Str
        {
            get
            {
                return Dokument7 != null ? Dokument7.NumerPelny : string.Empty;
            }
        }
        public string Dokument8Str
        {
            get
            {
                return Dokument8 != null ? Dokument8.NumerPelny : string.Empty;
            }
        }
        public string Dokument9Str
        {
            get
            {
                return Dokument9 != null ? Dokument9.NumerPelny : string.Empty;
            }
        }

        public bool HasDokument0
        {
            get { return Dokument0 != null; }
        }
        public bool HasDokument1
        {
            get { return Dokument1 != null; }
        }
        public bool HasDokument2
        {
            get { return Dokument2 != null; }
        }
        public bool HasDokument3
        {
            get { return Dokument3 != null; }
        }
        public bool HasDokument4
        {
            get { return Dokument4 != null; }
        }
        public bool HasDokument5
        {
            get { return Dokument5 != null; }
        }
        public bool HasDokument6
        {
            get { return Dokument6 != null; }
        }
        public bool HasDokument7
        {
            get { return Dokument7 != null; }
        }
        public bool HasDokument8
        {
            get { return Dokument8 != null; }
        }
        public bool HasDokument9
        {
            get { return Dokument9 != null; }
        }

        public double Dokument0Ilosc { get; set; }
        public double Dokument1Ilosc { get; set; }
        public double Dokument2Ilosc { get; set; }
        public double Dokument3Ilosc { get; set; }
        public double Dokument4Ilosc { get; set; }
        public double Dokument5Ilosc { get; set; }
        public double Dokument6Ilosc { get; set; }
        public double Dokument7Ilosc { get; set; }
        public double Dokument8Ilosc { get; set; }
        public double Dokument9Ilosc { get; set; }

        public bool Equals(PozycjaZwrotuAnalizaOld obj)
        {
            return obj.Ident == this.Ident;
        }

        public override bool Equals(object obj)
        {
            return this.Equals((PozycjaZwrotuAnalizaOld)obj);
        }

        public override int GetHashCode()
        {
            return this.Ident.GetHashCode();
        }
    }
}
