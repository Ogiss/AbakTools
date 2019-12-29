using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Enova.Business.Old.DB.Web
{
    public partial class Zdjecie : IDbContext
    {
        #region Fields

        public static string IMAGES_PATH = @"Z:\AbakSoft\EnovaTools\img\p\";

        #endregion

        #region Properties

        public bool IsDeleted = false;
        public string FileName { get; set; }
        public ObjectContext DbContext { get; set; }

        #endregion

        #region Methods

        public string GetFileName()
        {
            if (this.ID > 0 && this.Produkt != null && this.Produkt.ID > 0)
                return IMAGES_PATH + this.Produkt.ID + "-" + this.ID + ".jpg";
            return null;
        }

        public Image GetImage()
        {
            Image image = null;
            try
            {
                if (this.ImageBytes != null)
                {
                    using (MemoryStream ms = new MemoryStream(this.ImageBytes))
                        image = Image.FromStream(ms);
                }
                else
                {
                    string file = this.GetFileName();
                    if (file != null && File.Exists(file))
                    {
                        image = Image.FromFile(file);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            using (var dc = new WebContext())
                            {
                                var img = dc.Zdjecia.Where(z => z.ID == this.ID).FirstOrDefault();
                                img.ImageBytes = ms.ToArray();
                                dc.OptimisticSaveChanges();
                            }
                            if (this.DbContext != null)
                                this.DbContext.Refresh(RefreshMode.StoreWins, this);
                        }
                    }
                }
            }
            catch
            {
            }

            return image;
        }

        public void SetImage(Image image, bool save = false)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                this.ImageBytes = ms.ToArray();
            }

            if (this.DbContext != null && save)
                ((WebContext)this.DbContext).OptimisticSaveChanges();
        }

        public void SetImage(string file, bool save = false)
        {
            SetImage(Image.FromFile(file), save);
        }

        #endregion

    }
}
