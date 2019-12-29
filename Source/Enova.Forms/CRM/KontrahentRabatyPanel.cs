using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

[assembly: BAL.Forms.DataPanel("Rabaty", typeof(Enova.API.CRM.Kontrahent), typeof(Enova.Forms.CRM.KontrahentRabatyPanel))]

namespace Enova.Forms.CRM
{
    [BAL.Types.Priority(30)]
    public partial class KontrahentRabatyPanel : BAL.Forms.DataPanel
    {
        #region Fields

        private bool isLoaded;
        private KontrahenciView kontrahenciSelectView;

        #endregion

        #region Properties

        public KontrahenciView KontrahenciSelectView
        {
            get
            {
                if (kontrahenciSelectView == null)
                    kontrahenciSelectView = new KontrahenciView() { SelectionMode = true };
                return kontrahenciSelectView;
            }
        }

        public API.CRM.Kontrahent Kontrahent
        {
            get
            {
                if (DataContext != null)
                    return DataContext.Current as API.CRM.Kontrahent;
                return null;
            }
        }

        #endregion

        #region Methods

        public KontrahentRabatyPanel()
        {
            InitializeComponent();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (!isLoaded)
                loadRabaty();
        }

        private void loadRabaty()
        {
            if (DataContext != null && DataContext.Current is Enova.API.Business.ISessionable)
            {
                var session = ((Enova.API.Business.ISessionable)DataContext.Current).Session;
                var tm = session.GetModule<Enova.API.Towary.TowaryModule>();
                foreach (var grupa in tm.DefinicjeCen.GrupyTowarowe)
                    filterGrupyTowComboBox.Items.Add(grupa);
                if (filterGrupyTowComboBox.Items.Count > 0)
                    filterGrupyTowComboBox.SelectedIndex = 0;
                loadTabelaRabatow();
                isLoaded = true;
            }
        }

        private void loadTabelaRabatow()
        {
            var featureDef = filterGrupyTowComboBox.SelectedItem as Enova.API.Business.FeatureDefinition;
            if (featureDef != null)
            {
                rabatyBindingSource.DataSource = new Towary.RabatyGrupowe()
                {
                    Kontrahent = (API.CRM.Kontrahent)DataContext.Current,
                    FeatureDefinition = featureDef
                };
            }
        }

        private void filterGrupyTowComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadTabelaRabatow();
        }

        private void rabatyDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (rabatyDataGridView.Columns[e.ColumnIndex].Name == "rabatColumn")
            {
                if (e.Value != null)
                {
                    try
                    {
                        decimal d = decimal.Parse(e.Value.ToString());
                        e.Value = decimal.Round(d / 100, 2);
                        e.ParsingApplied = true;

                    }
                    catch
                    {
                        e.ParsingApplied = false;
                    }
                }
            }
        }

        private void kopiujRabatyButton_Click(object sender, EventArgs e)
        {
            if(BAL.Forms.FormManager.Instance.ShowGridFormDialog(KontrahenciSelectView) == DialogResult.OK
                && KontrahenciSelectView.Current != null)
            {
                var kontrahentFrom = KontrahenciSelectView.Current as API.CRM.Kontrahent;
                if (BAL.Forms.FormManager.Confirm("Czy napeno chcesz skopiować rabaty z " + kontrahentFrom.Kod + " ?"))
                {
                    using (new BAL.Forms.WaitCursor(this))
                    {
                        var cenyGrupowe = new Dictionary<int, API.Towary.CenaGrupowa>();
                        var tm = Kontrahent.Session.GetModule<API.Towary.TowaryModule>();

                        foreach (var cgFrom in kontrahentFrom.CenyGrupowe)
                        {
                            if (cgFrom.GrupaTowarowa != null)
                            {
                                var cg = tm.CenyGrupowe[Kontrahent, null, cgFrom.GrupaTowarowa];
                                if (cg == null)
                                {
                                    cg = tm.CenyGrupowe.Create(cgFrom.GrupaTowarowa, Kontrahent);
                                    tm.CenyGrupowe.AddRow(cg);
                                }
                                if (cg != null)
                                {
                                    cg.Rabat = cgFrom.Rabat;
                                    cg.RabatZdefiniowany = cgFrom.RabatZdefiniowany;
                                }
                                cenyGrupowe.Add(cgFrom.GrupaTowarowa.ID, cgFrom);
                            }
                            
                        }
                        foreach (var cg in Kontrahent.CenyGrupowe)
                        {
                            if (cg.GrupaTowarowa != null)
                            {
                                if (!cenyGrupowe.ContainsKey(cg.GrupaTowarowa.ID))
                                {
                                    cg.Rabat = 0;
                                    cg.RabatZdefiniowany = false;
                                }
                            }
                        }
                        loadTabelaRabatow();
                    }
                }
            }
        }



        #endregion
    }
}
