using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

namespace AbakTools.Towary.Forms
{
    public partial class WyborZdjeciaForm : Form
    {
        public Produkt Towar { get; set; }
        public List<Guid> ZdjeciaDeleted = null;

        private Enova.Business.Old.DB.Web.Zdjecie selectedImage = null;

        public Enova.Business.Old.DB.Web.Zdjecie SelectedImage
        {
            get { return this.selectedImage; }
        }

        public WyborZdjeciaForm()
        {
            InitializeComponent();
        }

        private void WyborZdjeciaForm_Load(object sender, EventArgs e)
        {
            if (this.Towar != null)
            {
                //const string IMG_DIR = @"Z:\AbakSoft\EnovaTools\img\p\";
                
                foreach (var zdjecie in Towar.Zdjecia.Where(z => z.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete && z.Deleted == false))
                {
                    if (ZdjeciaDeleted != null && ZdjeciaDeleted.Count > 0 && ZdjeciaDeleted.Contains(zdjecie.GUID))
                        continue;
                    /*
                    string imgPath = null;
                    if (string.IsNullOrEmpty(zdjecie.FileName))
                    {
                        imgPath = IMG_DIR + Towar.ID.ToString() + "-" + zdjecie.ID.ToString() + ".jpg";
                    }
                    else
                    {
                        imgPath = zdjecie.FileName;
                    }
                    imageViewer.Add(imgPath, zdjecie);
                     */
                    imageViewer.Add(zdjecie.GetImage(), zdjecie);

                }
            }
        }

        private void imageViewer_SelectedChanged(object sender, EventArgs e)
        {
            this.selectedImage = (Enova.Business.Old.DB.Web.Zdjecie)imageViewer.SelectedImageInfo;
            this.legendaTextBox.Text = this.selectedImage.Legenda;
        }
    }
}
