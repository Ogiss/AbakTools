using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Handel.Forms
{
    public class ReklamacjaDokHandlowyContext : Enova.Forms.Handel.DokHandlowyContext
    {
        private int ilocsLocalIndex = -1;
        private Dictionary<int, RowInfo> rows;

        public IEnumerable<RowInfo> Pozycje
        {
            get
            {
                return rows.Values;
            }
        }

        public override void InitPozycjeDokGrid(Enova.Forms.Handel.PozycjeDokHanGrid grid)
        {
            grid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            grid.Columns["IloscOrgColumn"].Visible = false;
            grid.Columns["CenaOrgColumn"].Visible = false;
            grid.Columns["RabatOrgColumn"].Visible = false;
            var iloscLocalColumn = grid.Columns["iloscLocalColumn"];
            iloscLocalColumn.Visible = true;
            iloscLocalColumn.ValueType = typeof(double);
            ilocsLocalIndex = iloscLocalColumn.Index;
            
            rows = new Dictionary<int, RowInfo>();
            grid.CellValueNeeded += grid_CellValueNeeded;
            grid.CellValuePushed += grid_CellValuePushed;
        }

        private void grid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= 0 && ilocsLocalIndex > -1 && e.ColumnIndex == ilocsLocalIndex)
            {
                if (!rows.ContainsKey(e.RowIndex))
                {
                    var pozycja = (Enova.API.Handel.PozycjaDokHandlowego)(((DataGridView)sender).Rows[e.RowIndex]).DataBoundItem;
                    rows.Add(e.RowIndex, new RowInfo(){ Pozycja = pozycja, IloscLocal = pozycja.Ilosc });
                }
                var rinfo = rows[e.RowIndex];
                e.Value = rinfo.IloscLocal;
            }
        }

        private void grid_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= 0  && ilocsLocalIndex > -1 && e.ColumnIndex == ilocsLocalIndex && rows.ContainsKey(e.RowIndex))
            {
                var rinfo = rows[e.RowIndex];
                rinfo.IloscLocal = (double)e.Value;
            }

        }

        public class RowInfo
        {
            public Enova.API.Handel.PozycjaDokHandlowego Pozycja;
            public double IloscLocal;
            public double Roznica
            {
                get
                {
                    return IloscLocal - Pozycja.Ilosc;
                }
            }
        }
    }
}
