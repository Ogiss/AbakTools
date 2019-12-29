using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Forms
{
    public partial class WprowadzOkresForm : Form
    {
        #region Fields

        private bool setDates;

        #endregion

        #region Properties

        public DateTime DateFrom
        {
            get { return dateFromTimePicker.Value; }
            set
            {
                dateFromTimePicker.Value = value;
                setDates = true;
            }
        }

        public DateTime DateTo
        {
            get { return dateToTimePicker.Value; }
            set
            {
                dateToTimePicker.Value = value;
                setDates = true;
            }
        }

        #endregion

        public WprowadzOkresForm()
        {
            InitializeComponent();
        }

        private void WprowadzOkresForm_Load(object sender, EventArgs e)
        {
            if (!setDates)
            {
                var n = DateTime.Now;
                dateFromTimePicker.Value = new DateTime(n.Year, n.Month, 1);
                dateToTimePicker.Value = dateFromTimePicker.Value.Date.AddMonths(1).AddDays(-1);
            }
        }
    }
}
