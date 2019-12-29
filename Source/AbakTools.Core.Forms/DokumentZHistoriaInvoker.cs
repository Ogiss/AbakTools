using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using AbakTools.Core;

//[assembly: RowInvoker(typeof(DokumentZHistoria), typeof(AbakTools.Core.Forms.DokumentZHistoriaInvoker))]

namespace AbakTools.Core.Forms
{
    /*
    public class DokumentZHistoriaInvoker
    {
        public void OnCreated(object sender, EventArgs e)
        {
            DokumentZHistoria row = (DokumentZHistoria)sender;

            if (row.OstatniaHistoria == null)
            {
                var cm = Core.CoreModule.GetInstance(row);
                var statusy = cm.StatusyDokumentow.WgKategorii[row.Table.TableName].WgOpcji[OpcjeStatusuDokumentu.Domyslny].ToList();
                StatusDokumentu status = null;

                if (statusy.Count > 1)
                {
                    var form = new WyborStatusuForm();
                    form.Statusy = statusy;
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        status = form.WybranyStatus;
                }
                else if (statusy.Count == 1)
                    status = statusy.FirstOrDefault();

                if (status != null)
                {
                    string opis = "";
                    if ((status.Opcje & OpcjeStatusuDokumentu.WymaganyOpis) == OpcjeStatusuDokumentu.WymaganyOpis)
                    {
                        var form = new Controls.HistoriaDokumentuOpisForm();
                        form.ShowDialog();
                        if (!string.IsNullOrEmpty(form.Opis))
                            opis = form.Opis;
                    }
                    row.ZmienStatus(status, opis);
                }
            }
        }
    }
     */
}
