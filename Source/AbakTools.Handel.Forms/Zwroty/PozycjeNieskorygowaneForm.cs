using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace AbakTools.Zwroty.Forms
{
    public partial class PozycjeNieskorygowaneForm : Form
    {
        public Zwrot Zwrot;

        public PozycjeNieskorygowaneForm()
        {
            InitializeComponent();
        }

        private void PozycjeNieskorygowaneForm_Load(object sender, EventArgs e)
        {
            if (Zwrot != null)
            {
                this.nrZrotuTextBox.Text = Zwrot.ID.ToString();
                this.kontrahentTextBox.Text = Zwrot.Kontrahent.Kod;

                this.bindingSource.DataSource = this.Zwrot.Pozycje.Where(p => p.Deleted == false
                    && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && (p.IloscSkorygowana == null || p.IloscSkorygowana < p.Ilosc)).ToList();

            }
        }

        private void generujZwrotButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy napewno chcesz wygenerować zwrot","AbakTools", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                this.Enabled = false;

                var dc = Enova.Business.Old.Core.ContextManager.WebContext;

                Zwrot nowyZwrot = new Zwrot()
                {
                    Guid = Guid.NewGuid(),
                    DataDodania = DateTime.Now,
                    DataModyfikacji = DateTime.Now,
                    Deleted = false,
                    IloscPaczek = 0,
                    SkorygowanyWCalosci = false,
                    Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                    Kontrahent = this.Zwrot.Kontrahent,
                    Opis = "Zwrot z pozycji nieskorygowanych ze zwrotu "+this.Zwrot.ID,
                    OstatniStatus = dc.StatusyZwrotow.Where(sz=>sz.Typ == (int)TypStatusuZwrotu.ZwrotZNieskorygowanych).FirstOrDefault()
                };

                nowyZwrot.HistoriaZwrotu.Add(new HistoriaZwrotu()
                {
                    Data = DateTime.Now,
                    Deleted = false,
                    Guid = Guid.NewGuid(),
                    Status = dc.StatusyZwrotow.Where(sz=>sz.Typ == (int)TypStatusuZwrotu.ZwrotZNieskorygowanych).FirstOrDefault(),
                    Synchronize = (int)RowSynchronizeOld.NotsynchronizedNew,
                    Uzytkownik = User.LoginedUser
                });

                dc.OptimisticSaveChanges();
                dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, nowyZwrot);

                var pozycje = this.Zwrot.Pozycje.Where(p => p.Deleted == false && p.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete 
                    && (p.IloscSkorygowana == null || p.IloscSkorygowana < p.Ilosc)).ToList();

                foreach (var poz in pozycje)
                {
                    var nowaPozycja = new PozycjaZwrotu()
                    {
                        Cena = poz.Cena,
                        Guid = Guid.NewGuid(),
                        Deleted = false,
                        Ilosc = poz.IloscPozostaloDoSkorygowania,
                        IloscOrg = poz.IloscPozostaloDoSkorygowania,
                        IloscSkorygowana = 0,
                        Opis = poz.Opis,
                        Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                        Towar = poz.Towar,
                        TowarNazwa = poz.TowarNazwa
                    };
                    nowyZwrot.Pozycje.Add(nowaPozycja);
                    if (poz.IloscOrg == null)
                        poz.IloscOrg = poz.Ilosc;
                    poz.Ilosc -= nowaPozycja.Ilosc;
                }

                this.Zwrot.SkorygowanyWCalosci = true;
                this.Zwrot.PrzeliczWartosc();
                nowyZwrot.ReIdentPozycje();
                nowyZwrot.PrzeliczWartosc();
                dc.OptimisticSaveChanges();
                dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, this.Zwrot);
                dc.Refresh(System.Data.Objects.RefreshMode.StoreWins, nowyZwrot);

                this.Enabled = true;
                this.Cursor = Cursors.Default;

                this.Close();
                MessageBox.Show("Wygenerowano nowy zwrot o numerze " + nowyZwrot.ID);
            }
        }
    }
}
