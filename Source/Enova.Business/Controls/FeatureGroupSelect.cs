using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class FeatureGroupSelect : UserControl
    {
        public FeatureGroupSelect()
        {
            WithAll = false;
            InitializeComponent();
        }

        private void FeatureGroupSelect_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                loadData();
                grupaComboBox.SelectedIndex = 0;
            }

        }

        private void loadData()
        {
            if (!string.IsNullOrEmpty(TableName))
            {
                var dc = Enova.Business.Old.Core.ContextManager.DataContext;
                List<FeatureDef> ds = new List<FeatureDef>();
                if (WithAll)
                {
                    ds.Add(new FeatureDef() { ID = 0, Name = "Wszystkie" });
                }
                ds.AddRange(dc.FeatureDefs.Where(f => f.TableName == TableName && f.Group == true && f.StrictDictionary == true).OrderBy(f => f.Name).ToList());
                grupyBindingSource.DataSource = ds;
            }
        }

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        private void grupaComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OnChanged(e);
        }

        [Browsable(false)]
        public FeatureDef DefinicjaCechy
        {
            get
            {
                return ((FeatureDef)grupaComboBox.SelectedItem).ID == 0 ? null : (FeatureDef)grupaComboBox.SelectedItem;
            }
        }

        [Browsable(false)]
        public string Grupa
        {
            get
            {
                return ((FeatureDef)grupaComboBox.SelectedItem).ID == 0 ? null : DefinicjaCechy.Name;
            }
        }


        [Browsable(true)]
        public string TableName { get; set; }

        [Browsable(true)]
        public bool WithAll { get; set; }

        [Browsable(true)]
        public string LabelText
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
                this.Size = new Size(label.Width + grupaComboBox.Width + 6, this.Height);
            }
        }

        [Browsable(true)]
        public event EventHandler Changed;

    }
}
