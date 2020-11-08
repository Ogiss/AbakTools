using AbakTools.Framework;
using AbakTools.Printer.DocumentPrintService;
using Unity;

namespace AbakTools.Printer
{
    public class ComponentSetup : IComponentSetup
    {
        public void Setup(IUnityContainer container)
        {
            container.RegisterType<IDocumentPrinterService, DocumentPrinterService>();
        }
    }
}
