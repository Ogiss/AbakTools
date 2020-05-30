using System.Linq;
using System.Data;

[assembly: BAL.Forms.MenuAction("Prowizje\\Etaty", typeof(AbakTools.Kadry.Forms.EtatyForm), Priority = 40)]

namespace AbakTools.Kadry.Forms
{
    public partial class EtatyForm : Enova.Business.Old.Forms.DataGridForm
    {
        public EtatyForm()
        {
            InitializeComponent();
        }

        protected override void LoadData()
        {
            this.DataSource = Enova.Business.Old.Core.ContextManager.WebContext.Etaty.OrderBy(e => e.Pracownik).ThenBy(e => e.Data).ToList();
        }
    }
}
