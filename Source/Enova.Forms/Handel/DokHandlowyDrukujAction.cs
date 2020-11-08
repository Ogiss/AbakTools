using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AbakTools.Framework;
using AbakTools.EnovaApi;

[assembly: BAL.Types.Action(
    ActionType = typeof(Enova.Forms.Handel.DokHandlowyDrukujAction),
    DataType = typeof(Enova.API.Handel.DokumentHandlowy),
    Path="Drukuj",
    Description = "Wydruk faktury sprzedaży",
    Priority = 10,
    Target = BAL.Types.ActionTarget.FormMenu,
    ActionData=new object[]{"Sprzedaż"})]

[assembly: BAL.Types.Action(
    ActionType = typeof(Enova.Forms.Handel.DokHandlowyDrukujAction),
    DataType = typeof(Enova.API.Handel.DokumentHandlowy),
    Path = "Drukuj",
    Description = "Wydruk korekty sprzedaży",
    Priority = 20,
    Target = BAL.Types.ActionTarget.FormMenu,
    ActionData = new object[] { "Korekta" })]



namespace Enova.Forms.Handel
{
    public class DokHandlowyDrukujAction : BAL.Business.IDataContexable
    {
        #region Fields

        private BAL.Business.DataContext dataContext;

        #endregion

        public BAL.Business.DataContext DataContext
        {
            get
            {
                return this.dataContext;
            }
            set
            {
                this.dataContext = value;
            }
        }

        public bool Korekta
        {
            get
            {
                return this.ActionData[0].ToString().Equals("Korekta");
            }
        }

        public Enova.API.Handel.DokumentHandlowy DokHandlowy
        {
            get
            {
                return this.DataContext.GetData<Enova.API.Handel.DokumentHandlowy>();
            }
        }

        public object[] ActionData { get; set; }

        public bool Visible
        {
            get
            {
                if (this.DataContext != null && this.ActionData != null)
                {
                    bool korekta = this.ActionData[0].ToString().Equals("Korekta");
                    var doc = this.DataContext.GetData<Enova.API.Handel.DokumentHandlowy>();
                    return doc.Korekta == korekta;
                }
                return false;
            }
        }

        public void Action()
        {
            /*
            var service = Enova.API.EnovaService.Instance;
            if (service != null && service.IsLogined)
            {
                using (var session = service.CreateSession())
                {
                    string template = null;
                    if (this.Korekta)
                    {
                        template = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFKReport");
                    }
                    else
                    {
                        template = Enova.Business.Old.Core.Configuration.GetSetting("EnovaFVReport");
                    }
                    if (string.IsNullOrEmpty(template))
                        throw new Exception("Nie skonfigurowano wzoru wydruku dla dokumentu sprzedaży");

                    template = Path.IsPathRooted(template) ? template : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aspx", template);
                    session.GetModule<API.Handel.HandelModule>().DrukujDokument(
                        null, this.DokHandlowy.Guid, template);

                }

            }
            */

            DependencyProvider.Resolve<IEnovaService>().PrintDocument(DokHandlowy.Guid);
        }

    }
}
