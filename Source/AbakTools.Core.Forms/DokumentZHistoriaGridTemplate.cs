using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using AbakTools.Core;

namespace AbakTools.Core.Forms
{
    public class DokumentZHistoriaGridTemplate : GridTemplate
    {
        public override void SetRowStyle(object row)
        {
            try
            {
                var r = (DataGridViewRow)row;
                var z = (Enova.Business.Old.DB.Web.IDokumentZHistoria)r.DataBoundItem;

                var hist = z.OstatniaHistoriaDokumentu;
                if (hist != null && hist.Status != null)
                {
                    if (!string.IsNullOrEmpty(hist.Status.Kolor))
                    {
                        var color = ColorTranslator.FromHtml(hist.Status.Kolor);
                        r.DefaultCellStyle.BackColor = color;
                        r.DefaultCellStyle.SelectionBackColor = color;
                        r.DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }

            }
            catch { }
        }

    }
}
