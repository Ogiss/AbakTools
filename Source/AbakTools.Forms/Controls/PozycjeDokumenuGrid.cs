using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AbakTools.Forms.Controls

{
    public class PozycjeDokumenuGrid : GridView
    {
        protected virtual BAL.Business.View SelectView
        {
            get { return null; }
        }

        protected override bool ProcessDataGridViewKey(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Alt == false && e.Control == false && e.Shift == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        this.EndEdit();
                        return true;
                    case Keys.Down:
                        if (this.CurrentRow != null && this.CurrentCell != null && this.CurrentRow.Index == this.Rows.Count - 1 && this.CurrentCell.IsInEditMode)
                        {
                            this.EndEdit();
                            //this.OnKeyDown(e);
                            this.ProcessAddNew();
                            return true;

                        }
                        break;
                }

            }

            return base.ProcessDataGridViewKey(e);
        }

        public virtual void ProcessAddNew()
        {
            if (this.SelectView != null)
            {
                if (BAL.Forms.FormManager.Instance.ShowGridFormDialog(this.SelectView) == DialogResult.OK)
                {
                    var row = this.CreateRow(this.SelectView.Current);
                    if (row != null)
                        this.View.Add(row);
                }
            }
        }

        protected virtual object CreateRow(object data)
        {
            return data;
        }


        #region Events
        #endregion

    }
}
