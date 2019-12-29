using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Enova.API.Connector.Module("Kasa",typeof(Enova.API.Connector.Kasa.KasaModule), typeof(Enova.API.Kasa.KasaModule))]

namespace Enova.API.Connector.Kasa
{
    internal class KasaModule : Business.Module, API.Kasa.KasaModule
    {
        private SposobyZaplaty tableSposobyZaplaty;
        private FormyPlatnosci tableFormyPlatnosci;
        private RozrachunkiIdx tableRozrachunkiIdx;
        private RozliczeniaSP tableRozliczeniaSP;
        private DokRozliczeniowe tableDokRozliczeniowe;
        private PozDokRozlicz tablePozDokRozlicz;
        private EwidencjeSP tableEwidencjeSP;
        private Zaplaty tableZaplaty;

        public API.Kasa.SposobyZaplaty SposobyZaplaty
        {
            get
            {
                if(tableSposobyZaplaty==null)
                    tableSposobyZaplaty = new SposobyZaplaty(){EnovaObject = GetEnovaTable("SposobyZaplaty"), module = this};
                return tableSposobyZaplaty;
            }
        }

        public API.Kasa.FormyPlatnosci FormyPlatnosci
        {
            get
            {
                if (tableFormyPlatnosci == null)
                    tableFormyPlatnosci = new FormyPlatnosci() { EnovaObject = GetEnovaTable("FormyPlatnosci"), module = this };
                return tableFormyPlatnosci;
            }
        }

        public API.Kasa.RozrachunkiIdx RozrachunkiIdx
        {
            get
            {
                if (tableRozrachunkiIdx == null)
                    tableRozrachunkiIdx = new RozrachunkiIdx() { EnovaObject = GetEnovaTable("RozrachunkiIdx"), module = this };
                return tableRozrachunkiIdx;
            }
        }

        public API.Kasa.RozliczeniaSP RozliczeniaSP
        {
            get
            {
                if (tableRozliczeniaSP == null)
                    tableRozliczeniaSP = new RozliczeniaSP() { EnovaObject = GetEnovaTable("RozliczeniaSP"), module = this };
                return tableRozliczeniaSP;
            }
        }

        public API.Kasa.DokRozliczeniowe DokRozliczeniowe
        {
            get
            {
                if (tableDokRozliczeniowe == null)
                    tableDokRozliczeniowe = new DokRozliczeniowe() { EnovaObject = GetEnovaTable("DokRozliczeniowe"), module = this };
                return tableDokRozliczeniowe;
            }
        }

        public API.Kasa.PozDokRozlicz PozDokRozlicz
        {
            get
            {
                if (tablePozDokRozlicz == null)
                    tablePozDokRozlicz = new PozDokRozlicz() { EnovaObject = GetEnovaTable("PozDokRozlicz"), module = this };
                return tablePozDokRozlicz;
            }
        }

        public API.Kasa.EwidencjeSP EwidencjeSP
        {
            get
            {
                if (tableEwidencjeSP == null)
                    tableEwidencjeSP = new EwidencjeSP() { EnovaObject = GetEnovaTable("EwidencjeSP"), module = this };
                return tableEwidencjeSP;
            }
        }

        public API.Kasa.Zaplaty Zaplaty
        {
            get
            {
                if (tableZaplaty == null)
                    tableZaplaty = new Zaplaty() { EnovaObject = GetEnovaTable("Zaplaty"), module = this };
                return tableZaplaty;
            }
        }


        #region Methods

        public KasaModule(Business.Session session) : base(session, "Kasa") { }

        #endregion


        public bool PrzeliczDokumnet(API.Handel.DokumentHandlowy dokument)
        {
            var plt = Type.GetType("Soneta.Kasa.Platnosc, Soneta.Kasa");
            var m = plt.GetMethod("Przelicz", new Type[] { Type.GetType("Soneta.Kasa.IDokumentPlatny2, Soneta.Kasa") });
            return (bool)m.Invoke(null, new object[] { dokument.EnovaObject });
        }

    }
}
