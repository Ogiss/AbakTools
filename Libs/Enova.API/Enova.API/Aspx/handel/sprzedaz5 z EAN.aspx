<%@ import Namespace="Soneta.Types" %>
<%@ import Namespace="Soneta.Waluty" %>
<%@ import Namespace="Soneta.Kasa" %>
<%@ import Namespace="Soneta.Handel" %>
<%@ import Namespace="Soneta.Business.App" %>
<%@ import Namespace="Soneta.Business" %>
<%@ import Namespace="Soneta.Core" %>
<%@ Register TagPrefix="cc1" Namespace="Soneta.Core.Web" Assembly="Soneta.Core.Web" %>
<%@ Register TagPrefix="ea" Namespace="Soneta.Web" Assembly="Soneta.Web" %>
<%@ Page Language="c#" CodePage="1200" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Sprzedaż</title>
		<script runat="server">

/*
    ParametryWydrukuDokumentu parametry;
    [Context]
    public ParametryWydrukuDokumentu Parametry {
        get { return parametry; }
        set { parametry = value; }
    }
*/

	public class ParametryWydrukuFV : ParametryWydrukuDokumentu
	{
		private bool drukujRozrachunki;
        private bool drukujEan;
		private Date dataDuplikatu;
		
        [Caption("Data duplikatu")]
		public Date DataDuplikatu
		{
			get { return dataDuplikatu; }
			set { dataDuplikatu = value; }
		}

        [Caption("Drukuj rozrach.")]
		public bool DrukujRozrachunki
		{
			get {return drukujRozrachunki; }
			set {drukujRozrachunki = value; }
		}

        [Caption("Drukuj EAN")]
        public bool DrukujEAN
        {
            get { return drukujEan; }
            set { drukujEan = value; }
        }
		
	    public ParametryWydrukuFV(Context cx) : base(cx)
		{
			dataDuplikatu = Date.Today;
			drukujRozrachunki = (bool)Dokument.Kontrahent.Features["Drukuj listę rozrachunków"];
            drukujEan = (bool)Dokument.Kontrahent.Features["Drukuj EAN"];
			
			int ik = (int)Dokument.Kontrahent.Features["Domyślna ilość kopi"];
			if(ik != 0)
				IloscKopii = ik;
			else
				IloscKopii = this.Dokument.Definicja.IloscKopii;
		}
		
	    public ParametryWydrukuFV(Context cx, bool oryginałkopia) : this(cx) {}

	}
	
	ParametryWydrukuFV parametry;
	[Context]
	public ParametryWydrukuFV Parametry
	{
		get { return parametry; }
		set { parametry = value; }
	}

    private bool SprawdźPłatności( DokumentHandlowy dokument ) {
        bool result = false;
        foreach( Platnosc platnosc in dokument.Platnosci ) {
            if( platnosc.Kwota.Symbol.Equals( "PLN" ) ) {
                result = true;
            }
        }
        return result;
    }

    private bool SprawdźSwift( DokumentHandlowy dokument ) {
        bool warunek1 = dokument.DaneKontrahenta == null || dokument.RachunekBankowy == null || dokument.RachunekBankowy.Rachunek == null || dokument.RachunekBankowy.Rachunek.SWIFT == "";
        bool warunek2 = false;
        if( dokument.Kontrahent != null && dokument.Kontrahent.RodzajPodmiotu == RodzajPodmiotu.Krajowy ) 
            warunek2 = dokument.RachunekBankowy != null && dokument.RachunekBankowy.Waluta.Symbol.Equals( "PLN" ) && SprawdźPłatności( dokument );   

        return ( warunek1 || warunek2 );
    }

    private bool SprawdźSwift2( DokumentHandlowy dokument ) {
        bool warunek1 = dokument.DaneKontrahenta == null || dokument.RachunekBankowy2 == null || dokument.RachunekBankowy2.Rachunek == null || dokument.RachunekBankowy2.Rachunek.SWIFT == "";
        bool warunek2 = false;
        if( !warunek1 && dokument.Kontrahent != null && dokument.Kontrahent.RodzajPodmiotu == RodzajPodmiotu.Krajowy ) 
            warunek2 = dokument.RachunekBankowy2 != null && dokument.RachunekBankowy2.Waluta.Symbol.Equals( "PLN" ) && SprawdźPłatności( dokument );   

        return ( warunek1 && warunek2 );
    }     		     
    
    void OnContextLoad(Object sender, EventArgs args) {
        DataRepeater1.DataSource = (IEnumerable)Parametry;
        DokumentHandlowy dokument = Parametry.Dokument;

		// TASK: 2526. [4.3]
		/***
		 * TASK: 5221. [7.3] /sbm
		 * 
		 * dc.AdditionalFooterInfo = true;
		 */
		dc.AdditionalFooterInfo = dokument.Definicja.InformacjeKRS;

    	if (dokument.RachunekBankowy==null
    	    || dokument.RachunekBankowy.Rachunek == null
    		|| dokument.RachunekBankowy.Rachunek.Bank==null)
			labelBank.Visible = false;

        if( !dokument.Definicja.DrukujSWIFTZawsze ) {
            if( SprawdźSwift( dokument ) )
                labelSwift.Visible = false;
        }

        if (dokument.RachunekBankowy2 == null
            || dokument.RachunekBankowy2.Rachunek == null
            || dokument.RachunekBankowy2.Rachunek.Bank == null)
            labelBank2.Visible = false;

        if( SprawdźSwift2( dokument ) )
            labelSwift2.Visible = false;

		//Tylko tyle zostało z kodu liczącego płatności.
        //platnik.Visible = dokument.InnyPłatnik;
        platnik.Visible = false;

        sww.Visible = dokument.JestSWW;
        RodzajKorektyCol.Visible = dokument.DokumentKorygowany != null;

        //Ukrywamy tabelkę VAT i kolumny VAT dla dokumentów nie VAT
        //Dostosowujemy nazwy kolumn
        string nazwa;
        if (dokument.Definicja.SumyVAT!=SposobLiczeniaSumVAT.NieLiczyć)
            nazwa = "faktury";
        else {
            SectionVAT.Visible = false;
            vat.Visible = false;
            nazwa = "rachunku";
        }
		
		string tytulWydruku = dokument.Definicja.TytulWydruku;
		
		if(dokument.Definicja.Symbol == "FV")
		{
			if(dokument.Data <= new Date(2012,12,31))
			{
				tytulWydruku = "Faktura VAT";
			}
			else
			{
				DataLabel18.Visible = false;
			}
		}
		
		TytulWydruku.EditValue = tytulWydruku;
        
        string terminZwrotu = (string)dokument.Features["TERMIN_ZWROTU"];
        
        if (string.IsNullOrEmpty(terminZwrotu))
        {
            TerminZwrotu.Visible = false;
        }
        else
        {
            TerminZwrotu.EditValue = terminZwrotu;
        }

		// Ukrywamy kolumne kwoty VAT, jesli dokument nie jest zaliczkowy.
        bool jestMniejszaKwota = dokument.LiczOd == SposobLiczeniaVAT.OdNetto ?
            dokument.SumaPozycji.Netto != dokument.Suma.Netto :
            dokument.SumaPozycji.Brutto != dokument.Suma.Brutto;
        bool jestVatZaliczk =
            (dokument.Definicja.EdycjaWartosci == EdycjaWartosciDokumentu.PozwalajNaMniejsząKwotę) &&
            (dokument.Wydruk.JestSumaPozycji && jestMniejszaKwota);
        bool nowyObieg;
        bool końcowy = dokument.JestKoncowy(out nowyObieg);
        SectionVATZamowienia.Visible = końcowy;
        SectionVATZaliczkowego.Visible = jestVatZaliczk && !dokument.Korekta;
		SectionKorektaZaliczki.Visible = jestVatZaliczk && dokument.Korekta;
		Grid1_VAT.Visible = jestVatZaliczk;
        Section3.Visible = !SectionVATZaliczkowego.Visible && !końcowy && jestMniejszaKwota;
        TabelaVatZaliczkiNapis.Visible = jestVatZaliczk && !końcowy;
        TabelaVatKoncowegoNapis.Visible = false;
        DataLabelDopłataZaliczki.EditValue = "Podlega opodatkowaniu";
        
        //Jeżeli dokumenty liczone od brutto, to wymieniamy nagłówki
        if (dokument.OdBrutto)
            wartosc.Caption = "Wartość brutto";
   
        //Formatujemy podpisy
        stPodpis.Caption = "<font size=2>"+dokument.Session.Login.Operator.FullName+"</font><br><br><font size=1>..................................................<br>Podpis osoby uprawnionej do wystawienia "+nazwa+"</font>";
        stOsoba.Caption = "<font size=2>"+dokument.Osoba+"</font><br><br><font size=1>..................................................<br>Podpis osoby upoważnionej do otrzymania "+nazwa+"</font>";
        
        // Ukrywanie kolumn z ceną przed rabatem i rabatem procentowym
        Grid1_CenaPrzedRabatem.Visible = dokument.Definicja.DrukowanieCenyIRabatu;
        Grid1_RabatP.Visible = dokument.Definicja.DrukowanieCenyIRabatu;

        if (dokument.ID < 0 || dokument.State == RowState.Modified)
            DataLabelOstrzezenie.EditValue = "Zmiany na dokumencie nie zostały zatwierdzone";        
			
		//Rozrachunki
		
		if(Parametry.DrukujRozrachunki)
		{
		
			ArrayList arrRozrachunki = new ArrayList();
			Date minDate = new Date(2009, 01, 01);
			FromTo okres = new FromTo(minDate, Date.Today);
			View rozrachunkiIdx = ((KasaModule)dokument.Session.Modules["Kasa"]).RozrachunkiIdx.Nierozliczone(dokument.Kontrahent, FromTo.All, Date.Today);
			rozrachunkiIdx.Sort = "Termin";
			foreach(RozrachunekIdx rozrachunek in rozrachunkiIdx)
			{
				if(rozrachunek.DoRozliczenia > 0 && rozrachunek.Termin <= Date.Today)
					arrRozrachunki.Add(rozrachunek);
			}
			GridRozrachunki.DataSource = arrRozrachunki;
			RozrachunkiData.EditValue = Date.Today;
		}
		else
			SectionRozrachunki.Visible = false;
    }
    
    void DataRepeater1_BeforeRow(Object sender, EventArgs args) {
		KopiaDokumentu kopia = (KopiaDokumentu)DataRepeater1.CurrentRow;
		DokumentHandlowy dokument = kopia.Dokument;
		if (kopia.Kopia==TypKopiiDokumentu.Duplikat || kopia.Kopia==TypKopiiDokumentu.OryginałDuplikat || kopia.Kopia==TypKopiiDokumentu.KopiaDuplikat) {
			//labelDataDuplikatu.EditValue = Date.Today;
			labelDataDuplikatu.EditValue = Parametry.DataDuplikatu;
			labelDataDuplikatuText.EditValue = "Data duplikatu: ";
		}
    }

    void Grid1_OnBeforeRow(Object sender, RowEventArgs args)
    {
        if (Parametry.DrukujEAN)
        {
            PozycjaDokHandlowego poz = (PozycjaDokHandlowego)args.Row;
            var ean = (string)poz.Towar.Features["Drukowany EAN"];
            if (!string.IsNullOrEmpty(ean))
            {
                EANColumn.EditValue = "EAN: " + ean;
            }
            else
            {
                if (((View)poz.Towar.KodyWlasne).Count == 1)
                {
                    EANColumn.EditValue = "EAN: " + poz.Towar.EAN;
                }
            }
        }
    }        
    
    void niezapłacone_BeforeRow(Object sender, RowEventArgs args) {
        WydrukDokumentu.NiezapłaconeInfo p = (WydrukDokumentu.NiezapłaconeInfo)args.Row;
        // Mateusz - task 10271
        if( p.Płatność.SposobZaplaty.Typ != TypySposobowZaplaty.Przelew 
            || ( p.Płatność.SposobZaplaty.Typ == TypySposobowZaplaty.Przelew && p.Płatność.EwidencjaSP.Rachunek.Numer == (p.Płatność.Dokument as DokumentHandlowy).RachunekBankowy.Rachunek.Numer 
            && (p.Płatność.Dokument as DokumentHandlowy).Wydruk.Niezapłacone.Count == 1 ) ) { // dla całej reszty zostawiamy jak leci
            SposobZaplaty.EditValue = p.Płatność.SposobZaplaty;
        }
        else  { // a dla przelewu doklejamy numer rachunku
            SposobZaplaty.AddLine( p.Płatność.SposobZaplaty + " na rachunek bankowy" );
            SposobZaplaty.AddLine( p.Płatność.EwidencjaSP.Rachunek.Numer );
        }

        if (p.Płatność.Podmiot!=p.Płatność.Dokument.Podmiot) {
            platnik.AddLine(p.Płatność.Podmiot.Nazwa);
            platnik.AddLine(p.Płatność.Podmiot.Adres);
            platnik.AddLine("NIP: " + p.Płatność.Podmiot.EuVAT);
        }
    }
	void gridZaliczki_BeforeRow(object sender, RowEventArgs args)
	{
		DokumentHandlowy z = (DokumentHandlowy)args.Row;
		DokumentHandlowy d = (DokumentHandlowy)this.dc.Context[typeof(DokumentHandlowy)];
		SubTable st = d.ZaliczkiRelacje;
		if (st.IsEmpty 
			&& z.SposobPrzenoszeniaZaliczki == SposobPrzenoszeniaZaliczki.NieDotyczy)
		{
			this.colZaliczka.EditValue = z.BruttoCy;
		}
		else
		{
			Currency v = new Currency(decimal.Zero, z.BruttoCy.Symbol);
			foreach (RelacjaHandlowa.Zaliczka rz in st)
			{
				if (rz.Nadrzedny == z)
				{
					v += rz.Wartosc;
				}
			}
			this.colZaliczka.EditValue = v;
		}
	}	
