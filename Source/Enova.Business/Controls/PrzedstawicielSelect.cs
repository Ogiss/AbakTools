using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class PrzedstawicielSelect : UserControl
    {
        public PrzedstawicielSelect()
        {
            InitializeComponent();
        }

        private void PrzedstawicielSelect_Load(object sender, EventArgs e)
        {
            loadPrzedstawiciele();
        }

        private void loadPrzedstawiciele()
        {
            if (!this.DesignMode)
            {
                var ds = new List<Dictionary>() { new Dictionary() { ID = 0, Value = "(Wszyscy)" } };
                ds.AddRange(Enova.Business.Old.Core.ContextManager.DataContext.DictionarySet.Where(d => d.Category == "F.PRZEDSTAWICIEL")
                    .OrderBy(d => d.Value).ToList());

                bindingSource.DataSource = ds;
            }
        }

        public string Przedstawiciel
        {
            get
            {
                if (comboBox.SelectedItem != null && ((Enova.Business.Old.DB.Dictionary)comboBox.SelectedItem).ID != 0)
                {
                    return ((Enova.Business.Old.DB.Dictionary)comboBox.SelectedItem).Value;
                }
                return null;
            }
            set
            {
                comboBox.SelectedItem = Enova.Business.Old.Core.ContextManager.DataContext.DictionarySet.Where(d => d.Category == "F.PRZEDSTAWICIEL" && d.Value == value).FirstOrDefault();
            }
        }

        [Browsable(true)]
        public event EventHandler SelectionChanged
        {
            add
            {
                comboBox.SelectionChangeCommitted += value;
            }
            remove
            {
                comboBox.SelectionChangeCommitted -= value;
            }
        }
    }
}
