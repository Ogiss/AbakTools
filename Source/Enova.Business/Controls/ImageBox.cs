using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class ImageBox : UserControl
    {
        private object imageInfo = null;
        private bool selected = false;


        public bool Selected
        {
            get { return this.selected; }
            set
            {
                this.selected = value;
                if (value)
                {
                    this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }
                else
                {
                    this.BorderStyle = System.Windows.Forms.BorderStyle.None;
                }
            }
        }

        public object ImageInfo
        {
            get { return this.imageInfo; }
        }

        public ImageBox()
        {
            InitializeComponent();
        }

        public ImageBox(object imageInfo)
            : this()
        {
            this.imageInfo = imageInfo;
        }

        public Image Image
        {
            get { return this.pictureBox.Image; }
            set { this.pictureBox.Image = value; }
        }

        private void ImageBox_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            ((ImageViewer)this.Parent.Parent).SelectImgBox(this);
        }

        private void saveImageMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var bm = new Bitmap(this.pictureBox.Image);
                bm.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
            }
        }
    }
}
