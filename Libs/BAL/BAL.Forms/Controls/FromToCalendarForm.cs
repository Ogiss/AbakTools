using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms.Controls
{
    public partial class FromToCalendarForm : Form
    {
        public DateTime DateFrom
        {
            get
            {
                return fromCalendar.SelectionStart;
            }
            set
            {
                fromCalendar.SelectionStart = value;
                fromCalendar.SelectionEnd = value;
            }
        }

        public DateTime DateTo
        {
            get
            {
                return toCalendar.SelectionStart;
            }
            set
            {
                toCalendar.SelectionStart = value;
                toCalendar.SelectionEnd = value;
            }
        }

        public BAL.Types.DateFromTo Value
        {
            get
            {
                return new Types.DateFromTo(DateFrom, DateTo);
            }
            set
            {
                DateFrom = value.From;
                DateTo = value.To;
            }
        }

        public FromToCalendarForm()
        {
            InitializeComponent();
        }
    }
}
