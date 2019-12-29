namespace AbakTools.Analizy.Forms
{
    partial class FormularzWgGrupForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.grupyTowaroweComboBox = new System.Windows.Forms.ComboBox();
            this.grupyTowaroweBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.dtpSprzedazOd = new System.Windows.Forms.DateTimePicker();
            this.dtpSprzedazDo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpZwrotDo = new System.Windows.Forms.DateTimePicker();
            this.dtpZwrotOd = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.kontrahentSelect = new Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect();
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grupa towarowa:";
            // 
            // grupyTowaroweComboBox
            // 
            this.grupyTowaroweComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.grupyTowaroweComboBox.DataSource = this.grupyTowaroweBindingSource;
            this.grupyTowaroweComboBox.DisplayMember = "Name";
            this.grupyTowaroweComboBox.FormattingEnabled = true;
            this.grupyTowaroweComboBox.Location = new System.Drawing.Point(106, 6);
            this.grupyTowaroweComboBox.MaxDropDownItems = 20;
            this.grupyTowaroweComboBox.Name = "grupyTowaroweComboBox";
            this.grupyTowaroweComboBox.Size = new System.Drawing.Size(193, 21);
            this.grupyTowaroweComboBox.TabIndex = 1;
            this.grupyTowaroweComboBox.ValueMember = "ID";
            // 
            // grupyTowaroweBindingSource
            // 
            this.grupyTowaroweBindingSource.DataSource = typeof(Enova.API.Business.FeatureDefinition);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sprzedaż od:";
            // 
            // dtpSprzedazOd
            // 
            this.dtpSprzedazOd.Location = new System.Drawing.Point(408, 39);
            this.dtpSprzedazOd.Name = "dtpSprzedazOd";
            this.dtpSprzedazOd.Size = new System.Drawing.Size(136, 20);
            this.dtpSprzedazOd.TabIndex = 7;
            // 
            // dtpSprzedazDo
            // 
            this.dtpSprzedazDo.Location = new System.Drawing.Point(620, 39);
            this.dtpSprzedazDo.Name = "dtpSprzedazDo";
            this.dtpSprzedazDo.Size = new System.Drawing.Size(136, 20);
            this.dtpSprzedazDo.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(545, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Sprzedaż do:";
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(1046, 4);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(105, 23);
            this.zatwierdzButton.TabIndex = 10;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(12, 91);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1246, 558);
            this.reportViewer.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(562, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Zwrot do:";
            // 
            // dtpZwrotDo
            // 
            this.dtpZwrotDo.Location = new System.Drawing.Point(620, 65);
            this.dtpZwrotDo.Name = "dtpZwrotDo";
            this.dtpZwrotDo.Size = new System.Drawing.Size(136, 20);
            this.dtpZwrotDo.TabIndex = 14;
            // 
            // dtpZwrotOd
            // 
            this.dtpZwrotOd.Location = new System.Drawing.Point(408, 65);
            this.dtpZwrotOd.Name = "dtpZwrotOd";
            this.dtpZwrotOd.Size = new System.Drawing.Size(136, 20);
            this.dtpZwrotOd.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(350, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Zwrot od:";
            // 
            // kontrahentSelect
            // 
            this.kontrahentSelect.DataContext = null;
            this.kontrahentSelect.Location = new System.Drawing.Point(305, 6);
            this.kontrahentSelect.Name = "kontrahentSelect";
            this.kontrahentSelect.ReadOnly = false;
            this.kontrahentSelect.Size = new System.Drawing.Size(735, 22);
            this.kontrahentSelect.TabIndex = 16;
            // 
            // FormularzWgGrupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1270, 661);
            this.Controls.Add(this.kontrahentSelect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpZwrotDo);
            this.Controls.Add(this.dtpZwrotOd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpSprzedazDo);
            this.Controls.Add(this.dtpSprzedazOd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grupyTowaroweComboBox);
            this.Controls.Add(this.label1);
            this.Name = "FormularzWgGrupForm";
            this.Text = "Formularz według grup towarowych";
            this.Load += new System.EventHandler(this.FormularzWgGrupForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox grupyTowaroweComboBox;
        private System.Windows.Forms.BindingSource grupyTowaroweBindingSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpSprzedazOd;
        private System.Windows.Forms.DateTimePicker dtpSprzedazDo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button zatwierdzButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpZwrotDo;
        private System.Windows.Forms.DateTimePicker dtpZwrotOd;
        private System.Windows.Forms.Label label7;
        private Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect kontrahentSelect;
    }
}
