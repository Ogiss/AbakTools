using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Data.Entity;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace AbakTools.Towary.Forms
{
    public partial class TowarEditForm : Enova.Business.Old.Forms.DataEditTabForm
    {
        const string IMG_DIR = @"Z:\AbakSoft\EnovaTools\img\p\";

        private static string imgFilePath = null;

        private bool kategorieIsloaded = false;
        private bool zdjeciaIsLoaded = false;
        private bool atrybutyIsLoaded = false;
        private Dictionary<int, KategoriaTreeNode> kategorieNodes = new Dictionary<int, KategoriaTreeNode>();
        private Dictionary<int, AtrybutTreeNode> atrybutyNodes = new Dictionary<int, AtrybutTreeNode>();
        private List<int> kategorieAdded = new List<int>();
        private List<int> kategorieDeleted = new List<int>();
        private Dictionary<int, Enova.Business.Old.DB.Web.AtrybutProduktu> atrybutyDeleted = new Dictionary<int, Enova.Business.Old.DB.Web.AtrybutProduktu>();
        private List<Enova.Business.Old.DB.Web.Zdjecie> zdjeciaDeleted = new List<Enova.Business.Old.DB.Web.Zdjecie>();
        private Enova.Business.Old.DB.Web.Zdjecie zdjecieOkladki = null;
        private AutoCompleteStringCollection userStateAutocompleteSource;


        public Produkt Towar
        {
            get { return (Enova.Business.Old.DB.Web.Produkt)this.DataSource; }
        }

        public TowarEditForm()
        {
            InitializeComponent();
        }

        private void TowarEditForm_Load(object sender, EventArgs e)
        {
            if (DataSource != null)
            {
                if (Towar.EntityState != System.Data.EntityState.Added && Towar.EntityState != System.Data.EntityState.Detached)
                {
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this.DataSource);
                    Towar.Zdjecia.Load();
                    Enova.Business.Old.Core.ContextManager.WebContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, Towar.Zdjecia.Where(z => z.EntityState != System.Data.EntityState.Added));
                    Towar.AtrybutyProduktu.Load();

                    /*
                    if (Towar.Synchronizacja == (int)RowSynchronize.Synchronized)
                    {
                        towarEnovaTextBox.Enabled = false;
                        zmienEnovaTowarButton.Enabled = false;
                    }
                     */
                }

                if (Towar.EnovaGuid != null && Towar.EnovaGuid.Value != Guid.Empty)
                {
                    Enova.Business.Old.DB.Towar enovaTowar = Enova.Business.Old.Core.ContextManager.DataContext.Towary.Where(t => t.Guid == Towar.EnovaGuid).FirstOrDefault();
                    if (enovaTowar != null)
                    {
                        towarEnovaTextBox.Text = "(" + enovaTowar.Kod + ") " + enovaTowar.Nazwa;
                    }

                    this.Text = "Edycja towaru - (" + this.Towar.ID.ToString() + ") " + this.Towar.Kod + " - " + this.Towar.Nazwa;

                }

                zdjecieOkladki = Towar.Zdjecia.Where(z => z.Okladka == true && z.Synchronizacja != (byte)RowSynchronizeOld.NotsynchronizedDelete && z.Deleted == false).FirstOrDefault();
                loadZdjecieOkladki();

                if (Towar.OutOfStock == null)
                    Towar.OutOfStock = 1;

                if (Towar.OutOfStock == 0)
                    outOfStock0RadioButton.Checked = true;
                else
                    outOfStock1RadioButton.Checked = true;

                if (Towar.Dostawca == null)
                    dostawcaComboBox.SelectedItem = null;

                userStateAutocompleteSource = new AutoCompleteStringCollection();
                userStateAutocompleteSource.AddRange(
                WebContext.Produkty.Where(x => x.UserState != null && x.UserState != "")
                    .GroupBy(x => x.UserState)
                    .Select(x => x.Key)
                    .OrderBy(x => x)
                    .ToArray());

                statusTextBox.AutoCompleteCustomSource = userStateAutocompleteSource;
                statusTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                statusTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        private void loadZdjecieOkladki()
        {
            pictureBox.Image = null;
            if (zdjecieOkladki != null)
                pictureBox.Image = zdjecieOkladki.GetImage();
            /*
            if (zdjecieOkladki != null && File.Exists(IMG_DIR + Towar.ID.ToString() + "-" + zdjecieOkladki.ID.ToString() + ".jpg"))
            {
                Image img = null;
                using (FileStream fs = new FileStream(IMG_DIR + Towar.ID.ToString() + "-" + zdjecieOkladki.ID.ToString() + ".jpg", FileMode.Open, FileAccess.Read))
                    img = Image.FromStream(fs);
                pictureBox.Image = (Image)img.Clone();
                img.Dispose();
            }
            pictureBox.Refresh();
             */

        }

        protected override void OnBeforeBinding(Enova.Business.Old.Forms.BindingEventArgs e)
        {
            base.OnBeforeBinding(e);

            stawkiVatBindingSource.DataSource = Enova.Business.Old.Core.ContextManager.WebContext.StawkiVat.OrderBy(s => s.Nazwa).ToList();
            opakowaniaBindingSource.DataSource = Enova.Business.Old.Core.ContextManager.WebContext.JednostkiMiary.OrderBy(j => j.Nazwa).ToList();
            dostawcyBindingSource.DataSource = Enova.Business.Old.Core.ContextManager.WebContext.Dostawcy.OrderBy(d => d.Nazwa).ToList();
            if (Towar != null)
            {
                if (Towar.EntityState == System.Data.EntityState.Added || Towar.EntityState == System.Data.EntityState.Detached)
                {

                    if (Towar.GUID == Guid.Empty || Towar.EnovaGuid == null || Towar.EnovaGuid.Value == Guid.Empty)
                    {
                        Towar.GUID = Guid.NewGuid();
                        Towar.AktywnyOld = true;
                        Towar.Widoczny = true;
                        Towar.Gotowy = false;

                        var jm = Enova.Business.Old.Core.ContextManager.WebContext.JednostkiMiary
                            .Where(j => j.Domyslna == true).FirstOrDefault();
                        Towar.JednostkaMiary = jm;
                        Towar.DataDodania = DateTime.Now;
                        Towar.DataAktualizacji = Towar.DataDodania;
                        Towar.Stamp = Towar.DataDodania;
                        Towar.PSID = 0;
                        Towar.Synchronizacja = (byte)RowSynchronizeOld.Notsaved;
                        Towar.Indexed = true;
                        Towar.LangID = 3;
                        Towar.Opis = string.Empty;
                        Towar.KrotkiOpis = string.Empty;
                        Towar.MetaOpis = string.Empty;
                        Towar.MetaTytul = string.Empty;
                        Towar.Podprodukt = false;
                        Towar.ProduktGrupujacy = false;
                        Towar.WlascicielID = 0;
                        Towar.Stan = 0;
                        Towar.Ilosc = 1;
                        Towar.OutOfStock = 1;
                    }
                }
            }
        }

        private void loadKategorie()
        {
            fireKategorieAfterCheck = false;
            kategorieNodes.Clear();
            kategorieTreeView.Nodes.Clear();

            var kategorie = Enova.Business.Old.Core.ContextManager.WebContext.KategorieOld.Where(k => k.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).OrderBy(k => k.PoziomGlebokosci).ThenBy(k => k.KolejnoscWyswietlania).ToList();
            KategoriaTreeNode root = null;
            
            foreach (var kategoria in kategorie)
            {
                if (kategoria.PoziomGlebokosci == 0)
                {
                    root = new KategoriaTreeNode(kategoria);
                    kategorieTreeView.Nodes.Add(root);
                    kategorieNodes.Add(kategoria.ID, root);
                }
                else
                {
                    if (kategoria.Wlasciciel != null && kategorieNodes.ContainsKey(kategoria.Wlasciciel.ID))
                    {
                        var parent = kategorieNodes[kategoria.Wlasciciel.ID];
                        var node = new KategoriaTreeNode(kategoria);
                        parent.Nodes.Add(node);
                        kategorieNodes.Add(kategoria.ID, node);
                    }
                }
            }

            root.Expand();

            foreach (var kat in Towar.KategorieProduktu.Where(k => k.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && k.Deleted == false).Select(k => k.KategoriaOld).ToList())
            {
                if (kat != null && kategorieNodes.ContainsKey(kat.ID))
                {
                    var node = kategorieNodes[kat.ID];
                    node.Checked = true;
                    if (node.Parent != null && !node.Parent.IsExpanded)
                        node.Parent.Expand();
                }
            }

            fireKategorieAfterCheck = true;
        }

        private void loadZdjecia()
        {
            foreach (var z in this.Towar.Zdjecia.Where(z=>z.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && z.Deleted == false).OrderBy(z=>z.ID).ThenBy(z=>z.Legenda).ToList())
            {
                //string imgPath = IMG_DIR + this.Towar.ID.ToString() + "-" + z.ID.ToString() + ".jpg";
                //zdjeciaImageViewer.Add(imgPath, z);
                var img = z.GetImage();
                if (img != null)
                    zdjeciaImageViewer.Add(img, z);
            }
        }

        private void loadAtrybuty()
        {
            fireAtrybutyAfterCheck = false;
            atrybutyTreeView.Nodes.Clear();
            atrybutyNodes.Clear();
            var grupy = Enova.Business.Old.Core.ContextManager.WebContext.GrupyAtrybutow.Include("Atrybuty").Where(g => g.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && g.is_deleted == false)
                .OrderBy(g => g.Nazwa).ToList();
            foreach (var grupa in grupy)
            {
                AtrybutTreeNode gnode = new AtrybutTreeNode(grupa);
                atrybutyTreeView.Nodes.Add(gnode);
                foreach (var atrybut in grupa.Atrybuty.Where(a=>a.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && a.is_deleted == false).OrderBy(a=>a.Kolejnosc).ThenBy(a => a.Nazwa))
                {
                    AtrybutTreeNode anode = new AtrybutTreeNode(atrybut);
                    atrybutyNodes.Add(atrybut.ID, anode);
                    gnode.Nodes.Add(anode);
                }
            }

            foreach (var atrybutProduktu in Towar.AtrybutyProduktu.ToList())
            {
                if (this.Towar.DbContext != null && this.Towar.ID > 0)
                    this.Towar.DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, atrybutProduktu);
                Enova.Business.Old.DB.Web.Atrybut atrybut = atrybutProduktu.Atrybut;

                if (atrybut != null)
                {
                    var anode = atrybutyNodes[atrybut.ID];
                    var adnode = new AtrybutDetailTreeNode(atrybutProduktu);
                    adnode.Checked = atrybutProduktu.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && atrybutProduktu.Deleted == false;
                    anode.Nodes.Add(adnode);
                    if (atrybutProduktu.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && atrybutProduktu.Deleted == false)
                    {
                        anode.Checked = true;
                        if (!anode.Parent.IsExpanded)
                            anode.Parent.Expand();
                    }
                }
            }
            fireAtrybutyAfterCheck = true;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex == 0)
            {
                pictureBox.Refresh();
            }
            else if (TabControl.SelectedIndex == 1 && !kategorieIsloaded)
            {
                loadKategorie();
                kategorieIsloaded = true;
            }
            else if (TabControl.SelectedIndex == 2 && !zdjeciaIsLoaded)
            {
                loadZdjecia();
                zdjeciaIsLoaded = true;
            }
            else if (TabControl.SelectedIndex == 3 && !atrybutyIsLoaded)
            {
                loadAtrybuty();
                atrybutyIsLoaded = true;
            }
        }

        private void zmienEnovaTowarButton_Click(object sender, EventArgs e)
        {
            if (Towar.EnovaGuid != null)
            {
                DialogResult result = MessageBox.Show("Czy napewno chcesz zmienić powiązanie z towarem Enova?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.No)
                    return;
            }

            //AbakTools.Towary.Forms.TowaryForm form = new Forms.TowaryForm();
            Enova.Forms.Towary.WyborTowaruForm form = new Enova.Forms.Towary.WyborTowaruForm();
            ((Enova.Business.Old.Controls.ISelectForm)form).SelectMode = true;
            form.ShowDialog();
            if (((Enova.Business.Old.Controls.ISelectForm)form).SelectedItem != null)
            {
                Enova.Business.Old.DB.TowarRow enovaTowarRow = (Enova.Business.Old.DB.TowarRow)((Enova.Business.Old.Controls.ISelectForm)form).SelectedItem;
                Enova.Business.Old.DB.Towar enovaTowar = Enova.Business.Old.Core.ContextManager.DataContext.Towary.Where(t => t.ID == enovaTowarRow.ID).FirstOrDefault();

                if (Towar.EnovaGuid != null)
                {
                    towarEnovaTextBox.Text = "(" + enovaTowar.Kod + ") " + enovaTowar.Nazwa;
                    Towar.Kod = enovaTowar.Kod;
                    Towar.EnovaGuid = enovaTowar.Guid;
                    Towar.EnovaStamp = 0;
                }
                else
                {
                    towarEnovaTextBox.Text = "(" + enovaTowar.Kod + ") " + enovaTowar.Nazwa;
                    Towar.Kod = enovaTowar.Kod;
                    Towar.EnovaGuid = enovaTowar.Guid;
                    Towar.Nazwa = enovaTowar.Nazwa;
                    Towar.Cena = (decimal)enovaTowar.CenaHurtowaNetto;
                    Towar.EnovaStamp = enovaTowar.Stamp;

                    var stawkaVat = Enova.Business.Old.Core.ContextManager.WebContext.StawkiVat
                        .Where(sv => sv.GUID == enovaTowar.DefinicjaStawki.Guid).FirstOrDefault();
                    Towar.StawkaVat = stawkaVat;
                    //Towar.LinkRewrite = Enova.Business.Old.Core.Tools.LinkRewrite(Towar.Nazwa);

                    string dostawcaStr = enovaTowar.GetDostawca();
                    if (!string.IsNullOrEmpty(dostawcaStr))
                    {
                        Enova.Business.Old.DB.Dictionary dict = Enova.Business.Old.Core.ContextManager.DataContext.DictionarySet
                            .Where(d => d.Category == "F.DOSTAWCY" && d.Value == dostawcaStr).FirstOrDefault();
                        if (dict != null)
                        {
                            Enova.Business.Old.DB.Web.Dostawca dostawca = Enova.Business.Old.Core.ContextManager.WebContext.Dostawcy.Where(d => d.GUID == dict.Guid).FirstOrDefault();
                            if (dostawca != null)
                                Towar.Dostawca = dostawca;
                        }
                    }

                    var grupy = Enova.Business.Old.DB.FeatureDef.GrupyRabatowe.ToList();
                    foreach (var grupa in grupy)
                    {
                        var features = enovaTowar.Features.Where(f => f.Name == grupa.Name).ToList();
                        foreach (var feature in features)
                        {
                            var disc = grupa.DictionarySet.Where(d => d.Value == feature.Data).FirstOrDefault();
                            if (disc != null)
                            {
                                var grupaRabatowa = Enova.Business.Old.Core.ContextManager.WebContext.GrupyRabatowe
                                    .Where(gr => gr.GUID == disc.Guid).FirstOrDefault();
                                if (grupaRabatowa != null)
                                {
                                    Towar.TowarGrupyRabatowe.Add(new TowarGrupaRabatowa()
                                    {
                                        GrupaRabatowa = grupaRabatowa
                                    });
                                }
                            }
                        }
                    }
                }
                
            }
        }


        public class KategoriaTreeNode : TreeNode
        {
            private Enova.Business.Old.DB.Web.KategoriaOld kategoria = null;

            public Enova.Business.Old.DB.Web.KategoriaOld Kategoria
            {
                get { return this.kategoria; }
            }

            public KategoriaTreeNode(Enova.Business.Old.DB.Web.KategoriaOld kategoria)
                : base(kategoria.Nazwa)
            {
                this.kategoria = kategoria;
            }
        }


        private void zdjeciaImageViewer_SelectedChanged(object sender, EventArgs e)
        {
            fireZdjecieOkladkiChanged = false;
            zdjeciaImageViewer.Focus();
            if (zdjeciaImageViewer.SelectedImageInfo != null)
            {
                selectedImgBindingSource.DataSource = zdjeciaImageViewer.SelectedImageInfo;
            }
            else
            {
                selectedImgBindingSource.Clear();
            }
            fireZdjecieOkladkiChanged = true;
        }

        private void atrybutyTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            atrybutImgBindingSource.Clear();
            if (e.Node is AtrybutTreeNode)
            {
                adminAttrGroupBox.Visible = false;

                AtrybutTreeNode node = (AtrybutTreeNode)e.Node;
                atrybutZdjeciePictureBox.Image = null;
                atrybutBindingSource.Clear();
                if (!node.IsGrupa)
                {
                    var atrybutProduktu = this.Towar.AtrybutyProduktu.Where(ap => ap.Atrybut != null && ap.Atrybut.ID == node.Atrybut.ID && ap.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && ap.Deleted == false).FirstOrDefault();
                    if (atrybutProduktu != null)
                    {
                        var zdjecie = atrybutProduktu.ProduktyAtrybutyZdjecia
                            .Where(apz => apz.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && apz.Zdjecie.Deleted == false && apz.Zdjecie.Synchronizacja != (byte)RowSynchronizeOld.NotsynchronizedDelete).ToList()
                            .Where(apz => apz.IsDeleted == false).Select(apz => apz.Zdjecie).FirstOrDefault();
                        atrybutBindingSource.DataSource = atrybutProduktu;
                        if (zdjecie != null && zdjecie.IsDeleted == false)
                        {
                            atrybutZdjeciePictureBox.Image = zdjecie.GetImage();
                            atrybutImgBindingSource.DataSource = zdjecie;
                        }
                    }
                }
            }
            else if (e.Node is AtrybutDetailTreeNode)
            {

                AtrybutDetailTreeNode node = (AtrybutDetailTreeNode)e.Node;


                atrybutZdjeciePictureBox.Image = null;
                atrybutBindingSource.Clear();

                var atrybutProduktu = node.AtrybutProduktu;
                var zdjecie = atrybutProduktu.ProduktyAtrybutyZdjecia
                    .Where(apz => apz.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && apz.Zdjecie.Deleted == false && apz.Zdjecie.Synchronizacja != (byte)RowSynchronizeOld.NotsynchronizedDelete).ToList()
                    .Where(apz => apz.IsDeleted == false).Select(apz => apz.Zdjecie).FirstOrDefault();
                atrybutBindingSource.DataSource = atrybutProduktu;
                node.Text = atrybutProduktu.ID.ToString() + " Synchronize: " + atrybutProduktu.Synchronizacja + " Delete: " + atrybutProduktu.Deleted;


                adminAttrGroupBox.Visible = true;


                if (zdjecie != null)
                {
                    atrybutZdjeciePictureBox.Image = zdjecie.GetImage();
                    atrybutImgBindingSource.DataSource = zdjecie;
                }
            }
            else if(e.Node is ZdjecieTreeNode)
            {
                var zdjecie = ((ZdjecieTreeNode)e.Node).Zdjecie;

                if (zdjecie != null)
                {
                    atrybutZdjeciePictureBox.Image = zdjecie.GetImage();
                    atrybutImgBindingSource.DataSource = zdjecie;
                }
            }
        }

        private void zdjeciaImageViewer_DoubleClick(object sender, EventArgs e)
        {

        }

        private void opakowanieComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Towar.JednostkaMiary = (Enova.Business.Old.DB.Web.JednostkaMiary)opakowanieComboBox.SelectedItem;
            cenaNettoOpkTextBox.DataBindings["Text"].ReadValue();
        }

        bool fireKategorieAfterCheck = true;
        private void kategorieTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (fireKategorieAfterCheck)
            {
                KategoriaTreeNode node = (KategoriaTreeNode)e.Node;
                if (node.Checked)
                {
                    if (kategorieDeleted.Exists(i => i == node.Kategoria.ID))
                    {
                        kategorieDeleted.Remove(node.Kategoria.ID);
                    }
                    kategorieAdded.Add(node.Kategoria.ID);
                }
                else
                {
                    if (kategorieAdded.Exists(i => i == node.Kategoria.ID))
                    {
                        kategorieAdded.Remove(node.Kategoria.ID);
                    }
                    kategorieDeleted.Add(node.Kategoria.ID);
                }
            }
        }

        private bool fireAtrybutyAfterCheck = true;
        private void atrybutyTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (fireAtrybutyAfterCheck)
            {
                fireAtrybutyAfterCheck = false;
                if (e.Node is AtrybutTreeNode)
                {
                    AtrybutTreeNode node = (AtrybutTreeNode)e.Node;
                    if (!node.IsGrupa)
                    {
                        AtrybutProduktu atrybutProduktu = Towar.AtrybutyProduktu.Where(ap => ap.KombinacjeAtrybutu.Any(a => a.AtrybutID == node.Atrybut.ID) &&
                            ap.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && ap.Deleted == false).FirstOrDefault();


                        if (node.Checked)
                        {
                            if (atrybutProduktu == null)
                            {
                                atrybutProduktu = new AtrybutProduktu()
                                {
                                    Cena = 0,
                                    CenaHurtowa = 0,
                                    DefaultOn = false,
                                    Deleted = false,
                                    EAN13 = string.Empty,
                                    EcoTax = 0,
                                    Gotowy = false,
                                    GUID = Guid.NewGuid(),
                                    Ilosc = 0,
                                    Kod = string.Empty,
                                    KodDostawcy = string.Empty,
                                    PSID = 0,
                                    PSProduktID = 0,
                                    Lokalizacja = string.Empty,
                                    Waga = 0,
                                    Stan = 0,
                                    Stamp = DateTime.Now,
                                    Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                                    Dostepny = true,
                                    Active = true
                                };

                                Towar.AtrybutyProduktu.Add(atrybutProduktu);

                                atrybutProduktu.KombinacjeAtrybutu.Add(new KombinacjaAtrybutu()
                                {
                                    Atrybut = node.Atrybut,
                                    Gotowy = false,
                                    PSAtrybutID = 0,
                                    PSAtrybutProduktuID = 0,
                                    Stamp = DateTime.Now,
                                    Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew
                                });
                            }
                            else
                            {
                                if (atrybutyDeleted.ContainsKey(atrybutProduktu.ID))
                                    atrybutyDeleted.Remove(atrybutProduktu.ID);
                            }
                            if (atrybutyTreeView.SelectedNode == node)
                                atrybutBindingSource.DataSource = atrybutProduktu;
                        }
                        else
                        {
                            if (atrybutProduktu.EntityState == System.Data.EntityState.Added)
                            {
                                foreach (var z in atrybutProduktu.ProduktyAtrybutyZdjecia.ToList())
                                    Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(z);
                                foreach (var k in atrybutProduktu.KombinacjeAtrybutu.ToList())
                                    Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(k);
                                Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(atrybutProduktu);
                            }
                            else
                            {
                                atrybutyDeleted.Add(atrybutProduktu.ID, atrybutProduktu);
                            }
                            if (atrybutyTreeView.SelectedNode == node)
                            {
                                atrybutBindingSource.Clear();
                                atrybutZdjeciePictureBox.Image = null;
                            }
                        }
                    }
                }
                else if (e.Node is AtrybutDetailTreeNode)
                {
                    AtrybutDetailTreeNode node = (AtrybutDetailTreeNode)e.Node;
                    

                    if (node.Checked)
                    {
                        node.AtrybutProduktu.Deleted = false;
                        if (node.AtrybutProduktu.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedNew)
                            node.AtrybutProduktu.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                        node.Parent.Checked = true;
                    }
                    else
                    {
                        node.AtrybutProduktu.Deleted = true;
                        node.AtrybutProduktu.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                        node.Parent.Checked = false;
                        
                    }
                    node.SetText();

                }
                else if (e.Node is ZdjecieAtrybutuTreeNode)
                {
                    ZdjecieAtrybutuTreeNode node = (ZdjecieAtrybutuTreeNode)e.Node;
                    if (node.Checked)
                    {
                        node.ProduktAtrybutZdjecie.deleted = false;
                        node.ProduktAtrybutZdjecie.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                        node.ProduktAtrybutZdjecie.Zdjecie.Deleted = false;
                        node.ProduktAtrybutZdjecie.Zdjecie.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                    }
                    else
                    {
                        node.ProduktAtrybutZdjecie.deleted = true;
                        node.ProduktAtrybutZdjecie.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                        node.ProduktAtrybutZdjecie.Zdjecie.Deleted = true;
                        node.ProduktAtrybutZdjecie.Zdjecie.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                    }
                    node.SetText();

                }
                else if (e.Node is ZdjecieTreeNode)
                {
                    var zdjecie = ((ZdjecieTreeNode)e.Node).Zdjecie;
                    if (e.Node.Checked)
                    {
                        zdjecie.Deleted = false;
                        zdjecie.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                    }
                    else
                    {
                        zdjecie.Deleted = true;
                        zdjecie.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                    }
                    ((ZdjecieTreeNode)e.Node).SetText();
                }
                fireAtrybutyAfterCheck = true;
            }
        }

        protected override void OnBeforeSaveChanges(EventArgs e)
        {
            base.OnBeforeSaveChanges(e);
            foreach (var kid in kategorieAdded)
            {
                var rcp = Towar.KategorieProduktu.Where(kp => kp.KategoriaID == kid && kp.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && kp.Deleted == false).FirstOrDefault();
                if (rcp == null)
                {
                    Enova.Business.Old.DB.Web.KategoriaOld kategoria = Enova.Business.Old.Core.ContextManager.WebContext.KategorieOld.Where(k => k.ID == kid).FirstOrDefault();
                    if (kategoria != null)
                    {
                        Towar.KategorieProduktu.Add(new KategoriaProdukt()
                        {
                            Gotowy = false,
                            KategoriaOld = kategoria,
                            Pozycja = 0,
                            PSKategoriaID = 0,
                            PSProduktID = 0,
                            Stamp = DateTime.Now,
                            Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                            Deleted = false
                        });
                    }
                }
                else if (rcp.Deleted == true)
                {
                    rcp.Deleted = false;
                    rcp.Stamp = DateTime.Now;
                    if (rcp.Synchronizacja == (int)RowSynchronizeOld.Synchronized)
                        rcp.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
                }
                
            }

            foreach (var kid in kategorieDeleted)
            {
                KategoriaProdukt kp = Towar.KategorieProduktu.Where(k => k.KategoriaID == kid 
                    && k.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && k.Deleted == false).FirstOrDefault();
                if (kp != null)
                {
                    kp.Stamp = DateTime.Now;
                    kp.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                }
            }

            foreach (KeyValuePair<int, AtrybutProduktu> kvp in atrybutyDeleted)
            {
                //if (kvp.Value.Synchronizacja == (int)RowSynchronize.Synchronized)
                //{
                    kvp.Value.Stamp = DateTime.Now;
                    kvp.Value.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                //}
                //else
                //{
                //    foreach (var z in kvp.Value.ProduktyAtrybutyZdjecia.ToList())
                //        Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(z);
                //    foreach (var k in kvp.Value.KombinacjeAtrybutu.ToList())
                //        Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(k);
               //     Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(kvp.Value);
               // }
            }
            atrybutyDeleted.Clear();

            foreach (var ap in Towar.AtrybutyProduktu.ToList())
            {
                foreach (var paz in ap.ProduktyAtrybutyZdjecia.ToList())
                {
                    if (paz.IsDeleted)
                    {
                        //if (paz.Synchronizacja == (int)RowSynchronize.Synchronized)
                        //{
                            paz.Stamp = DateTime.Now;
                            paz.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                            ap.Stamp = paz.Stamp;
                            if (ap.Synchronizacja == (int)RowSynchronizeOld.Synchronized)
                                ap.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedEdit;
                       // }
                        //else
                        //{
                       //     Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(paz);
                       // }
                    }
                }
            }

            foreach (var zdjecie in zdjeciaDeleted)
            {
               // if (zdjecie.Synchronizacja == (int)RowSynchronize.Synchronized)
                //{
                    zdjecie.Stamp = DateTime.Now;
                    zdjecie.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedDelete;
                //}
                //else
               // {
                //    if (File.Exists(IMG_DIR + Towar.ID.ToString() + "-" + zdjecie.ID.ToString() + ".jpg"))
               //         File.Delete(IMG_DIR + Towar.ID.ToString() + "-" + zdjecie.ID.ToString() + ".jpg");
               //     foreach (var a in zdjecie.ProduktyAtrybutyZdjecia.ToList())
               //         Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(a);
                //    Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(zdjecie);
               // }
            }

            zdjeciaDeleted.Clear();

            int outOfstock = outOfStock0RadioButton.Checked ? 0 : 1;
            if (Towar.OutOfStock != outOfstock)
                Towar.OutOfStock = outOfstock;

        }

        private void okButton_Click(object sender, EventArgs e)
        {

            
        }

        private void zdjecieDodajButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(imgFilePath))
                openFileDialog.InitialDirectory = imgFilePath;
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                imgFilePath = Path.GetDirectoryName(openFileDialog.FileName);
                string tmpDir = Directory.GetCurrentDirectory() + "\\tmp\\";
                string tmpFilename = Guid.NewGuid().ToString() + ".jpg";
                Bitmap btm = (Bitmap)Bitmap.FromFile(openFileDialog.FileName);
                Bitmap dstbtm = Enova.Business.Old.Core.Tools.RenderImage(btm, 600, 600);
                
                /*
                using (FileStream fs = new FileStream(tmpDir + tmpFilename, FileMode.CreateNew))
                    dstbtm.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                //dstbtm.Save(tmpDir + tmpFilename);
                 */

                Enova.Business.Old.DB.Web.Zdjecie zdjecie = new Enova.Business.Old.DB.Web.Zdjecie()
                {
                    Produkt = Towar,
                    Okladka = false,
                    Gotowy = false,
                    GUID = Guid.NewGuid(),
                    LangID = 3,
                    Legenda = this.Towar.Nazwa,
                    Pozycja = 0,
                    PSID = 0,
                    PSProduktID = 0,
                    Stamp = DateTime.Now,
                    Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew,
                    FileStamp = 1
                };

                using (MemoryStream ms = new MemoryStream())
                {
                    dstbtm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    zdjecie.ImageBytes = ms.ToArray();
                }

                
                btm.Dispose();
                dstbtm.Dispose();
                btm = null;
                dstbtm = null;
