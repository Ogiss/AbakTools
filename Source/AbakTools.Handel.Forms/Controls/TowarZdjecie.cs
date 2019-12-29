using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Enova.Business.Old.DB.Web;

namespace AbakTools.Handel.Forms
{
    public partial class TowarZdjecie : UserControl
    {
        const string IMG_DIR = @"Z:\AbakSoft\EnovaTools\img\p\";

        private Zdjecie zdjecie = null;

        public Zdjecie Zdjecie
        {
            get { return this.zdjecie; }
        }

        public TowarZdjecie(Zdjecie zdjecie)
        {
            InitializeComponent();
            this.zdjecie = zdjecie;
            this.TabStop = true;
        }

        public TowarZdjecie() : this(null) { }

        public void LoadZdjecie()
        {
            if (this.zdjecie != null)
            {
                string imgPath = IMG_DIR + this.zdjecie.Produkt.ID.ToString() + "-" + this.zdjecie.ID.ToString() + ".jpg";
                if (File.Exists(imgPath))
                {
                    Image img = Image.FromFile(imgPath);
                    this.pictureBox.Image = (Image)img.Clone();
                    img.Dispose();
                }
            }
        }
    }
}