</script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
<body>
    <form id="Sprzedaż" method="post" runat="server">
        <ea:DataContext ID="dc" runat="server" TypeName="Soneta.Handel.DokumentHandlowy,Soneta.Handel"
            OnContextLoad="OnContextLoad" RightMargin="-1" LeftMargin="-1"></ea:DataContext>
        <ea:DataRepeater ID="DataRepeater1" runat="server" OnBeforeRow="DataRepeater1_BeforeRow"
            RowTypeName="Soneta.Handel.KopiaDokumentu,Soneta.Handel" Width="100%" Height="161px">
            <ea:SectionMarker ID="SectionMarker9" runat="server">
            </ea:SectionMarker>
            <ea:PageBreak ID="PageBreak1" runat="server" BreakFirstTimes="False" ResetPageCounter="True">
            </ea:PageBreak>
            <cc1:reportheader id="ReportHeader" title="Faktura {0}"
                runat="server" DataMember0="Dokument.Numer" FirstHeader="False" ></cc1:reportheader>
            <div style="border-top: 1px solid; width: 100%; border-bottom: 1px solid">
                <table id="Table4" style="font-size: xx-small; font-family: Tahoma" width="100%">
                    <tr>
                        <td style="font-weight: bold; font-size: medium" valign="top" align="left">
							<ea:DataLabel ID="TytulWydruku" runat="server" Format="{0}"></ea:DataLabel>
                            <!--<ea:DataLabel ID="DataLabel19" runat="server" DataMember="Dokument.Definicja.TytulWydruku">
                            </ea:DataLabel>-->
                            <ea:DataLabel ID="DataLabel20" runat="server" DataMember="Dokument.Session.Handel.Config.Ogólne.MałyPodatnik">
                                <ValuesMap>
                                    <ea:ValuesPair Key="False" Value=""></ea:ValuesPair>
                                    <ea:ValuesPair Key="True" Value=" MP"></ea:ValuesPair>
                                </ValuesMap>
                            </ea:DataLabel>
                            &nbsp;nr
                            <ea:DataLabel ID="DataLabel15" runat="server" DataMember="Dokument.Numer">
                            </ea:DataLabel>
                            <ea:DataLabel ID="DataLabel4" runat="server" DataMember="Dokument.Stan">
                                <ValuesMap>
                                    <ea:ValuesPair Key="Anulowany" Value="&lt;br&gt;Dokument został anulowany"></ea:ValuesPair>
                                    <ea:ValuesPair Key="Bufor" Value="&lt;br&gt;Dokument nie został zatwierdzony"></ea:ValuesPair>
                                    <ea:ValuesPair Key="Zablokowany" Value=""></ea:ValuesPair>
                                    <ea:ValuesPair Key="Zatwierdzony" Value=""></ea:ValuesPair>
                                </ValuesMap>
                            </ea:DataLabel>
                            <br />
                            <ea:DataLabel ID="DataLabelOstrzezenie" runat="server"></ea:DataLabel>
                            <br/>
                            <span style="font-weight: normal; font-size: medium">
                                <ea:DataLabel ID="DataLabel18" runat="server" DataMember="Kopia" Bold="True">
                                </ea:DataLabel>
                            </span>
                        </td>
                        <td valign="top" align="right">
                            <ea:DataLabel ID="DataLabel36" runat="server" DataMember="Dokument.Definicja.NazwaDatyEtykieta"
                                Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            <ea:DataLabel ID="DataLabel35" runat="server" DataMember="Dokument.Definicja.NazwaDatyOperacjiEtykieta"
                                Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            <ea:DataLabel ID="labelDataDuplikatuText" runat="server" Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                        </td>
                        <td width="10">
                        </td>
                        <td valign="top" align="right">
                            <ea:DataLabel ID="DataLabel21" runat="server" DataMember="Dokument.Data" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            <ea:DataLabel ID="DataLabel22" runat="server" DataMember="Dokument.DataOperacji" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            <ea:DataLabel ID="labelDataDuplikatu" runat="server" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                        </td>
                    </tr>
                </table>
            </div>
            <table id="Table1" width="100%">
                <tr>
                    <td valign="top" colspan="2">
                        <ea:Section ID="Section4" runat="server" Width="100%" DataMember="Dokument.DokumentKorygowany"
                            ConditionValue="IS NOT NULL">
                            <em style="text-decoration: underline;">Dokument korygowany:</em>
                            <div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative">
                                <ea:DataLabel ID="DataLabel23" runat="server" DataMember="Dokument.DokumentKorygowanyPierwszy.Numer" EncodeHTML="True">
                                </ea:DataLabel>
                                <br/>
                                <ea:DataLabel ID="DataLabel37" runat="server" DataMember="Dokument.DokumentKorygowanyPierwszy.Definicja.NazwaDatyEtykieta"
                                    Bold="False" EncodeHTML="True">
                                </ea:DataLabel>
                                <ea:DataLabel ID="DataLabel38" runat="server" DataMember="Dokument.DokumentKorygowanyPierwszy.Data" EncodeHTML="True">
                                </ea:DataLabel>
                                <br/>
                                <ea:DataLabel ID="DataLabel39" runat="server" DataMember="Dokument.DokumentKorygowanyPierwszy.Definicja.NazwaDatyOperacjiEtykieta"
                                    Bold="False" EncodeHTML="True">
                                </ea:DataLabel>
                                <ea:DataLabel ID="DataLabel40" runat="server" DataMember="Dokument.DokumentKorygowanyPierwszy.DataOperacji" EncodeHTML="True">
                                </ea:DataLabel>
                            </div>
                        </ea:Section>
                    </td>
                </tr>
                <tr>
                    <td valign="top" width="50%">
                        <em style="text-decoration: underline;">Sprzedawca:</em>
                        <div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative">
                            <ea:DataLabel ID="DataLabel13" runat="server" DataMember="Dokument.Session.Core.Config.Firma.Pieczątka.NazwaWieleLinii" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            <ea:DataLabel ID="Datalabel16" runat="server" DataMember="Dokument.Session.Core.Config.Firma.AdresSiedziby.Linia1"
                                Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            <ea:DataLabel ID="Datalabel17" runat="server" DataMember="Dokument.Session.Core.Config.Firma.AdresSiedziby.Linia2"
                                Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            NIP:
                            <ea:DataLabel ID="DataLabel14" runat="server" DataMember="Dokument.Wydruk.NIP" Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                            <br />Tel./Fax: 15 6882084 , www.pthabak.pl
                        </div>
                        
                        <!-- Oddział firmy -->
                        <ea:Section ID="OddzialFirmy" runat="server" 
							DataMember="Dokument.Wydruk.JestOddzial">
							<em style="text-decoration: underline;">Oddział:</em>
							<div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative;">
								<ea:DataLabel ID="DataLabel42" runat="server" EncodeHTML="True"
									DataMember="Dokument.Wydruk.Oddział.Nazwa" ></ea:DataLabel><br />
								<ea:DataLabel ID="DataLabel44" runat="server" EncodeHTML="True" Bold="false"
									DataMember="Dokument.Wydruk.Oddział.Adres.Linia1" ></ea:DataLabel><br />
								<ea:DataLabel ID="DataLabel45" runat="server" EncodeHTML="True" Bold="false"
									DataMember="Dokument.Wydruk.Oddział.Adres.Linia2" ></ea:DataLabel>
							</div>
                        </ea:Section>
                        <!-- Oddział firmy -->
                        
                        <em style="text-decoration: underline;">Konto bankowe:</em>
                        <div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative">
                            BZ WBK S.A. 03 1090 2750 0000 0001 1151 1117
                            <!-- <ea:DataLabel ID="labelBank" runat="server" DataMember="Dokument.RachunekBankowy.Rachunek.Bank.Nazwa"
                                Bold="False" Format="{0}   "> 
                            </ea:DataLabel>
                            -->
							<!--
                            <ea:DataLabel ID="labelSwift" runat="server" DataMember="Dokument.RachunekBankowy.Rachunek.SWIFT"
                                Bold="False" Format="SWIFT: {0}">
                            </ea:DataLabel>
                            <ea:DataLabel ID="DataLabel12" runat="server" DataMember="Dokument.NumerRachunkuBankowego"
                                Bold="False">
                            </ea:DataLabel>
							-->
                        </div>
                        <ea:Section ID="DrugiRachunekSection" runat="server" DataMember="Dokument.IsRachunekBankowy2">
                        <em style="text-decoration: underline;">Drugie konto bankowe:</em>
                        <div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative">
                            <ea:DataLabel ID="labelBank2" runat="server" DataMember="Dokument.RachunekBankowy2.Rachunek.Bank.Nazwa"
                                Bold="False" Format="{0}<br>">
                            </ea:DataLabel>
                            <ea:DataLabel ID="labelSwift2" runat="server" DataMember="Dokument.RachunekBankowy2.Rachunek.SWIFT"
                                Bold="False" Format="SWIFT: {0}<br>">
                            </ea:DataLabel>
                            <ea:DataLabel ID="DataLabel51" runat="server" DataMember="Dokument.RachunekBankowy2.Rachunek.Numer"
                                Bold="False">
                            </ea:DataLabel>
                        </div>
                        </ea:Section>
                    </td>
                    <td valign="top">
                        <em style="text-decoration: underline;">Nabywca:</em>
                        <div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative">
                            <ea:DataLabel ID="DataLabel1" runat="server" DataMember="Dokument.DaneKontrahenta.NazwaFormatowana" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            <ea:DataLabel ID="DataLabel2" runat="server" DataMember="Dokument.DaneKontrahenta.Adres.Linia1"
                                Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            <ea:DataLabel ID="DataLabel3" runat="server" DataMember="Dokument.DaneKontrahenta.Adres.Linia2"
                                Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                            <br/>
                            NIP:
                            <ea:DataLabel ID="DataLabel11" runat="server" DataMember="Dokument.DaneKontrahenta.EuVAT"
                                Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                        </div>
                        <ea:Section ID="sectionOdbiorca" runat="server" DataMember="Dokument.Wydruk.JestOdbiorca">
                            <em style="text-decoration: underline;">Odbiorca:</em>
                            <div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative">
                                <ea:DataLabel ID="DataLabel10" runat="server" DataMember="Dokument.DaneOdbiorcy.NazwaFormatowana" EncodeHTML="True">
                                </ea:DataLabel>
                                <br/>
                                <ea:DataLabel ID="DataLabel9" runat="server" DataMember="Dokument.DaneOdbiorcy.Adres.Linia1"
                                    Bold="False" EncodeHTML="True">
                                </ea:DataLabel>
                                <br/>
                                <ea:DataLabel ID="DataLabel8" runat="server" DataMember="Dokument.DaneOdbiorcy.Adres.Linia2"
                                    Bold="False" EncodeHTML="True">
                                </ea:DataLabel>
                                <br/>
                                NIP:
                                <ea:DataLabel ID="DataLabel7" runat="server" DataMember="Dokument.DaneOdbiorcy.EuVAT"
                                    Bold="False" EncodeHTML="True">
                                </ea:DataLabel>
                            </div>
                        </ea:Section>
                    </td>
                </tr>
            </table>
            <ea:Section ID="KursSection" runat="server" Width="100%" DataMember="Dokument.Wydruk.JestWaluta">
                <font size="2">Kurs <strong>1 </strong>
                    <ea:DataLabel ID="DataLabel31" runat="server" DataMember="Dokument.BruttoCy.Symbol" EncodeHTML="True">
                    </ea:DataLabel>
                    &nbsp;=
                    <ea:DataLabel ID="KursWaluty" runat="server" DataMember="Dokument.KursWaluty" EncodeHTML="True">
                    </ea:DataLabel>
                    <strong>&nbsp;PLN</strong> z dnia
                    <ea:DataLabel ID="DataLabel32" runat="server" DataMember="Dokument.DataOgłoszeniaKursu" EncodeHTML="True">
                    </ea:DataLabel>
                    &nbsp;(<ea:DataLabel ID="DataLabel33" runat="server" DataMember="Dokument.TabelaKursowa" EncodeHTML="True">
                    </ea:DataLabel>
                    )</font></ea:Section>
            <ea:Section ID="KorektaKursuSection" runat="server" Width="100%" DataMember="Dokument.Wydruk.JestKorektaKursu">
                Korekta kursu:
                <div style="font-size: smaller">
                     Kurs przed korektą: <strong>1</strong> <ea:DataLabel ID="DataLabel53" runat="server" DataMember="Dokument.DokumentKorygowany.BruttoCy.Symbol" EncodeHTML="True" />
                     &nbsp;=&nbsp;<ea:DataLabel ID="DataLabel54" runat="server" DataMember="Dokument.DokumentKorygowany.KursWaluty" EncodeHTML="True" /><strong>&nbsp;PLN</strong> z dnia
                     <ea:DataLabel ID="DataLabel55" runat="server" DataMember="Dokument.DokumentKorygowany.DataOgłoszeniaKursu" EncodeHTML="True" />
                     &nbsp;(<ea:DataLabel ID="DataLabel56" runat="server" DataMember="Dokument.DokumentKorygowany.TabelaKursowa" EncodeHTML="True" />)
                     <br />
                     Kurs po korekcie: <strong>1</strong> <ea:DataLabel ID="DataLabel46" runat="server" DataMember="Dokument.BruttoCy.Symbol" EncodeHTML="True" />
                     &nbsp;=&nbsp;<ea:DataLabel ID="DataLabel49" runat="server" DataMember="Dokument.KursWaluty" EncodeHTML="True" /><strong>&nbsp;PLN</strong> z dnia
                     <ea:DataLabel ID="DataLabel52" runat="server" DataMember="Dokument.DataOgłoszeniaKursu" EncodeHTML="True" />
                     &nbsp;(<ea:DataLabel ID="DataLabel57" runat="server" DataMember="Dokument.TabelaKursowa" EncodeHTML="True" />)</div>
            </ea:Section>
            <ea:Grid ID="Grid1" runat="server" RowTypeName="Soneta.Handel.PozycjaDokHandlowego,Soneta.Handel" OnBeforeRow="Grid1_OnBeforeRow" 
                DataMember="Dokument.Wydruk.PozycjeRazem" RowsInRow="2" GroupData0="Workers.WydrukPozycji.SekcjaDokumentu"
                GroupLine="{0}">
                <Columns>
                    <ea:GridColumn Width="4" Align="Right" DataMember="Lp" Caption="Lp." RowSpan="2" runat="server"></ea:GridColumn>
                    <ea:GridColumn DataMember="NazwaPierwszaLinia" Caption="Nazwa towaru/usługi" runat="server" EncodeHTML="True"></ea:GridColumn>
                    <ea:GridColumn ID="EANColumn" Caption="EAN" runat="server"></ea:GridColumn>
                    <ea:GridColumn Width="10" RightBorder="None" Align="Right" DataMember="Ilosc.Value" Caption="Ilość" RowSpan="2" runat="server"></ea:GridColumn>
                    <ea:GridColumn Width="5" DataMember="Ilosc.Symbol" Caption="jm." RowSpan="2" runat="server"></ea:GridColumn>
                    <ea:GridColumn runat="server" ID="Grid1_CenaPrzedRabatem" DataMember="Cena" Width="15" RowSpan="2" Caption="Cena przed rabatem" Align="Right"></ea:GridColumn>
                    <ea:GridColumn runat="server" ID="Grid1_RabatP" DataMember="Rabat" Width="10" RowSpan="2" Caption="Rabat %" Align="Right"></ea:GridColumn>
                    <ea:GridColumn Width="15" Align="Right" DataMember="CenaNettoPoRabacie" Caption="Cena netto" RowSpan="2" runat="server"></ea:GridColumn>
                    <ea:GridColumn Width="15" Align="Right" DataMember="CenaBruttoPoRabacie" Caption="Cena brutto" RowSpan="2" runat="server"></ea:GridColumn>
                    <ea:GridColumn Width="16" Align="Right" DataMember="WartoscCy" Caption="Wartość netto" Format="&lt;b&gt;{0}&lt;/b&gt;" ID="wartosc" RowSpan="2" runat="server"></ea:GridColumn>
                    <ea:GridColumn Width="10" Align="Right" DataMember="DefinicjaStawki" Caption="Stawka VAT" ID="vat" RowSpan="2" runat="server"></ea:GridColumn>
                   <ea:GridColumn Width="15" Align="Right" DataMember="Suma.VAT" Caption="Kwota VAT" ID="Grid1_VAT" RowSpan="2" runat="server"></ea:GridColumn>
                    <ea:GridColumn Width="15" DataMember="SWW" Caption="PKWiU" ID="sww" RowSpan="2" runat="server"></ea:GridColumn>
                    <ea:GridColumn runat="server" ID="RodzajKorektyCol" DataMember="RodzajKorektyOpis" Width="16" Caption="Zmiana" RowSpan="2" Align="Center"></ea:GridColumn>
                </Columns>
            </ea:Grid>
            
            <!-- etykieta: Korekta zaliczki -->
            <ea:Section ID="SectionKorektaZaliczki" runat="server" Width="100%" Visible="false">
			<table id="Table5" cellspacing="0" cellpadding="0" width="90%" border="0">
				<tr><td>&nbsp;</td></tr>
                <tr>
                    <td style="width: 151px" align="right">&nbsp;</td>
                    <td style="width: 145px; border-top: black 1px solid" valign="bottom" align="left">&nbsp;</td>
                    <td style="font-weight: bold; font-size: medium; border-top: black 1px solid; height: 22px" 
						valign="bottom" align="right">Korekta zaliczki:
                    </td>
                </tr>
            </table>			
            </ea:Section>
            <!-- etykieta: Korekta zaliczki -->
            
            <ea:Section ID="SectionVATZamowienia" runat="server" Width="100%">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr align="right">
                        <td width="100%" style="font-size: x-small; text-align:right; vertical-align:bottom;">
                            <ea:Section runat="server" id="SectionVATZamowieniaNapis">
                                <em>Wartość zamówienia:</em>&nbsp;
                            </ea:Section>
                        </td>                    
                        <td align="right">
                            <ea:Grid ID="Grid_VATZamowienia" runat="server" RowTypeName="Soneta.Handel.DokumentZaliczkowy.SumaVATAdapter,Soneta.Handel"
                                DataMember="Workers.DokumentZaliczkowy.TabelaVAT" WithSections="False">
                                <Columns>
                                    <ea:GridColumn Width="15" Align="Right" DataMember="DefinicjaStawki" Total="Info"
                                        Caption="Stawka VAT" runat="server">
                                    </ea:GridColumn>
                                    <ea:GridColumn Width="17" Align="Right" DataMember="Suma.NettoCy" Total="Sum" Caption="Netto"
                                        runat="server">
                                    </ea:GridColumn>
                                    <ea:GridColumn Width="17" Align="Right" DataMember="Suma.VATCy" Total="Sum" Caption="Kwota VAT"
                                        Format="{0:n}" runat="server">
                                    </ea:GridColumn>
                                    <ea:GridColumn Width="17" Align="Right" DataMember="Suma.BruttoCy" Total="Sum" Caption="Brutto"
                                        runat="server">
                                    </ea:GridColumn>
                                </Columns>
                            </ea:Grid>
                        </td>
                    </tr>
                </table>
                <table id="Table6" cellspacing="0" cellpadding="0" width="90%">
                    <tr>
                        <td style="width: 151px" align="right" width="151">
                        </td>
                        <td style="width: 195px; border-bottom: black 1px solid" valign="bottom" align="left"
                            width="145" colspan="1" rowspan="1">
                            <ea:DataLabel ID="DataLabelDopłataZaliczki" runat="server" Bold="False" Format="{0}:"></ea:DataLabel>
                        </td>
                        <td style="font-weight: bold; font-size: medium; border-bottom: black 1px solid;
                            height: 22px" valign="bottom" align="right">
                            <ea:DataLabel ID="DataLabel47" runat="server" DataMember="Dokument.BruttoCy" Bold="False"
                                Format="{0:+u}">
                            </ea:DataLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 151px" align="right">
                        </td>
                        <td style="width: 145px" align="left">
                            <font size="2"><em>Słownie:</em></font></td>
                        <td align="right">
                            <font size="2"><em>
                                <ea:DataLabel ID="DataLabel48" runat="server" DataMember="Dokument.BruttoCy" Bold="False"
                                    Format="{0:+t}">
                                </ea:DataLabel>
                            </em></font>
                        </td>
                    </tr>
                </table>                
            </ea:Section>
            
            <ea:Section ID="SectionVATZaliczkowego" runat="server" Width="100%">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="100%" style="font-size: x-small; text-align:right; vertical-align:bottom;">
                            <em>Wartość zamówienia:</em>&nbsp;
                        </td>
                        <td align="right">
                            <ea:Grid ID="Grid_VATZaliczkowego" runat="server" RowTypeName="Soneta.Handel.DokumentZaliczkowy.SumaVATAdapter,Soneta.Handel"
                                DataMember="Workers.DokumentZaliczkowy.TabelaVAT"  WithSections="False">
                                <Columns>
                                    <ea:GridColumn Width="15" Align="Right" DataMember="DefinicjaStawki" Total="Info"
                                        Caption="Stawka VAT" runat="server">
                                    </ea:GridColumn>
                                    <ea:GridColumn Width="17" Align="Right" DataMember="Suma.NettoCy" Total="Sum" Caption="Netto"
                                        runat="server">
                                    </ea:GridColumn>
                                    <ea:GridColumn Width="17" Align="Right" DataMember="Suma.VATCy" Total="Sum" Caption="Kwota VAT"
                                        Format="{0:n}" runat="server">
                                    </ea:GridColumn>
                                    <ea:GridColumn Width="17" Align="Right" DataMember="Suma.BruttoCy" Total="Sum" Caption="Brutto"
                                        runat="server">
                                    </ea:GridColumn>
                                </Columns>
                            </ea:Grid>
                        </td>
                    </tr>
                </table>
              <table id="Table2" cellspacing="0" cellpadding="0" width="90%">
                <tr>
                    <td style="width: 151px" align="right" width="151">
                    </td>
                    <td style="width: 145px; border-bottom: black 1px solid" valign="bottom" align="left"
                        width="145" colspan="1" rowspan="1">Kwota zaliczki:</td>
                    <td style="font-weight: bold; font-size: medium; border-bottom: black 1px solid;
                        height: 22px" valign="bottom" align="right">
                        <ea:DataLabel ID="DataLabel50" runat="server" DataMember="Dokument.BruttoCy" Bold="False"
                            Format="{0:+u}">
                        </ea:DataLabel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 151px" align="right">
                    </td>
                    <td style="width: 145px" align="left">
                        <font size="2"><em>Słownie:</em></font></td>
                    <td align="right">
                        <font size="2"><em>
                            <ea:DataLabel ID="DataLabel60" runat="server" DataMember="Dokument.BruttoCy" Bold="False"
                                Format="{0:+t}">
                            </ea:DataLabel>
                        </em></font>
                    </td>
                </tr>
            </table>  
            </ea:Section>            
            <ea:Section ID="SectionVAT" runat="server" Width="100%">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="25%" style="font-size: xx-small;">
                            <ea:DataLabel ID="DataLabel24" runat="server" DataMember="Dokument.Wydruk.InfoKorekty1"
                                Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                            <br />
                            <ea:DataLabel ID="DataLabel41" runat="server" DataMember="Dokument.Wydruk.InfoKorekty2"
                                Bold="False" EncodeHTML="True">
                            </ea:DataLabel>
                        </td>
                        <td width="100%" style="font-size: x-small; text-align:right; vertical-align:bottom;">
                            <ea:Section runat="server" id="TabelaVatZaliczkiNapis">
                                <em>Tabela VAT zaliczki:</em>&nbsp;
                            </ea:Section>                        
                            <ea:Section runat="server" id="TabelaVatKoncowegoNapis">
                                <em>Tabela VAT dopłaty do zaliczki:</em>&nbsp;
                            </ea:Section>
                        </td>                        
                        <td align="right">
                            <ea:Grid ID="Grid5" runat="server" RowTypeName="Soneta.Handel.SumaVAT,Soneta.Handel"
                                DataMember="Dokument.SumyVAT" WithSections="False">
                                <Columns>
                                    <ea:GridColumn Width="15" Align="Right" DataMember="DefinicjaStawki" Total="Info"
                                        Caption="Stawka VAT" runat="server">
                                    </ea:GridColumn>
                                    <ea:GridColumn Width="17" Align="Right" DataMember="Suma.NettoCy" Total="Sum" Caption="Netto"
                                        runat="server">
                                    </ea:GridColumn>
                                    <ea:GridColumn Width="17" Align="Right" DataMember="Suma.VATCy" Total="Sum" Caption="Kwota VAT"
                                        Format="{0:n}" runat="server">
                                    </ea:GridColumn>
                                    <ea:GridColumn Width="17" Align="Right" DataMember="Suma.BruttoCy" Total="Sum" Caption="Brutto"
                                        runat="server">
                                    </ea:GridColumn>
                                </Columns>
                            </ea:Grid>
                        </td>
                    </tr>
                </table>
            </ea:Section>
            <ea:Section ID="Section3" runat="server" DataMember="Dokument.Wydruk.JestSumaPozycji"
                Width="100%">
                <em>Wartość&nbsp;brutto zamówienia:</em>
                <ea:DataLabel ID="DataLabel34" runat="server" DataMember="Dokument.SumaPozycji.Brutto" Bold="False" EncodeHTML="True">
                </ea:DataLabel>
                &nbsp;PLN<br/>
            </ea:Section>
            <ea:Section ID="sectionZaliczki" runat="server" DataMember="Dokument.DokumentyZaliczkowe">
                <em>Faktury zaliczkowe:<br/>
                </em>
                <ea:Grid ID="gridZaliczki" runat="server" RowTypeName="Soneta.Handel.DokumentHandlowy,Soneta.Handel"
                    DataMember="Dokument.Wydruk.DokumentyZaliczkowe" WithSections="False" OnBeforeRow="gridZaliczki_BeforeRow">
                    <Columns>
                        <ea:GridColumn runat="server" Width="4" Align="Right" DataMember="#" Caption="Lp.">
                        </ea:GridColumn>
                        <ea:GridColumn runat="server" Width="30" DataMember="Numer">
                        </ea:GridColumn>
                        <ea:GridColumn runat="server" Width="15" Align="Center" DataMember="Data" Total="Info">
                        </ea:GridColumn>
                        <ea:GridColumn runat="server" Width="18" Align="Right" DataMember="BruttoCy" Total="Sum"
                            Caption="Wartość">
                        </ea:GridColumn>
                        <ea:GridColumn runat="server" Width="18" Align="Right" Total="Sum" ID="colZaliczka"
							Caption="Rozliczona zaliczka"></ea:GridColumn>
                    </Columns>
                </ea:Grid>
                <br/>
            </ea:Section>
            <table id="Table3" cellspacing="0" cellpadding="0" width="90%">
                <tr>
                    <td style="width: 151px" align="right" width="151">
                    </td>
                    <td style="width: 145px; border-bottom: black 1px solid" valign="bottom" align="left"
                        width="145" colspan="1" rowspan="1">
                        <ea:DataLabel ID="doZaplaty" runat="server" DataMember="Dokument.Wydruk.KierunekZapłaty"
                            Bold="False" Format="{0}:">
                        </ea:DataLabel>
                    </td>
                    <td style="font-weight: bold; font-size: medium; border-bottom: black 1px solid;
                        height: 22px" valign="bottom" align="right">
                        <ea:DataLabel ID="DataLabel5" runat="server" DataMember="Dokument.BruttoCy" Bold="False"
                            Format="{0:+u}">
                        </ea:DataLabel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 151px" align="right">
                    </td>
                    <td style="width: 145px" align="left">
                        <font size="2"><em>Słownie:</em></font></td>
                    <td align="right">
                        <font size="2"><em>
                            <ea:DataLabel ID="DataLabel6" runat="server" DataMember="Dokument.BruttoCy" Bold="False"
                                Format="{0:+t}">
                            </ea:DataLabel>
                        </em></font>
                    </td>
                </tr>
            </table>
            <ea:Section ID="sectionWplaty" runat="server" DataMember="Dokument.Zaliczki">
                <em>Rozliczone zaliczki:<br/>
                </em>
                <ea:Grid ID="Grid2" runat="server" RowTypeName="Soneta.Handel.RelacjaZaliczki,Soneta.Handel"
                    DataMember="Dokument.Zaliczki" WithSections="False">
                    <Columns>
                        <ea:GridColumn runat="server" Width="4" Align="Right" DataMember="#" Caption="Lp.">
                        </ea:GridColumn>
                        <ea:GridColumn runat="server" Width="20" DataMember="Zaplata.SposobZaplaty" Caption="Spos&#243;b zapłaty">
                        </ea:GridColumn>
                        <ea:GridColumn runat="server" Width="15" Align="Center" DataMember="Zaplata.DataDokumentu"
                            Caption="Data">
                        </ea:GridColumn>
                        <ea:GridColumn runat="server" Width="20" Align="Right" DataMember="Kwota">
                        </ea:GridColumn>
                        <ea:GridColumn runat="server" Width="25" DataMember="Zaplata.NumerDokumentu" Caption="Numer">
                        </ea:GridColumn>
                    </Columns>
                </ea:Grid>
            </ea:Section>
            <br />
            <ea:Section ID="Section1" runat="server" DataMember="Dokument.Wydruk.ZapłataCzęściowa">
                <em>
                    <ea:DataLabel ID="DataLabel26" runat="server" DataMember="Dokument.Wydruk.Zapłacono"
                        Bold="False">
                    </ea:DataLabel>
                    <ea:DataLabel ID="DataLabel29" runat="server" DataMember="Dokument.Zapłata.SposobZaplaty.Biernik"
                        Bold="False">
                    </ea:DataLabel>
                    : <strong>
                        <ea:DataLabel ID="DataLabel27" runat="server" DataMember="Dokument.Zapłata.SłownieUpr"
                            Bold="False">
                        </ea:DataLabel>
                    </strong></em>
            </ea:Section>
            <ea:Section ID="Section2" runat="server" DataMember="Dokument.Wydruk.ZapłataCałkowita">
                <em>
                    <ea:DataLabel ID="DataLabel28" runat="server" DataMember="Dokument.Wydruk.Zapłacono">
                    </ea:DataLabel>
                    <ea:DataLabel ID="DataLabel30" runat="server" DataMember="Dokument.Zapłata.SposobZaplaty.Biernik">
                    </ea:DataLabel>
                </em><br />
            </ea:Section>
            <!-- Mateusz - zapłata zaliczkami 100% -->
            <ea:Section runat="server" DataMember="Dokument.Wydruk.ZaliczkaPokrywaCałość">
                <em>
                    Pozostało do zapłaty 0 PLN.
                </em>
            </ea:Section>
            <ea:Section ID="sectionNiezaplacone" runat="server" DataMember="Dokument.Wydruk.SąNiezapłacone">
                <div>
	                <em>
						<ea:DataLabel ID="DataLabel43" runat="server" DataMember="Dokument.Wydruk.KierunekZapłaty" Bold="False" Format="{0}:">
                            <ValuesMap>
                                <ea:ValuesPair Key="Do zapłaty" Value="Pozostało do zapłaty"></ea:ValuesPair>
                                <ea:ValuesPair Key="Do zwrotu" Value="Pozostało do zwrotu"></ea:ValuesPair>
                                <ea:ValuesPair Key="Wartość" Value="Wartość" />
                                <ea:ValuesPair Key="Zapłacona zaliczka" Value="Zapłacona zaliczka"></ea:ValuesPair>
                                <ea:ValuesPair Key="Zwr&#243;cona zaliczka" Value="Zwr&#243;cona zaliczka"></ea:ValuesPair>
                            </ValuesMap>
                        </ea:DataLabel></em>
                    <ea:Grid ID="niezapłacone" runat="server" OnBeforeRow="niezapłacone_BeforeRow" RowTypeName="Soneta.Kasa.Platnosc,Soneta.Kasa"
                        DataMember="Dokument.Wydruk.Niezapłacone" WithSections="False">
                        <Columns>
                            <ea:GridColumn runat="server" Width="4" Align="Right" DataMember="#" Caption="Lp.">
                            </ea:GridColumn>
                            <ea:GridColumn runat="server" Width="40" ID="SposobZaplaty" Caption="Spos&#243;b zapłaty">
                            </ea:GridColumn>
                            <ea:GridColumn runat="server" Width="15" Align="Center" DataMember="Płatność.Termin"
                                Caption="Termin">
                            </ea:GridColumn>
                            <ea:GridColumn runat="server" Width="20" Align="Right" DataMember="Kwota">
                            </ea:GridColumn>
                            <ea:GridColumn runat="server" Caption="Płatnik" Format="{0:H}" ID="platnik">
                            </ea:GridColumn>
                        </Columns>
                    </ea:Grid>
                </div>
            </ea:Section>

			
			
            <ea:Section ID="sectionNumeryNadrzednych" runat="server" DataMember="Dokument.Wydruk.CzyDrukowacNumeryPowiazanych">
				<em>Dokumenty powiązane:</em>
				<div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative">
					<ea:DataLabel ID="labelNumeryNadrzednych" runat="server" DataMember="Dokument.Wydruk.NumeryNadrzędneBK" Bold="False"></ea:DataLabel>
					<ea:DataLabel ID="labelNumeryPodrzednych" runat="server" DataMember="Dokument.Wydruk.NumeryPodrzędneBK" Bold="False"></ea:DataLabel>
	            </div>
            </ea:Section>
            <ea:Section runat="server" DataMember="Dokument.Wydruk.CzyDrukowacNumeryKorekt">
                <em>Poprzenie korekty:</em>
                <div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative">
                    <ea:DataLabel runat="server" DataMember="Dokument.Wydruk.NumeryPoprzednichKorekt" Bold="false"></ea:DataLabel>
                </div>
            </ea:Section>
            <p>
                <ea:DataLabel ID="Opis" runat="server" DataMember="Dokument.Opis" Bold="False">
                </ea:DataLabel>
            </p>
            <p>
                <ea:DataLabel ID="DataLabel25" runat="server" DataMember="Dokument.Definicja.OpisWydruku"
                    Bold="False">
                </ea:DataLabel>
            </p>
            <p>
                <center>
                <!--<div style="font-size: x-small; left: 10px; font-family: Tahoma; position: relative">-->
                <H3><ea:DataLabel ID="TerminZwrotu" runat="server" Bold="False" Format="{0}"></ea:DataLabel></H3>
                <!--</div>-->
                </center>
            </p>

            <!--<center>ZAPRASZAMY NA NASZĄ STRONĘ INTERNETOWĄ - www.pthabak.pl</center>-->
			
			
            <cc1:ReportFooter ID="ReportFooter1" runat="server" Height="105px" TheEnd="False">
                <Subtitles>
                    <cc1:FooterSubtitle runat="server" Caption="Operator" ID="stPodpis" SubtitleType="CenterText"
                        Width="50">
                    </cc1:FooterSubtitle>
                    <cc1:FooterSubtitle runat="server" Caption="Osoba" ID="stOsoba" SubtitleType="CenterText"
                        Width="50">
                    </cc1:FooterSubtitle>
                </Subtitles>
            </cc1:ReportFooter>
			
			<ea:Section runat="server" ID="SectionRozrachunki" width="100%">
			<div>
			<em style="font-size: x-small; font-family: Tahoma; font-weight: normal">Lista przeterminowanych rozrachunków na dzień <ea:DataLabel ID="RozrachunkiData" runat="server" Bold="False"></ea:DataLabel> :</em>
			<ea:Grid ID="GridRozrachunki" runat="server" 
				RowTypeName="Soneta.Kasa.RozrachunekIdx,Soneta.Kasa">
				<Columns>
					<ea:GridColumn runat="server" Width="4" Align="Right" DataMember="#" Caption="Lp."></ea:GridColumn>
					<ea:GridColumn runat="server" Width="20" DataMember="Data" Caption="Data" Align="Center"></ea:GridColumn>
					<ea:GridColumn runat="server" Width="20" DataMember="Numer" Caption="Numer" Align="Center"></ea:GridColumn>
					<ea:GridColumn runat="server" Width="20" DataMember="Termin" Caption="Termin" Align="Center"></ea:GridColumn>
					<ea:GridColumn runat="server" Width="20" DataMember="Kwota" Caption="Kwota" Total="Sum" Align="Right"></ea:GridColumn>
					<ea:GridColumn runat="server" Width="20" DataMember="DoRozliczenia" Caption="Pozostało" Total="Sum" Align="Right"></ea:GridColumn>
				</Columns>
			</ea:Grid>
			</div>
			</ea:Section>

			
            <ea:SectionMarker ID="SectionMarker8" runat="server" SectionType="Footer">
            </ea:SectionMarker>
        </ea:DataRepeater>
    </form>
</body>
</html>
