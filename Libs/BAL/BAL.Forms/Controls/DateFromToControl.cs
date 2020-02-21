using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Extensions;


namespace BAL.Forms.Controls
{
    public partial class DateFromToControl : BAL.Forms.Controls.BALControl
    {
        #region Fields

        private bool blockEvents;
        private BAL.Types.DateFromTo value;
        private FromToType type;

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

        #endregion

        #region Properties

        public BAL.Types.DateFromTo FromTo
        {
            get { return this.value; }
        }

        public override object Value
        {
            get
            {
                return this.value;
            }
        }

        #endregion

        #region Methods

        public DateFromToControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (value == null)
            {
                var n = DateTime.Now;
                var from = new DateTime(n.Year, n.Month, 1);
                var to = from.AddMonths(1).AddDays(-1);
                blockEvents = true;
                this.SetValue(new BAL.Types.DateFromTo(from, to), FromToType.Month);
                blockEvents = false;

            }
            addYeards();
            addMonths();
            showFromTo();
            base.OnLoad(e);

        }


        public void SetValue(BAL.Types.DateFromTo fromTo, FromToType type)
        {
            var args = new CancelWithDataEventArgs(fromTo);
            OnValueChanging(args);
            if (!args.Cancel)
            {
                this.value = fromTo;
                this.type = type;
                showFromTo();
                if (!blockEvents)
                    OnValueChanged(new EventArgs());
            }
        }

        public void SetValue(DateTime from, FromToType type)
        {
            switch (type)
            {
                case FromToType.Day:
                    SetValue(new Types.DateFromTo(from, from), FromToType.Day);
                    break;
                case FromToType.Week:
                    break;
                case FromToType.Month:
                    from = new DateTime(from.Year, from.Month, 1);
                    SetValue(new Types.DateFromTo(from, from.AddMonths(1).AddDays(-1)), FromToType.Month);
                    break;
                case FromToType.Year:
                    SetValue(new Types.DateFromTo(new DateTime(from.Year, 1, 1), new DateTime(from.Year, 12, 31)), FromToType.Year);
                    break;
                case FromToType.FullTime:
                    SetValue(new Types.DateFromTo(DateTime.MinValue, DateTime.MaxValue), FromToType.FullTime);
                    break;
            }
        }

        public void SetValue(FromToType type)
        {
            SetValue(DateTime.Now.Date, type);
        }

        private void showFromTo()
        {
            if (this.value != null)
                this.textBox.Text = this.value.ToString();
        }

