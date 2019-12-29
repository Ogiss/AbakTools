using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AbakTools.Towary.Forms
{
    public partial class ImagePreviewForm : Form
    {
        public Enova.Business.Old.DB.Web.Zdjecie Zdjecie { get; set; }

        public ImagePreviewForm()
        {
            InitializeComponent();
        }

        private void ImagePreviewForm_Load(object sender, EventArgs e)
        {
            if (this.Zdjecie != null)
            {

            }
        }
    }
}
