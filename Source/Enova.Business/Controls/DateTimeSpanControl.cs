using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class DateTimeSpanControl : UserControl, BAL.Types.INotifyValueChanged
    {
        private DateTime dateFrom;
        private DateTime dateTo;
        private SpanType spanType;
        private TimeSpan timeSpan;
        public SpanType DefaultSpanType = SpanType.Month;

        private Dictionary<int, string> month2string = new Dictionary<int, string>()
        {
            {1, "Styczeń"},
            {2, "Luty"},
            {3, "Marzec"},
            {4, "Kwiecień"},
            {5, "Maj"},
            {6, "Czerwiec"},
            {7, "Lipiec"},
            {8, "Sierpień"},
            {9, "Wrzesień"},
            {10, "Październik"},
            {11, "Listopad"},
            {12, "Grudzień"}
        };

        private bool fireChangedEvent = false;

        public DateTimeSpanControl()
        {
            InitializeComponent();
            this.SetSpan(DefaultSpanType);
        }

        private void DateTimeSpanControl_Load(object sender, EventArgs e)
        {
            addYeards();
            addMonths();
            

            this.showSpan();
            this.fireChangedEvent = true;
        }

        private void addYeards()
        {
            int year = DateTime.Now.Year;
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add("Rok "+(year - 3).ToString());
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add("Rok "+(year - 2).ToString());
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add("Rok "+(year - 1).ToString());
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add("-");
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add("Rok " + (year + 1).ToString());
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add("Rok " + (year + 2).ToString());
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add("Rok " + (year + 3).ToString());
        }

        private void addMonths()
        {
            DateTime now = DateTime.Now;
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(-6).Month] + " " + now.AddMonths(-6).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(-5).Month] + " " + now.AddMonths(-5).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(-4).Month] + " " + now.AddMonths(-4).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(-3).Month] + " " + now.AddMonths(-3).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(-2).Month] + " " + now.AddMonths(-2).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(-1).Month] + " " + now.AddMonths(-1).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add("-");
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(1).Month] + " " + now.AddMonths(1).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(2).Month] + " " + now.AddMonths(2).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(3).Month] + " " + now.AddMonths(3).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(4).Month] + " " + now.AddMonths(4).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(5).Month] + " " + now.AddMonths(5).Year.ToString());
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(month2string[now.AddMonths(6).Month] + " " + now.AddMonths(6).Year.ToString());
        }

        private void showSpan()
        {
            this.textBox.Text = this.dateFrom.ToString("yyyy-MM-dd") + " ... " + this.dateTo.ToString("yyyy-MM-dd");
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = false;
                e.Handled = true;
                this.setPrevSpan();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = false;
                e.Handled = true;
                this.setNextSpan();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                this.parseSpan();
            }

        }

        private void parseSpan()
        {
            try
            {
                string[] parts = this.textBox.Text.Split(new string[] { "..." }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Count() != 2)
                {
                    this.showSpan();
                    return;
                }

                DateTime df = DateTime.Parse(parts[0].Trim());
                DateTime dt = DateTime.Parse(parts[1].Trim()).AddDays(1).AddSeconds(-1);

                if (df.Ticks > dt.Ticks)
                {
                    this.showSpan();
                    return;
                }

                this.dateFrom = df;
                this.dateTo = dt;
                this.timeSpan = dateTo - dateFrom;
                this.spanType = SpanType.AnySpan;
                this.OnChanged(null);

            }
            catch {
                this.showSpan();
            }
        }

        public void SetSpan(SpanType spanType)
        {
            this.SetSpan(spanType, null, null, null);
        }

        public void SetSpan(SpanType spanType, int? year, int? month, int? day)
        {
            DateTime d = DateTime.Now;
            switch (spanType)
            {
                case SpanType.Day:
                    this.dateFrom = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                    this.dateTo = this.dateFrom.AddDays(1).AddSeconds(-1);
                    break;
                case SpanType.Week:
                    DayOfWeek dw = d.DayOfWeek;
                    this.dateFrom = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0).AddDays(-(int)dw);
                    this.dateTo = this.dateFrom.AddDays(7).AddSeconds(-1);
                    break;
                case SpanType.Month:
                    this.dateFrom = new DateTime(year == null ? d.Year : year.Value, month == null ? d.Month : month.Value, 1, 0, 0, 0);
                    this.dateTo = this.dateFrom.AddMonths(1).AddDays(-1).Date;
                    break;
                case SpanType.Year:
                    this.dateFrom = new DateTime(year == null ? d.Year : year.Value, 1, 1, 0, 0, 0);
                    this.dateTo = this.dateFrom.AddYears(1).AddSeconds(-1);
                    break;
                case SpanType.Full:
                    this.dateFrom = new DateTime(1979,1,1,0,0,0);
                    this.dateTo = new DateTime(2059,12,31,23,59,59);
                    break;
                case SpanType.AnyMonth:
                    this.dateFrom = new DateTime(d.Year,(int)month, 1, 0, 0, 0);
                    this.dateTo = this.dateFrom.AddMonths(1).AddSeconds(-1);
                    break;
            }

            this.spanType = spanType;
            this.showSpan();
            this.OnChanged(null);
        }

        private void setNextSpan()
        {
            switch (this.spanType)
            {
                case SpanType.Day:
                    this.dateFrom = this.dateFrom.AddDays(1);
                    this.dateTo = this.dateTo.AddDays(1);
                    break;
                case SpanType.Week:
                    this.dateFrom = this.dateFrom.AddDays(7);
                    this.dateTo = this.dateTo.AddDays(7);
                    break;
                case SpanType.Month:
                case SpanType.AnyMonth:
                    this.dateFrom = this.dateFrom.AddMonths(1);
                    this.dateTo = this.dateFrom.AddMonths(1).AddDays(-1);
                    break;
                case SpanType.Year:
                case SpanType.AnyYear:
                    this.dateFrom = this.dateFrom.AddYears(1);
                    this.dateTo = this.dateTo.AddYears(1);
                    break;
                case SpanType.AnySpan:
                    this.dateFrom = this.dateFrom.AddTicks(this.timeSpan.Ticks);
                    this.dateTo = this.dateTo.AddTicks(this.timeSpan.Ticks);
                    break;
                default:
                    return;
            }
            this.showSpan();
            this.OnChanged(null);
        }

        private void setPrevSpan()
        {
            switch (this.spanType)
            {
                case SpanType.Day:
                    this.dateFrom = this.dateFrom.AddDays(-1);
                    this.dateTo = this.dateTo.AddDays(-1);
                    break;
                case SpanType.Week:
                    this.dateFrom = this.dateFrom.AddDays(-7);
                    this.dateTo = this.dateTo.AddDays(-7);
                    break;
                case SpanType.Month:
                case SpanType.AnyMonth:
                    this.dateFrom = this.dateFrom.AddMonths(-1);
                    this.dateTo = this.dateFrom.AddMonths(1).AddDays(-1);
                    break;
                case SpanType.Year:
                case SpanType.AnyYear:
                    this.dateFrom = this.dateFrom.AddYears(-1);
                    this.dateTo = this.dateTo.AddYears(-1);
                    break;
                case SpanType.AnySpan:
                    this.dateFrom = this.dateFrom.AddTicks(-this.timeSpan.Ticks);
                    this.dateTo = this.dateTo.AddTicks(-this.timeSpan.Ticks);
                    break;
                default:
                    return;
            }
            this.showSpan();
            this.OnChanged(null);
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            string item = ((ToolStripMenuItem)sender).Name.Replace("ToolStripMenuItem", "").ToLower();

            switch (item)
            {
                case "bieżącydzień":
                    this.SetSpan(SpanType.Day);
                    break;
                case "bieżącytydzień":
                    this.SetSpan(SpanType.Week);
                    break;
                case "bieżącymiesiąc":
                    this.SetSpan(SpanType.Month);
                    break;
                case "bieżącyrok":
                    this.SetSpan(SpanType.Year);
                    break;
                case "pełnyokres":
                    this.SetSpan(SpanType.Full);
                    break;
                case "styczeń":
                    this.SetSpan(SpanType.AnyMonth, null, 1, null);
                    break;
                case "luty":
                    this.SetSpan(SpanType.AnyMonth, null, 2, null);
                    break;
                case "marzec":
                    this.SetSpan(SpanType.AnyMonth, null, 3, null);
                    break;
                case "kwiecień":
                    this.SetSpan(SpanType.AnyMonth, null, 4, null);
                    break;
                case "maj":
                    this.SetSpan(SpanType.AnyMonth, null, 5, null);
                    break;
                case "czerwiec":
                    this.SetSpan(SpanType.AnyMonth, null, 6, null);
                    break;
                case "lipiec":
                    this.SetSpan(SpanType.AnyMonth, null, 7, null);
                    break;
                case "sierpień":
                    this.SetSpan(SpanType.AnyMonth, null, 8, null);
                    break;
                case "wrzesień":
                    this.SetSpan(SpanType.AnyMonth, null, 9, null);
                    break;
                case "październik":
                    this.SetSpan(SpanType.AnyMonth, null, 10, null);
                    break;
                case "listopad":
                    this.SetSpan(SpanType.AnyMonth, null, 11, null);
                    break;
                case "grudzień":
                    this.SetSpan(SpanType.AnyMonth, null, 12, null);
                    break;
            }
        }

        protected virtual void OnChanged(EventArgs e)
        {
            if (this.Changed != null && fireChangedEvent)
                this.Changed(this, e);
            if (this.ValueChanged != null && fireChangedEvent)
                this.ValueChanged(this, e);
        }

        public enum SpanType { Day, Week, Month, AnyMonth, Year, Full, AnyYear, AnySpan };

        [Browsable(true)]
        public DateTime DateFrom
        {
            get { return this.dateFrom; }
            set
            {
                this.dateFrom = value;
                this.showSpan();
                OnChanged(null);
            }
        }

        [Browsable(true)]
        public DateTime DateTo
        {
            get { return this.dateTo; }
            set
            {
                this.dateTo = value;
                this.showSpan();
                OnChanged(null);
            }
        }

        [Browsable(false)]
        public BAL.Types.DateFromTo Value
        {
            get { return new BAL.Types.DateFromTo(this.DateFrom, this.DateTo); }
        }

        [Browsable(true)]
        public event EventHandler Changed;
        [Browsable(false)]
        public event EventHandler ValueChanged;

        private void upButton_Click(object sender, EventArgs e)
        {
            setNextSpan();
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            setPrevSpan();
        }

        private void przejdźDoMiesiącaToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int year = int.Parse(e.ClickedItem.Text.Substring(e.ClickedItem.Text.Length - 4));
            string s = e.ClickedItem.Text.Substring(0, e.ClickedItem.Text.Length - 5);
            int month = month2string.Where(m => m.Value == s).First().Key;
            SetSpan(SpanType.Month, year, month, null);
        }

        private void przejdźDoRokuToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int year = int.Parse(e.ClickedItem.Text.Substring(4));
            SetSpan(SpanType.Year, year, null, null);
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {

        }

        private void kalendarzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateSpanCalendarForm form = new DateSpanCalendarForm();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                fireChangedEvent = false;
                DateFrom = form.DateFrom;
                DateTo = form.DateTo;
                fireChangedEvent = true;
                OnChanged(null);
            }
        }

        private bool customChanged = false;
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!customChanged)
                customChanged = true;
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (customChanged)
            {
                customChanged = false;
                parseSpan();
            }
        }

        private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }
    }
}
