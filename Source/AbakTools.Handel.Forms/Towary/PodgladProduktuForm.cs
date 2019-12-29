using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;
using System.IO;

namespace AbakTools.Towary.Forms
{
    public partial class PodgladProduktuForm : Form
    {
        ProduktAtrybut product = null;
        TowarAtrybut towarAtrybut = null;

        public ProduktAtrybut Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
                if (product != null)
                {
                    setProduct();
                }
            }
        }

        public TowarAtrybut TowarAtrybut
        {
            get
            {
                return this.towarAtrybut;
            }
            set
            {
                this.towarAtrybut = value;
                if (towarAtrybut != null)
                    setTowarAtrybut();
            }
        }

        private void setProduct()
        {
            if (this.InvokeRequired)
            {
                var d = new MethodInvoker(setProduct);
                this.Invoke(d);
            }
            else
            {
                try
                {
                    this.Text = product.Nazwa;

                    if (this.pictureBox.Image != null)
                        this.pictureBox.Image.Dispose();
                    this.pictureBox.Image = null;
                    setImageLegend("");
                    setKodAbak(product.Kod);
                    setKodProdAbak(product.KodDostawcy);
                    setOpis(product.Opis);

                    Enova.Business.Old.DB.Web.Zdjecie zdjecie = product.Zdjecie;
                    if (zdjecie != null)
                    {
                        Image image = zdjecie.GetImage();
                        setImageLegend(zdjecie.Legenda);
                        if (image != null)
                        {
                            pictureBox.Image = (Image)image.Clone();
                            image.Dispose();
                            return;
                        }
                    }
                    var img = AbakTools.Handel.Forms.Properties.Resources.BrakZdjecia;
                    pictureBox.Image = (Image)img.Clone();
                }
                catch { }

            }
        }

        private void setTowarAtrybut()
        {
            if (this.InvokeRequired)
            {
                var d = new MethodInvoker(setTowarAtrybut);
                this.Invoke(d);
            }
            else
            {
                try
                {
                    this.Text = towarAtrybut.TowarNazwa;

                    if (this.pictureBox.Image != null)
                        this.pictureBox.Image.Dispose();
                    this.pictureBox.Image = null;
                    setImageLegend("");
                    setKodAbak(towarAtrybut.TowarKod);
                    setKodProdAbak(towarAtrybut.TowarKodDostawcy);
                    setOpis(towarAtrybut.Towar.Opis);

                    Enova.Business.Old.DB.Web.Zdjecie zdjecie = towarAtrybut.Zdjecie;
                    if (zdjecie != null)
                    {
                        Image image = zdjecie.GetImage();
                        setImageLegend(zdjecie.Legenda);
                        if (image != null)
                        {
                            pictureBox.Image = (Image)image.Clone();
                            image.Dispose();
                            return;
                        }
                    }
                    var img = AbakTools.Handel.Forms.Properties.Resources.BrakZdjecia;
                    pictureBox.Image = (Image)img.Clone();
                }
                catch { }

            }
        }


        private delegate void MethodInvokeStrDelegate(string legend);
        private void setImageLegend(string legend)
        {
            if (imageLegendTextBox.InvokeRequired)
            {
                var d = new MethodInvokeStrDelegate(setImageLegend);
                imageLegendTextBox.Invoke(d, new object[] { legend });
            }
            else
            {
                imageLegendTextBox.Text = legend;
            }
        }

        private void setKodAbak(string kod)
        {
            if (kodAbakTextBox.InvokeRequired)
            {
                var d = new MethodInvokeStrDelegate(setKodAbak);
                kodAbakTextBox.Invoke(d, new object[] { kod });
            }
            else
            {
                kodAbakTextBox.Text = kod;
            }
        }

        private void setKodProdAbak(string kod)
        {
            if (kodprodTextBox.InvokeRequired)
            {
                var d = new MethodInvokeStrDelegate(setKodProdAbak);
                kodprodTextBox.Invoke(d, new object[] { kod });
            }
            else
            {
                kodprodTextBox.Text = kod;
            }
        }

        private void setOpis(string opis)
        {
            if (webBrowser.InvokeRequired)
            {
                var d = new MethodInvokeStrDelegate(setOpis);
                webBrowser.Invoke(d, new object[] { opis });
            }
            else
            {
                webBrowser.DocumentText = opis;
            }
        }


        public PodgladProduktuForm()
        {
            InitializeComponent();
        }

        private void PodgladProduktuForm_Load(object sender, EventArgs e)
        {

        }


     }
}
