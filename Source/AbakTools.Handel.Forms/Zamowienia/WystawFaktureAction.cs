using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB.Web;

[assembly: RowAction(typeof(AbakTools.Zamowienia.Forms.WystawFaktureAction), DataContextKey = "ZamowieniaView")]


namespace AbakTools.Zamowienia.Forms
{
    [Priority(400), Caption("Wystaw fakturę")]
    public class WystawFaktureAction : ZamowieniaViewActionBase
    {
        #region Methods

        public override bool TylkoMagazynier
        {
            get
            {
                return true;
            }
        }

        protected override bool TylkoPojedynczyWiersz
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Methods

        public WystawFaktureAction()
        {
            AddStatus(TypStatusuZamowienia.Pakowanie);
            AddStatus(TypStatusuZamowienia.Spakowane);
        }

        public void OnAction()
        {
            throw new NotImplementedException();
            //var zam = new ZamowienieProxy();
            /*
            foreach (Enova.Business.Old.DB.Web.ZamowienieView row in StatusAction.SelectedRows)
            {
                zam.Zamowienia.Add(row.Zamowienie);
            }
             */
            /*
            var form = new WystawFaktureForm()
            {
                Zamowienie = (Enova.Business.Old.DB.Web.Zamowienie)(zam.Zamowienia.FirstOrDefault())
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK && form.Magazyn != null)
            {
                using (new BAL.Forms.WaitCursor())
                {
                    using (var session = Enova.API.EnovaService.Instance.CreateSession())
                    {
                        var hm = session.GetModule<Enova.API.Handel.IHandelModule>();

                        var cechy = form.GetCechy();
                        List<Enova.API.Handel.IPozycjaDokHandlowego> exPozycje = null;
                        if (form.DoliczKosztWysylki && form.KosztWysylki > 0)
                        {
                            string kod = Enova.Business.Old.Core.ContextManager.WebContext.GetConfigString("KOD_TOWARU_USLUGI_TRANSPORTU");
                            if (!string.IsNullOrEmpty(kod))
                            {
                                var towar = session.GetModule<Enova.API.Towary.ITowaryModule>().Towary[kod];
                                if (towar != null)
                                {
                                    exPozycje = new List<Enova.API.Handel.IPozycjaDokHandlowego>();

                                    exPozycje.Add(Enova.API.EnovaService.Instance.CreateObject<Enova.API.Handel.IPozycjaDokHandlowego>(
                                        new
                                        {
                                            Towar = towar,
                                            Ilosc = 1D,
                                            Cena = form.KosztWysylki
                                        }));
                                }
                                else
                                    MessageBox.Show("Nie istnie towar z kodem równym " + kod);
                            }
                            else
                                MessageBox.Show("Brak skonfigurowanego towaru usługi transportu");
                        }

             Enova.API.EnovaService.Instance.SetTools(typeof(Enova.API.ITowaryTools), new ZamowienieEditForm.TowaryTools());

                        var dokument = hm.FakturaDoZamowienia(
                            form.Magazyn,
                            form.DataWystawienia,
                            zam,
                            form.DefDokHandlowego,
                            cechy,
                            form.ZatwierdzFakture,
                            exPozycje,
                            form.Termin);

                        if (dokument != null)
                        {
                            foreach (Enova.Business.Old.DB.Web.Zamowienie z in zam.Zamowienia)
                            {
                                z.FakturaGuid = dokument.Guid;
                                z.FakturaNumer = dokument.NumerPelny;
                                z.SaveChanges();

                                if (z.StatusZamowienia != null && z.StatusZamowienia.Pakowanie == true)
                                {
                                    z.ZmienStatus(z.OstatniaHistoriaZamowienia.Pracownik, StatusyZamowieniaTyp.Spakowane);
                                    z.SaveChanges();
                                }

                                if (z.RodzajTransportu == RodzajTransportu.Kurier)
                                    z.ZmienStatus(StatusyZamowieniaTyp.Kurier);
                                else if (z.RodzajTransportu == RodzajTransportu.Przedstawiciel)
                                    z.ZmienStatus(StatusyZamowieniaTyp.Przedstawiciel);
                                z.SaveChanges();
                                Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, z);
                            }

                            if (MessageBox.Show("Czy chcesz wydrukować fakturę ?", "AbakTools",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                var template = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFVReport");
                                if (string.IsNullOrEmpty(template))
                                    throw new Exception("Nie skonfigurowano wzorca wydruku dla faktury sprzedaży");
                                hm.DrukujDokument(new Form(), dokument.Guid, template);
                            }
                        }
                         
                    }
                }
            }
             */

        }

