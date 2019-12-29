using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbakTools.Web
{
    public partial class AttachmentsForm : Form
    {

        public List<System.Net.Mail.Attachment> Attachments { get; set; }

        public AttachmentsForm()
        {
            InitializeComponent();
            this.dataGridView.AutoGenerateColumns = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.bindingSource.DataSource = this.Attachments;
        }
    }
}
