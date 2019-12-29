using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using Enova.Business.Old.DB.Web;

namespace AbakTools.Zamowienia.Forms
{
    public class ZamowieniaViewGridTemplate : GridTemplate
    {
        #region Methods

        public override void SetRowStyle(object row)
        {
            try
            {
                var r = (DataGridViewRow)row;
                var z = (ZamowienieView)r.DataBoundItem;
                if (z.StatusTyp != null)
                {
                    Color color = ColorTranslator.FromHtml(z.StatusKolor);
                    if (z.Pilne != null && z.Pilne.Value && (z.TypStatusu == TypStatusuZamowienia.NoweZamowienie || z.TypStatusu == TypStatusuZamowienia.DoMagazynu))
                        color = Color.Coral;
                    else if (z.ZamowienieOdlozone && z.TypStatusu == TypStatusuZamowienia.DoMagazynu)
                        color = Color.LightPink;

                    r.DefaultCellStyle.BackColor = color;
                    r.DefaultCellStyle.SelectionBackColor = color;
                    r.DefaultCellStyle.SelectionForeColor = Color.Black;
                    if (r.DataGridView != null)
                    {
                        if (r.Selected)
                            r.DefaultCellStyle.Font = new Font(r.DataGridView.DefaultCellStyle.Font, FontStyle.Bold);
                        else
                            r.DefaultCellStyle.Font = r.DataGridView.DefaultCellStyle.Font;
                    }
                }
            }
            catch { }
        }

        #endregion
    }
}
