using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Kasa
{
    public interface KasaModule : Business.Module
    {
        SposobyZaplaty SposobyZaplaty { get; }
        FormyPlatnosci FormyPlatnosci { get; }
        RozrachunkiIdx RozrachunkiIdx { get; }
        RozliczeniaSP RozliczeniaSP { get; }
        DokRozliczeniowe DokRozliczeniowe { get; }
        PozDokRozlicz PozDokRozlicz { get; }
        EwidencjeSP EwidencjeSP { get; }
        Zaplaty Zaplaty { get; }
        bool PrzeliczDokumnet(Handel.DokumentHandlowy dokument);
    }
}