        protected override bool CheckAnd()
        {
            return StatusAction.TenSamKontrahent && !StatusAction.WystawionoFakture;
        }

        #endregion

        #region Nested Types

        public class ZamowienieProxy : Enova.API.Handel.DokumentHandlowy
        {
            #region Fields

            private List<Enova.API.Handel.DokumentHandlowy> zamowienia;

            #endregion

            #region Properties

            public List<Enova.API.Handel.DokumentHandlowy> Zamowienia
            {
                get
                {
                    if (this.zamowienia == null)
                        this.zamowienia = new List<Enova.API.Handel.DokumentHandlowy>();
                    return this.zamowienia;
                }
            }

            #endregion


            public string NumerPelny
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.CRM.Kontrahent Kontrahent
            {
                get
                {
                    return this.Zamowienia.Select(z => z.Kontrahent).FirstOrDefault();
                }
            }

            public IEnumerable<Enova.API.Handel.PozycjaDokHandlowego> Pozycje
            {
                get
                {
                    foreach (var zam in this.Zamowienia)
                    {
                        foreach (var poz in zam.Pozycje)
                            yield return poz;
                    }
                }
            }

            public Enova.API.Types.Currency WartoscNetto
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Types.Currency WartoscVat
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Types.Currency WartoscBrutto
            {
                get { throw new NotImplementedException(); }
            }

            public bool Korekta
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Handel.DokumentHandlowy DokumentKorygowany
            {
                get { throw new NotImplementedException(); }
            }

            public bool Zatwierdzony
            {
                get { throw new NotImplementedException(); }
            }

            public bool Anulowany
            {
                get { throw new NotImplementedException(); }
            }

            public bool Bufor
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Handel.StanDokumentuHandlowego Stan
            {
                get { throw new NotImplementedException(); }
                set { }
            }

            public Guid Guid
            {
                get { throw new NotImplementedException(); }
            }

            public int ID
            {
                get { throw new NotImplementedException(); }
            }


            public object GetValue(string name)
            {
                throw new NotImplementedException();
            }

            public void SetValue(string name, object value)
            {
                throw new NotImplementedException();
            }

            public Enova.API.Handel.DefDokHandlowego Definicja
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            Enova.API.CRM.Kontrahent Enova.API.Handel.DokumentHandlowy.Kontrahent
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public Enova.API.Handel.PozycjaDokHandlowego NowaPozycja(Enova.API.Towary.Towar towar, double ilosc)
            {
                throw new NotImplementedException();
            }


            public object Record
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public Enova.API.Business.Session Session
            {
                get { throw new NotImplementedException(); }
            }

            public object CallMethod(string name, Type[] argsTypes, object[] args)
            {
                throw new NotImplementedException();
            }


            public Enova.API.Magazyny.Magazyn Magazyn
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public Enova.API.Magazyny.Magazyn MagazynDo
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }


            public string Opis
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public object EnovaObject
            {
                get { throw new NotImplementedException(); }
            }

            public T GetValue<T>(string name)
            {
                throw new NotImplementedException();
            }

            public object CallMethod(string name, params object[] args)
            {
                throw new NotImplementedException();
            }

            public object CallMethodFull(string name, Type[] paramTypes, object[] parameters)
            {
                throw new NotImplementedException();
            }


            Enova.API.Types.Currency Enova.API.Handel.DokumentHandlowy.WartoscNetto
            {
                get { throw new NotImplementedException(); }
            }

