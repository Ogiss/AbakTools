using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Business.Forms.Controls
{
    public partial class DictionaryTextBox : BAL.Forms.Controls.TextBox
    {
        #region Properties

        [Browsable(true)]
        public string DictionaryCategory { get; set; }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        #endregion

        public DictionaryTextBox()
        {
            InitializeComponent();
        }

        protected override void OnAfterBinding(EventArgs e)
        {
            base.OnAfterBinding(e);
        }
    }
}
