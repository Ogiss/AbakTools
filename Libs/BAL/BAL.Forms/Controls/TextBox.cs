using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms.Controls
{
    [DefaultBindingProperty("Text")]
    public partial class TextBox : BAL.Forms.Controls.BALControl
    {
        public override string Text
        {
            get
            {
                return innerTextBox.Text;
            }
            set
            {
                innerTextBox.Text = value;
            }
        }

        public TextBox()
        {
            InitializeComponent();
        }
    }
}
