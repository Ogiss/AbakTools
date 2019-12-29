﻿namespace AbakTools.Analizy.Forms
{
    partial class CennikiForm
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
            this.grupaComboBox = new System.Windows.Forms.ComboBox();
            this.grupyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.podgrupaComboBox = new System.Windows.Forms.ComboBox();
            this.podgrupyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.grupyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.podgrupyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grupa:";
            // 
            // grupaComboBox
            // 
            this.grupaComboBox.DataSource = this.grupyBindingSource;
            this.grupaComboBox.DisplayMember = "Name";
            this.grupaComboBox.FormattingEnabled = true;
            this.grupaComboBox.Location = new System.Drawing.Point(57, 6);
            this.grupaComboBox.MaxDropDownItems = 25;
            this.grupaComboBox.Name = "grupaComboBox";
            this.grupaComboBox.Size = new System.Drawing.Size(179, 21);
            this.grupaComboBox.TabIndex = 1;
            this.grupaComboBox.ValueMember = "Name";
            this.grupaComboBox.SelectedIndexChanged += new System.EventHandler(this.grupaComboBox_SelectedIndexChanged);
            // 
            // grupyBindingSource
            // 
            this.grupyBindingSource.DataSource = typeof(Enova.API.Business.FeatureDefinition);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Podgrupa:";
            // 
            // podgrupaComboBox
            // 
            this.podgrupaComboBox.DataSource = this.podgrupyBindingSource;
            this.podgrupaComboBox.DisplayMember = "Value";
            this.podgrupaComboBox.FormattingEnabled = true;
            this.podgrupaComboBox.Location = new System.Drawing.Point(304, 6);
            this.podgrupaComboBox.MaxDropDownItems = 25;
            this.podgrupaComboBox.Name = "podgrupaComboBox";
            this.podgrupaComboBox.Size = new System.Drawing.Size(179, 21);
            this.podgrupaComboBox.TabIndex = 3;
            this.podgrupaComboBox.ValueMember = "Value";
            // 
            // podgrupyBindingSource
            // 
            this.podgrupyBindingSource.DataSource = typeof(Enova.API.Business.DictionaryItem);
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Location = new System.Drawing.Point(489, 4);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(108, 23);
            this.zatwierdzButton.TabIndex = 4;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer.Location = new System.Drawing.Point(12, 33);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1216, 566);
            this.reportViewer.TabIndex = 5;
            // 
            // CennikiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 622);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.podgrupaComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grupaComboBox);
            this.Controls.Add(this.label1);
            this.Name = "CennikiForm";
            this.Text = "CennikiForm";
            this.Load += new System.EventHandler(this.CennikiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grupyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.podgrupyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox grupaComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox podgrupaComboBox;
        private System.Windows.Forms.Button zatwierdzButton;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource grupyBindingSource;
        private System.Windows.Forms.BindingSource podgrupyBindingSource;
    }
}