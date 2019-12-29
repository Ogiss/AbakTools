using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Enova.Business.Old.Zwroty;

namespace AbakTools.Handel.Forms
{
    public class PozycjeAnalizyZwrotuGrid : Enova.Business.Old.Controls.DataGrid
    {
        #region Fields

        private DokumentyAnalizyZwrotu dokumenty;
        private List<DataGridViewCheckBoxColumn> kolumnyDokumentow;

        #endregion

        #region Methods

        public void ClearDokumenty()
        {
            if (kolumnyDokumentow != null)
            {
                foreach (var col in this.kolumnyDokumentow)
                    this.Columns.Remove(col);
                this.kolumnyDokumentow = null;
            }
        }

        public void SetDokumenty(DokumentyAnalizyZwrotu dokumenty)
        {
            if (this.kolumnyDokumentow == null)
                this.kolumnyDokumentow = new List<DataGridViewCheckBoxColumn>();
            else
            {
                foreach (var col in this.kolumnyDokumentow)
                    this.Columns.Remove(col);
                this.kolumnyDokumentow.Clear();
            }

            this.dokumenty = dokumenty;
            int idx = 0;
            foreach (var dokument in dokumenty)
            {
                DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                column.Name = "Dokument" + idx.ToString();
                column.HeaderText = dokument.NumerPelny;
                this.Columns.Add(column);
                this.kolumnyDokumentow.Add(column);

                idx++;
            }
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var row = this.Rows[e.RowIndex];
                PozycjaAnalizyZwrotu pozycja = (PozycjaAnalizyZwrotu)row.DataBoundItem;
                var column = this.Columns[e.ColumnIndex];
                if (column.Name.StartsWith("Dokument"))
                {
                    var dokument = pozycja.GetDokumentByName(column.Name);
                    if (dokument != null)
                    {
                        var iloscPoKorektach = dokument.GetIloscPoKorektach(pozycja);
                        Color backColor = Color.LightGray;
                        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                            backColor = this.DefaultCellStyle.SelectionBackColor;

                        Brush brush = new SolidBrush(backColor);

                        e.Graphics.FillRectangle(brush, e.CellBounds);
                        e.PaintContent(e.CellBounds);

                        if (iloscPoKorektach < pozycja.Ilosc)
                        {
                            int y = e.CellBounds.Y + (this.Font.Height / 2);
                            e.Graphics.DrawString(iloscPoKorektach.ToString(), Font, Brushes.Black, new PointF(e.CellBounds.X + 5, y));
                        }

                        e.Graphics.DrawLine(Pens.White, new Point(e.CellBounds.X, e.CellBounds.Y), new Point(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height));

                        if (e.RowIndex > 0)
                        {
                            e.Graphics.DrawLine(Pens.White, new Point(e.CellBounds.X, e.CellBounds.Y), new Point(e.CellBounds.X + e.CellBounds.Width, e.CellBounds.Y));
                        }

                        e.Handled = true;
                    }
                    else
                    {
                        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                        {
                            Color selectedBackColor = this.DefaultCellStyle.SelectionBackColor;
                            Brush brush = new SolidBrush(selectedBackColor);
                            e.Graphics.FillRectangle(brush, e.CellBounds);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
                        }
                        e.Graphics.DrawRectangle(Pens.LightGray, e.CellBounds);
                        e.Handled = true;

                    }
                }
                else if (column.Name.StartsWith("pozostalo"))
                {
                    if (pozycja.Dokumenty.Count == 0)
                    {
                        e.Graphics.FillRectangle(Brushes.Red, e.CellBounds);
                        e.PaintContent(e.CellBounds);
                        e.Handled = true;
                    }
                    else if (pozycja.PozostaloDoSkorygowania > 0)
                    {
                        e.Graphics.FillRectangle(Brushes.Orange, e.CellBounds);
                        e.PaintContent(e.CellBounds);
                        e.Handled = true;
                    }
                }
            }
        }

        protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
        {
            base.OnCellValueNeeded(e);

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var column = this.Columns[e.ColumnIndex];
                if (column.Name.StartsWith("Dokument"))
                {
                    var row = this.Rows[e.RowIndex];
                    PozycjaAnalizyZwrotu pozycja = (PozycjaAnalizyZwrotu)row.DataBoundItem;
                    e.Value = pozycja.GetHasDokumentKorygowany(column.Name);
                }
            }
        }

        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            base.OnCellValueChanged(e);
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var column = this.Columns[e.ColumnIndex];
                if (column.Name.StartsWith("Dokument"))
                {
                    var row = this.Rows[e.RowIndex];
                    var pozycja = (PozycjaAnalizyZwrotu)row.DataBoundItem;
                    var dokument = pozycja.GetDokumentByName(column.Name);
                    if (dokument != null)
                    {
                        bool val = (bool)row.Cells[e.ColumnIndex].EditedFormattedValue;
                        if (!dokument.SetKorygowany(pozycja, val, false))
                            this.CancelEdit();
                        this.Refresh();
                    }

                }
            }
        }

        protected override void OnCurrentCellDirtyStateChanged(EventArgs e)
        {
            var column = this.Columns[this.CurrentCell.ColumnIndex];
            if (column.Name.StartsWith("Dokument"))
            {
                this.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            base.OnCurrentCellDirtyStateChanged(e);
        }

        #endregion

    }
}
