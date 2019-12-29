namespace AbakTools.Finanse.Forms
{
    partial class RozliczeniaForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.zatwierdźButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.saldoCheckBox = new System.Windows.Forms.CheckBox();
            this.saldoKoncoweCheckBox = new System.Windows.Forms.CheckBox();
            this.podmiotSelect = new Enova.Forms.Controls.PodmiotSelect();
            this.dateFromTo = new BAL.Forms.Controls.DateFromToControl();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(643, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Okres:";
            // 
            // zatwierdźButton
            // 
            this.zatwierdźButton.Location = new System.Drawing.Point(1215, 4);
            this.zatwierdźButton.Name = "zatwierdźButton";
            this.zatwierdźButton.Size = new System.Drawing.Size(75, 23);
            this.zatwierdźButton.TabIndex = 4;
            this.zatwierdźButton.Text = "Zatwierdź";
            this.zatwierdźButton.UseVisualStyleBackColor = true;
            this.zatwierdźButton.Click += new System.EventHandler(this.zatwierdźButton_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(12, 33);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1278, 742);
            this.reportViewer.TabIndex = 5;
            // 
            // saldoCheckBox
            // 
            this.saldoCheckBox.AutoSize = true;
            this.saldoCheckBox.Location = new System.Drawing.Point(898, 9);
            this.saldoCheckBox.Name = "saldoCheckBox";
            this.saldoCheckBox.Size = new System.Drawing.Size(137, 17);
            this.saldoCheckBox.TabIndex = 7;
            this.saldoCheckBox.Text = "Z saldem początkowym";
            this.saldoCheckBox.UseVisualStyleBackColor = true;
            // 
            // saldoKoncoweCheckBox
            // 
            this.saldoKoncoweCheckBox.AutoSize = true;
            this.saldoKoncoweCheckBox.Location = new System.Drawing.Point(1053, 9);
            this.saldoKoncoweCheckBox.Name = "saldoKoncoweCheckBox";
            this.saldoKoncoweCheckBox.Size = new System.Drawing.Size(123, 17);
            this.saldoKoncoweCheckBox.TabIndex = 8;
            this.saldoKoncoweCheckBox.Text = "Z saldem końcowym";
            this.saldoKoncoweCheckBox.UseVisualStyleBackColor = true;
            // 
            // podmiotSelect
            // 
            this.podmiotSelect.DataContext = null;
            this.podmiotSelect.Location = new System.Drawing.Point(12, 6);
            this.podmiotSelect.Name = "podmiotSelect";
            this.podmiotSelect.ReadOnly = false;
            this.podmiotSelect.Size = new System.Drawing.Size(610, 22);
            this.podmiotSelect.TabIndex = 9;
            this.podmiotSelect.TypPodmiotu = Enova.API.Core.TypPodmiotu.NieOkreślony;
            // 
            // dateFromTo
            // 
            this.dateFromTo.DataContext = null;
            this.dateFromTo.Location = new System.Drawing.Point(687, 4);
            this.dateFromTo.Name = "dateFromTo";
            this.dateFromTo.ReadOnly = false;
            this.dateFromTo.Size = new System.Drawing.Size(173, 24);
            this.dateFromTo.TabIndex = 10;
            // 
            // RozliczeniaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 787);
            this.Controls.Add(this.dateFromTo);
            this.Controls.Add(this.podmiotSelect);
            this.Controls.Add(this.saldoKoncoweCheckBox);
            this.Controls.Add(this.saldoCheckBox);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdźButton);
            this.Controls.Add(this.label2);
            this.Name = "RozliczeniaForm";
            this.Text = "Rozliczenia";
            this.ResizeEnd += new System.EventHandler(this.RozliczeniaForm_ResizeEnd);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button zatwierdźButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.CheckBox saldoCheckBox;
        private System.Windows.Forms.CheckBox saldoKoncoweCheckBox;
        private Enova.Forms.Controls.PodmiotSelect podmiotSelect;
        private BAL.Forms.Controls.DateFromToControl dateFromTo;
    }
}