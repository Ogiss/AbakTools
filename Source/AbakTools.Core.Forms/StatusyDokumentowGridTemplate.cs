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
    public class StatusyDokumentowGridTemplate : GridTemplate
    {
        public override void SetRowStyle(object row)
        {
            try
            {
                var r = (DataGridViewRow)row;
                var s = (Enova.Business.Old.DB.Web.StatusDokumentu)r.DataBoundItem;
                if (!string.IsNullOrEmpty(s.Kolor))
                {
                    Color color = ColorTranslator.FromHtml(s.Kolor);
                    r.DefaultCellStyle.BackColor = color;
                    r.DefaultCellStyle.SelectionBackColor = color;
                    r.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                if (r.DataGridView != null)
                {
                    if (r.Selected)
                        r.DefaultCellStyle.Font = new Font(r.DataGridView.DefaultCellStyle.Font, FontStyle.Bold);
                    else
                        r.DefaultCellStyle.Font = r.DataGridView.DefaultCellStyle.Font;
                }

            }
            catch { }
        }
    }
}
