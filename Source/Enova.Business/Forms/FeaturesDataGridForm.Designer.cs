namespace Enova.Business.Old.Forms
{
    partial class FeaturesDataGridForm
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
            this.FeaturesTreeView = new Enova.Business.Old.Controls.FeaturesTree();
            this.TopSplitContainer.Panel2.SuspendLayout();
            this.TopSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).BeginInit();
            this.LeftSplitContainer.Panel1.SuspendLayout();
            this.LeftSplitContainer.Panel2.SuspendLayout();
            this.LeftSplitContainer.SuspendLayout();
            this.RightSplitContainer.Panel1.SuspendLayout();
            this.RightSplitContainer.SuspendLayout();
            this.BottomSplitContainer.Panel1.SuspendLayout();
            this.BottomSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TopSplitContainer
            // 
            this.TopSplitContainer.Panel1Collapsed = true;
            this.TopSplitContainer.SplitterDistance = 55;
            // 
            // LeftSplitContainer
            // 
            // 
            // LeftSplitContainer.Panel1
            // 
            this.LeftSplitContainer.Panel1.Controls.Add(this.FeaturesTreeView);
            this.LeftSplitContainer.Size = new System.Drawing.Size(1351, 664);
            this.LeftSplitContainer.SplitterDistance = 188;
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Panel2Collapsed = true;
            this.RightSplitContainer.Size = new System.Drawing.Size(1159, 664);
            this.RightSplitContainer.SplitterDistance = 1028;
            // 
            // BottomSplitContainer
            // 
            this.BottomSplitContainer.Panel2Collapsed = true;
            this.BottomSplitContainer.Size = new System.Drawing.Size(1159, 664);
            this.BottomSplitContainer.SplitterDistance = 500;
            // 
            // DataGrid
            // 
            this.DataGrid.AllowDrop = true;
            this.DataGrid.Size = new System.Drawing.Size(1159, 664);
            this.DataGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.DataGrid_DragEnter);
            // 
            // FeaturesTreeView
            // 
            this.FeaturesTreeView.AllowDrop = true;
            this.FeaturesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FeaturesTreeView.LineColor = System.Drawing.Color.Black;
            this.FeaturesTreeView.Location = new System.Drawing.Point(0, 0);
            this.FeaturesTreeView.Name = "FeaturesTreeView";
            this.FeaturesTreeView.Size = new System.Drawing.Size(188, 664);
            this.FeaturesTreeView.TabIndex = 0;
            this.FeaturesTreeView.TableName = null;
            this.FeaturesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FeaturesTreeView_AfterSelect);
            this.FeaturesTreeView.Load += new System.EventHandler(this.FeaturesTreeView_Load);
            this.FeaturesTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.FeaturesTreeView_DragDrop);
            this.FeaturesTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.FeaturesTreeView_DragEnter);
            this.FeaturesTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.FeaturesTreeView_DragOver);
            this.FeaturesTreeView.DragLeave += new System.EventHandler(this.FeaturesTreeView_DragLeave);
            this.FeaturesTreeView.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.FeaturesTreeView_GiveFeedback);
            this.FeaturesTreeView.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.FeaturesTreeView_QueryContinueDrag);
            // 
            // FeaturesDataGridForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BottomPanelCollapsed = true;
            this.ClientSize = new System.Drawing.Size(1351, 689);
            this.Name = "FeaturesDataGridForm";
            this.RightPanelCollapsed = true;
            this.TopPanelCollapsed = true;
            this.Controls.SetChildIndex(this.TopSplitContainer, 0);
            this.TopSplitContainer.Panel2.ResumeLayout(false);
            this.TopSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).EndInit();
            this.LeftSplitContainer.Panel1.ResumeLayout(false);
            this.LeftSplitContainer.Panel2.ResumeLayout(false);
            this.LeftSplitContainer.ResumeLayout(false);
            this.RightSplitContainer.Panel1.ResumeLayout(false);
            this.RightSplitContainer.ResumeLayout(false);
            this.BottomSplitContainer.Panel1.ResumeLayout(false);
            this.BottomSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected Enova.Business.Old.Controls.FeaturesTree FeaturesTreeView;




    }
}