/*
                Enova.Business.Old.DB.Web.Zdjecie zdjecie = new Enova.Business.Old.DB.Web.Zdjecie()
                {
                    Produkt = Towar,
                    FileName = tmpDir+tmpFilename,
                    Okladka = false,
                    Gotowy = false,
                    GUID = Guid.NewGuid(),
                    LangID = 3,
                    Legenda = this.Towar.Nazwa,
                    Pozycja = 0,
                    PSID = 0,
                    PSProduktID = 0,
                    Stamp = DateTime.Now,
                    Synchronizacja = (int)RowSynchronize.NotsynchronizedNew,
                    FileStamp = 1
                };

 */              if (Towar.Zdjecia.Count(r=>r.Synchronizacja!=(byte)RowSynchronizeOld.NotsynchronizedDelete && r.Deleted == false ) == 1)
                    zdjecie.Okladka = true;

                //zdjeciaImageViewer.Add(tmpDir + tmpFilename, zdjecie);
                 zdjeciaImageViewer.Add(zdjecie.GetImage(), zdjecie);

            }
        }

        private void zdjecieUsunButton_Click(object sender, EventArgs e)
        {
            Enova.Business.Old.Controls.ImageBox box = zdjeciaImageViewer.SelectedImgBox;
            if (box != null)
            {
                DialogResult result = MessageBox.Show("Czy napewno chcesz usunąć zdjęcie?", "EnovaTools", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    Enova.Business.Old.DB.Web.Zdjecie zdjecie = (Enova.Business.Old.DB.Web.Zdjecie)box.ImageInfo;
                    if (zdjecie.EntityState == System.Data.EntityState.Added)
                    {
                        if (File.Exists(zdjecie.FileName))
                            File.Delete(zdjecie.FileName);
                        foreach (var a in zdjecie.ProduktyAtrybutyZdjecia.ToList())
                            Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(a);
                        Enova.Business.Old.Core.ContextManager.WebContext.DeleteObject(zdjecie);
                    }
                    else
                    {
                        zdjeciaDeleted.Add(zdjecie);
                    }
                    zdjeciaImageViewer.Remove(box);
                }
            }
        }


        private void atrybutZdjeciePictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (atrybutBindingSource.DataSource != null && atrybutBindingSource.DataSource.GetType() == typeof(Enova.Business.Old.DB.Web.AtrybutProduktu))
            {
                AbakTools.Towary.Forms.WyborZdjeciaForm form = new WyborZdjeciaForm()
                {
                    Towar = this.Towar,
                    ZdjeciaDeleted = zdjeciaDeleted.Select(z=>z.GUID).ToList()
                };

                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Enova.Business.Old.DB.Web.AtrybutProduktu atrybutProduktu = (Enova.Business.Old.DB.Web.AtrybutProduktu)atrybutBindingSource.DataSource;
                    var pimg = form.SelectedImage;

                    foreach (var aimg in atrybutProduktu.ProduktyAtrybutyZdjecia.Where(paz => paz.deleted == false && paz.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete).ToList())
                    {
                        aimg.IsDeleted = true;
                    }

                    var pai = atrybutProduktu.ProduktyAtrybutyZdjecia.Where(ai => ai.ZdjecieID == pimg.ID).FirstOrDefault();

                    if (pai == null)
                    {
                        pai = new Enova.Business.Old.DB.Web.ProduktAtrybutZdjecie()
                        {
                            AtrybutProduktu = atrybutProduktu,
                            Zdjecie = pimg,
                            Gotowy = false,
                            PSAtrybutProduktuID = 0,
                            PSZdjecieID = 0,
                            Stamp = DateTime.Now,
                            Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew
                        };
                    }
                    else
                    {
                        pai.IsDeleted = false;
                        pai.deleted = false;
                        pai.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
                    }


                    /*
                    ProduktAtrybutZdjecie produktAtrybutZdjecie = atrybutProduktu.ProduktyAtrybutyZdjecia.FirstOrDefault();

                    if (produktAtrybutZdjecie != null)
                        produktAtrybutZdjecie.IsDeleted = true;

                    
                    
                    
                    var produktAtrybutZdjecie = new ProduktAtrybutZdjecie()
                    {
                        AtrybutProduktu = atrybutProduktu,
                        Gotowy = false,
                        PSAtrybutProduktuID = 0,
                        PSZdjecieID = 0,
                        Stamp = DateTime.Now,
                        Synchronizacja = (int)RowSynchronize.NotsynchronizedNew
                    };

                    produktAtrybutZdjecie.Zdjecie = form.SelectedImage;
                     */

                    atrybutProduktu.Stamp = DateTime.Now;
                    /*
                    string imgPath = null;
                    if (string.IsNullOrEmpty(form.SelectedImage.FileName))
                        imgPath = IMG_DIR + Towar.ID.ToString() + "-" + form.SelectedImage.ID.ToString() + ".jpg";
                    else
                        imgPath = form.SelectedImage.FileName;
                    Image img = null;
                    if (File.Exists(imgPath))
                        using (FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                            img = Image.FromStream(fs);
                    atrybutZdjeciePictureBox.Image = img;
                     */
                    atrybutZdjeciePictureBox.Image = form.SelectedImage.GetImage();
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void synchImportButton_Click(object sender, EventArgs e)
        {
            /*
            if (this.SaveChanges())
            {
                Net.EClient.Client.SendMessage(new Net.Types.NetMessageRunSynchronize(new Net.Types.RunSynchronizeInfo()
                {
                    TableName = "towarimport",
                    GUID = Towar.GUID
                }));

            }
             */
        }

        private void nazwaTextBox_Leave(object sender, EventArgs e)
        {
            //Towar.LinkRewrite = Enova.Business.Old.Core.Tools.LinkRewrite(nazwaTextBox.Text);
        }

        bool fireZdjecieOkladkiChanged = true;
        private void zdjecieOkladkaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fireZdjecieOkladkiChanged)
            {
                Enova.Business.Old.DB.Web.Zdjecie zdjecie = (Enova.Business.Old.DB.Web.Zdjecie)zdjeciaImageViewer.SelectedImageInfo;
                if (zdjecieOkladkaCheckBox.Checked)
                {
                    fireZdjecieOkladkiChanged = false;
                    if (zdjecie != null)
                    {
                        foreach (var z in Towar.Zdjecia)
                        {
                            if (z.GUID != zdjecie.GUID && z.Okladka != null && z.Okladka.Value)
                                z.Okladka = false;
                        }

                    }
                    zdjecieOkladki = zdjecie;
                    fireZdjecieOkladkiChanged = true;
                }
                else
                {
                    zdjecieOkladki = null;
                }
                loadZdjecieOkladki();
            }
        }

        private void TowarEditForm_BeforeSaveChanges(object sender, EventArgs e)
        {

        }

        private void krotkiOpisTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Shift && e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                    krotkiOpisTextBox.SelectAll();
                    break;
                }
            }
        }

        #region Nested Types

        public class AtrybutTreeNode : TreeNode
        {
            private Enova.Business.Old.DB.Web.GrupaAtrybutow grupa = null;
            private Enova.Business.Old.DB.Web.Atrybut atrybut = null;

            public Enova.Business.Old.DB.Web.GrupaAtrybutow GrupaAtrybutow
            {
                get { return this.grupa; }
            }

            public Enova.Business.Old.DB.Web.Atrybut Atrybut
            {
                get { return this.atrybut; }
            }

            public bool IsGrupa
            {
                get { return this.atrybut == null; }
            }

            public AtrybutTreeNode(Enova.Business.Old.DB.Web.GrupaAtrybutow grupa)
                : base(grupa.Nazwa)
            {
                this.grupa = grupa;
            }

            public AtrybutTreeNode(Enova.Business.Old.DB.Web.Atrybut atrybut)
                : base(atrybut.Nazwa)
            {
                this.grupa = atrybut.GrupaAtrybutow;
                this.atrybut = atrybut;
            }
        }

        public class AtrybutDetailTreeNode : TreeNode
        {
            private Enova.Business.Old.DB.Web.AtrybutProduktu atrybutProduktu;

            public Enova.Business.Old.DB.Web.AtrybutProduktu AtrybutProduktu
            {
                get { return this.atrybutProduktu; }
            }

            public AtrybutDetailTreeNode(AtrybutProduktu atrybutProduktu)
            {
                this.atrybutProduktu = atrybutProduktu;
                this.addImages();
                SetText();
            }

            public void SetText()
            {
                this.Text = atrybutProduktu.ID.ToString() + " Synchronize: " + atrybutProduktu.Synchronizacja + " Deleted: " + atrybutProduktu.Deleted;
            }

            private void addImages()
            {
                foreach (var pai in this.atrybutProduktu.ProduktyAtrybutyZdjecia)
                {
                    this.Nodes.Add(new ZdjecieAtrybutuTreeNode(pai));
                }
            }

        }

        public class ZdjecieAtrybutuTreeNode : TreeNode
        {
            private Enova.Business.Old.DB.Web.ProduktAtrybutZdjecie paz;

            public Enova.Business.Old.DB.Web.ProduktAtrybutZdjecie ProduktAtrybutZdjecie
            {
                get { return paz; }
            }

            public ZdjecieAtrybutuTreeNode(Enova.Business.Old.DB.Web.ProduktAtrybutZdjecie paz)
            {
                this.paz = paz;

                this.Text = "Image ID: " + paz.ZdjecieID + " PAI Deleted: " + paz.deleted + " PAI Synchr: " + paz.Synchronizacja;

                this.Checked = paz.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && paz.deleted == false
                    && paz.Zdjecie != null && paz.Zdjecie.Deleted == false && paz.Zdjecie.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete;
                if (paz.Zdjecie != null)
                    this.Nodes.Add(new ZdjecieTreeNode(paz.Zdjecie));

            }

            public void SetText()
            {
                this.Text = "Image ID: " + paz.ZdjecieID + " PAI Deleted: " + paz.deleted + " PAI Synchr: " + paz.Synchronizacja;
                if (this.Nodes.Count > 0)
                    this.Nodes[0].Text = "Image Deleted: " + paz.Zdjecie.Deleted + " Synchr: " + paz.Zdjecie.Synchronizacja;
            }

        }

        public class ZdjecieTreeNode : TreeNode
        {
            private Enova.Business.Old.DB.Web.Zdjecie zdjecie;

            public Enova.Business.Old.DB.Web.Zdjecie Zdjecie
            {
                get { return zdjecie; }
            }

            public ZdjecieTreeNode(Enova.Business.Old.DB.Web.Zdjecie zdjecie)
            {
                this.zdjecie = zdjecie;
                this.SetText();
            }

            public void SetText()
            {
                this.Text = "Image Deleted: " + zdjecie.Deleted + " Synchr: " + zdjecie.Synchronizacja;
            }
        }

        #endregion

        private void opisTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Shift && e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        opisTextBox.SelectAll();
                        break;
                }
            }
        }

        private void copyTowarGuidButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Towar.GUID.ToString());
        }

        private void setFormText()
        {
            this.Text = "Edycja towaru - (" + this.Towar.ID.ToString() + ") " + this.kodTextBox.Text + " - " + this.nazwaTextBox.Text;
        }

        private void kodTextBox_TextChanged(object sender, EventArgs e)
        {
            setFormText();
        }

        private void nazwaTextBox_TextChanged(object sender, EventArgs e)
        {
            setFormText();
        }

        private void blokadaCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
