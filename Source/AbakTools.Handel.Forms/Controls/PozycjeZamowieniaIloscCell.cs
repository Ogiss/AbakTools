using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Handel.Forms
{
    public class PozycjeZamowieniaIloscCell : DataGridViewTextBoxCell
    {
        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            base.OnKeyDown(e, rowIndex);
        }

        public override Type EditType
        {
            get
            {
                //return base.EditType;
                return typeof(PozycjeZamowieniaIloscEditingControl);
            }
        }
    }
}
