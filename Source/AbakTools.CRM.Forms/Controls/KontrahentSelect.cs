using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AbakTools.CRM.Forms.Controls
{
    [ToolboxItem(false)]
    public class KontrahentSelect : BAL.Forms.Controls.SelectBox
    {

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Size = new System.Drawing.Size(175, 20);
            // 
            // KontrahentSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "KontrahentSelect";
            this.Size = new System.Drawing.Size(200, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
