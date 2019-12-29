namespace Enova.Forms.Controls
{
    partial class PodmiotSelect
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.podmiotTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.podmiotComboBox = new System.Windows.Forms.ComboBox();
            this.podmiotBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.podmiotBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Typ podmiotu:";
            // 
            // podmiotTypeComboBox
            // 
            this.podmiotTypeComboBox.FormattingEnabled = true;
            this.podmiotTypeComboBox.Location = new System.Drawing.Point(84, 1);
            this.podmiotTypeComboBox.Name = "podmiotTypeComboBox";
            this.podmiotTypeComboBox.Size = new System.Drawing.Size(174, 21);
            this.podmiotTypeComboBox.TabIndex = 1;
            this.podmiotTypeComboBox.SelectionChangeCommitted += new System.EventHandler(this.podmiotTypeComboBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Podmiot:";
            // 
            // podmiotComboBox
            // 
            this.podmiotComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.podmiotComboBox.DataSource = this.podmiotBindingSource;
            this.podmiotComboBox.DisplayMember = "Kod";
            this.podmiotComboBox.FormattingEnabled = true;
            this.podmiotComboBox.Location = new System.Drawing.Point(328, 0);
            this.podmiotComboBox.Name = "podmiotComboBox";
            this.podmiotComboBox.Size = new System.Drawing.Size(282, 21);
            this.podmiotComboBox.TabIndex = 3;
            this.podmiotComboBox.ValueMember = "ID";
            // 
            // podmiotBindingSource
            // 
            this.podmiotBindingSource.DataSource = typeof(Enova.API.Core.IPodmiot);
            // 
            // PodmiotSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.podmiotComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.podmiotTypeComboBox);
            this.Controls.Add(this.label1);
            this.Name = "PodmiotSelect";
            this.Size = new System.Drawing.Size(610, 22);
            this.Load += new System.EventHandler(this.PodmiotSelectControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.podmiotBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox podmiotTypeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox podmiotComboBox;
        private System.Windows.Forms.BindingSource podmiotBindingSource;
    }
}
