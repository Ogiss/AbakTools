using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Handel.Forms
{
    public class PozycjeZamowieniaIloscColumn : DataGridViewTextBoxColumn
    {
        public PozycjeZamowieniaIloscColumn()
            : base()
        {
            CellTemplate = new PozycjeZamowieniaIloscCell();
        }
    }
}
