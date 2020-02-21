using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Business;

namespace BAL.Forms
{
    public partial class DataEditForm : BAL.Forms.DataForm
    {
        #region Fields

        private List<UserControl> panels;
        private UserControl currentPanel;
        private Dictionary<string, MenuItem> menuItems;
        

        #endregion

        #region Properties

        public override Business.DataContext DataContext
        {
            get
            {
                return base.DataContext;
            }
            set
            {
                if (!this.DesignMode)
                {
                    base.DataContext = value;
                    if (base.DataContext != null)
                    {
                        //base.BindingSource.DataSource = DataContext.GetData(base.DataContext.GetDataType());
                        base.BindingSource.DataSource = DataContext;
                        this.InitMenu();
                        this.InitPanels();
                    }
                }
            }
        }

        #endregion

        #region Methods

        public DataEditForm()
        {
            InitializeComponent();
            this.panels = new List<UserControl>();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AutoSize = true;
        }

        public void AddPanel(string path, UserControl panel)
        {
            this.treeView.Nodes.Add(new PanelTreeNode(path, panel));
            this.panels.Add(panel);
            this.SplitContainer.Panel2.Controls.Add(panel);
            if (panel is IDataContexable)
                ((IDataContexable)panel).DataContext = this.DataContext;

        }

        protected virtual void InitMenu()
        {
            this.menuItems = new Dictionary<string, MenuItem>();
            var attrs = ActionAttribute.GetActions(this.DataContext.GetDataType(), ActionTarget.FormMenu);
            if (attrs != null)
            {
                foreach (var attr in attrs)
                    this.addMenuAction(new Types.Action(attr, this.DataContext));

                foreach (var item in this.menuItems.Values)
                    this.addMenuItem(null, item);
                
            }
        }

        private void addMenuAction(BAL.Types.Action action)
        {
            if (action.Visible)
            {
                var item = getMenuItem(null, action.ActionAttribute.Path);
                if (item != null)
                    item.Action = action;
            }
        }

        private MenuItem getMenuItem(MenuItem parent, string path)
        {
            var parts = path.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                var s = path.Substring(parts[0].Length);
                s = s.Trim('\\');
                Dictionary<string, MenuItem> list = (parent != null) ? parent.Childs : menuItems;
                if (list.ContainsKey(parts[0]))
                {
                    var item = list[parts[0]];
                    return getMenuItem(item, s);
                }
                else
                {
                    MenuItem item = new MenuItem();
                    item.Name = parts[0];
                    list.Add(parts[0], item);
                    return getMenuItem(item, s);
                }
            }
            return parent;
        }


        private void addMenuItem(ToolStripItem parent, MenuItem item)
        {
            bool flag = item.Childs.Count > 0;
            ToolStripItem toolItem = null;
            if (parent == null)
            {
                toolItem = flag ? (ToolStripItem)new ToolStripDropDownButton(item.Name) : (ToolStripItem)new ToolStripButton(item.Name);
                toolItem.AutoSize = true;
                if (item.Action != null)
                {
                    toolItem.Tag = item.Action;
                    if (!string.IsNullOrEmpty(item.Action.ActionAttribute.Description))
                        toolItem.ToolTipText = item.Action.ActionAttribute.Description;
                }
                toolItem.Click += new EventHandler(toolItem_Click);
                this.ToolBar.Items.Add(toolItem);
            }
            else
            {
                ToolStripDropDownItem dropDown = parent as ToolStripDropDownItem;
                if (dropDown != null)
                {
                    toolItem = dropDown.DropDownItems[item.Name];
                    if (toolItem == null)
                    {
                        //toolItem = flag ? (ToolStripItem)new ToolStripDropDownButton(item.Name) : (ToolStripItem)new ToolStripButton(item.Name);
                        toolItem = new ToolStripMenuItem(item.Name);
                        toolItem.AutoSize = true;
                        if (item.Action != null)
                        {
                            toolItem.Tag = item.Action;
                            if (!string.IsNullOrEmpty(item.Action.ActionAttribute.Description))
                                toolItem.ToolTipText = item.Action.ActionAttribute.Description;
                        }
                        toolItem.Click += new EventHandler(toolItem_Click);
                        dropDown.DropDownItems.Add(toolItem);
                    }
                }
            }
            foreach (var child in item.Childs.Values)
                addMenuItem(toolItem, child);
        }

