namespace AbakTools.Towary.Forms
{
    partial class KategorieForm
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
            this.zatwierdzbbutton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nazwaTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.delButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.opisTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.przyjaznayLinkTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.metaTytulTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.metaOpisTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.metaSłowaTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.kolejnoscTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.aktywnaTheckBox = new System.Windows.Forms.CheckBox();
            this.kategoriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kategorieTreeView = new Enova.Business.Old.Controls.KategorieTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.kategoriaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // zatwierdzbbutton
            // 
            this.zatwierdzbbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zatwierdzbbutton.Location = new System.Drawing.Point(497, 578);
            this.zatwierdzbbutton.Name = "zatwierdzbbutton";
            this.zatwierdzbbutton.Size = new System.Drawing.Size(75, 23);
            this.zatwierdzbbutton.TabIndex = 11;
            this.zatwierdzbbutton.Text = "Zatwierdź";
            this.zatwierdzbbutton.UseVisualStyleBackColor = true;
            this.zatwierdzbbutton.Click += new System.EventHandler(this.zatwierdzbbutton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(578, 578);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(659, 578);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 13;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(348, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nazwa:";
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kategoriaBindingSource, "Nazwa", true));
            this.nazwaTextBox.Location = new System.Drawing.Point(392, 10);
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.Size = new System.Drawing.Size(342, 20);
            this.nazwaTextBox.TabIndex = 1;
            this.nazwaTextBox.TextChanged += new System.EventHandler(this.nazwaTextBox_TextChanged);
            this.nazwaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nazwaTextBox_KeyDown);
            this.nazwaTextBox.Leave += new System.EventHandler(this.nazwaTextBox_Leave);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addButton.Location = new System.Drawing.Point(4, 578);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 9;
            this.addButton.Text = "Dodaj";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // delButton
            // 
            this.delButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.delButton.Location = new System.Drawing.Point(85, 578);
            this.delButton.Name = "delButton";
            this.delButton.Size = new System.Drawing.Size(75, 23);
            this.delButton.TabIndex = 10;
            this.delButton.Text = "Usuń";
            this.delButton.UseVisualStyleBackColor = true;
            this.delButton.Click += new System.EventHandler(this.delButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Opis:";
            // 
            // opisTextBox
            // 
            this.opisTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kategoriaBindingSource, "Opis", true));
            this.opisTextBox.Location = new System.Drawing.Point(392, 36);
            this.opisTextBox.Multiline = true;
            this.opisTextBox.Name = "opisTextBox";
            this.opisTextBox.Size = new System.Drawing.Size(342, 97);
            this.opisTextBox.TabIndex = 2;
            this.opisTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.opisTextBox_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Przyjazny link:";
            // 
            // przyjaznayLinkTextBox
            // 
            this.przyjaznayLinkTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kategoriaBindingSource, "PrzyjaznyLink", true));
            this.przyjaznayLinkTextBox.Location = new System.Drawing.Point(392, 139);
            this.przyjaznayLinkTextBox.Name = "przyjaznayLinkTextBox";
            this.przyjaznayLinkTextBox.Size = new System.Drawing.Size(342, 20);
            this.przyjaznayLinkTextBox.TabIndex = 3;
            this.przyjaznayLinkTextBox.Enter += new System.EventHandler(this.przyjaznayLinkTextBox_Enter);
            this.przyjaznayLinkTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.przyjaznayLinkTextBox_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Meta tytuł:";
            // 
            // metaTytulTextBox
            // 
            this.metaTytulTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kategoriaBindingSource, "MetaTytul", true));
            this.metaTytulTextBox.Location = new System.Drawing.Point(392, 165);
            this.metaTytulTextBox.Name = "metaTytulTextBox";
            this.metaTytulTextBox.Size = new System.Drawing.Size(342, 20);
            this.metaTytulTextBox.TabIndex = 4;
            this.metaTytulTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.metaTytulTextBox_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(335, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Meta opis:";
            // 
            // metaOpisTextBox
            // 
            this.metaOpisTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kategoriaBindingSource, "Opis", true));
            this.metaOpisTextBox.Location = new System.Drawing.Point(392, 191);
            this.metaOpisTextBox.Multiline = true;
            this.metaOpisTextBox.Name = "metaOpisTextBox";
            this.metaOpisTextBox.Size = new System.Drawing.Size(342, 93);
            this.metaOpisTextBox.TabIndex = 5;
            this.metaOpisTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.metaOpisTextBox_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(325, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Meta słowa:";
            // 
            // metaSłowaTextBox
            // 
            this.metaSłowaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kategoriaBindingSource, "MetaSlowa", true));
            this.metaSłowaTextBox.Location = new System.Drawing.Point(392, 290);
            this.metaSłowaTextBox.Name = "metaSłowaTextBox";
            this.metaSłowaTextBox.Size = new System.Drawing.Size(342, 20);
            this.metaSłowaTextBox.TabIndex = 6;
            this.metaSłowaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.metaSłowaTextBox_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(335, 320);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Kolejność:";
            // 
            // kolejnoscTextBox
            // 
            this.kolejnoscTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kategoriaBindingSource, "KolejnoscWyswietlania", true));
            this.kolejnoscTextBox.Location = new System.Drawing.Point(392, 317);
            this.kolejnoscTextBox.Name = "kolejnoscTextBox";
            this.kolejnoscTextBox.Size = new System.Drawing.Size(100, 20);
            this.kolejnoscTextBox.TabIndex = 7;
            this.kolejnoscTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kolejnoscTextBox_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(335, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Aktywna:";
            // 
            // aktywnaTheckBox
            // 
            this.aktywnaTheckBox.AutoSize = true;
            this.aktywnaTheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.kategoriaBindingSource, "Aktywna", true));
            this.aktywnaTheckBox.Location = new System.Drawing.Point(392, 347);
            this.aktywnaTheckBox.Name = "aktywnaTheckBox";
            this.aktywnaTheckBox.Size = new System.Drawing.Size(15, 14);
            this.aktywnaTheckBox.TabIndex = 8;
            this.aktywnaTheckBox.UseVisualStyleBackColor = true;
            this.aktywnaTheckBox.CheckedChanged += new System.EventHandler(this.aktywnaTheckBox_CheckedChanged);
            this.aktywnaTheckBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.aktywnaCheckBox_KeyDown);
            // 
            // kategoriaBindingSource
            // 
            this.kategoriaBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.KategoriaOld);
            // 
            // kategorieTreeView
            // 
            this.kategorieTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.kategorieTreeView.Location = new System.Drawing.Point(4, 12);
            this.kategorieTreeView.Name = "kategorieTreeView";
            this.kategorieTreeView.SelectedNode = null;
            this.kategorieTreeView.Size = new System.Drawing.Size(285, 557);
            this.kategorieTreeView.TabIndex = 0;
            this.kategorieTreeView.WithEmptyRoot = false;
            this.kategorieTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.kategorieTreeView_AfterSelect);
            // 
            // KategorieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 613);
            this.Controls.Add(this.aktywnaTheckBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.kolejnoscTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.metaSłowaTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.metaOpisTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.metaTytulTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.przyjaznayLinkTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.opisTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.delButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.nazwaTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.zatwierdzbbutton);
            this.Controls.Add(this.kategorieTreeView);
            this.KeyPreview = true;
            this.Name = "KategorieForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kategorie";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KategorieForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.kategoriaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Enova.Business.Old.Controls.KategorieTreeView kategorieTreeView;
        private System.Windows.Forms.Button zatwierdzbbutton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nazwaTextBox;
        private System.Windows.Forms.BindingSource kategoriaBindingSource;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button delButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox opisTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox przyjaznayLinkTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox metaTytulTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox metaOpisTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox metaSłowaTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox kolejnoscTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox aktywnaTheckBox;
    }
}