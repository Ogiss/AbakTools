namespace BAL.Forms.Controls
{
    partial class DateFromToControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateFromToControl));
            this.textBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kalendarzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bieżącyDzieńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bieżącyTydzieńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bieżącyMiesiącToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dowolnyMiesiącToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.styczeńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lutyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marzecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kwiecieńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.majToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.czerwiecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lipiecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sierpieńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wrzesieńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.październikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listopadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grudzieńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bieżącyRokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pełnyOkresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.przejdźDoMiesiącaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.przejdźDoRokuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.ContextMenuStrip = this.contextMenuStrip;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.textBox.Location = new System.Drawing.Point(0, 1);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(179, 22);
            this.textBox.TabIndex = 3;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kalendarzToolStripMenuItem,
            this.bieżącyDzieńToolStripMenuItem,
            this.bieżącyTydzieńToolStripMenuItem,
            this.bieżącyMiesiącToolStripMenuItem,
            this.dowolnyMiesiącToolStripMenuItem,
            this.bieżącyRokToolStripMenuItem,
            this.pełnyOkresToolStripMenuItem,
            this.toolStripSeparator1,
            this.przejdźDoMiesiącaToolStripMenuItem,
            this.przejdźDoRokuToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(178, 230);
            this.contextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // kalendarzToolStripMenuItem
            // 
            this.kalendarzToolStripMenuItem.Name = "kalendarzToolStripMenuItem";
            this.kalendarzToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.kalendarzToolStripMenuItem.Tag = "1";
            this.kalendarzToolStripMenuItem.Text = "Kalendarz";
            // 
            // bieżącyDzieńToolStripMenuItem
            // 
            this.bieżącyDzieńToolStripMenuItem.Name = "bieżącyDzieńToolStripMenuItem";
            this.bieżącyDzieńToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bieżącyDzieńToolStripMenuItem.Tag = "2";
            this.bieżącyDzieńToolStripMenuItem.Text = "Bieżący Dzień";
            // 
            // bieżącyTydzieńToolStripMenuItem
            // 
            this.bieżącyTydzieńToolStripMenuItem.Name = "bieżącyTydzieńToolStripMenuItem";
            this.bieżącyTydzieńToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bieżącyTydzieńToolStripMenuItem.Tag = "3";
            this.bieżącyTydzieńToolStripMenuItem.Text = "Bieżący Tydzień";
            // 
            // bieżącyMiesiącToolStripMenuItem
            // 
            this.bieżącyMiesiącToolStripMenuItem.Name = "bieżącyMiesiącToolStripMenuItem";
            this.bieżącyMiesiącToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bieżącyMiesiącToolStripMenuItem.Tag = "4";
            this.bieżącyMiesiącToolStripMenuItem.Text = "Bieżący Miesiąc";
            // 
            // dowolnyMiesiącToolStripMenuItem
            // 
            this.dowolnyMiesiącToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.styczeńToolStripMenuItem,
            this.lutyToolStripMenuItem,
            this.marzecToolStripMenuItem,
            this.kwiecieńToolStripMenuItem,
            this.majToolStripMenuItem,
            this.czerwiecToolStripMenuItem,
            this.lipiecToolStripMenuItem,
            this.sierpieńToolStripMenuItem,
            this.wrzesieńToolStripMenuItem,
            this.październikToolStripMenuItem,
            this.listopadToolStripMenuItem,
            this.grudzieńToolStripMenuItem});
            this.dowolnyMiesiącToolStripMenuItem.Name = "dowolnyMiesiącToolStripMenuItem";
            this.dowolnyMiesiącToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.dowolnyMiesiącToolStripMenuItem.Text = "Dowolny Miesiąc";
            this.dowolnyMiesiącToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // styczeńToolStripMenuItem
            // 
            this.styczeńToolStripMenuItem.Name = "styczeńToolStripMenuItem";
            this.styczeńToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.styczeńToolStripMenuItem.Tag = "5;1";
            this.styczeńToolStripMenuItem.Text = "Styczeń";
            // 
            // lutyToolStripMenuItem
            // 
            this.lutyToolStripMenuItem.Name = "lutyToolStripMenuItem";
            this.lutyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lutyToolStripMenuItem.Tag = "5;2";
            this.lutyToolStripMenuItem.Text = "Luty";
            // 
            // marzecToolStripMenuItem
            // 
            this.marzecToolStripMenuItem.Name = "marzecToolStripMenuItem";
            this.marzecToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.marzecToolStripMenuItem.Tag = "5;3";
            this.marzecToolStripMenuItem.Text = "Marzec";
            // 
            // kwiecieńToolStripMenuItem
            // 
            this.kwiecieńToolStripMenuItem.Name = "kwiecieńToolStripMenuItem";
            this.kwiecieńToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.kwiecieńToolStripMenuItem.Tag = "5;4";
            this.kwiecieńToolStripMenuItem.Text = "Kwiecień";
            // 
            // majToolStripMenuItem
            // 
            this.majToolStripMenuItem.Name = "majToolStripMenuItem";
            this.majToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.majToolStripMenuItem.Tag = "5;5";
            this.majToolStripMenuItem.Text = "Maj";
            // 
            // czerwiecToolStripMenuItem
            // 
            this.czerwiecToolStripMenuItem.Name = "czerwiecToolStripMenuItem";
            this.czerwiecToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.czerwiecToolStripMenuItem.Tag = "5;6";
            this.czerwiecToolStripMenuItem.Text = "Czerwiec";
            // 
            // lipiecToolStripMenuItem
            // 
            this.lipiecToolStripMenuItem.Name = "lipiecToolStripMenuItem";
            this.lipiecToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lipiecToolStripMenuItem.Tag = "5;7";
            this.lipiecToolStripMenuItem.Text = "Lipiec";
            // 
            // sierpieńToolStripMenuItem
            // 
            this.sierpieńToolStripMenuItem.Name = "sierpieńToolStripMenuItem";
            this.sierpieńToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sierpieńToolStripMenuItem.Tag = "5;8";
            this.sierpieńToolStripMenuItem.Text = "Sierpień";
            // 
            // wrzesieńToolStripMenuItem
            // 
            this.wrzesieńToolStripMenuItem.Name = "wrzesieńToolStripMenuItem";
            this.wrzesieńToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.wrzesieńToolStripMenuItem.Tag = "5;9";
            this.wrzesieńToolStripMenuItem.Text = "Wrzesień";
            // 
            // październikToolStripMenuItem
            // 
            this.październikToolStripMenuItem.Name = "październikToolStripMenuItem";
            this.październikToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.październikToolStripMenuItem.Tag = "5;10";
            this.październikToolStripMenuItem.Text = "Październik";
            // 
            // listopadToolStripMenuItem
            // 
            this.listopadToolStripMenuItem.Name = "listopadToolStripMenuItem";
            this.listopadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.listopadToolStripMenuItem.Tag = "5;11";
            this.listopadToolStripMenuItem.Text = "Listopad";
            // 
            // grudzieńToolStripMenuItem
            // 
            this.grudzieńToolStripMenuItem.Name = "grudzieńToolStripMenuItem";
            this.grudzieńToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.grudzieńToolStripMenuItem.Tag = "5;12";
            this.grudzieńToolStripMenuItem.Text = "Grudzień";
            // 
            // bieżącyRokToolStripMenuItem
            // 
            this.bieżącyRokToolStripMenuItem.Name = "bieżącyRokToolStripMenuItem";
            this.bieżącyRokToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bieżącyRokToolStripMenuItem.Tag = "6";
            this.bieżącyRokToolStripMenuItem.Text = "Bieżący Rok";
            // 
            // pełnyOkresToolStripMenuItem
            // 
            this.pełnyOkresToolStripMenuItem.Name = "pełnyOkresToolStripMenuItem";
            this.pełnyOkresToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.pełnyOkresToolStripMenuItem.Tag = "7";
            this.pełnyOkresToolStripMenuItem.Text = "Pełny Okres";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
            // 
            // przejdźDoMiesiącaToolStripMenuItem
            // 
            this.przejdźDoMiesiącaToolStripMenuItem.Name = "przejdźDoMiesiącaToolStripMenuItem";
            this.przejdźDoMiesiącaToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.przejdźDoMiesiącaToolStripMenuItem.Tag = "";
            this.przejdźDoMiesiącaToolStripMenuItem.Text = "Przejdź do miesiąca";
            this.przejdźDoMiesiącaToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // przejdźDoRokuToolStripMenuItem
            // 
            this.przejdźDoRokuToolStripMenuItem.Name = "przejdźDoRokuToolStripMenuItem";
            this.przejdźDoRokuToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.przejdźDoRokuToolStripMenuItem.Text = "Przejdź do roku";
            this.przejdźDoRokuToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // downButton
            // 
            this.downButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("downButton.BackgroundImage")));
            this.downButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.downButton.Location = new System.Drawing.Point(180, 12);
            this.downButton.Margin = new System.Windows.Forms.Padding(0);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(20, 13);
            this.downButton.TabIndex = 5;
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.upButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("upButton.BackgroundImage")));
            this.upButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.upButton.Location = new System.Drawing.Point(180, 0);
            this.upButton.Margin = new System.Windows.Forms.Padding(0);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(20, 13);
            this.upButton.TabIndex = 4;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // DateFromToControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.textBox);
            this.Name = "DateFromToControl";
            this.Size = new System.Drawing.Size(200, 24);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem kalendarzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bieżącyDzieńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bieżącyTydzieńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bieżącyMiesiącToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dowolnyMiesiącToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem styczeńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lutyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marzecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kwiecieńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem majToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem czerwiecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lipiecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sierpieńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wrzesieńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem październikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listopadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grudzieńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bieżącyRokToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pełnyOkresToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem przejdźDoMiesiącaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem przejdźDoRokuToolStripMenuItem;
    }
}