        private void endEdit()
        {
            foreach (var p in this.panels)
            {
                if (p is IEndEdit)
                    ((IEndEdit)p).EndEdit();
            }
        }

        private void toolItem_Click(object sender, EventArgs e)
        {
            endEdit();
            ToolStripItem item = sender as ToolStripItem;
            if (item != null && item.Tag != null)
            {
                BAL.Types.Action action = item.Tag as BAL.Types.Action;
                if (action != null)
                    action.Run();
            }
        }

        private void PrintStripSplitButton_ButtonClick(object sender, EventArgs e)
        {

        }

        protected virtual void InitPanels()
        {
            var attrs = DataPanelAttribute.GetDataPanels(this.DataContext.GetDataType());
            SortedList<int, DataPanelAttribute> list = new SortedList<int, DataPanelAttribute>();
            if (attrs != null)
            {
                foreach (var attr in attrs)
                {
                    int priority = PriorityAttribute.GetPriority(attr.DataPanelType);
                    list.Add(priority, attr);
                }
            }

            foreach (var attr in list.Values)
            {
                UserControl panel = (UserControl)attr.DataPanelType.GetConstructor(new Type[0]).Invoke(new object[0]);
                panel.Visible = false;
                panel.AutoSize = true;
                panel.Dock = DockStyle.Fill;
                this.AddPanel(attr.Path, panel);
            }

            if (this.panels.Count > 1)
                this.SplitContainer.Panel1Collapsed = false;

            this.calculateSize();

            if (this.panels.Count > 0)
                this.SelectPanel(this.panels[0]);
        }

        public virtual void SelectPanel(UserControl panel)
        {
            if (this.currentPanel != null)
                this.currentPanel.Visible = false;
            this.currentPanel = panel;
            this.currentPanel.Visible = true;
            panel.Select();
        }

        private void calculateSize()
        {
            int width = 0;
            int height = 0;
            foreach (var panel in this.panels)
            {
                width = panel.Size.Width > width ? panel.Size.Width : width;
                height = panel.Size.Height > height ? panel.Size.Height : height;
            }
            this.Size = new Size(width + this.SplitContainer.Panel1.Width + 25, height + 70);
        }

        public virtual void ProcessOKButton()
        {
            foreach (var panel in this.panels)
            {
                if (panel is IEndEdit)
                    ((IEndEdit)panel).EndEdit();
                string msg = "";
                if (panel is IValidator && !((IValidator)panel).IsValid(out msg))
                {
                    if (string.IsNullOrEmpty(msg))
                        msg = panel.GetType().ToString() + " validator error";
                    FormManager.Alert(msg);
                    return;
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ToolBar.Select();
            foreach (var p in this.panels)
            {
                if (typeof(DataPanel).IsAssignableFrom(p.GetType()))
                    ((DataPanel)p).EndEdit();
            }
            this.Close();
        }

        public virtual void ProcessCancelButton()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.ProcessOKButton();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.ProcessCancelButton();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
            {
                PanelTreeNode node = (PanelTreeNode)e.Node;
                this.SelectPanel(node.Panel);
            }
        }

        private void DataEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Shift && e.Control && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = false;
                this.okButton_Click(null, null);
            }
            else if (!e.Control && !e.Alt && !e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        e.Handled = true;
                        e.SuppressKeyPress = false;
                        this.cancelButton_Click(null, null);
                        break;
                    
                }
            }
        }

        #endregion

        #region Nested Types

        public class PanelTreeNode : TreeNode
        {
            #region Fields

            private UserControl panel;

            #endregion

            #region Properties

            public UserControl Panel
            {
                get { return this.panel; }
            }

            #endregion

            #region Methods

            public PanelTreeNode(string text, UserControl panel)
                : base(text)
            {
                this.panel = panel;
            }

            #endregion
        }

        private class MenuItem
        {
            public string Name;
            public MenuItem Parent;
            public int Priority;
            public BAL.Types.Action Action;
            public Dictionary<string, MenuItem> Childs = new Dictionary<string, MenuItem>();

        }

        #endregion

    }
}
