namespace Enova.Forms.CRM
{
    partial class KontrahentKontaktyPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KontrahentKontaktyPanel));
            this.osobyGroupBox = new System.Windows.Forms.GroupBox();
            this.kontaktOsobyDataGridView = new System.Windows.Forms.DataGridView();
            this.kontaktOsobyBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.kontaktGroupBox = new System.Windows.Forms.GroupBox();
            this.kontaktWWWTextBox = new System.Windows.Forms.TextBox();
            this.kontaktEmailTextBox = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.kontaktTelefonKomorkowyTextBox = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.osobyGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kontaktOsobyDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kontaktOsobyBindingNavigator)).BeginInit();
            this.kontaktOsobyBindingNavigator.SuspendLayout();
            this.kontaktGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.DataSource = typeof(Enova.API.CRM.Kontrahent);
            // 
            // osobyGroupBox
            // 
            this.osobyGroupBox.Controls.Add(this.kontaktOsobyDataGridView);
            this.osobyGroupBox.Controls.Add(this.kontaktOsobyBindingNavigator);
            this.osobyGroupBox.Location = new System.Drawing.Point(3, 120);
            this.osobyGroupBox.Name = "osobyGroupBox";
            this.osobyGroupBox.Size = new System.Drawing.Size(714, 385);
            this.osobyGroupBox.TabIndex = 3;
            this.osobyGroupBox.TabStop = false;
            this.osobyGroupBox.Text = "Osoby";
            // 
            // kontaktOsobyDataGridView
            // 
            this.kontaktOsobyDataGridView.AllowUserToAddRows = false;
            this.kontaktOsobyDataGridView.AllowUserToDeleteRows = false;
            this.kontaktOsobyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kontaktOsobyDataGridView.Location = new System.Drawing.Point(0, 44);
            this.kontaktOsobyDataGridView.Name = "kontaktOsobyDataGridView";
            this.kontaktOsobyDataGridView.RowHeadersVisible = false;
            this.kontaktOsobyDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.kontaktOsobyDataGridView.Size = new System.Drawing.Size(711, 341);
            this.kontaktOsobyDataGridView.TabIndex = 1;
            // 
            // kontaktOsobyBindingNavigator
            // 
            this.kontaktOsobyBindingNavigator.AddNewItem = null;
            this.kontaktOsobyBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.kontaktOsobyBindingNavigator.DeleteItem = null;
            this.kontaktOsobyBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.kontaktOsobyBindingNavigator.Location = new System.Drawing.Point(3, 16);
            this.kontaktOsobyBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.kontaktOsobyBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.kontaktOsobyBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.kontaktOsobyBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.kontaktOsobyBindingNavigator.Name = "kontaktOsobyBindingNavigator";
            this.kontaktOsobyBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.kontaktOsobyBindingNavigator.Size = new System.Drawing.Size(708, 25);
            this.kontaktOsobyBindingNavigator.TabIndex = 0;
            this.kontaktOsobyBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // kontaktGroupBox
            // 
            this.kontaktGroupBox.Controls.Add(this.kontaktWWWTextBox);
            this.kontaktGroupBox.Controls.Add(this.kontaktEmailTextBox);
            this.kontaktGroupBox.Controls.Add(this.label33);
            this.kontaktGroupBox.Controls.Add(this.label32);
            this.kontaktGroupBox.Controls.Add(this.kontaktTelefonKomorkowyTextBox);
            this.kontaktGroupBox.Controls.Add(this.label31);
            this.kontaktGroupBox.Location = new System.Drawing.Point(3, 3);
            this.kontaktGroupBox.Name = "kontaktGroupBox";
            this.kontaktGroupBox.Size = new System.Drawing.Size(714, 110);
            this.kontaktGroupBox.TabIndex = 2;
            this.kontaktGroupBox.TabStop = false;
            this.kontaktGroupBox.Text = "Kontakt";
            // 
            // kontaktWWWTextBox
            // 
            this.kontaktWWWTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "KontaktWWW", true));
            this.kontaktWWWTextBox.Location = new System.Drawing.Point(141, 74);
            this.kontaktWWWTextBox.Name = "kontaktWWWTextBox";
            this.kontaktWWWTextBox.Size = new System.Drawing.Size(243, 20);
            this.kontaktWWWTextBox.TabIndex = 5;
            // 
            // kontaktEmailTextBox
            // 
            this.kontaktEmailTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "KontaktEMAIL", true));
            this.kontaktEmailTextBox.Location = new System.Drawing.Point(141, 48);
            this.kontaktEmailTextBox.Name = "kontaktEmailTextBox";
            this.kontaktEmailTextBox.Size = new System.Drawing.Size(243, 20);
            this.kontaktEmailTextBox.TabIndex = 4;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(62, 77);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(73, 13);
            this.label33.TabIndex = 3;
            this.label33.Text = "Adres WWW:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(70, 51);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(65, 13);
            this.label32.TabIndex = 2;
            this.label32.Text = "Adres Email:";
            // 
            // kontaktTelefonKomorkowyTextBox
            // 
            this.kontaktTelefonKomorkowyTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "KontaktTelefonKomorkowy", true));
            this.kontaktTelefonKomorkowyTextBox.Location = new System.Drawing.Point(141, 22);
            this.kontaktTelefonKomorkowyTextBox.Name = "kontaktTelefonKomorkowyTextBox";
            this.kontaktTelefonKomorkowyTextBox.Size = new System.Drawing.Size(243, 20);
            this.kontaktTelefonKomorkowyTextBox.TabIndex = 1;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(32, 25);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(103, 13);
            this.label31.TabIndex = 0;
            this.label31.Text = "Telefon komórkowy:";
            // 
            // KontrahentKontaktyPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.osobyGroupBox);
            this.Controls.Add(this.kontaktGroupBox);
            this.Name = "KontrahentKontaktyPanel";
            this.Size = new System.Drawing.Size(723, 513);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.osobyGroupBox.ResumeLayout(false);
            this.osobyGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kontaktOsobyDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kontaktOsobyBindingNavigator)).EndInit();
            this.kontaktOsobyBindingNavigator.ResumeLayout(false);
            this.kontaktOsobyBindingNavigator.PerformLayout();
            this.kontaktGroupBox.ResumeLayout(false);
            this.kontaktGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox osobyGroupBox;
        private System.Windows.Forms.DataGridView kontaktOsobyDataGridView;
        private System.Windows.Forms.BindingNavigator kontaktOsobyBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.GroupBox kontaktGroupBox;
        private System.Windows.Forms.TextBox kontaktWWWTextBox;
        private System.Windows.Forms.TextBox kontaktEmailTextBox;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox kontaktTelefonKomorkowyTextBox;
        private System.Windows.Forms.Label label31;
    }
}
