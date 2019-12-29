<%@ import Namespace="Soneta.Waluty" %>
<%@ import Namespace="Soneta.Kasa" %>
<%@ import Namespace="Soneta.Business.App" %>
<%@ import Namespace="Soneta.Core" %>
<%@ Register TagPrefix="cc1" Namespace="Soneta.Core.Web" Assembly="Soneta.Core.Web" %>
<%@ Register TagPrefix="ea" Namespace="Soneta.Web" Assembly="Soneta.Web" %>
<%@ Page Language="c#" CodePage="1200" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Kompensata</title>
		
		<script runat="server">

		void OnContextLoad(Object sender, EventArgs args)
		{
   			var pieczatka = ReportHeader.GetPieczątka(dc);

			IdPieczatka1x.EditValue = IdPieczatka1.EditValue = pieczatka.NazwaFormatowana;
			IdPieczatka2x.EditValue = IdPieczatka2.EditValue = pieczatka.Adres.Linia1;
			IdPieczatka3x.EditValue = IdPieczatka3.EditValue = pieczatka.Adres.Linia2;
		}


		</script>
	</HEAD>
	<body>
		<form id="Kompensata" method="post" runat="server">
			<ea:datacontext id="dc" runat="server" TypeName="Soneta.Kasa.Kompensata,Soneta.Kasa" OnContextLoad="OnContextLoad"></ea:datacontext>
			<cc1:reportheader id="ReportHeader1" title="Kompensata należności i zobowiązań|</strong>Numer: <strong>{0}|</strong>Data: <strong>{1}|</strong>Waluta: <strong>{2}|ODCINEK A" runat="server" DataMember2="Kwota.Symbol" DataMember1="Data" DataMember0="Numer"></cc1:reportheader>
			<p><em>Nadawca:</em>
				<br>
				<ea:datalabel id="IdPieczatka1" runat="server" ></ea:datalabel><br>
				<ea:datalabel id="IdPieczatka2" runat="server" ></ea:datalabel><br>
				<ea:datalabel id="IdPieczatka3" runat="server" ></ea:datalabel></p>
			<p><em>Odbiorca:</em>
				<br>
				<ea:datalabel id="DataLabel1" runat="server" DataMember="Podmiot.NazwaFormatowana"></ea:datalabel><br/>
				<ea:datalabel id="DataLabel2" runat="server" DataMember="Podmiot.Adres.Linia1"></ea:datalabel><br/>
				<ea:datalabel id="DataLabel3" runat="server" DataMember="Podmiot.Adres.Linia2"></ea:datalabel></p>
			<p>Informujemy o dokonaniu kompensaty wzajemnych należności i zobowiązań na 
				kwotę&nbsp;
				<ea:datalabel id="DataLabel4" runat="server" DataMember="Kwota"></ea:datalabel>&nbsp;w 
				dniu &nbsp;
				<ea:datalabel id="DataLabel20" runat="server" DataMember="Data" Width="84px"></ea:datalabel>&nbsp;wg 
				podanego poniżej zestawienia:
			</p>
			<p>
			<ea:grid id="Grid1" runat="server" DataMember="Pozycje" RowsInRow="2" RowTypeName="Soneta.Kasa.KompensataPozycja,Soneta.Kasa" Sort="IsNależność desc,DataDokumentu,NumerDokumentu">
				<Columns>
					<ea:GridColumn ID="GridColumn1" runat="server" Width="20" Align="Left"  DataMember="DataDokumentu" Caption="Data" />
					<ea:GridColumn ID="GridColumn2" runat="server" Width="20" Align="Left"  DataMember="NumerDokumentu" Caption="Dokument" NoWrap="True" />
					<ea:GridColumn ID="GridColumn4" runat="server" Width="0"  Align="Left"  DataMember="NumerEwidencji" Caption="Ewidencja" NoWrap="True" />
					<ea:GridColumn ID="GridColumn3" runat="server" Width="0"  Align="Left"  DataMember="Opis" Caption="Opis dokumentu" />
					<ea:GridColumn ID="GridColumn5" runat="server" Width="16" Align="Right" DataMember="Należność" Total="Sum" Caption="Należności" HideZero="True" RowSpan="2" />
					<ea:GridColumn ID="GridColumn6" runat="server" Width="16" Align="Right" DataMember="Zobowiązanie" Total="Sum" Caption="Zobowiązania" HideZero="True" RowSpan="2" />
					<ea:GridColumn ID="GridColumn7" runat="server" Width="16" Align="Right" DataMember="DokRozliczany.DoRozliczenia" Caption="Pozostaje" RowSpan="2" />
				</Columns>
			</ea:grid>
			</p>
			<p>
				<table id="Table1" cellSpacing="0" cellPadding="0" width="90%">
					<tbody>
						<tr>
							<td align="right" width="40%" colSpan="1" rowSpan="1">Razem:</td>
							<td align="right"><ea:datalabel id="DataLabel5" runat="server" DataMember="Kwota"></ea:datalabel></td>
						</tr>
						<tr>
							<td align="right"><font size="2"><em>Słownie:</em></font></td>
							<td align="right"><font size="2"><em><ea:datalabel id="DataLabel6" runat="server" DataMember="Słownie" Bold="False"></ea:datalabel></em></font></td>
						</tr>
					</tbody>
				</table>
			</p>
			<p><ea:datalabel id="DataLabel7" runat="server" DataMember="Opis" Bold="False"></ea:datalabel></p>
			<p>Prosimy o zgodne z nami księgowanie i odesłanie potwierdzonej kompensaty.
			</p>
			<cc1:reportfooter id="ReportFooter1" runat="server" Height="105px" TheEnd="False">
				<Subtitles>
					<cc1:FooterSubtitle SubtitleType="Operator" Width="50"></cc1:FooterSubtitle>
					<cc1:FooterSubtitle Caption="Data i podpis" Width="50"></cc1:FooterSubtitle>
				</Subtitles>
			</cc1:reportfooter><ea:pagebreak id="PageBreak1" runat="server"></ea:pagebreak><cc1:reportheader id="Reportheader2" title="Kompensata należności i zobowiązań|</strong>Numer: <strong>{0}|</strong>Data: <strong>{1}|</strong>Waluta: <strong>{2}|ODCINEK B"
				runat="server" DataMember2="Kwota.Symbol" DataMember1="Data" DataMember0="Numer"></cc1:reportheader>
			<p><em>Nadawca:</em><br/>
				<ea:datalabel id="Datalabel14" runat="server" DataMember="Podmiot.NazwaFormatowana"></ea:datalabel><br/>
				<ea:datalabel id="Datalabel15" runat="server" DataMember="Podmiot.Adres.Linia1"></ea:datalabel><br/>
				<ea:datalabel id="Datalabel16" runat="server" DataMember="Podmiot.Adres.Linia2"></ea:datalabel></p>
			<p><em>Odbiorca:</em>
				<br>
				<ea:datalabel id="IdPieczatka1x" runat="server"></ea:datalabel><br>
				<ea:datalabel id="IdPieczatka2x" runat="server"></ea:datalabel><br>
				<ea:datalabel id="IdPieczatka3x" runat="server"></ea:datalabel></p>
			<p>Potwierdzamy dokonanie kompensaty wzajemnych należności i zobowiązań na 
				kwotę&nbsp;
				<ea:datalabel id="Datalabel17" runat="server" DataMember="Kwota"></ea:datalabel>&nbsp;w 
				dniu&nbsp;
				<ea:datalabel id="DataLabel21" runat="server" DataMember="Data"></ea:datalabel>&nbsp;wg 
				podanego poniżej zestawienia:
			</p>
			<p>
			<ea:grid id="Grid2" runat="server" DataMember="Pozycje" RowsInRow="2" RowTypeName="Soneta.Kasa.KompensataPozycja,Soneta.Kasa" Sort="IsNależność desc,DataDokumentu,NumerDokumentu">
				<Columns>
					<ea:GridColumn ID="GridColumn8" runat="server" Width="20" Align="Left"  DataMember="DataDokumentu" Caption="Data" />
					<ea:GridColumn ID="GridColumn9" runat="server" Width="20" Align="Left"  DataMember="NumerDokumentu" Caption="Dokument" NoWrap="True" />
					<ea:GridColumn ID="GridColumn10" runat="server" Width="0"  Align="Left"  DataMember="NumerEwidencji" Caption="Ewidencja" NoWrap="True" />
					<ea:GridColumn ID="GridColumn11" runat="server" Width="0"  Align="Left"  DataMember="Opis" Caption="Opis dokumentu" />
					<ea:GridColumn ID="GridColumn12" runat="server" Width="16" Align="Right" DataMember="Należność" Total="Sum" Caption="Należności" HideZero="True" RowSpan="2" />
					<ea:GridColumn ID="GridColumn13" runat="server" Width="16" Align="Right" DataMember="Zobowiązanie" Total="Sum" Caption="Zobowiązania" HideZero="True" RowSpan="2" />
					<ea:GridColumn ID="GridColumn14" runat="server" Width="16" Align="Right" DataMember="DokRozliczany.DoRozliczenia" Caption="Pozostaje" RowSpan="2" />
				</Columns>
			</ea:grid>
			</p>
			<p>
				<table id="Table1" cellSpacing="0" cellPadding="0" width="90%">
					<tbody>
						<tr>
							<td align="right" width="40%" colSpan="1" rowSpan="1">Razem:</td>
							<td align="right"><ea:datalabel id="Datalabel18" runat="server" DataMember="Kwota"></ea:datalabel></td>
						</tr>
						<tr>
							<td align="right"><font size="2"><em>Słownie:</em></font></td>
							<td align="right"><font size="2"><em><ea:datalabel id="Datalabel19" runat="server" DataMember="Słownie" Bold="False"></ea:datalabel></em></font></td>
						</tr>
					</tbody>
				</table>
			</p>
			<cc1:reportfooter id="Reportfooter2" runat="server" Height="105px" TheEnd="False">
				<Subtitles>
					<cc1:FooterSubtitle SubtitleType="Operator" Width="50"></cc1:FooterSubtitle>
					<cc1:FooterSubtitle Caption="Data i podpis" Width="50"></cc1:FooterSubtitle>
				</Subtitles>
			</cc1:reportfooter></form>
	</body>
</HTML>
