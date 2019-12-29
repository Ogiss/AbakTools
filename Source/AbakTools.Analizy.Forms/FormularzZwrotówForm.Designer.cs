namespace AbakTools.Analizy.Forms
{
    /*
    partial class FormularzZwrotówForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.dataDoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dataOdDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.kontrahenciComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.przedstawicieleComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grupyTowaroweComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grupyTowaroweBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.przedstawicieleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kontrahenciBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.przedstawicieleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kontrahenciBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(864, 10);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(105, 23);
            this.zatwierdzButton.TabIndex = 21;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(563, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Data do:";
            // 
            // dataDoDateTimePicker
            // 
            this.dataDoDateTimePicker.Location = new System.Drawing.Point(618, 45);
            this.dataDoDateTimePicker.Name = "dataDoDateTimePicker";
            this.dataDoDateTimePicker.Size = new System.Drawing.Size(136, 20);
            this.dataDoDateTimePicker.TabIndex = 19;
            // 
            // dataOdDateTimePicker
            // 
            this.dataOdDateTimePicker.Location = new System.Drawing.Point(406, 45);
            this.dataOdDateTimePicker.Name = "dataOdDateTimePicker";
            this.dataOdDateTimePicker.Size = new System.Drawing.Size(136, 20);
            this.dataOdDateTimePicker.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Data od:";
            // 
            // kontrahenciComboBox
            // 
            this.kontrahenciComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.kontrahenciComboBox.DataSource = this.kontrahenciBindingSource;
            this.kontrahenciComboBox.DisplayMember = "Kod";
            this.kontrahenciComboBox.FormattingEnabled = true;
            this.kontrahenciComboBox.Location = new System.Drawing.Point(618, 12);
            this.kontrahenciComboBox.Name = "kontrahenciComboBox";
            this.kontrahenciComboBox.Size = new System.Drawing.Size(224, 21);
            this.kontrahenciComboBox.TabIndex = 16;
            this.kontrahenciComboBox.ValueMember = "ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(549, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Kontrahent:";
            // 
            // przedstawicieleComboBox
            // 
            this.przedstawicieleComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.przedstawicieleComboBox.DataSource = this.przedstawicieleBindingSource;
            this.przedstawicieleComboBox.DisplayMember = "Nazwa";
            this.przedstawicieleComboBox.FormattingEnabled = true;
            this.przedstawicieleComboBox.Location = new System.Drawing.Point(406, 12);
            this.przedstawicieleComboBox.MaxDropDownItems = 20;
            this.przedstawicieleComboBox.Name = "przedstawicieleComboBox";
            this.przedstawicieleComboBox.Size = new System.Drawing.Size(136, 21);
            this.przedstawicieleComboBox.TabIndex = 14;
            this.przedstawicieleComboBox.ValueMember = "Nazwa";
            this.przedstawicieleComboBox.SelectedIndexChanged += new System.EventHandler(this.przedstawicieleComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Przedstawiciel:";
            // 
            // grupyTowaroweComboBox
            // 
            this.grupyTowaroweComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.grupyTowaroweComboBox.DataSource = this.grupyTowaroweBindingSource;
            this.grupyTowaroweComboBox.DisplayMember = "Nazwa";
            this.grupyTowaroweComboBox.FormattingEnabled = true;
            this.grupyTowaroweComboBox.Location = new System.Drawing.Point(104, 12);
            this.grupyTowaroweComboBox.MaxDropDownItems = 20;
            this.grupyTowaroweComboBox.Name = "grupyTowaroweComboBox";
            this.grupyTowaroweComboBox.Size = new System.Drawing.Size(193, 21);
            this.grupyTowaroweComboBox.TabIndex = 12;
            this.grupyTowaroweComboBox.ValueMember = "Nazwa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Grupa towarowa:";
            // 
            // grupyTowaroweBindingSource
            // 
            this.grupyTowaroweBindingSource.DataSource = typeof(Enova.Business.Old.Types.GrupyTowaroweViewRow);
            // 
            // przedstawicieleBindingSource
            // 
            this.przedstawicieleBindingSource.DataSource = typeof(Enova.Business.Old.Types.PrzedstawicieleViewRow);
            // 
            // kontrahenciBindingSource
            // 
            this.kontrahenciBindingSource.DataSource = typeof(Enova.Business.Old.DB.Kontrahent);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(12, 71);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1187, 506);
            this.reportViewer.TabIndex = 22;
            // 
            // FormularzZwrotówForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 589);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataDoDateTimePicker);
            this.Controls.Add(this.dataOdDateTimePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.kontrahenciComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.przedstawicieleComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grupyTowaroweComboBox);
            this.Controls.Add(this.label1);
            this.Name = "FormularzZwrotówForm";
            this.Text = "Formularz Zwrotów";
            this.Load += new System.EventHandler(this.FormularzZwrotówForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grupyTowaroweBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.przedstawicieleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kontrahenciBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button zatwierdzButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dataDoDateTimePicker;
        private System.Windows.Forms.DateTimePicker dataOdDateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox kontrahenciComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox przedstawicieleComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox grupyTowaroweComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource kontrahenciBindingSource;
        private System.Windows.Forms.BindingSource przedstawicieleBindingSource;
        private System.Windows.Forms.BindingSource grupyTowaroweBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
    */
}