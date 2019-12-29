namespace AbakTools.Business.Forms
{
    partial class UzytkownicyForm
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
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdminColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsWarehousemanColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AgentColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AgentCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).BeginInit();
            this.TopSplitContainer.Panel2.SuspendLayout();
            this.TopSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).BeginInit();
            this.LeftSplitContainer.Panel2.SuspendLayout();
            this.LeftSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).BeginInit();
            this.RightSplitContainer.Panel1.SuspendLayout();
            this.RightSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer)).BeginInit();
            this.BottomSplitContainer.Panel1.SuspendLayout();
            this.BottomSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TopSplitContainer
            // 
            this.TopSplitContainer.Panel1Collapsed = true;
            this.TopSplitContainer.Size = new System.Drawing.Size(624, 410);
            // 
            // DataGridBindingSource
            // 
            this.DataGridBindingSource.DataSource = typeof(Enova.Business.Old.DB.Web.User);
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Panel1Collapsed = true;
            this.LeftSplitContainer.Size = new System.Drawing.Size(624, 410);
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(624, 410);
            this.RightSplitContainer.SplitterDistance = 1198;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(624, 410);
            this.BottomSplitContainer.SplitterDistance = 549;
            // 
            // DataGrid
            // 
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Login,
            this.AdminColumn,
            this.IsWarehousemanColumn,
            this.AgentColumn,
            this.AgentCodeColumn});
            this.DataGrid.Size = new System.Drawing.Size(624, 410);
            // 
            // Login
            // 
            this.Login.DataPropertyName = "Login";
            this.Login.HeaderText = "User";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            // 
            // AdminColumn
            // 
            this.AdminColumn.DataPropertyName = "IsAdmin";
            this.AdminColumn.HeaderText = "Admin";
            this.AdminColumn.Name = "AdminColumn";
            this.AdminColumn.ReadOnly = true;
            // 
            // IsWarehousemanColumn
            // 
            this.IsWarehousemanColumn.DataPropertyName = "IsWarehouseman";
            this.IsWarehousemanColumn.HeaderText = "Magazynier";
            this.IsWarehousemanColumn.Name = "IsWarehousemanColumn";
            this.IsWarehousemanColumn.ReadOnly = true;
            this.IsWarehousemanColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsWarehousemanColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // AgentColumn
            // 
            this.AgentColumn.DataPropertyName = "IsAgent";
            this.AgentColumn.HeaderText = "Agent";
            this.AgentColumn.Name = "AgentColumn";
            this.AgentColumn.ReadOnly = true;
            // 
            // AgentCodeColumn
            // 
            this.AgentCodeColumn.DataPropertyName = "AgentCode";
            this.AgentCodeColumn.HeaderText = "Kod agenta";
            this.AgentCodeColumn.Name = "AgentCodeColumn";
            this.AgentCodeColumn.ReadOnly = true;
            // 
            // UzytkownicyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(624, 435);
            this.DataSource = typeof(Enova.Business.Old.DB.Web.User);
            this.LeftPanelCollapsed = true;
            this.Name = "UzytkownicyForm";
            this.RightPanelCollapsed = true;
            this.Text = "Użytkownicy";
            this.TopPanelCollapsed = true;
            this.Load += new System.EventHandler(this.UzytkownicyForm_Load);
            this.Controls.SetChildIndex(this.TopSplitContainer, 0);
            this.TopSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).EndInit();
            this.TopSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).EndInit();
            this.LeftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).EndInit();
            this.LeftSplitContainer.ResumeLayout(false);
            this.RightSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).EndInit();
            this.RightSplitContainer.ResumeLayout(false);
            this.BottomSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BottomSplitContainer)).EndInit();
            this.BottomSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AdminColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsWarehousemanColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AgentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgentCodeColumn;


    }
}
