namespace AbakTools.Zwroty.Forms
{
    partial class AnalizaZwrotuEditForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pozycjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.wystawButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.analizujOdDTPicker = new System.Windows.Forms.DateTimePicker();
            this.pozycjeAnalizyZwrotuGrid = new AbakTools.Handel.Forms.PozycjeAnalizyZwrotuGrid();
            this.identColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pozostaloColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.analizujButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeAnalizyZwrotuGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zwrot:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Zwrot", true));
            this.textBox1.Location = new System.Drawing.Point(56, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(144, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(Enova.Business.Old.Zwroty.AnalizaZwrotu);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kontrahent:";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Kontrahent", true));
            this.textBox2.Location = new System.Drawing.Point(298, 10);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(500, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.TabStop = false;
            // 
            // pozycjeBindingSource
            // 
            this.pozycjeBindingSource.DataSource = typeof(Enova.Business.Old.Zwroty.PozycjaAnalizyZwrotu);
            // 
            // wystawButton
            // 
            this.wystawButton.Location = new System.Drawing.Point(804, 8);
            this.wystawButton.Name = "wystawButton";
            this.wystawButton.Size = new System.Drawing.Size(118, 23);
            this.wystawButton.TabIndex = 5;
            this.wystawButton.Text = "Wystaw korekty";
            this.wystawButton.UseVisualStyleBackColor = true;
            this.wystawButton.Click += new System.EventHandler(this.wystawButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Analizuj od:";
            // 
            // analizujOdDTPicker
            // 
            this.analizujOdDTPicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "AnalizujOd", true));
            this.analizujOdDTPicker.Location = new System.Drawing.Point(80, 37);
            this.analizujOdDTPicker.Name = "analizujOdDTPicker";
            this.analizujOdDTPicker.Size = new System.Drawing.Size(139, 20);
            this.analizujOdDTPicker.TabIndex = 7;
            // 
            // pozycjeAnalizyZwrotuGrid
            // 
            this.pozycjeAnalizyZwrotuGrid.AllowUserToAddRows = false;
            this.pozycjeAnalizyZwrotuGrid.AllowUserToDeleteRows = false;
            this.pozycjeAnalizyZwrotuGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pozycjeAnalizyZwrotuGrid.AutoGenerateColumns = false;
            this.pozycjeAnalizyZwrotuGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.identColumn,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.pozostaloColumn});
            this.pozycjeAnalizyZwrotuGrid.DataSource = this.pozycjeBindingSource;
            this.pozycjeAnalizyZwrotuGrid.Location = new System.Drawing.Point(12, 68);
            this.pozycjeAnalizyZwrotuGrid.Name = "pozycjeAnalizyZwrotuGrid";
            this.pozycjeAnalizyZwrotuGrid.ParentForm = null;
            this.pozycjeAnalizyZwrotuGrid.Size = new System.Drawing.Size(1252, 596);
            this.pozycjeAnalizyZwrotuGrid.TabIndex = 4;
            this.pozycjeAnalizyZwrotuGrid.VirtualMode = true;
            // 
            // identColumn
            // 
            this.identColumn.DataPropertyName = "Ident";
            this.identColumn.FillWeight = 50F;
            this.identColumn.HeaderText = "Ident";
            this.identColumn.Name = "identColumn";
            this.identColumn.ReadOnly = true;
            this.identColumn.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Towar";
            this.dataGridViewTextBoxColumn2.FillWeight = 400F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Towar";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Ilosc";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn3.FillWeight = 60F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Ilosc";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // pozostaloColumn
            // 
            this.pozostaloColumn.DataPropertyName = "PozostaloDoSkorygowania";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.pozostaloColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.pozostaloColumn.FillWeight = 60F;
            this.pozostaloColumn.HeaderText = "Pozostalo";
            this.pozostaloColumn.Name = "pozostaloColumn";
            this.pozostaloColumn.ReadOnly = true;
            this.pozostaloColumn.Width = 60;
            // 
            // analizujButton
            // 
            this.analizujButton.Location = new System.Drawing.Point(225, 34);
            this.analizujButton.Name = "analizujButton";
            this.analizujButton.Size = new System.Drawing.Size(75, 23);
            this.analizujButton.TabIndex = 8;
            this.analizujButton.Text = "Analizuj";
            this.analizujButton.UseVisualStyleBackColor = true;
            this.analizujButton.Click += new System.EventHandler(this.analizujButton_Click);
            // 
            // AnalizaZwrotuEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 676);
            this.Controls.Add(this.analizujButton);
            this.Controls.Add(this.analizujOdDTPicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.wystawButton);
            this.Controls.Add(this.pozycjeAnalizyZwrotuGrid);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "AnalizaZwrotuEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AnalizaZwrotuEditForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AnalizaZwrotuEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pozycjeAnalizyZwrotuGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private AbakTools.Handel.Forms.PozycjeAnalizyZwrotuGrid pozycjeAnalizyZwrotuGrid;
        private System.Windows.Forms.BindingSource pozycjeBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn identColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn pozostaloColumn;
        private System.Windows.Forms.Button wystawButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker analizujOdDTPicker;
        private System.Windows.Forms.Button analizujButton;
    }
}