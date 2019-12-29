using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using System.Data.SqlClient;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;

namespace Enova.Business.Old.DB.Web
{
    public partial class PozycjaZamowienia : IEditableObject, IDeleteRecord, Enova.API.Handel.PozycjaDokHandlowego, Enova.Business.Old.IIsLive, Enova.Business.Old.IDbContext
    {
        public System.Data.Objects.ObjectContext DbContext { get; set; }

        public bool IsLive { get; set; }

        public string NazwaProduktu
        {
            get
            {
                if (ProduktIndywidualny!=null && ProduktIndywidualny.Value)
                    return ProduktNazwa;
                else if (Produkt != null)
                    return Produkt.Nazwa;
                return null;
            }
        }

        public string ProductNazwaPelna
        {
            get
            {
                if (ProduktIndywidualny != null && ProduktIndywidualny.Value)
                    return ProduktNazwa + (string.IsNullOrEmpty(Opis) ? "" : "\r\n" + Opis);
                return Produkt.ToString() + (string.IsNullOrEmpty(Opis) ? "" : "\r\n" + Opis);
            }
            set
            {
            }
        }

        public Atrybut Atrybut
        {
            get
            {
                if (AtrybutProduktu != null)
                    return AtrybutProduktu.Atrybut;
                return null;
            }
            set
            {
                /*
                if (AtrybutProduktu != null)
                    AtrybutProduktu.Atrybut = value;
                 */
            }
        }

        public string NazwaPełna
        {
            get
            {
                //return Produkt.Nazwa + (Atrybut != null ? " - " + Atrybut.GrupaAtrybutow.NazwaPubliczna + ": " + Atrybut.Nazwa : "");
                return Produkt.Nazwa + " - " + AtrybutNazwaPelna;
            }
        }

        public string AtrybutNazwaPelna
        {
            get
            {
                if (AtrybutProduktu != null && AtrybutProduktu.Atrybut != null)
                {
                    return AtrybutProduktu.Atrybut.GrupaAtrybutow.NazwaPubliczna + ": " +
                        (AtrybutProduktu.AtrybutPrefix != null ? AtrybutProduktu.AtrybutPrefix : "") +
                        AtrybutProduktu.Atrybut.Nazwa +
                        (AtrybutProduktu.AtrybutSuffix != null ? AtrybutProduktu.AtrybutSuffix : "");
                }
                return "";
            }
        }

        public string AtrybutNazwa
        {
            get
            {
                return Atrybut != null ? Atrybut.Nazwa : "";
            }
        }

        public int Kolejnosc
        {
            get
            {
                if (this.Produkt != null && this.Produkt.Kolejnosc > 0)
                    return this.Produkt.Kolejnosc;
                return 1000;
            }
        }

        public string Kod
        {
            get
            {
                return Produkt != null ? Produkt.Kod : null;
            }
        }


        public decimal? WartoscNetto
        {
            get
            {
                if (Cena != null && Ilosc != null)
                {
                    decimal rabat = this.Rabat == null ? 0 : this.Rabat.Value;
                    decimal wartosc = decimal.Round((decimal)Cena * (decimal)Ilosc, 2);

                    return decimal.Round(wartosc - wartosc * rabat, 2);
                }
                return (decimal?)null;
            }
        }

        public decimal? WartoscBrutto
        {
            get
            {
                if (WartoscNetto != null && StawkaVatValue != null)
                {
                    return decimal.Round((decimal)WartoscNetto * (1 + (Decimal)StawkaVatValue / 100M), 2);
                }
                return (decimal?)null;
            }
        }

        public decimal? WartoscOrgNetto
        {
            get
            {
                if (Cena != null && (IloscOrg != null || Ilosc != null))
                {
                    double il = IloscOrg == null ? Ilosc.Value : IloscOrg.Value;
                    decimal rabat = this.Rabat == null ? 0 : this.Rabat.Value;
                    decimal wartosc = decimal.Round((decimal)Cena * (decimal)il, 2);
                    return decimal.Round(wartosc - wartosc * rabat, 2);
                }
                return (decimal?)null;
            }
        }

        public decimal? WartoscBrakiNetto
        {
            get
            {
                if (Cena != null && IloscOrg != null && Ilosc != null && IloscOrg > Ilosc)
                {
                    decimal rabat = this.Rabat == null ? 0 : this.Rabat.Value;
                    decimal wartosc = decimal.Round((decimal)Cena * (decimal)(Ilosc - IloscOrg), 2);

                    return decimal.Round((wartosc < 0 ? (wartosc + wartosc * rabat) : (wartosc - wartosc * rabat)), 2);
                }
                return (decimal?)null;
            }
        }

