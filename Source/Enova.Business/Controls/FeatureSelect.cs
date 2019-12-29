using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class FeatureSelect : UserControl
    {

        #region Properties

        public FeatureDef FeatureDef
        {
            get
            {
                return (FeatureDef)this.featureComboBox.SelectedItem;
            }
        }

        public Dictionary Dictionary
        {
            get
            {
                return (Dictionary)this.valueComboBox.SelectedItem;
            }
        }

        [Browsable(true)]
        public string TableName { get; set; }

        #endregion

        public FeatureSelect()
        {
            InitializeComponent();
        }

        public void LoadGrupy()
        {
            var dc = Enova.Business.Old.Core.ContextManager.DataContext;
            var ds = new List<FeatureDef>() { new FeatureDef() { ID = 0, Name = "(Wszystkie)" } };
            ds.AddRange(dc.FeatureDefs.Where(fd => fd.TableName == this.TableName && fd.StrictDictionary == true && fd.Group == true).OrderBy(fd => fd.Name).ToList());
            this.groupBindingSource.DataSource = ds;
            this.valueBindingSource.DataSource = new List<Dictionary>() { new Dictionary() { ID = 0, Value = "(Wszystkie)" } };

        }

        public void LoadWartosci()
        {
            var ds = new List<Dictionary>() { new Dictionary() { ID = 0, Value = "(Wszystkie)" } };;
            
            FeatureDef fd = (FeatureDef)featureComboBox.SelectedItem;
            if (fd != null && fd.ID > 0)
            {
                var dc = Enova.Business.Old.Core.ContextManager.DataContext;
                ds.AddRange(dc.DictionarySet.Where(d => d.Category == "F." + fd.Dictionary).OrderBy(d => d.Value).ToList());
            }
            this.valueBindingSource.DataSource = ds;
        }

        private void featureComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.LoadWartosci();
        }

        [Bindable(true)]
        public event EventHandler Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            if (this.Changed != null)
                this.Changed(this, e);
        }
    }
}
