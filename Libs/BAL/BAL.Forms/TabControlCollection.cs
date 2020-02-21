using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public class TabControlCollection : IList
    {
        #region Fields

        private TabControl tabControl;
        private Dictionary<string, int> byKey;
        private Dictionary<int, Control> byIndex;

        #endregion

        #region Properties

        public int SelectedIndex
        {
            get { return this.tabControl.SelectedIndex; }
            set { this.tabControl.SelectedIndex = value; }
        }

        #endregion

        #region Methods

        public TabControlCollection(TabControl tabControl)
        {
            this.tabControl = tabControl;
            this.byKey = new Dictionary<string, int>();
            this.byIndex = new Dictionary<int, Control>();
        }

        public int Add(object value)
        {
            Control control = value as Control;
            string key = control is IControlKey ? ((IControlKey)control).Key : control.Name;
            if (control != null)
            {
                if (byKey.ContainsKey(key))
                {
                    int idx = byKey[key];
                    tabControl.SelectedIndex = idx;
                    return idx;
                }
                else
                {
                    TabPage page = null;
                    if (tabControl.SelectedTab.Name == "AddPage")
                    {
                        page = new TabPage();
                        tabControl.TabPages.Insert(0, page);
                        tabControl.SelectedIndex = 0;
                    }
                    else
                    {
                        foreach (Control c in tabControl.SelectedTab.Controls)
                        {
                            string k = c is IControlKey ? ((IControlKey)c).Key : c.Name;
                            this.byKey.Remove(k);
                            c.Dispose();
                        }
                        tabControl.SelectedTab.Controls.Clear();
                        page = tabControl.SelectedTab;
                    }
                    page.Text = control.Text;
                    page.ToolTipText = control.Text;
                    page.Controls.Add(control);
                    this.byKey[key] = tabControl.SelectedIndex;
                    this.byIndex[tabControl.SelectedIndex] = control;
                    if (typeof(Form).IsAssignableFrom(control.GetType()))
                        ((Form)control).Show();
                    control.Select();
                    return tabControl.SelectedIndex;
                }
            }
            return -1;
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < this.tabControl.TabPages.Count)
            {
                Control c = null;
                if (this.byIndex.ContainsKey(index))
                    c = this.byIndex[index];
                this.tabControl.TabPages.RemoveAt(index);
                if (c != null)
                {
                    string key = c is IControlKey ? ((IControlKey)c).Key : c.Name;
                    this.byKey.Remove(key);
                    this.byIndex.Remove(index);
                    var form = c as Form;
                    if (form != null)
                        form.Close();
                    c.Dispose();
                }
            }
        }

        public void Insert(int index, object value)
        {
            this.tabControl.TabPages.Insert(index, value == null ? "" : (string)value);
            this.byIndex.Clear();
            this.byKey.Clear();

            for (int i = 0; i < this.tabControl.TabPages.Count; i++)
            {
                TabPage tab = this.tabControl.TabPages[i];
                if (tab.Controls.Count > 0)
                {
                    Control c = tab.Controls[0];
                    string key = c is IControlKey ? ((IControlKey)c).Key : c.Name;
                    this.byIndex.Add(i, c);
                    this.byKey.Add(key, i);
                }
            }

        }


        #endregion

        #region IList Implementation


        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }


        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }


        public object this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
