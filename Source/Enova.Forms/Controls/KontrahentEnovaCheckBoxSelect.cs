using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace Enova.Forms.Controls
{
    public partial class KontrahentEnovaCheckBoxSelect : BAL.Forms.Controls.BALControl
    {
        #region Fields

        private API.Business.Session enovaSession;

        #endregion

        #region Properties
        /*
        [Browsable(false)]
        public API.Business.Session EnovaSession
        {
            get
            {
                if (enovaSession == null)
                {
                    var form = this.FindForm();
                    if (form != null && form is API.Business.ISessionable)
                        enovaSession = ((API.Business.ISessionable)form).Session;
                }
                return enovaSession;
            }
        }
         */

        [Browsable(false)]
        public string Przedstawiciel
        {
            get
            {
                if (przedstawicielComboBox.SelectedIndex > 0)
                    return (string)przedstawicielComboBox.SelectedItem;
                return null;
            }
        }

        [Browsable(false)]
        public string Trasa
        {
            get
            {
                if (trasaComboBox.SelectedIndex > 0)
                    return (string)trasaComboBox.SelectedItem;
                return null;
            }
        }

        [Browsable(false)]
        public API.CRM.Kontrahent Kontrahent
        {
            get
            {
                if (kontrahentComboBox.SelectedIndex > 0)
                    return (API.CRM.Kontrahent)kontrahentComboBox.SelectedItem;
                return null;
            }
        }

        [Browsable(false)]
        public ArrayList KontrahenciIDChecked
        {
            get
            {
                return this.kontrahentComboBox.SelectedValues;
            }
        }

        [Browsable(true)]
        public bool IsAllChecked
        {
            get
            {
                return this.kontrahentComboBox.IsAllChecked;
            }
        }

        #endregion

        #region Methods

        public KontrahentEnovaCheckBoxSelect()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                loadPrzedstawiciele();
                loadTrasy();
                loadKontrahenci();
            }
            base.OnLoad(e);
        }

        private void loadPrzedstawiciele()
        {
            using (var session = Enova.API.EnovaService.Instance.CreateSession())
            {
                //var bm = EnovaSession.GetModule<API.Business.BusinessModule>();
                var bm = session.GetModule<API.Business.BusinessModule>();
                List<string> dc = new List<string>();
                przedstawicielComboBox.Items.Clear();
                przedstawicielComboBox.Items.Add("(Wszyscy)");
                var view = bm.Dictionary.CreateView();
                view.Filter = "Category = 'F.przedstawiciel'";
                foreach (API.Business.DictionaryItem item in view)
                    dc.Add(item.Value);
                przedstawicielComboBox.Items.AddRange(dc.OrderBy(r => r).ToArray());
                przedstawicielComboBox.SelectedIndex = 0;
            }
        }

        private void loadTrasy()
        {
            using (var session = Enova.API.EnovaService.Instance.CreateSession())
            {
                trasaComboBox.Items.Clear();
                trasaComboBox.Items.Add("(Wszystkie)");
                var przedstawiciel = (string)(przedstawicielComboBox.SelectedIndex <= 0 ? null : przedstawicielComboBox.SelectedItem);
                if (przedstawiciel != null)
                {
                    //var bm = EnovaSession.GetModule<API.Business.BusinessModule>();
                    var bm = session.GetModule<API.Business.BusinessModule>();
                    var parent = bm.Dictionary["F.TRASY"].ToList().Where(r => r.Value == przedstawiciel).FirstOrDefault();
                    if (parent != null)
                        trasaComboBox.Items.AddRange(bm.Dictionary.WgParent(parent).ToList().OrderBy(r => r.Value).Select(r => r.Value).ToArray());
                }
                trasaComboBox.SelectedIndex = 0;
            }
        }

        private void loadKontrahenci()
        {
            using (var session = Enova.API.EnovaService.Instance.CreateSession())
            {
                var pr = Przedstawiciel;
                var tr = Trasa;

                //var view = EnovaSession.GetModule<API.CRM.CRMModule>().Kontrahenci.CreateView();
                var view = session.GetModule<API.CRM.CRMModule>().Kontrahenci.CreateView();
                /*
                if (tr == null)
                {
                    if (pr != null)
                        view.Filter = string.Format("Features.[{0}] = '{1}'", "przedstawiciel", pr);
                }
                else if (pr != null)
                {
                    var path = "\\" + pr + "\\" + tr + "\\%";
                    view.Filter = string.Format("Features.[{0}] LIKE '{1}'", "TRASY", path);

                }
                 */
                if(pr != null)
                {
                    view.Filter = string.Format("Features.[{0}] = '{1}'", "przedstawiciel", pr);
                }
                var filter = view.Filter;
                var dc = new Dictionary<int, API.CRM.Kontrahent>();
                if (!string.IsNullOrWhiteSpace(tr))
                    tr = "\\" + pr + "\\" + tr + "\\";
                foreach(API.CRM.Kontrahent kh in view)
                {
                    if (dc.ContainsKey(kh.ID))
                        continue;
                    if (tr != null)
                    {
                        var trasy = (string[])kh.Features["TRASY"];
                        foreach (var trasa in trasy)
                        {
                            if (trasa.StartsWith(tr) && !dc.ContainsKey(kh.ID))
                            {
                                dc.Add(kh.ID, kh);
                                continue;
                            }
                        }
                    }
                    else
                        dc.Add(kh.ID, kh);
                }

                //kontrahentComboBox.DataSource = view.Cast<API.CRM.Kontrahent>().OrderBy(r => r.Kod).ToList();
                kontrahentComboBox.DataSource = dc.Values.OrderBy(r => r.Kod).ToList();
                kontrahentComboBox.SelectedIndex = 0;
            }

        }

        private void przedstawicielComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                loadTrasy();
                loadKontrahenci();
                OnChanged(e);
            }
        }

        private void trasaComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                var idx = kontrahentComboBox.SelectedIndex;
                loadKontrahenci();
                OnChanged(e);
                if (idx > 0)
                {
                    kontrahentComboBox.SelectedIndex = 0;
                    OnKontrahentChanged(e);
                }

            }
        }

        private void kontrahentComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OnKontrahentChanged(e);
            OnChanged(e);
        }

        /*
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, e);
        }
         */

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
            this.OnValueChanged(e);
        }

        protected virtual void OnKontrahentChanged(EventArgs e)
        {
            if (KontrahentChanged != null)
                KontrahentChanged(this, e);
            this.OnValueChanged(e);
        }

        #endregion

        #region Events

        [Browsable(true)]
        public event EventHandler Changed;

        [Browsable(true)]
        public event EventHandler KontrahentChanged;

        /*
        [Browsable(false)]
        public event EventHandler ValueChanged;
         */

        #endregion

    }
}
