using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Kadry.Forms
{
    public class DokumentInfo
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public int Magazyn { get; set; }
        public string NumerPelny { get; set; }
        public string KontrahentKod { get; set; }
        public string KontrahentNazwa { get; set; }
        public decimal ZakupNetto { get; set; }
        public decimal SprzedazNetto { get; set; }
        public decimal SprzedazBrutto { get; set; }

        public DateTime DataRozliczenia { get; set; }
        public bool Korekta { get; set; }
        public bool NieLiczPotracen { get; set; }
        public decimal Kwota { get; set; }
        public decimal KwotaRozliczona { get; set; }
        public decimal KwotaDoRozliczenia { get { return Kwota - KwotaRozliczona; } }
        public DateTime Termin { get; set; }

        public void Przelicz(Enova.Business.Old.DB.EnovaContext dc)
        {
            var sql = "";
            if (this.Korekta)
            {
                sql = string.Format(
                    "SELECT CONVERT(DECIMAL(10,2), SUM(ISNULL((CONVERT(DECIMAL(10,2), f.Data)*(pdh.IloscMagazynuValue-npdh.IloscMagazynuValue)),(pdh.SumaNetto-npdh.SumaNetto)*0.75))) Prowizja " +
                    "FROM dbo.PozycjeDokHan pdh INNER JOIN dbo.PozRelHandlowej prh ON (prh.PodrzednyDok=pdh.Dokument AND prh.PodrzednaIdent=pdh.Ident) " +
                    "INNER JOIN dbo.PozycjeDokHan npdh ON (prh.NadrzednyDok=npdh.Dokument AND prh.NadrzednaIdent=npdh.Ident) INNER JOIn dbo.RelacjeHandlowe rh ON (rh.ID=prh.Relacja) " +
                    "LEFT JOIN dbo.Features f ON (f.Parent=npdh.ID AND f.ParentType='PozycjeDokHan' AND f.Name='Prowizja' AND f.Lp=0) " +
                    "WHERE pdh.Dokument={0} AND rh.Definicja=1 AND rh.Glowna=1", this.ID);
            }
            else
            {
                sql = string.Format("SELECT CONVERT(DECIMAL(10,2), SUM(ZakupNetto)) ZakupNetto FROM (" +
                    "SELECT ISNULL((CONVERT(decimal(10,2), f.Data)*IloscMagazynuValue),CONVERT(decimal(10,2), pdh.SumaNetto*0.75)) ZakupNetto " +
                    "FROM dbo.PozycjeDokHan pdh LEFT JOIN dbo.Features f ON (f.Parent=pdh.ID AND f.ParentType='PozycjeDokHan' AND f.Name='Prowizja' AND f.Lp=0) " +
                    "WHERE pdh.Dokument={0})t0", this.ID);
            }
            this.ZakupNetto = dc.ExecuteStoreQuery<decimal>(sql).First();

            sql = string.Format("SELECT CONVERT(BIT, ISNULL(f.Data, '0')) Data FROM dbo.DokHandlowe dh " +
                "LEFT JOIN dbo.Features f ON (f.Parent = dh.ID AND f.ParentType = 'DokHandlowe' AND f.Name = 'NIE LICZ POTRACEN') " +
                "WHERE dh.ID = {0}", this.ID);
            this.NieLiczPotracen = dc.ExecuteStoreQuery<bool>(sql).First();
        }
    }
}