        public decimal? WartoscOrgBrutto
        {
            get
            {
                if (WartoscOrgNetto != null && StawkaVatValue != null)
                {
                    return decimal.Round((decimal)WartoscOrgNetto + (1 + (decimal)StawkaVatValue / 100M), 2);
                }
                return (decimal?)null;
            }
        }

        partial void OnIloscChanging(double? value)
        {
            if (EntityState != EntityState.Added && EntityState != EntityState.Detached && Zamowienie.HistoriaZamowienia
                    .Any(h => h.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && h.Status.Pakowanie.Value))
            {
                if (IloscOrg == null)
                    IloscOrg = Ilosc;
            }
        }

        public bool GetTowarDostepny()
        {
            if (this.Produkt != null)
            {
                if (this.DbContext != null)
                    this.DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this.Produkt);
                var dostepny = this.Produkt.Dostepny;
                if (dostepny && this.AtrybutProduktu != null)
                {
                    if (this.DbContext != null)
                        this.DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this.AtrybutProduktu);
                    dostepny = this.AtrybutProduktu.Dostepny;
                }
                return dostepny;
            }
            return true;
        }

        public void SetTowarDostepny(bool dostepny)
        {
            if (this.Produkt != null)
            {
                if (this.AtrybutProduktu != null)
                    this.AtrybutProduktu.Dostepny = dostepny;
                else
                    Produkt.Dostepny = dostepny;
            }
        }

        public void SetVisibleAV(bool visible)
        {
            if (this.AtrybutProduktu != null)
                this.AtrybutProduktu.VisibleAV = visible;
            else if (this.Produkt != null)
                this.Produkt.VisibleAV = visible;
        }

        partial void OnIloscChanged()
        {
            if (EntityState != EntityState.Deleted && EntityState != EntityState.Detached)
            {
                if (EntityState == EntityState.Added || !Zamowienie.HistoriaZamowienia
                    .Any(h => h.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && h.Status.Pakowanie.Value))
                    IloscOrg = Ilosc;
                else if (EntityState == EntityState.Modified && Zamowienie.Pakowanie)
                {
                    Zamowienie.PrzeliczZamowienie();
                    /*
                    if (Ilosc < IloscOrg)
                    {
                        var pav = GetProductAvaliable(true);
                        pav.Visible = true;
                        //Core.ContextManager.WebContext.SaveChanges();
                    }
                     */
                }
            }
            
        }

        partial void OnRabatChanged()
        {
            if (this.IsLive)
            {
                this.ZmienionoRabat = true;
            }
        }

        #region IPozycjaDokHan Implementation

        int Enova.API.Handel.PozycjaDokHandlowego.Lp
        {
            get
            {
                return this.Ident == null ? 0 : this.Ident.Value;
            }
        }

        int Enova.API.Handel.PozycjaDokHandlowego.Ident
        {
            get { return this.Ident == null ? 0 : this.Ident.Value; }
        }

        Enova.API.Towary.Towar Enova.API.Handel.PozycjaDokHandlowego.Towar
        {
            get
            {
                return Enova.API.EnovaService.Instance.CreateObject<Enova.API.Towary.Towar>(new
                {
                    Guid = this.Produkt.EnovaGuid.Value,
                    Kod = this.Produkt.Kod,
                    Nazwa = this.Produkt.Nazwa
                });
            }
            set { }
        }

        double Enova.API.Handel.PozycjaDokHandlowego.Ilosc
        {
            get
            {
                return this.Ilosc == null ? 0 : this.Ilosc.Value;
            }
            set
            {
                this.Ilosc = value;
            }
        }

        decimal? Enova.API.Handel.PozycjaDokHandlowego.Cena
        {
            get { return null; }
            set { }
        }

        decimal? Enova.API.Handel.PozycjaDokHandlowego.Rabat
        {
            get
            {
                if (this.ZmienionoRabat != null && this.ZmienionoRabat.Value && this.Rabat != null)
                    return this.Rabat;
                return null;
            }
            set { }
        }

        bool Enova.API.Handel.PozycjaDokHandlowego.Korekta
        {
            get { throw new NotImplementedException(); }
        }

        Enova.API.Handel.PozycjaDokHandlowego Enova.API.Handel.PozycjaDokHandlowego.PozycjaKorygowana
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IEditableObject Implementation

        private bool isEditing = false;
        private int? orgProduktId = null;

        void IEditableObject.BeginEdit()
        {
            if (!isEditing)
            {
                if (Produkt != null)
                    orgProduktId = Produkt.ID;
                isEditing = true;
            }
        }

        void IEditableObject.CancelEdit()
        {
            if (isEditing)
            {
                if (EntityState == System.Data.EntityState.Modified)
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
                isEditing = false;
            }
        }

        void IEditableObject.EndEdit()
        {
            if (isEditing)
            {
                if (Synchronizacja == (int)RowSynchronizeOld.Synchronized)
                {
                    if (EntityState == EntityState.Modified || (orgProduktId != null && orgProduktId != Produkt.ID) || (orgProduktId == null && Produkt != null))
                    {
                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                        Stamp = DateTime.Now;
                    }
                }
                Zamowienie.PrzeliczZamowienie();
            }
        }

        #endregion

        #region Edit Record Implementation

        bool IDeleteRecord.DeleteRecord()
        {
            if (EntityState != EntityState.Deleted && EntityState != EntityState.Detached)
            {
                if (EntityState == EntityState.Added)
                {
                    ContextManager.WebContext.DeleteObject(this);
                }
                else
                {
                    if (Zamowienie.HistoriaZamowienia.Any(h => h.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && h.Status.Pakowanie.Value))
                    {
                        if (IloscOrg == null)
                            IloscOrg = Ilosc;
                        Ilosc = 0;
                    }
                    else
                    {
                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                    }
                }
                return true;
            }
            return false;
        }

        #endregion

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

        public object GetValue(string name)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string name, object value)
        {
            throw new NotImplementedException();
        }


        public API.Business.Session Session
        {
            get { throw new NotImplementedException(); }
        }

        public object CallMethod(string name, Type[] argsTypes, object[] args)
        {
            throw new NotImplementedException();
        }

        public void AktualizujStanMagazynu(string connectionString, out Guid towarGuid, out double zmianaStanu, out double stanMag)
        {
            towarGuid = Guid.Empty;
            zmianaStanu = 0;
            stanMag = 0;

                using (var t = new TransactionScope())
                {
                    using (var conn = new SqlConnection(connectionString))
                    {

                        conn.Open();
                        var command = new SqlCommand("AktualizujStanMagazynuWgPozycji")
                        {
                            CommandType = CommandType.StoredProcedure,
                            Connection = conn

                        };

                        command.Parameters.Add("@pozycjaID", SqlDbType.Int);
                        command.Parameters["@pozycjaID"].Direction = ParameterDirection.Input;
                        command.Parameters["@pozycjaID"].Value = this.ID;

                        command.Parameters.Add("@towarGuid", SqlDbType.UniqueIdentifier);
                        command.Parameters["@towarGuid"].Direction = ParameterDirection.InputOutput;
                        command.Parameters["@towarGuid"].Value = towarGuid;

                        command.Parameters.Add("@zmianaStanu", SqlDbType.Float);
                        command.Parameters["@zmianaStanu"].Direction = ParameterDirection.InputOutput;
                        command.Parameters["@zmianaStanu"].Value = zmianaStanu;

                        command.Parameters.Add("@stanMag", SqlDbType.Float);
                        command.Parameters["@stanMag"].Direction = ParameterDirection.InputOutput;
                        command.Parameters["@stanMag"].Value = zmianaStanu;

                        command.ExecuteNonQuery();


                      if(!System.DBNull.Equals(command.Parameters["@towarGuid"].Value,System.DBNull.Value))
                            towarGuid = (Guid)command.Parameters["@towarGuid"].Value;
                      if (!System.DBNull.Equals(command.Parameters["@zmianaStanu"].Value, System.DBNull.Value))
                          zmianaStanu = (double)command.Parameters["@zmianaStanu"].Value;
                      if (!System.DBNull.Equals(command.Parameters["@stanMag"].Value, System.DBNull.Value))
                          stanMag = (double)command.Parameters["@stanMag"].Value;

                    }
                    t.Complete();
                }
                if (this.DbContext != null)
                    this.DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
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


        public object GetValue(string name, object[] idexes)
        {
            throw new NotImplementedException();
        }


        API.Business.FeatureCollection API.Business.Row.Features
        {
            get { throw new NotImplementedException(); }
        }

        object API.Types.IObjectBase.EnovaObject
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

        public bool IsReadOnly()
        {
            throw new NotImplementedException();
        }

        API.Business.FeatureCollection API.Business.IRow.Features
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.IRow Parent
        {
            get { throw new NotImplementedException(); }
        }

        public string Prefix
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.Row Root
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.Table Table
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


        public API.Business.SubTable Obroty
        {
            get { throw new NotImplementedException(); }
        }


        public double IloscMagazynu
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

        public API.Towary.DefinicjaCeny DefinicjaCeny
        {
            get { throw new NotImplementedException(); }
        }

        public void UstawCenę(API.Towary.WyliczenieCeny wylicz, API.Towary.DefinicjaCeny definicjaCeny, bool wymuśZmianęIlości)
        {
            throw new NotImplementedException();
        }


        public void Refresh()
        {
            throw new NotImplementedException();
        }


        public API.Handel.BruttoNetto Suma
        {
            get { throw new NotImplementedException(); }
        }
    }
}
