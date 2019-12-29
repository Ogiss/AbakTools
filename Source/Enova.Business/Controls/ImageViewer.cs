using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class ImageViewer : UserControl
    {
        private ImageBox selectedImgBox = null;

        public ImageViewer()
        {
            InitializeComponent();
        }

        public object SelectedImageInfo
        {
            get { return this.selectedImgBox == null ? null : this.selectedImgBox.ImageInfo; }
        }

        public ImageBox SelectedImgBox
        {
            get { return this.selectedImgBox; }
        }

        public void Add(Image image, object imageInfo)
        {
            ImageBox box = new ImageBox(imageInfo);
            box.Image = image;
            this.flowLayoutPanel.Controls.Add(box);
            this.SelectImgBox(box);
        }

        public void Add(Image image)
        {
            this.Add(image, null);
        }

        public void Add(string imgPath, object imageInfo)
        {
            if (File.Exists(imgPath))
            {
                Image img = null;
                using (FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                    img = Image.FromStream(fs);
                this.Add((Image)img.Clone(), imageInfo);
                img.Dispose();
                img = null;
            }
            else
            {
                var img = Properties.Resources.BrakZdjecia;
                this.Add((Image)img.Clone(), imageInfo);
                img.Dispose();
                img = null;
            }
        }

        public void Add(string imgPath)
        {
            this.Add(imgPath, null);
        }

        public void Remove(ImageBox imageBox)
        {
            this.flowLayoutPanel.Controls.Remove(imageBox);
            if (this.selectedImgBox == imageBox)
                SelectImgBox(imageBox);
        }

        internal void SelectImgBox(ImageBox imgBox)
        {
            if (this.selectedImgBox != imgBox || this.selectedImgBox == null)
            {
                if (this.selectedImgBox != null)
                    this.selectedImgBox.Selected = false;

                this.selectedImgBox = imgBox;
                if (imgBox != null)
                    this.selectedImgBox.Selected = true;
                OnSelectedChanged(new EventArgs());
            }
        }

        protected virtual void OnSelectedChanged(EventArgs e)
        {
            if (SelectedChanged != null)
                SelectedChanged(this, e);
        }

        [Browsable(true)]
        public event EventHandler SelectedChanged;

    }
}
