using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API
{
    public interface IEnovaService
    {
        Business.Session CreateSession();
        T CreateRow<T>(object record = null) where T : Business.Row;
        T GetWorker<T>();
        void DrukujRow(System.Windows.Forms.Form form, API.Business.Row row, string template,
            Printer.Destinations destination = Printer.Destinations.Preview, string outputFile = null);

        bool IsLogined { get; }
        string OperatorName { get; }

        void Init(string databaseName);
        void Login(string user, string password);
        void SetTools(Type toolsType, object tools);
        void Finish();
    }
}
