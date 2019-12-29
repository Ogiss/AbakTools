using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Enova.API;

namespace Enova.API.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var enovaPath = ConfigurationManager.AppSettings["enovaPath"];
            var enovaDatabase = ConfigurationManager.AppSettings["enovaDatabase"];
            var enovaLogin = ConfigurationManager.AppSettings["enovaLogin"];
            var enovaPassword = ConfigurationManager.AppSettings["enovaPassword"];
            var service = EnovaService.Initialize(enovaPath);
            service.Login(enovaDatabase, enovaLogin, enovaPassword);

            using(var session = service.CreateSession())
            {
                var cm = session.GetModule<Enova.API.CRM.CRMModule>();
                var kontrah = cm.Kontrahenci.CreateView().SetFilter("Kod = 'OGI'").Cast<Enova.API.CRM.Kontrahent>().FirstOrDefault() as Enova.API.Core.IPodmiot;
                Console.WriteLine("Kod:" + kontrah.Kod);
                Console.WriteLine("Nazwa:" + kontrah.Nazwa);
                var adres = kontrah.Adres;
                Console.WriteLine("Ulica:" + adres.Ulica);
                /*
                var mm = session.GetModule<Enova.API.Magazyny.MagazynyModule>();
                var mag = mm.Magazyny.Firma;
                Console.WriteLine("Magazyn główny:" + mag.Nazwa);
                 */

            }

            service.Logout();
            Console.ReadLine();
        }
    }
}
