namespace AbakTools.Finanse.Forms
{
    partial class RaportPrzeterminowanForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.przedstawicieleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rokTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.miesiąceComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.przedstawicieleComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.wszystkieCheckBox = new System.Windows.Forms.CheckBox();
            this.stareCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.przedstawicieleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // przedstawicieleBindingSource
            // 
            this.przedstawicieleBindingSource.DataSource = typeof(Enova.Business.Old.Types.PrzedstawicieleViewRow);
            // 
            // rokTextBox
            // 
            this.rokTextBox.Location = new System.Drawing.Point(483, 7);
            this.rokTextBox.Name = "rokTextBox";
            this.rokTextBox.Size = new System.Drawing.Size(91, 20);
            this.rokTextBox.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(447, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Rok:";
            // 
            // miesiąceComboBox
            // 
            this.miesiąceComboBox.FormattingEnabled = true;
            this.miesiąceComboBox.Items.AddRange(new object[] {
            "Styczeń",
            "Luty",
            "Marzec",
            "Kwiecień",
            "Maj",
            "Czerwiec",
            "Lipiec",
            "Sierpień",
            "Wrzesień",
            "Październik",
            "Listopad",
            "Grudzień"});
            this.miesiąceComboBox.Location = new System.Drawing.Point(310, 7);
            this.miesiąceComboBox.Name = "miesiąceComboBox";
            this.miesiąceComboBox.Size = new System.Drawing.Size(121, 21);
            this.miesiąceComboBox.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Miesiąc:";
            // 
            // przedstawicieleComboBox
            // 
            this.przedstawicieleComboBox.DataSource = this.przedstawicieleBindingSource;
            this.przedstawicieleComboBox.DisplayMember = "Nazwa";
            this.przedstawicieleComboBox.FormattingEnabled = true;
            this.przedstawicieleComboBox.Location = new System.Drawing.Point(94, 6);
            this.przedstawicieleComboBox.MaxDropDownItems = 20;
            this.przedstawicieleComboBox.Name = "przedstawicieleComboBox";
            this.przedstawicieleComboBox.Size = new System.Drawing.Size(130, 21);
            this.przedstawicieleComboBox.TabIndex = 28;
            this.przedstawicieleComboBox.ValueMember = "Nazwa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Przedstawiciel:";
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(777, 5);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(110, 23);
            this.zatwierdzButton.TabIndex = 33;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(12, 34);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1244, 559);
            this.reportViewer.TabIndex = 34;
            // 
            // wszystkieCheckBox
            // 
            this.wszystkieCheckBox.AutoSize = true;
            this.wszystkieCheckBox.Location = new System.Drawing.Point(594, 9);
            this.wszystkieCheckBox.Name = "wszystkieCheckBox";
            this.wszystkieCheckBox.Size = new System.Drawing.Size(74, 17);
            this.wszystkieCheckBox.TabIndex = 35;
            this.wszystkieCheckBox.Text = "Wszystkie";
            this.wszystkieCheckBox.UseVisualStyleBackColor = true;
            // 
            // stareCheckBox
            // 
            this.stareCheckBox.AutoSize = true;
            this.stareCheckBox.Checked = true;
            this.stareCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stareCheckBox.Location = new System.Drawing.Point(675, 9);
            this.stareCheckBox.Name = "stareCheckBox";
            this.stareCheckBox.Size = new System.Drawing.Size(51, 17);
            this.stareCheckBox.TabIndex = 36;
            this.stareCheckBox.Text = "Stare";
            this.stareCheckBox.UseVisualStyleBackColor = true;
            // 
            // RaportPrzeterminowanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 605);
            this.Controls.Add(this.stareCheckBox);
            this.Controls.Add(this.wszystkieCheckBox);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.rokTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.miesiąceComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.przedstawicieleComboBox);
            this.Controls.Add(this.label1);
            this.Name = "RaportPrzeterminowanForm";
            this.Text = "Raport przeterminowań";
            this.Load += new System.EventHandler(this.RaportPrzeterminowanForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.przedstawicieleBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource przedstawicieleBindingSource;
        private System.Windows.Forms.TextBox rokTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox miesiąceComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox przedstawicieleComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button zatwierdzButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.CheckBox wszystkieCheckBox;
        private System.Windows.Forms.CheckBox stareCheckBox;
    }
}