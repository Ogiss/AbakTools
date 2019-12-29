namespace Enova.Business.Old.Controls.Design
{
    partial class ExtDataGridAddColumnDialog
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
            this.dataBoundColumnRadioButton = new System.Windows.Forms.RadioButton();
            this.dataColumns = new System.Windows.Forms.ListBox();
            this.unboundColumnRadioButton = new System.Windows.Forms.RadioButton();
            this.nameLabel = new System.Windows.Forms.Label();
            this.typelabel = new System.Windows.Forms.Label();
            this.headerTextLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.columnTypesCombo = new System.Windows.Forms.ComboBox();
            this.headerTextBox = new System.Windows.Forms.TextBox();
            this.visibleCheckBox = new System.Windows.Forms.CheckBox();
            this.readOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.frozenCheckBox = new System.Windows.Forms.CheckBox();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dataBoundColumnRadioButton
            // 
            this.dataBoundColumnRadioButton.AutoSize = true;
            this.dataBoundColumnRadioButton.Checked = true;
            this.dataBoundColumnRadioButton.Location = new System.Drawing.Point(13, 13);
            this.dataBoundColumnRadioButton.Name = "dataBoundColumnRadioButton";
            this.dataBoundColumnRadioButton.Size = new System.Drawing.Size(157, 17);
            this.dataBoundColumnRadioButton.TabIndex = 0;
            this.dataBoundColumnRadioButton.TabStop = true;
            this.dataBoundColumnRadioButton.Text = "Kolumna związana z danymi";
            this.dataBoundColumnRadioButton.UseVisualStyleBackColor = true;
            this.dataBoundColumnRadioButton.CheckedChanged += new System.EventHandler(this.dataBoundColumnRadioButton_CheckedChanged);
            // 
            // dataColumns
            // 
            this.dataColumns.FormattingEnabled = true;
            this.dataColumns.Location = new System.Drawing.Point(13, 37);
            this.dataColumns.Name = "dataColumns";
            this.dataColumns.Size = new System.Drawing.Size(353, 134);
            this.dataColumns.TabIndex = 1;
            this.dataColumns.SelectedIndexChanged += new System.EventHandler(this.dataColumns_SelectedIndexChanged);
            // 
            // unboundColumnRadioButton
            // 
            this.unboundColumnRadioButton.AutoSize = true;
            this.unboundColumnRadioButton.Location = new System.Drawing.Point(13, 178);
            this.unboundColumnRadioButton.Name = "unboundColumnRadioButton";
            this.unboundColumnRadioButton.Size = new System.Drawing.Size(171, 17);
            this.unboundColumnRadioButton.TabIndex = 2;
            this.unboundColumnRadioButton.Text = "Kolumna niezwiązana z danymi";
            this.unboundColumnRadioButton.UseVisualStyleBackColor = true;
            this.unboundColumnRadioButton.CheckedChanged += new System.EventHandler(this.unboundColumnRadioButton_CheckedChanged);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(19, 204);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(43, 13);
            this.nameLabel.TabIndex = 3;
            this.nameLabel.Text = "Nazwa:";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typelabel
            // 
            this.typelabel.AutoSize = true;
            this.typelabel.Location = new System.Drawing.Point(34, 230);
            this.typelabel.Name = "typelabel";
            this.typelabel.Size = new System.Drawing.Size(28, 13);
            this.typelabel.TabIndex = 4;
            this.typelabel.Text = "Typ:";
            // 
            // headerTextLabel
            // 
            this.headerTextLabel.AutoSize = true;
            this.headerTextLabel.Location = new System.Drawing.Point(2, 257);
            this.headerTextLabel.Name = "headerTextLabel";
            this.headerTextLabel.Size = new System.Drawing.Size(60, 13);
            this.headerTextLabel.TabIndex = 5;
            this.headerTextLabel.Text = "Nagłówek:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(68, 201);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(298, 20);
            this.nameTextBox.TabIndex = 6;
            this.nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nameTextBox_Validating);
            // 
            // columnTypesCombo
            // 
            this.columnTypesCombo.FormattingEnabled = true;
            this.columnTypesCombo.Location = new System.Drawing.Point(68, 227);
            this.columnTypesCombo.Name = "columnTypesCombo";
            this.columnTypesCombo.Size = new System.Drawing.Size(298, 21);
            this.columnTypesCombo.TabIndex = 7;
            // 
            // headerTextBox
            // 
            this.headerTextBox.Location = new System.Drawing.Point(68, 254);
            this.headerTextBox.Name = "headerTextBox";
            this.headerTextBox.Size = new System.Drawing.Size(298, 20);
            this.headerTextBox.TabIndex = 8;
            // 
            // visibleCheckBox
            // 
            this.visibleCheckBox.AutoSize = true;
            this.visibleCheckBox.Location = new System.Drawing.Point(68, 280);
            this.visibleCheckBox.Name = "visibleCheckBox";
            this.visibleCheckBox.Size = new System.Drawing.Size(74, 17);
            this.visibleCheckBox.TabIndex = 9;
            this.visibleCheckBox.Text = "Widoczna";
            this.visibleCheckBox.UseVisualStyleBackColor = true;
            // 
            // readOnlyCheckBox
            // 
            this.readOnlyCheckBox.AutoSize = true;
            this.readOnlyCheckBox.Location = new System.Drawing.Point(148, 280);
            this.readOnlyCheckBox.Name = "readOnlyCheckBox";
            this.readOnlyCheckBox.Size = new System.Drawing.Size(107, 17);
            this.readOnlyCheckBox.TabIndex = 10;
            this.readOnlyCheckBox.Text = "Tylko do odczytu";
            this.readOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // frozenCheckBox
            // 
            this.frozenCheckBox.AutoSize = true;
            this.frozenCheckBox.Location = new System.Drawing.Point(261, 280);
            this.frozenCheckBox.Name = "frozenCheckBox";
            this.frozenCheckBox.Size = new System.Drawing.Size(79, 17);
            this.frozenCheckBox.TabIndex = 11;
            this.frozenCheckBox.Text = "Zamrożona";
            this.frozenCheckBox.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.addButton.Location = new System.Drawing.Point(210, 314);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "Dodaj";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(291, 314);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ExtDataGridAddColumnDialog
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(384, 345);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.frozenCheckBox);
            this.Controls.Add(this.readOnlyCheckBox);
            this.Controls.Add(this.visibleCheckBox);
            this.Controls.Add(this.headerTextBox);
            this.Controls.Add(this.columnTypesCombo);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.headerTextLabel);
            this.Controls.Add(this.typelabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.unboundColumnRadioButton);
            this.Controls.Add(this.dataColumns);
            this.Controls.Add(this.dataBoundColumnRadioButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtDataGridAddColumnDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj kolumnę";
            this.Load += new System.EventHandler(this.ExtDataGridAddColumnDialog_Load);
            this.VisibleChanged += new System.EventHandler(this.ExtDataGridAddColumnDialog_VisibleChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExtDataGridAddColumnDialog_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton dataBoundColumnRadioButton;
        private System.Windows.Forms.ListBox dataColumns;
        private System.Windows.Forms.RadioButton unboundColumnRadioButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label typelabel;
        private System.Windows.Forms.Label headerTextLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox columnTypesCombo;
        private System.Windows.Forms.TextBox headerTextBox;
        private System.Windows.Forms.CheckBox visibleCheckBox;
        private System.Windows.Forms.CheckBox readOnlyCheckBox;
        private System.Windows.Forms.CheckBox frozenCheckBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;

    }
}