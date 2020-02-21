using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public partial class FormWithTabs : BAL.Forms.FormWithPanels
    {
        #region Fields

        private TabControlCollection tabControlCollection;
        private bool disableAddNewPage = false;

        #endregion

        #region Properties
        #endregion

        #region Methods

        public FormWithTabs()
        {
            InitializeComponent();
            this.tabControlCollection = new TabControlCollection(this.TabControl);
        }

        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0 && e.Index < this.TabControl.TabPages.Count)
            {
                TabPage page = this.TabControl.TabPages[e.Index];
                Rectangle tabRec = this.TabControl.GetTabRect(e.Index);
                Graphics g = e.Graphics;
                Pen pen = new Pen(Color.LightGray);
                g.DrawRectangle(pen, tabRec);
                if (page.Name == "AddPage")
                {
                    Rectangle r = new Rectangle(tabRec.X + tabRec.Width / 2 - 8, tabRec.Y + 4, 16, 16);
                    g.DrawIcon(Properties.Resources.add, r);

                }
                else
                {

                    Font font = new Font("Arial", 10.0f, e.Index == this.TabControl.SelectedIndex ? FontStyle.Bold : FontStyle.Regular);
                    string s = getStringByWith(g, font, page.Text, tabRec.Width - 19);
                    SolidBrush brush = new SolidBrush(Color.Black);
                    g.DrawString(s, font, brush, new Rectangle(tabRec.X+2, tabRec.Y+2, tabRec.Width - 19, tabRec.Height));
                    Rectangle r = new Rectangle(tabRec.X + tabRec.Width - 18, tabRec.Y + 4, 16, 16);
                    g.DrawIcon(Properties.Resources.dialog_cancel, r);
                }
            }
        }

        private string getStringByWith(Graphics g, Font f, string str, int with)
        {
            string s = str;
            bool flag = false;
            SizeF size = g.MeasureString(s, f);
            while (size.Width > with)
            {
                flag = true;
                s = s.Substring(0, s.Length - 1);
                size = g.MeasureString(s + "...", f);
            }


            return s + (flag ? "..." : "");
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.disableAddNewPage && this.TabControl.SelectedTab.Name == "AddPage")
            {
                int index = this.TabControl.SelectedIndex;
                //TabControl.TabPages.Insert(this.TabControl.SelectedIndex, "");
                this.tabControlCollection.Insert(this.TabControl.SelectedIndex, null);
                this.TabControl.SelectedIndex = index;
            }

            if (this.TabControl.SelectedTab.Controls.Count > 0)
            {
                this.TabControl.SelectedTab.Controls[0].Select();
            }

        }

        public override System.Collections.IList GetControlCollection()
        {
            return this.tabControlCollection;
        }

        public override Rectangle GetClientRectagle()
        {
            return this.TabControl.SelectedTab.ClientRectangle;
        }

        #endregion

        private void TabControl_Click(object sender, EventArgs e)
        {

        }

        private void TabControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks > 1)
                return;

            int idx = -1;

            for (int i = 0; i < this.TabControl.TabPages.Count; i++)
            {
                Rectangle r = this.TabControl.GetTabRect(i);
                //if (e.X >= r.X && e.Y >= r.Y && e.X <= r.X + r.Width && e.Y <= r.Y + r.Height)
                if(this.TabControl.GetTabRect(i).Contains(e.Location))
                {
                    idx = i;
                    break;
                }
            }

            if (idx >= 0)
            {
                var page = this.TabControl.TabPages[idx];
                if (page.Name != "AddPage")
                {
                    Rectangle r = this.TabControl.GetTabRect(idx);
                    if (e.X >= r.X + r.Width - 16)
                    {
                        this.disableAddNewPage = true;
                        this.tabControlCollection.RemoveAt(idx);
                        this.disableAddNewPage = false;
                    }
                }
            }

        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }


    }
}
