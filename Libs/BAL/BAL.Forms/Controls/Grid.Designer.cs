namespace BAL.Forms.Controls
{
    partial class Grid
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gridNavigator = new BAL.Forms.Controls.GridNavigator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.positionToolTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.couterToolLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.gridView = new BAL.Forms.Controls.GridView();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.moveFirstToolButton = new System.Windows.Forms.ToolStripButton();
            this.movePrevToolButton = new System.Windows.Forms.ToolStripButton();
            this.moveNextToolButton = new System.Windows.Forms.ToolStripButton();
            this.moveLastToolButton = new System.Windows.Forms.ToolStripButton();
            this.addToolButton = new System.Windows.Forms.ToolStripButton();
            this.editToolButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolButton = new System.Windows.Forms.ToolStripButton();
            this.refreshToolButton = new System.Windows.Forms.ToolStripButton();
            this.printToolButton = new System.Windows.Forms.ToolStripSplitButton();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNavigator)).BeginInit();
            this.gridNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.gridNavigator, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.gridView, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(607, 352);
            this.tableLayoutPanel.TabIndex = 0;
            this.tableLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel_Paint);
            // 
            // gridNavigator
            // 
            this.gridNavigator.AddNewItem = null;
            this.gridNavigator.BindingSource = this.bindingSource;
            this.gridNavigator.CountItem = null;
            this.gridNavigator.DeleteItem = null;
            this.gridNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveFirstToolButton,
            this.movePrevToolButton,
            this.toolStripSeparator1,
            this.positionToolTextBox,
            this.couterToolLabel,
            this.toolStripSeparator2,
            this.moveNextToolButton,
            this.moveLastToolButton,
            this.toolStripSeparator3,
            this.addToolButton,
            this.editToolButton,
            this.deleteToolButton,
            this.toolStripSeparator4,
            this.refreshToolButton,
            this.printToolButton});
            this.gridNavigator.Location = new System.Drawing.Point(0, 0);
            this.gridNavigator.MoveFirstItem = null;
            this.gridNavigator.MoveLastItem = null;
            this.gridNavigator.MoveNextItem = null;
            this.gridNavigator.MovePreviousItem = null;
            this.gridNavigator.Name = "gridNavigator";
            this.gridNavigator.PositionItem = null;
            this.gridNavigator.Size = new System.Drawing.Size(607, 25);
            this.gridNavigator.TabIndex = 0;
            this.gridNavigator.Text = "gridNavigator1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // positionToolTextBox
            // 
            this.positionToolTextBox.Name = "positionToolTextBox";
            this.positionToolTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.positionToolTextBox.Size = new System.Drawing.Size(60, 25);
            this.positionToolTextBox.Text = "0";
            // 
            // couterToolLabel
            // 
            this.couterToolLabel.Name = "couterToolLabel";
            this.couterToolLabel.Size = new System.Drawing.Size(35, 22);
            this.couterToolLabel.Text = "of {0}";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // gridView
            // 
            this.gridView.AutoGenerateColumns = false;
            this.gridView.DataSource = this.bindingSource;
            this.gridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView.Location = new System.Drawing.Point(3, 28);
            this.gridView.Name = "gridView";
            this.gridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridView.Size = new System.Drawing.Size(601, 321);
            this.gridView.TabIndex = 1;
            this.gridView.VirtualMode = true;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // moveFirstToolButton
            // 
            this.moveFirstToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveFirstToolButton.Image = global::BAL.Forms.Properties.Resources.MoveFirstItem;
            this.moveFirstToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveFirstToolButton.Name = "moveFirstToolButton";
            this.moveFirstToolButton.Size = new System.Drawing.Size(23, 22);
            this.moveFirstToolButton.Text = "First";
            // 
            // movePrevToolButton
            // 
            this.movePrevToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePrevToolButton.Image = global::BAL.Forms.Properties.Resources.MovePreviousItem;
            this.movePrevToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePrevToolButton.Name = "movePrevToolButton";
            this.movePrevToolButton.Size = new System.Drawing.Size(23, 22);
            this.movePrevToolButton.Text = "Prev";
            // 
            // moveNextToolButton
            // 
            this.moveNextToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveNextToolButton.Image = global::BAL.Forms.Properties.Resources.MoveNextItem;
            this.moveNextToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveNextToolButton.Name = "moveNextToolButton";
            this.moveNextToolButton.Size = new System.Drawing.Size(23, 22);
            this.moveNextToolButton.Text = "Next";
            // 
            // moveLastToolButton
            // 
            this.moveLastToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveLastToolButton.Image = global::BAL.Forms.Properties.Resources.MoveLastItem;
            this.moveLastToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveLastToolButton.Name = "moveLastToolButton";
            this.moveLastToolButton.Size = new System.Drawing.Size(23, 22);
            this.moveLastToolButton.Text = "Last";
            // 
            // addToolButton
            // 
            this.addToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addToolButton.Image = global::BAL.Forms.Properties.Resources.AddNewItem;
            this.addToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToolButton.Name = "addToolButton";
            this.addToolButton.Size = new System.Drawing.Size(23, 22);
            this.addToolButton.Text = "Add";
            // 
            // editToolButton
            // 
            this.editToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolButton.Image = global::BAL.Forms.Properties.Resources.OpenForm;
            this.editToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolButton.Name = "editToolButton";
            this.editToolButton.Size = new System.Drawing.Size(23, 22);
            this.editToolButton.Text = "Edit";
            // 
            // deleteToolButton
            // 
            this.deleteToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolButton.Image = global::BAL.Forms.Properties.Resources.DeleteItem;
            this.deleteToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolButton.Name = "deleteToolButton";
            this.deleteToolButton.Size = new System.Drawing.Size(23, 22);
            this.deleteToolButton.Text = "Delete";
            // 
            // refreshToolButton
            // 
            this.refreshToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolButton.Image = global::BAL.Forms.Properties.Resources.Refresh;
            this.refreshToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshToolButton.Name = "refreshToolButton";
            this.refreshToolButton.Size = new System.Drawing.Size(23, 22);
            this.refreshToolButton.Text = "Refresh";
            // 
            // printToolButton
            // 
            this.printToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolButton.Image = global::BAL.Forms.Properties.Resources.Print;
            this.printToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolButton.Name = "printToolButton";
            this.printToolButton.Size = new System.Drawing.Size(32, 22);
            this.printToolButton.Text = "Print";
            // 
            // DataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "DataGrid";
            this.Size = new System.Drawing.Size(607, 352);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNavigator)).EndInit();
            this.gridNavigator.ResumeLayout(false);
            this.gridNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        public GridNavigator gridNavigator;
        public GridView gridView;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.ToolStripButton moveFirstToolButton;
        private System.Windows.Forms.ToolStripButton movePrevToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox positionToolTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel couterToolLabel;
        private System.Windows.Forms.ToolStripButton moveNextToolButton;
        private System.Windows.Forms.ToolStripButton moveLastToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton addToolButton;
        private System.Windows.Forms.ToolStripButton editToolButton;
        private System.Windows.Forms.ToolStripButton deleteToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton refreshToolButton;
        private System.Windows.Forms.ToolStripSplitButton printToolButton;

    }
}
