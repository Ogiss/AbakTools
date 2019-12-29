namespace AbakTools.Analizy.Forms
{
    partial class AnalizaZwrotowWgGrupForm
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
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.grupyTowaroweComboBox = new System.Windows.Forms.ComboBox();
            this.grupyTowaroweBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpData1Do = new System.Windows.Forms.DateTimePicker();
            this.dtpData1Od = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpData2Do = new System.Windows.Forms.DateTimePicker();
            this.dtpData2Od = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tylkoPoZwotachCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.sortujWgComboBox = new System.Windows.Forms.ComboBox();
            this.sortujMalejacoCheckBox = new System.Windows.Forms.CheckBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.tylkoZObrotemICheckBox = new System.Windows.Forms.CheckBox();
            this.kontrahentSelect = new Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect();
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(1140, 10);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(105, 23);
            this.zatwierdzButton.TabIndex = 16;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // grupyTowaroweComboBox
            // 
            this.grupyTowaroweComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.grupyTowaroweComboBox.DataSource = this.grupyTowaroweBindingSource;
            this.grupyTowaroweComboBox.DisplayMember = "Name";
            this.grupyTowaroweComboBox.FormattingEnabled = true;
            this.grupyTowaroweComboBox.Location = new System.Drawing.Point(105, 12);
            this.grupyTowaroweComboBox.MaxDropDownItems = 20;
            this.grupyTowaroweComboBox.Name = "grupyTowaroweComboBox";
            this.grupyTowaroweComboBox.Size = new System.Drawing.Size(193, 21);
            this.grupyTowaroweComboBox.TabIndex = 12;
            this.grupyTowaroweComboBox.ValueMember = "ID";
            // 
            // grupyTowaroweBindingSource
            // 
            this.grupyTowaroweBindingSource.DataSource = typeof(Enova.API.Business.FeatureDefinition);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Grupa towarowa:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(257, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Data I do:";
            // 
            // dtpData1Do
            // 
            this.dtpData1Do.Location = new System.Drawing.Point(317, 39);
            this.dtpData1Do.Name = "dtpData1Do";
            this.dtpData1Do.Size = new System.Drawing.Size(136, 20);
            this.dtpData1Do.TabIndex = 19;
            // 
            // dtpData1Od
            // 
            this.dtpData1Od.Location = new System.Drawing.Point(105, 39);
            this.dtpData1Od.Name = "dtpData1Od";
            this.dtpData1Od.Size = new System.Drawing.Size(136, 20);
            this.dtpData1Od.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Data I od:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Data II do:";
            // 
            // dtpData2Do
            // 
            this.dtpData2Do.Location = new System.Drawing.Point(315, 65);
            this.dtpData2Do.Name = "dtpData2Do";
            this.dtpData2Do.Size = new System.Drawing.Size(136, 20);
            this.dtpData2Do.TabIndex = 23;
            // 
            // dtpData2Od
            // 
            this.dtpData2Od.Location = new System.Drawing.Point(103, 65);
            this.dtpData2Od.Name = "dtpData2Od";
            this.dtpData2Od.Size = new System.Drawing.Size(136, 20);
            this.dtpData2Od.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Data II od:";
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(14, 96);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1231, 561);
            this.reportViewer.TabIndex = 26;
            // 
            // tylkoPoZwotachCheckBox
            // 
            this.tylkoPoZwotachCheckBox.AutoSize = true;
            this.tylkoPoZwotachCheckBox.Location = new System.Drawing.Point(476, 73);
            this.tylkoPoZwotachCheckBox.Name = "tylkoPoZwotachCheckBox";
            this.tylkoPoZwotachCheckBox.Size = new System.Drawing.Size(113, 17);
            this.tylkoPoZwotachCheckBox.TabIndex = 27;
            this.tylkoPoZwotachCheckBox.Text = "Tylko po zwrotach";
            this.tylkoPoZwotachCheckBox.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(473, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Sortuj wg:";
            // 
            // sortujWgComboBox
            // 
            this.sortujWgComboBox.FormattingEnabled = true;
            this.sortujWgComboBox.Items.AddRange(new object[] {
            "Kod kontr.",
            "Obrót I",
            "Sprzedaż II",
            "Procent sprzedaży II",
            "Zwrot II",
            "Procent zwr. II",
            "Zwrot teor."});
            this.sortujWgComboBox.Location = new System.Drawing.Point(533, 40);
            this.sortujWgComboBox.Name = "sortujWgComboBox";
            this.sortujWgComboBox.Size = new System.Drawing.Size(206, 21);
            this.sortujWgComboBox.TabIndex = 29;
            // 
            // sortujMalejacoCheckBox
            // 
            this.sortujMalejacoCheckBox.AutoSize = true;
            this.sortujMalejacoCheckBox.Location = new System.Drawing.Point(758, 42);
            this.sortujMalejacoCheckBox.Name = "sortujMalejacoCheckBox";
            this.sortujMalejacoCheckBox.Size = new System.Drawing.Size(98, 17);
            this.sortujMalejacoCheckBox.TabIndex = 30;
            this.sortujMalejacoCheckBox.Text = "Sortuj malejaco";
            this.sortujMalejacoCheckBox.UseVisualStyleBackColor = true;
            // 
            // refreshButton
            // 
            this.refreshButton.Enabled = false;
            this.refreshButton.Location = new System.Drawing.Point(879, 40);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(105, 23);
            this.refreshButton.TabIndex = 31;
            this.refreshButton.Text = "Odśwież";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // tylkoZObrotemICheckBox
            // 
            this.tylkoZObrotemICheckBox.AutoSize = true;
            this.tylkoZObrotemICheckBox.Location = new System.Drawing.Point(595, 73);
            this.tylkoZObrotemICheckBox.Name = "tylkoZObrotemICheckBox";
            this.tylkoZObrotemICheckBox.Size = new System.Drawing.Size(107, 17);
            this.tylkoZObrotemICheckBox.TabIndex = 32;
            this.tylkoZObrotemICheckBox.Text = "Tylko z obrotem I";
            this.tylkoZObrotemICheckBox.UseVisualStyleBackColor = true;
            // 
            // kontrahentSelect
            // 
            this.kontrahentSelect.DataContext = null;
            this.kontrahentSelect.Location = new System.Drawing.Point(317, 11);
            this.kontrahentSelect.Name = "kontrahentSelect";
            this.kontrahentSelect.ReadOnly = false;
            this.kontrahentSelect.Size = new System.Drawing.Size(735, 22);
            this.kontrahentSelect.TabIndex = 33;
            // 
            // AnalizaZwrotowWgGrupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 676);
            this.Controls.Add(this.kontrahentSelect);
            this.Controls.Add(this.tylkoZObrotemICheckBox);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.sortujMalejacoCheckBox);
            this.Controls.Add(this.sortujWgComboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tylkoPoZwotachCheckBox);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpData2Do);
            this.Controls.Add(this.dtpData2Od);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpData1Do);
            this.Controls.Add(this.dtpData1Od);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.grupyTowaroweComboBox);
            this.Controls.Add(this.label1);
            this.Name = "AnalizaZwrotowWgGrupForm";
            this.Text = "Analiza zwrotów wg grup";
            this.Load += new System.EventHandler(this.AnalizaZwrotowWgGrup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button zatwierdzButton;
        private System.Windows.Forms.ComboBox grupyTowaroweComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpData1Do;
        private System.Windows.Forms.DateTimePicker dtpData1Od;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpData2Do;
        private System.Windows.Forms.DateTimePicker dtpData2Od;
        private System.Windows.Forms.Label label6;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource grupyTowaroweBindingSource;
        private System.Windows.Forms.CheckBox tylkoPoZwotachCheckBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox sortujWgComboBox;
        private System.Windows.Forms.CheckBox sortujMalejacoCheckBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.CheckBox tylkoZObrotemICheckBox;
        private Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect kontrahentSelect;
    }
}