            Enova.API.Types.Currency Enova.API.Handel.DokumentHandlowy.WartoscVat
            {
                get { throw new NotImplementedException(); }
            }

            Enova.API.Types.Currency Enova.API.Handel.DokumentHandlowy.WartoscBrutto
            {
                get { throw new NotImplementedException(); }
            }


            public object GetValue(string name, object[] idexes)
            {
                throw new NotImplementedException();
            }


            Enova.API.Business.FeatureCollection Enova.API.Business.Row.Features
            {
                get { throw new NotImplementedException(); }
            }


            Enova.API.Types.Date Enova.API.Handel.DokumentHandlowy.Data
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            object Enova.API.Types.IObjectBase.EnovaObject
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public object GetObjValue(object obj, string name, Type[] types = null, object[] index = null)
            {
                throw new NotImplementedException();
            }

            public void SetObjValue(object obj, string name, object value)
            {
                throw new NotImplementedException();
            }

            public object CallObjMethod(object obj, string name, Type[] paramTypes, object[] parameters)
            {
                throw new NotImplementedException();
            }


            public System.Collections.IEnumerable Platnosci
            {
                get { throw new NotImplementedException(); }
            }


            public Enova.API.CRM.Kontrahent Odbiorca
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }


            public Enova.API.Types.Date DataOperacji
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public Enova.API.Core.NumerDokumentu Numer
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Handel.PozycjaDokHandlowego PozycjaWgIdent(int ident)
            {
                throw new NotImplementedException();
            }


            public object GetObjValue(object obj, string name, Type[] types = null, object[] index = null, Type fromType = null)
            {
                throw new NotImplementedException();
            }


            public bool Is<T>()
            {
                throw new NotImplementedException();
            }

            public T As<T>()
            {
                throw new NotImplementedException();
            }

            public Enova.API.Types.Date WyliczDateZaplaty(Enova.API.Core.KierunekPlatnosci kierunek)
            {
                throw new NotImplementedException();
            }

            Enova.API.Core.IKontrahent Enova.API.Core.IDokumentHandlowy.Kontrahent
            {
                get { throw new NotImplementedException(); }
            }

            Enova.API.Types.Date Enova.API.Core.IDokument.Data
            {
                get { throw new NotImplementedException(); }
            }

            Enova.API.Core.IDefinicjaDokumentu Enova.API.Core.IDokument.Definicja
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Core.IPodmiot Podmiot
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsReadOnly()
            {
                throw new NotImplementedException();
            }

            Enova.API.Business.FeatureCollection Enova.API.Business.IRow.Features
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsLive
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Business.IRow Parent
            {
                get { throw new NotImplementedException(); }
            }

            public string Prefix
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Business.Row Root
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Business.Table Table
            {
                get { throw new NotImplementedException(); }
            }

            public int TableHandle
            {
                get { throw new NotImplementedException(); }
            }


            public T FromEnova<T>(string name, Type fromType = null)
            {
                throw new NotImplementedException();
            }


            public Enova.API.Types.Date DataWpływu
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Core.DokEwidencji Ewidencja
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Core.TypDokumentu Typ
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Types.Currency Wartosc
            {
                get { throw new NotImplementedException(); }
            }


            public Enova.API.Handel.DokumentHandlowy DokumentMagazynowyGłówny
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Business.SubTable NadrzedneRelacje
            {
                get { throw new NotImplementedException(); }
            }

            public Enova.API.Business.SubTable PodrzedneRelacje
            {
                get { throw new NotImplementedException(); }
            }


            public void Refresh()
            {
                throw new NotImplementedException();
            }


            public Enova.API.Handel.BruttoNetto Suma
            {
                get { throw new NotImplementedException(); }
            }


            public Enova.API.Handel.DokumentObcy Obcy
            {
                get { throw new NotImplementedException(); }
            }


            public void UstawTermin(int dni)
            {
                throw new NotImplementedException();
            }
        }
        

        #endregion
    }
}
