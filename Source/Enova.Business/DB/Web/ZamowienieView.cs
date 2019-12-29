using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using BAL.Forms;
using Enova.Business.Old.Types;

namespace Enova.Business.Old.DB.Web
{
    [DataForm("AbakTools.Zamowienia.Forms.ZamowienieEditForm,AbakTools.Handel.Forms")]
    public partial class ZamowienieView : Core.IProxyObject
    {
        #region Fields

        private Zamowienie zamowienie;

        #endregion

        public object ProxyData
        {
            get
            {
                return Core.ContextManager.WebContext.Zamowienia.Where(z => z.ID == this.ID).FirstOrDefault();
            }
            set
            {
               
            }
        }

        public Zamowienie Zamowienie
        {
            get
            {
                if (this.zamowienie == null)
                    this.zamowienie = (Zamowienie)this.ProxyData;
                return this.zamowienie;
            }
        }

        public string NumerPelny
        {
            get
            {
                return ID.ToString() + "/" + PSID.ToString();
            }
        }

        public string KontrahentZam
        {
            get
            {
                if (this.ZamPrzedstawiciela != null && this.ZamPrzedstawiciela.Value)
                    return this.KontrahentInfo;
                return this.KontrahentKod;
            }
        }

        public string KtoZamowil
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ZrodloKod))
                    return this.ZrodloKod;
                if (this.ZamPrzedstawiciela == null || !this.ZamPrzedstawiciela.Value)
                {
                    return this.FirstStatusPrKod;
                }
                return (this.ZamPrzedstawiciela != null && this.ZamPrzedstawiciela.Value ? this.PrzedstawicielKod : "") + "WWW";
            }
        }

        public int KolejnoscPora
        {
            get
            {
                if (RodzajTransportu == Types.RodzajTransportu.Kurier)
                    return 5;
                else if (string.IsNullOrEmpty(NaKiedyTyp) || NaKiedyTyp == "W")
                    return 10;
                else if (NaKiedyTyp == "P")
                    return 5;
                else
                    return 1;

            }
        }


        public DateTime? NaKiedyData
        {
            get
            {
                if (this.NaKiedy != null)
                    return this.NaKiedy.Value.Date;
                return null;
            }
        }
 
        string[] deyMap = new string[]
        {
            "N",
            "Pn",
            "Wt",
            "Śr",
            "Cz",
            "Pt",
            "So"
        };

        public string NaKiedyDzienTygodnia
        {
            get
            {
                return NaKiedy == null ? null : deyMap[(int)NaKiedy.Value.DayOfWeek];
            }
        }

        public string NaKiedyDzienFull
        {
            get
            {
                return NaKiedyDzienTygodnia + ((string.IsNullOrEmpty(NaKiedyTyp) || NaKiedyTyp.ToLower() == "n") ? "" :
                    (NaKiedyTyp.ToLower() == "r" ? " - Rano" : (NaKiedyTyp.ToLower() == "p" ? " - Południe" :
                    (NaKiedyTyp.ToLower() == "w" ? " - Wieczór" : ""))));
            }
        }

        public RodzajTransportu RodzajTransportu
        {
            get
            {
                return this.Transport == null ? RodzajTransportu.NieWybrano : (RodzajTransportu)this.Transport;
            }
            set
            {
                this.Transport = (int)value;
            }
        }

        public string RodzajTransportuStr
        {
            get
            {
                switch (this.RodzajTransportu)
                {
                    case Types.RodzajTransportu.NieWybrano:
                        return "Nie wybrano";
                    case Types.RodzajTransportu.Kurier:
                        return "Kurier";
                    case Types.RodzajTransportu.Przedstawiciel:
                        return "Przedstawiciel";
                    case Types.RodzajTransportu.DoDostawcy:
                        return "Do dostawcy";
                }
                return null;
            }
        }

        public string StatusZamowieniaPelny
        {
            get
            {
               
                return string.IsNullOrEmpty(this.StatusNazwa) ? null : (this.StatusNazwa + (this.StatusPrKod != null ? " (" + StatusPrKod + ")" : ""));
            }
        }

        public string WartoscBrakiStr
        {
            get
            {
                if (WartoscBraki != null && WartoscBraki.Value != 0)
                {
                    if (AnulujBraki != null && AnulujBraki.Value)
                        return decimal.Round(WartoscBraki.Value, 2).ToString() + "zł (OLAĆ)";
                    else if (ZamowionoBraki != null && ZamowionoBraki.Value)
                        return decimal.Round(WartoscBraki.Value, 2).ToString() + "zł (" + ZamowienieBrakow.ToString() + ")";
                    return decimal.Round(WartoscBraki.Value, 2).ToString() + "zł";
                }
                return null;
            }
        }

        public TypStatusuZamowienia TypStatusu
        {
            get
            {
                return this.StatusTyp == null ? TypStatusuZamowienia.Nieznany : (TypStatusuZamowienia)this.StatusTyp.Value;
            }
        }

        public bool ZamowienieOdlozone
        {
            get
            {
                if (TypStatusu == TypStatusuZamowienia.Nieznany || (TypStatusu != TypStatusuZamowienia.Wyslane && TypStatusu != TypStatusuZamowienia.Spakowane))
                {
                    DateTime n = DateTime.Now;
                    DateTime k = this.NaKiedy == null ? this.DataDodania.Value : this.NaKiedy.Value;
                    DateTime now = new DateTime(n.Year, n.Month, n.Day, 0, 0, 0);
                    DateTime nakiedy = new DateTime(k.Year, k.Month, k.Day, 0, 0, 0);

                    int diff = (nakiedy - now).Days;
                    if ((now.DayOfWeek == DayOfWeek.Saturday && diff > 2) || (now.DayOfWeek != DayOfWeek.Saturday && diff >= 2))
                        return true;
                }
                return false;
            }
        }

        public int Kolejnosc
        {
            get
            {

                if (Pilne != null && (bool)Pilne && (TypStatusu == TypStatusuZamowienia.NoweZamowienie || TypStatusu == TypStatusuZamowienia.DoMagazynu))
                    return 100;
                if (TypStatusu == TypStatusuZamowienia.DoDostawcy)
                    return 2000;
                if (TypStatusu == TypStatusuZamowienia.Wyslane || TypStatusu == TypStatusuZamowienia.Anulowane)
                    return 1000;
                if (TypStatusu == TypStatusuZamowienia.Blokada)
                    return 900;
                else if (ZamowienieOdlozone)
                    return 800;
                else if (TypStatusu == TypStatusuZamowienia.Kurier || TypStatusu == TypStatusuZamowienia.Przedstawiciel)
                    return 500;
                else
                {
                    return 300;
                }
            }
        }

        public string SezonRazem
        {
            get
            {
                return string.IsNullOrEmpty(this.Sezon) ? "" : (this.Sezon + (string.IsNullOrEmpty(this.SezonDodatkowy) ? "" : " + " + this.SezonDodatkowy));
            }
        }

        public void SaveChanges()
        {
            var dc = Core.ContextManager.WebContext;
            if (this.Zamowienie != null)
            {
                dc.OptimisticSaveChanges();
                dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this.Zamowienie);
                dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
            }

        }

        public void ZmienStatusZamowienia(TypStatusuZamowienia nowyStatus, User user, bool save = false)
        {
            var dc = Core.ContextManager.WebContext;
            Operator @operator = User.GetOperator(user);
            StatusZamowienia status = dc.StatusyZamowien.Where(sz => sz.Typ == (int)nowyStatus).FirstOrDefault();
            if (status != null)
            {
                this.Zamowienie.ZmienStatus(@operator, status);
                if (save)
                    this.SaveChanges();
            }
        }

        public void ZmienStatusZamowienia(TypStatusuZamowienia nowyStatus, bool save = false)
        {
            this.ZmienStatusZamowienia(nowyStatus, User.LoginedUser, save);
        }

        public void CofnijStatusZamowienia(bool save = false)
        {
            if (this.Zamowienie != null)
            {
                var h = this.Zamowienie.OstatniaHistoriaZamowienia;
                if (h != null)
                {
                    h.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                    h.Deleted = true;
                    if (this.Zamowienie.OstatniaHistoriaZamowienia == null || 
                        this.Zamowienie.OstatniaHistoriaZamowienia.Status == null ||
                        this.Zamowienie.OstatniaHistoriaZamowienia.Status.Typ == (int)TypStatusuZamowienia.Nieznany ||
                        this.Zamowienie.OstatniaHistoriaZamowienia.Status.Typ == (int)TypStatusuZamowienia.NoweZamowienie)
                        this.Zamowienie.RodzajTransportu = Types.RodzajTransportu.NieWybrano;
                    if (save)
                        this.SaveChanges();
                }
            }
        }


    }
}