        private void addYeards()
        {
            int year = DateTime.Now.Year;
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem("Rok " + (year - 3).ToString()) { Tag = "9;-3" });
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem("Rok " + (year - 2).ToString()) { Tag = "9;-2" });
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem("Rok " + (year - 1).ToString()) { Tag = "9;-1" });
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add("-");
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem("Rok " + (year + 1).ToString()) { Tag = "9;1" });
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem("Rok " + (year + 2).ToString()) { Tag = "9;2" });
            przejdźDoRokuToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem("Rok " + (year + 3).ToString()) { Tag = "9;3" });
        }

        private void addMonths()
        {
            DateTime now = DateTime.Now;
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(-6).Month] + " " + now.AddMonths(-6).Year.ToString()) { Tag = "8;-6" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(-5).Month] + " " + now.AddMonths(-5).Year.ToString()) { Tag = "8;-5" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(-4).Month] + " " + now.AddMonths(-4).Year.ToString()) { Tag = "8;-4" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(-3).Month] + " " + now.AddMonths(-3).Year.ToString()) { Tag = "8;-3" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(-2).Month] + " " + now.AddMonths(-2).Year.ToString()) { Tag = "8;-2" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(-1).Month] + " " + now.AddMonths(-1).Year.ToString()) { Tag = "8;-1" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add("-");
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(1).Month] + " " + now.AddMonths(1).Year.ToString()) { Tag = "8;1" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(2).Month] + " " + now.AddMonths(2).Year.ToString()) { Tag = "8;2" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(3).Month] + " " + now.AddMonths(3).Year.ToString()) { Tag = "8;3" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(4).Month] + " " + now.AddMonths(4).Year.ToString()) { Tag = "8;4" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(5).Month] + " " + now.AddMonths(5).Year.ToString()) { Tag = "8;5" });
            przejdźDoMiesiącaToolStripMenuItem.DropDownItems.Add(
                new ToolStripMenuItem(month2string[now.AddMonths(6).Month] + " " + now.AddMonths(6).Year.ToString()) { Tag = "8;6" });
        }

        public void SetNext()
        {
            switch (this.type)
            {
                case FromToType.Day:
                    SetValue(value.From.AddDays(1), FromToType.Day);
                    break;
                case FromToType.Week:
                    SetValue(value.From.AddDays(7), FromToType.Week); 
                    break;
                case FromToType.Month:
                    SetValue(value.From.FirstDayOfMonth().AddMonths(1), FromToType.Month);
                    break;
                case FromToType.Year:
                    SetValue(value.From.FirstDayOfYear().AddYears(1), FromToType.Year);
                    break;
                case FromToType.FromTo:
                    SetValue(value.AddDays(value.DaysInterval), FromToType.FromTo);
                    break;
            }
        }

        public void SetPrev()
        {
            switch (this.type)
            {
                case FromToType.Day:
                    SetValue(value.From.AddDays(-1), FromToType.Day);
                    break;
                case FromToType.Week:
                    SetValue(value.From.AddDays(-7), FromToType.Week);
                    break;
                case FromToType.Month:
                    SetValue(value.From.FirstDayOfMonth().AddMonths(-1), FromToType.Month);
                    break;
                case FromToType.Year:
                    SetValue(value.From.FirstDayOfYear().AddYears(-1), FromToType.Year);
                    break;
                case FromToType.FromTo:
                    SetValue(value.AddDays(-value.DaysInterval), FromToType.FromTo);
                    break;
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            SetNext();
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            SetPrev();
        }

        private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var item = e.ClickedItem;
            if (item.Tag != null && item.Tag.ToString() != "")
            {
                var from = DateTime.Now;
                var parts = item.Tag.ToString().Split(';');
                var action = (MenuAction)int.Parse(parts[0]);
                int opt = parts.Length == 2 ? int.Parse(parts[1]) : 0;
                switch (action)
                {
                    case MenuAction.Calendar:

                        var form = new FromToCalendarForm();
                        form.Value = this.value;
                        //var p = this.FindForm().PointToScreen(this.Location);
                        var p = this.PointToScreen(Point.Empty);
                        form.Location = new Point(p.X, p.Y + this.Size.Height);
                        if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                            SetValue(form.Value, FromToType.FromTo);
                        
                        break;
                    case MenuAction.CurrentDay:
                        SetValue(FromToType.Day);
                        break;
                    case MenuAction.CurrentWeek:
                        SetValue(FromToType.Week);
                        break;
                    case MenuAction.CurrentMonth:
                        SetValue(FromToType.Month);
                        break;
                    case MenuAction.AnyMonth:
                        from = new DateTime(from.Year, opt, 1);
                        SetValue(new Types.DateFromTo(from, from.AddMonths(1).AddDays(-1)), FromToType.Month);
                        break;
                    case MenuAction.CurrentYear:
                        SetValue(FromToType.Year);
                        break;
                    case MenuAction.FullTime:
                        SetValue(FromToType.FullTime);
                        break;
                    case MenuAction.GoToMonth:
                        from = new DateTime(from.Year, from.Month, 1).AddMonths(opt);
                        SetValue(new Types.DateFromTo(from, from.AddMonths(1).AddDays(-1)), FromToType.Month);
                        break;
                    case MenuAction.GoToYear:
                        from = new DateTime(from.Year, 1, 1).AddYears(opt);
                        SetValue(new Types.DateFromTo(from, from.AddYears(1).AddDays(-1)), FromToType.Year);
                        break;
                }
            }

        }


        #endregion

        #region Nested Types

        public enum FromToType
        {
            None,
            Day,
            Week,
            Month,
            Year,
            FromTo,
            FullTime
        }

        public enum MenuAction
        {
            Unknow       = 0,
            Calendar     = 1,
            CurrentDay   = 2,
            CurrentWeek  = 3,
            CurrentMonth = 4,
            AnyMonth     = 5,
            CurrentYear  = 6,
            FullTime     = 7,
            GoToMonth    = 8,
            GoToYear     = 9
        }

        #endregion



    }
}
