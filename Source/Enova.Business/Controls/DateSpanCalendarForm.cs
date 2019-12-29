using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class DateSpanCalendarForm : Form
    {
        public DateSpanCalendarForm()
        {
            InitializeComponent();
        }

        public DateTime DateFrom
        {
            get
            {
                DateTime dt = dataOdCalendar.SelectionStart;
                return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            }
        }

        public DateTime DateTo
        {
            get
            {
                DateTime dt = dataDoCalendar.SelectionStart;
                return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
            }
        }
    }
}
