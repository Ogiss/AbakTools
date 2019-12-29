namespace AbakTools.Towary.Forms
{
    partial class AtrybutyForm
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
            this.treeView = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.nazwaPublicznaTextBox = new System.Windows.Forms.TextBox();
            this.nazwaTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.atrybutGroupBox = new System.Windows.Forms.GroupBox();
            this.usunTekstureButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.texturePictureBox = new System.Windows.Forms.PictureBox();
            this.kolorButton = new System.Windows.Forms.Button();
            this.kolejnoscTextBox = new System.Windows.Forms.TextBox();
            this.kolorTextBox = new System.Windows.Forms.TextBox();
            this.nazwaAtrybutuTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.okButton = new System.Windows.Forms.Button();
            this.anulujButton = new System.Windows.Forms.Button();
            this.zatwierdzButton = new System.Windows.Forms.Button();
            this.nowaGrupaButton = new System.Windows.Forms.Button();
            this.nowyAtrybytButton = new System.Windows.Forms.Button();
            this.usunButton = new System.Windows.Forms.Button();
            this.grupaAttrGuidTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.copyGrupaAttrGuidButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.copyAttrGuidButton = new System.Windows.Forms.Button();
            this.atrybutBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grupaAtrybutowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.atrybutGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texturePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.atrybutBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grupaAtrybutowBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.Location = new System.Drawing.Point(12, 12);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(305, 535);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.copyGrupaAttrGuidButton);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.grupaAttrGuidTextBox);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.nazwaPublicznaTextBox);
            this.groupBox1.Controls.Add(this.nazwaTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(323, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 142);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grupa";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.grupaAtrybutowBindingSource, "GrupaKolorow", true));
            this.checkBox1.Location = new System.Drawing.Point(101, 75);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // nazwaPublicznaTextBox
            // 
            this.nazwaPublicznaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.grupaAtrybutowBindingSource, "NazwaPubliczna", true));
            this.nazwaPublicznaTextBox.Location = new System.Drawing.Point(101, 48);
            this.nazwaPublicznaTextBox.Name = "nazwaPublicznaTextBox";
            this.nazwaPublicznaTextBox.Size = new System.Drawing.Size(221, 20);
            this.nazwaPublicznaTextBox.TabIndex = 4;
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.grupaAtrybutowBindingSource, "Nazwa", true));
            this.nazwaTextBox.Location = new System.Drawing.Point(101, 22);
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.Size = new System.Drawing.Size(221, 20);
            this.nazwaTextBox.TabIndex = 3;
            this.nazwaTextBox.TextChanged += new System.EventHandler(this.nazwaTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Grupa kolorów:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nazwa publiczna:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa:";
            // 
            // atrybutGroupBox
            // 
            this.atrybutGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.atrybutGroupBox.Controls.Add(this.copyAttrGuidButton);
            this.atrybutGroupBox.Controls.Add(this.textBox1);
            this.atrybutGroupBox.Controls.Add(this.label9);
            this.atrybutGroupBox.Controls.Add(this.usunTekstureButton);
            this.atrybutGroupBox.Controls.Add(this.label7);
            this.atrybutGroupBox.Controls.Add(this.texturePictureBox);
            this.atrybutGroupBox.Controls.Add(this.kolorButton);
            this.atrybutGroupBox.Controls.Add(this.kolejnoscTextBox);
            this.atrybutGroupBox.Controls.Add(this.kolorTextBox);
            this.atrybutGroupBox.Controls.Add(this.nazwaAtrybutuTextBox);
            this.atrybutGroupBox.Controls.Add(this.label6);
            this.atrybutGroupBox.Controls.Add(this.label5);
            this.atrybutGroupBox.Controls.Add(this.label4);
            this.atrybutGroupBox.Location = new System.Drawing.Point(323, 160);
            this.atrybutGroupBox.Name = "atrybutGroupBox";
            this.atrybutGroupBox.Size = new System.Drawing.Size(352, 159);
            this.atrybutGroupBox.TabIndex = 2;
            this.atrybutGroupBox.TabStop = false;
            this.atrybutGroupBox.Text = "Atrybut";
            // 
            // usunTekstureButton
            // 
            this.usunTekstureButton.Location = new System.Drawing.Point(146, 70);
            this.usunTekstureButton.Name = "usunTekstureButton";
            this.usunTekstureButton.Size = new System.Drawing.Size(75, 23);
            this.usunTekstureButton.TabIndex = 9;
            this.usunTekstureButton.Text = "Usuń";
            this.usunTekstureButton.UseVisualStyleBackColor = true;
            this.usunTekstureButton.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tekstura:";
            // 
            // texturePictureBox
            // 
            this.texturePictureBox.Location = new System.Drawing.Point(101, 72);
            this.texturePictureBox.Name = "texturePictureBox";
            this.texturePictureBox.Size = new System.Drawing.Size(20, 20);
            this.texturePictureBox.TabIndex = 7;
            this.texturePictureBox.TabStop = false;
            // 
            // kolorButton
            // 
            this.kolorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kolorButton.Location = new System.Drawing.Point(227, 44);
            this.kolorButton.Name = "kolorButton";
            this.kolorButton.Size = new System.Drawing.Size(48, 23);
            this.kolorButton.TabIndex = 6;
            this.kolorButton.UseVisualStyleBackColor = true;
            this.kolorButton.Click += new System.EventHandler(this.kolorButton_Click);
            // 
            // kolejnoscTextBox
            // 
            this.kolejnoscTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.atrybutBindingSource, "Kolejnosc", true));
            this.kolejnoscTextBox.Location = new System.Drawing.Point(101, 98);
            this.kolejnoscTextBox.Name = "kolejnoscTextBox";
            this.kolejnoscTextBox.Size = new System.Drawing.Size(120, 20);
            this.kolejnoscTextBox.TabIndex = 5;
            // 
            // kolorTextBox
            // 
            this.kolorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.atrybutBindingSource, "Kolor", true));
            this.kolorTextBox.Location = new System.Drawing.Point(101, 46);
            this.kolorTextBox.Name = "kolorTextBox";
            this.kolorTextBox.Size = new System.Drawing.Size(120, 20);
            this.kolorTextBox.TabIndex = 4;
            // 
            // nazwaAtrybutuTextBox
            // 
            this.nazwaAtrybutuTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.atrybutBindingSource, "Nazwa", true));
            this.nazwaAtrybutuTextBox.Location = new System.Drawing.Point(101, 20);
            this.nazwaAtrybutuTextBox.Name = "nazwaAtrybutuTextBox";
            this.nazwaAtrybutuTextBox.Size = new System.Drawing.Size(221, 20);
            this.nazwaAtrybutuTextBox.TabIndex = 3;
            this.nazwaAtrybutuTextBox.TextChanged += new System.EventHandler(this.nazwaAtrybutuTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Kolejność:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Kolor:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nazwa:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(595, 563);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // anulujButton
            // 
            this.anulujButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.anulujButton.Location = new System.Drawing.Point(514, 563);
            this.anulujButton.Name = "anulujButton";
            this.anulujButton.Size = new System.Drawing.Size(75, 23);
            this.anulujButton.TabIndex = 4;
            this.anulujButton.Text = "Anuluj";
            this.anulujButton.UseVisualStyleBackColor = true;
            this.anulujButton.Click += new System.EventHandler(this.anulujButton_Click);
            // 
            // zatwierdzButton
            // 
            this.zatwierdzButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zatwierdzButton.Location = new System.Drawing.Point(433, 563);
            this.zatwierdzButton.Name = "zatwierdzButton";
            this.zatwierdzButton.Size = new System.Drawing.Size(75, 23);
            this.zatwierdzButton.TabIndex = 5;
            this.zatwierdzButton.Text = "Zatwierdź";
            this.zatwierdzButton.UseVisualStyleBackColor = true;
            this.zatwierdzButton.Click += new System.EventHandler(this.zatwierdzButton_Click);
            // 
            // nowaGrupaButton
            // 
            this.nowaGrupaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nowaGrupaButton.Location = new System.Drawing.Point(12, 563);
            this.nowaGrupaButton.Name = "nowaGrupaButton";
            this.nowaGrupaButton.Size = new System.Drawing.Size(90, 23);
            this.nowaGrupaButton.TabIndex = 6;
            this.nowaGrupaButton.Text = "Nowa grupa";
            this.nowaGrupaButton.UseVisualStyleBackColor = true;
            this.nowaGrupaButton.Click += new System.EventHandler(this.nowaGrupaButton_Click);
            // 
            // nowyAtrybytButton
            // 
            this.nowyAtrybytButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nowyAtrybytButton.Location = new System.Drawing.Point(108, 563);
            this.nowyAtrybytButton.Name = "nowyAtrybytButton";
            this.nowyAtrybytButton.Size = new System.Drawing.Size(90, 23);
            this.nowyAtrybytButton.TabIndex = 8;
            this.nowyAtrybytButton.Text = "Nowy atrybut";
            this.nowyAtrybytButton.UseVisualStyleBackColor = true;
            this.nowyAtrybytButton.Click += new System.EventHandler(this.nowyAtrybytButton_Click);
            // 
            // usunButton
            // 
            this.usunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.usunButton.Location = new System.Drawing.Point(204, 563);
            this.usunButton.Name = "usunButton";
            this.usunButton.Size = new System.Drawing.Size(90, 23);
            this.usunButton.TabIndex = 9;
            this.usunButton.Text = "Usuń";
            this.usunButton.UseVisualStyleBackColor = true;
            this.usunButton.Click += new System.EventHandler(this.usunButton_Click);
            // 
            // grupaAttrGuidTextBox
            // 
            this.grupaAttrGuidTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.grupaAtrybutowBindingSource, "GUID", true));
            this.grupaAttrGuidTextBox.Location = new System.Drawing.Point(101, 95);
            this.grupaAttrGuidTextBox.Name = "grupaAttrGuidTextBox";
            this.grupaAttrGuidTextBox.ReadOnly = true;
            this.grupaAttrGuidTextBox.Size = new System.Drawing.Size(221, 20);
            this.grupaAttrGuidTextBox.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(63, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Guid:";
            // 
            // copyGrupaAttrGuidButton
            // 
            this.copyGrupaAttrGuidButton.Location = new System.Drawing.Point(329, 93);
            this.copyGrupaAttrGuidButton.Name = "copyGrupaAttrGuidButton";
            this.copyGrupaAttrGuidButton.Size = new System.Drawing.Size(17, 23);
            this.copyGrupaAttrGuidButton.TabIndex = 8;
            this.copyGrupaAttrGuidButton.Text = "K";
            this.copyGrupaAttrGuidButton.UseVisualStyleBackColor = true;
            this.copyGrupaAttrGuidButton.Click += new System.EventHandler(this.copyGrupaAttrGuidButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(64, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Guid:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.atrybutBindingSource, "GUID", true));
            this.textBox1.Location = new System.Drawing.Point(102, 124);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(221, 20);
            this.textBox1.TabIndex = 11;
            // 
            // copyAttrGuidButton
            // 
            this.copyAttrGuidButton.Location = new System.Drawing.Point(329, 121);
            this.copyAttrGuidButton.Name = "copyAttrGuidButton";
            this.copyAttrGuidButton.Size = new System.Drawing.Size(17, 23);
            this.copyAttrGuidButton.TabIndex = 12;
            this.copyAttrGuidButton.Text = "K";
            this.copyAttrGuidButton.UseVisualStyleBackColor = true;
            this.copyAttrGuidButton.Click += new System.EventHandler(this.copyAttrGuidButton_Click);
            // 
            // atrybutBindingSource
            // 
            this.atrybutBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.Atrybut);
            // 
            // grupaAtrybutowBindingSource
            // 
            this.grupaAtrybutowBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.GrupaAtrybutow);
            // 
            // AtrybutyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 598);
            this.Controls.Add(this.usunButton);
            this.Controls.Add(this.nowyAtrybytButton);
            this.Controls.Add(this.nowaGrupaButton);
            this.Controls.Add(this.zatwierdzButton);
            this.Controls.Add(this.anulujButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.atrybutGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.treeView);
            this.KeyPreview = true;
            this.Name = "AtrybutyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Atrybuty";
            this.Load += new System.EventHandler(this.AtrybutyForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AtrybutyForm_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.AtrybutyForm_PreviewKeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.atrybutGroupBox.ResumeLayout(false);
            this.atrybutGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texturePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.atrybutBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grupaAtrybutowBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox atrybutGroupBox;
        private System.Windows.Forms.TextBox nazwaPublicznaTextBox;
        private System.Windows.Forms.TextBox nazwaTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.BindingSource grupaAtrybutowBindingSource;
        private System.Windows.Forms.TextBox kolejnoscTextBox;
        private System.Windows.Forms.BindingSource atrybutBindingSource;
        private System.Windows.Forms.TextBox kolorTextBox;
        private System.Windows.Forms.TextBox nazwaAtrybutuTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button kolorButton;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.PictureBox texturePictureBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button usunTekstureButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button anulujButton;
        private System.Windows.Forms.Button zatwierdzButton;
        private System.Windows.Forms.Button nowaGrupaButton;
        private System.Windows.Forms.Button nowyAtrybytButton;
        private System.Windows.Forms.Button usunButton;
        private System.Windows.Forms.Button copyGrupaAttrGuidButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox grupaAttrGuidTextBox;
        private System.Windows.Forms.Button copyAttrGuidButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
    }
}