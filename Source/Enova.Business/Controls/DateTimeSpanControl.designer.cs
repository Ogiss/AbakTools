namespace Enova.Business.Old.Controls
{
    partial class DateTimeSpanControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeSpanControl));
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
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.ContextMenuStrip = this.contextMenuStrip;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(189, 20);
            this.textBox.TabIndex = 0;
            this.textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox.Leave += new System.EventHandler(this.textBox_Leave);
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
            this.contextMenuStrip.Size = new System.Drawing.Size(178, 208);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            this.contextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // kalendarzToolStripMenuItem
            // 
            this.kalendarzToolStripMenuItem.Name = "kalendarzToolStripMenuItem";
            this.kalendarzToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.kalendarzToolStripMenuItem.Text = "Kalendarz";
            this.kalendarzToolStripMenuItem.Click += new System.EventHandler(this.kalendarzToolStripMenuItem_Click);
            // 
            // bieżącyDzieńToolStripMenuItem
            // 
            this.bieżącyDzieńToolStripMenuItem.Name = "bieżącyDzieńToolStripMenuItem";
            this.bieżącyDzieńToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bieżącyDzieńToolStripMenuItem.Text = "Bieżący Dzień";
            this.bieżącyDzieńToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // bieżącyTydzieńToolStripMenuItem
            // 
            this.bieżącyTydzieńToolStripMenuItem.Name = "bieżącyTydzieńToolStripMenuItem";
            this.bieżącyTydzieńToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bieżącyTydzieńToolStripMenuItem.Text = "Bieżący Tydzień";
            this.bieżącyTydzieńToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // bieżącyMiesiącToolStripMenuItem
            // 
            this.bieżącyMiesiącToolStripMenuItem.Name = "bieżącyMiesiącToolStripMenuItem";
            this.bieżącyMiesiącToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bieżącyMiesiącToolStripMenuItem.Text = "Bieżący Miesiąc";
            this.bieżącyMiesiącToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
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
            // 
            // styczeńToolStripMenuItem
            // 
            this.styczeńToolStripMenuItem.Name = "styczeńToolStripMenuItem";
            this.styczeńToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.styczeńToolStripMenuItem.Text = "Styczeń";
            this.styczeńToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // lutyToolStripMenuItem
            // 
            this.lutyToolStripMenuItem.Name = "lutyToolStripMenuItem";
            this.lutyToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.lutyToolStripMenuItem.Text = "Luty";
            this.lutyToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // marzecToolStripMenuItem
            // 
            this.marzecToolStripMenuItem.Name = "marzecToolStripMenuItem";
            this.marzecToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.marzecToolStripMenuItem.Text = "Marzec";
            this.marzecToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // kwiecieńToolStripMenuItem
            // 
            this.kwiecieńToolStripMenuItem.Name = "kwiecieńToolStripMenuItem";
            this.kwiecieńToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.kwiecieńToolStripMenuItem.Text = "Kwiecień";
            this.kwiecieńToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // majToolStripMenuItem
            // 
            this.majToolStripMenuItem.Name = "majToolStripMenuItem";
            this.majToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.majToolStripMenuItem.Text = "Maj";
            this.majToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // czerwiecToolStripMenuItem
            // 
            this.czerwiecToolStripMenuItem.Name = "czerwiecToolStripMenuItem";
            this.czerwiecToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.czerwiecToolStripMenuItem.Text = "Czerwiec";
            this.czerwiecToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // lipiecToolStripMenuItem
            // 
            this.lipiecToolStripMenuItem.Name = "lipiecToolStripMenuItem";
            this.lipiecToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.lipiecToolStripMenuItem.Text = "Lipiec";
            this.lipiecToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // sierpieńToolStripMenuItem
            // 
            this.sierpieńToolStripMenuItem.Name = "sierpieńToolStripMenuItem";
            this.sierpieńToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.sierpieńToolStripMenuItem.Text = "Sierpień";
            this.sierpieńToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // wrzesieńToolStripMenuItem
            // 
            this.wrzesieńToolStripMenuItem.Name = "wrzesieńToolStripMenuItem";
            this.wrzesieńToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.wrzesieńToolStripMenuItem.Text = "Wrzesień";
            this.wrzesieńToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // październikToolStripMenuItem
            // 
            this.październikToolStripMenuItem.Name = "październikToolStripMenuItem";
            this.październikToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.październikToolStripMenuItem.Text = "Październik";
            this.październikToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // listopadToolStripMenuItem
            // 
            this.listopadToolStripMenuItem.Name = "listopadToolStripMenuItem";
            this.listopadToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.listopadToolStripMenuItem.Text = "Listopad";
            this.listopadToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // grudzieńToolStripMenuItem
            // 
            this.grudzieńToolStripMenuItem.Name = "grudzieńToolStripMenuItem";
            this.grudzieńToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.grudzieńToolStripMenuItem.Text = "Grudzień";
            this.grudzieńToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // bieżącyRokToolStripMenuItem
            // 
            this.bieżącyRokToolStripMenuItem.Name = "bieżącyRokToolStripMenuItem";
            this.bieżącyRokToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bieżącyRokToolStripMenuItem.Text = "Bieżący Rok";
            this.bieżącyRokToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // pełnyOkresToolStripMenuItem
            // 
            this.pełnyOkresToolStripMenuItem.Name = "pełnyOkresToolStripMenuItem";
            this.pełnyOkresToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.pełnyOkresToolStripMenuItem.Text = "Pełny Okres";
            this.pełnyOkresToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
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
            this.przejdźDoMiesiącaToolStripMenuItem.Text = "Przejdź do miesiąca";
            this.przejdźDoMiesiącaToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.przejdźDoMiesiącaToolStripMenuItem_DropDownItemClicked);
            // 
            // przejdźDoRokuToolStripMenuItem
            // 
            this.przejdźDoRokuToolStripMenuItem.Name = "przejdźDoRokuToolStripMenuItem";
            this.przejdźDoRokuToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.przejdźDoRokuToolStripMenuItem.Text = "Przejdź do roku";
            this.przejdźDoRokuToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.przejdźDoRokuToolStripMenuItem_DropDownItemClicked);
            // 
            // upButton
            // 
            this.upButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.upButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("upButton.BackgroundImage")));
            this.upButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.upButton.Location = new System.Drawing.Point(190, 0);
            this.upButton.Margin = new System.Windows.Forms.Padding(0);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(20, 10);
            this.upButton.TabIndex = 1;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("downButton.BackgroundImage")));
            this.downButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.downButton.Location = new System.Drawing.Point(190, 10);
            this.downButton.Margin = new System.Windows.Forms.Padding(0);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(20, 10);
            this.downButton.TabIndex = 2;
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // DateTimeSpanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.textBox);
            this.Name = "DateTimeSpanControl";
            this.Size = new System.Drawing.Size(213, 21);
            this.Load += new System.EventHandler(this.DateTimeSpanControl_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
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
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem przejdźDoMiesiącaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem przejdźDoRokuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kalendarzToolStripMenuItem;
    }
}
