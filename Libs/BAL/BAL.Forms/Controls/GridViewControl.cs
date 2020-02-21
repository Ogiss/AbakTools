using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using BAL.Types;
using BAL.Business;

namespace BAL.Forms.Controls
{
    public partial class GridViewControl : BAL.Forms.Controls.BALControl
    {
        #region Fields

        private object loadingLock = new object();
        private bool isLoadingRows;
        private bool disableFirePanels_SplitterMoved = false;
        private SortedSet<ActionInfo> actions;
        private List<MethodInvoker> onSelectionChangeActions;
        private Dictionary<string, ParamInfo> dataContextParams;
        private TableLayoutPanel headerTable;
        private FlowLayoutPanel systemPanel;
        private FlowLayoutPanel paramsPanel;
        private FlowLayoutPanel actionsPanel;
        private TextBox findTextBox;
        private bool? topPanelCollapsed;
        private bool cleanFindText;
        private AutoResetEvent reloadResetEvent;
        private IGridTemplate gridTemplate;


        #endregion

        #region Properties

        [Browsable(false)]
        public bool IsLoadingRows
        {
            get
            {
                lock (loadingLock)
                    return this.isLoadingRows;
            }
        }

        [Browsable(false)]
        public BAL.Business.View View
        {
            get
            {
                return base.DataContext as BAL.Business.View;
            }
        }

        #endregion

        #region Methods

        public GridViewControl()
        {
            InitializeComponent();
            this.reloadResetEvent = new AutoResetEvent(true);
            this.refreshItem.Click += new EventHandler(bindingNavigatorRefreshItem_Click);
            this.addNewItem.Click += new EventHandler(bindingNavigatorAddNewItem_Click);
            this.editItem.Click += new EventHandler(bindingNavigatorEditItem_Click);
            this.deleteItem.Click += new EventHandler(bindingNavigatorDeleteItem_Click);
            this.gridConfigItem.Click += new EventHandler(bindingNavigatorGridConfig_Click);
            //this.extraPanelVisibleItem.Click += new EventHandler(bindingNavigatorExtraPanelVisible_Click);
        }

        protected override void OnAfterBinding(EventArgs e)
        {
            if (View != null)
            {
                View.PositionChanged += (sender, arg) =>
                {
                    if (this.BindingSource.Position != this.View.Position)
                        this.BindingSource.Position = this.View.Position;
                };
                this.BindingSource.PositionChanged += (sender, arg) =>
                {
                    if (this.View.Position != this.BindingSource.Position)
                        this.View.Position = this.BindingSource.Position;
                };
                this.Init();
             }
            base.OnAfterBinding(e);
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            this.addNewItem.Enabled = !this.ReadOnly;
            this.deleteItem.Enabled = !this.ReadOnly;
            base.OnReadOnlyChanged(e);
        }

        protected virtual void Init()
        {
            if (!this.View.IsLoaded)
                this.View.Load();
            addNewItem.Enabled = this.View.AllowNew;
            editItem.Enabled = this.View.AllowEdit;
            deleteItem.Enabled = this.View.AllowRemove;
            this.actions = this.GetAllActions();
            this.InitSystemPanel();
            InitExtraPanel();
            InitColumns();
            this.InitGridTemplate();
            this.InitStoredProperties();
            this.ActiveControl = this.GridView;
            Reload();
        }

        public virtual SortedSet<ActionInfo> GetAllActions()
        {
            SortedSet<ActionInfo> list = new SortedSet<ActionInfo>();
            if (this.DataContext != null)
            {
                var types = this.DataContext.GetRowActionsTypes();
                if (types != null)
                {
                    foreach (var type in types)
                    {
                        var action = new ActionInfo(type);
                        if (action.PropertyFireReload != null)
                            action.PropertyFireReload.SetValue(action.Action, new ActionInvoker((sender) => { this.Reload(); }), null);
                        if (action.PropertyFireRefresh != null)
                            action.PropertyFireRefresh.SetValue(action.Action, new ActionInvoker((sender) => { this.GridView.Refresh(); }), null);
                        if (action.PropertyFireSelectionChanged != null)
                            action.PropertyFireSelectionChanged.SetValue(action.Action, new ActionInvoker((sender) => { this.GridView_SelectionChanged(sender, null); }), null);
                        list.Add(action);
                    }
                }
            }
            return list;
        }

        protected virtual void InitSystemPanel()
        {
            if (this.DataContext != null)
            {
                this.disableFirePanels_SplitterMoved = true;
                var dcParams = this.DataContext.GetParams();
                var actions = new List<ActionInfo>();
                foreach (var a in this.actions)
                {
                    if ((a.Target & Types.ActionTarget.GridHeader) == Types.ActionTarget.GridHeader)
                        actions.Add(a);
                }

                this.headerTable = new TableLayoutPanel();
                this.headerTable.ColumnCount = 1;
                this.headerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                this.headerTable.Dock = DockStyle.Fill;
                int rowsCount = 1;
                if (dcParams != null && dcParams.Count() > 0)
                    rowsCount++;
                if (actions != null && actions.Count > 0)
                    rowsCount++;
                this.headerTable.RowCount = rowsCount;
                for (int i = 0; i < rowsCount; i++)
                    this.headerTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                if (rowsCount > 1)
                    this.topSplitContainer.Panel1Collapsed = false;
                this.topSplitContainer.SplitterDistance = rowsCount * 34;
                this.topSplitContainer.Panel1.Controls.Add(this.headerTable);

                this.initSystemControls();
                if (dcParams != null && dcParams.Count() > 0) this.initParams(dcParams);
                if (actions != null && actions.Count > 0) this.initActionsButtons(actions);
                int? height = (int?)DataContext.GetStoredProperty("TopPanelSpliterDistance");
                if (height != null && height >= 0)
                    this.topSplitContainer.SplitterDistance = height.Value;
                this.disableFirePanels_SplitterMoved = false;

            }
        }

        private void initSystemControls()
        {
            this.systemPanel = new FlowLayoutPanel();
            this.systemPanel.Dock = DockStyle.Fill;
            this.systemPanel.AutoSize = true;
            this.headerTable.Controls.Add(this.systemPanel, 0, 0);

            this.findTextBox = new TextBox();
            this.findTextBox.Size = new Size(300, this.findTextBox.Size.Height);
            this.findTextBox.Visible = false;
            this.findTextBox.KeyDown += new KeyEventHandler(findTextBox_KeyDown);
            this.findTextBox.TextChanged += new EventHandler(findTextBox_TextChanged);
            this.systemPanel.Controls.Add(this.findTextBox);
        }

        private void initParams(IEnumerable<DataContextParam> __params__)
        {
            this.dataContextParams = new Dictionary<string, ParamInfo>();
            this.paramsPanel = new FlowLayoutPanel();
            this.paramsPanel.Dock = DockStyle.Fill;
            this.paramsPanel.AutoSize = true;
            this.headerTable.Controls.Add(this.paramsPanel, 0, 1);
            foreach (var param in __params__)
            {
                Control control = null;
                if (param.ControlType != null)
                    control = (Control)param.ControlType.GetConstructor(new Type[0]).Invoke(new object[0]);
                else
                {
                    Type type = null;
                    var attrs =  param.PropertyPath.Last.GetCustomAttributes(typeof(BAL.Types.ParamControlAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                        type = ((BAL.Types.ParamControlAttribute)attrs[0]).ControlType;
                    else
                        type = ParamControlAttribute.GetParamControlType(param.PropertyPath.Last.PropertyType);
                    if (type != null)
                        control = (Control)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                }

                if (control == null)
                    control = FormManager.FormService.GetDataContextParamControl(param);

                if (control != null)
                {
                    control.Name = param.Name;
                    Label label = null;
                    if (!string.IsNullOrEmpty(param.Label))
                    {
                        label = new Label();
                        label.AutoSize = true;
                        label.Text = param.Label;
                        label.Location = new Point(label.Location.X, control.Location.Y + 3);
                        this.paramsPanel.Controls.Add(label);
                    }
                    this.paramsPanel.Controls.Add(control);
                    if (label != null)
                    {
                        int y = control.Location.Y;
                        if (label.Size.Height < control.Size.Height)
                            y += (control.Size.Height - label.Size.Height) / 2;
                        label.Margin = new System.Windows.Forms.Padding(label.Margin.Left, y, label.Margin.Right, label.Margin.Bottom);
                    }
                }
                this.dataContextParams.Add(param.Name, new ParamInfo() { Param = param, Control = control });
                this.DataContext.FireInitParam(param, control);
                if (control is BAL.Types.INotifyValueChanged)
                    ((BAL.Types.INotifyValueChanged)control).ValueChanged += new EventHandler(ParamControl_ValueChanged);
            }
        }

        private void initActionsButtons(IEnumerable<ActionInfo> actions)
        {
            this.actionsPanel = new FlowLayoutPanel();
            this.actionsPanel.AutoSize = true;
            this.actionsPanel.Dock = DockStyle.Fill;
            this.headerTable.Controls.Add(this.actionsPanel, 0, 2);
            this.onSelectionChangeActions = new List<MethodInvoker>();
            foreach (var action in actions)
            {
                var ctype = ControlTypeAttribute.GetControlType(action.Action, typeof(Controls.ActionButton));
                Control control = (Control)ctype.GetConstructor(new Type[] { typeof(object) }).Invoke(new object[] { action.Action });
                if (action.MarginLeft != null || action.MarginTop != null || action.MarginBottom != null || action.MarginRight != null)
                {
                    Padding margin = new Padding(
                        action.MarginLeft != null ? action.MarginLeft.Value : control.Margin.Left,
                        action.MarginTop != null ? action.MarginTop.Value : control.Margin.Top,
                        action.MarginRight != null ? action.MarginRight.Value : control.Margin.Right,
                        action.MarginBottom != null ? action.MarginBottom.Value : control.Margin.Bottom);
                    control.Margin = margin;
                }
                action.control = control;
                control.AutoSize = true;
                this.actionsPanel.Controls.Add(control);
            }
        }

        protected virtual void InitExtraPanel()
        {
            if (this.View != null)
            {
                if (typeof(GridViewContext).IsAssignableFrom(this.View.GetType()) && ((GridViewContext)this.View).ExtraPanelAvailable)
                {
                    disableFirePanels_SplitterMoved = true;
                    this.extraPanelVisibleItem.Visible = true;
                    this.extraPanelVisibleItem.Checked = ((GridViewContext)this.View).ExtraPanelVisible;
                    this.verticalSplitContainer.Panel1Collapsed = !((GridViewContext)this.View).ExtraPanelVisible;
                    ((GridViewContext)this.View).InitExtraPanel(this.verticalSplitContainer.Panel1);
                    int? width = null;
                    if (this.DataContext != null)
                        width = (int?)this.DataContext.GetStoredProperty("LeftPanelSpliterDistance");
                    if (width == null)
                    {
                        foreach (Control c in this.verticalSplitContainer.Panel1.Controls)
                        {
                            if (c.Width > width)
                                width = c.Width;
                        }
                    }
                    if (width != null && width.Value > 0)
                        this.verticalSplitContainer.SplitterDistance = width.Value;



                    disableFirePanels_SplitterMoved = false;
                }
            }
        }

        protected virtual void InitColumns()
        {
            if (this.DataContext != null)
            {
                
                var columns = this.DataContext.VisibleColumns.GetVisible().OrderBy(c => c.Order);
                foreach (object col in columns)
                {
                    var column = this.CreateGridColumn(col);
                    this.GridView.Columns.Add(column);
                    var gc = this.DataContext as GridViewContext;
                    if (gc != null)
                        gc.InitGridColumn(column);

                }
                this.DataContext.ColumnChanged += new EventHandler<Types.ColumnChangedEventArgs>(DataContext_ColumnChanged);
                this.DataContext.ColumnAdded += new EventHandler<Types.ColumnEventArgs>(DataContext_ColumnAdded);
            }
        }

        protected virtual DataGridViewColumn CreateGridColumn(object columnInfo)
        {
            BAL.Types.Column info = columnInfo as BAL.Types.Column;
            if (info != null)
            {
                DataGridViewColumn column = null;
                Type ut = Nullable.GetUnderlyingType(info.Type);
                Type dataType = ut ?? info.Type;
                switch (dataType.Name.ToLower())
                {
                    case "boolean":
                        column = new BAL.Forms.Controls.GridViewCheckBoxColumn();
                        break;
                    default:
                        column = new DataGridViewTextBoxColumn();
                        break;
                }
                column.Name = info.Name;
                column.HeaderText = info.HeaderText;
                column.DataPropertyName = info.PropertyDescriptorPath!= null ? info.PropertyDescriptorPath.ToString() : info.PropertyPath.ToString();
                column.Width = info.Width > 0 ? info.Width : 100;
                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.ReadOnly = info.ReadOnly;
                //column.SortMode = DataGridViewColumnSortMode.Programmatic;
                column.ToolTipText = info.PropertyDescriptorPath != null ? info.PropertyDescriptorPath.ToString() : info.PropertyPath.ToString();
                switch (info.TextAlign)
                {
                    case Types.TextAlign.None:
                    case Types.TextAlign.Left:
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case Types.TextAlign.Right:
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case Types.TextAlign.Center:
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                }
                switch (info.HeaderTextAlign)
                {
                    case Types.TextAlign.None:
                    case Types.TextAlign.Left:
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case Types.TextAlign.Right:
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case Types.TextAlign.Center:
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                }

                if (!string.IsNullOrEmpty(info.Format))
                    column.DefaultCellStyle.Format = info.Format;

                return column;
            }
            return null;
        }

        private void reorderColumns()
        {
            int i = 0;
            foreach (var column in this.DataContext.VisibleColumns.GetVisible().OrderBy(c => c.Order))
            {
                this.GridView.Columns[column.Name].DisplayIndex = i++;
            }
        }

        protected virtual void InitGridTemplate()
        {
            string key = this.DataContext != null ? this.DataContext.Key : null;
            var service = FormManager.GetGridService(key);
            if (service != null)
            {
                var t = service.GetDefaultGridTemplateType(key);
                if (t != null)
                    this.gridTemplate = (IGridTemplate)t.GetConstructor(new Type[0]).Invoke(new object[0]);
                if (this.gridTemplate != null)
                    this.GridView.RowPrePaint += new DataGridViewRowPrePaintEventHandler(GridView_RowPrePaint);
            }
        }

        protected virtual void InitStoredProperties()
        {
            if (this.DataContext != null)
            {
                var properties = this.DataContext.GetStoredProperties().ToList();
                foreach (var kvp in properties)
                {
                    switch (kvp.Key.ToLower())
                    {
                        case "columnheadersheight":
                            this.GridView.ColumnHeadersHeight = (int)kvp.Value;
                            break;
                        case "gridviewreadonly":
                            this.GridView.ReadOnly = (bool)kvp.Value;
                            break;
                    }
                }
            }
        }

        public virtual void Reload()
        {
            if (!this.loadRowsWorker.IsBusy)
                this.loadRowsWorker.RunWorkerAsync();
        }

        protected virtual IEnumerable GetRows()
        {
            if (this.View != null)
                this.View.Reload();
            return this.View;
        }

        private void loadRows()
        {
            if (this.ParentForm.InvokeRequired)
            {
                var d = new MethodInvoker(this.loadRows);
                this.ParentForm.Invoke(d);
            }
            else
            {
                using (new WaitCursor(this))
                {
                    try
                    {
                        this.GridView.Invoke(new MethodInvoker(() =>
                        {
                            this.BindingSource.DataSource = this.View;
                            this.GridView.Refresh();
                        }));
                    }
                    catch (Exception ex)
                    {
                        AppController.ThrowException(ex);
                    }
                }

            }
        }


        private void gridViewRefresh()
        {
            if (this.InvokeRequired)
                this.Invoke(new System.Action(this.gridViewRefresh));
            else
                this.GridView.Refresh();
        }

        private IEnumerable getSelectedRows()
        {
            ArrayList list = new ArrayList();
            foreach (DataGridViewRow row in this.GridView.SelectedRows)
                list.Add(row.DataBoundItem);
            return list;
        }

        private void addNewRow()
        {
            if (this.View != null && this.View.AllowNew)
            {
                this.View.AddNew();
            }
        }

        private void editCurrentRow()
        {
            if (this.View != null && View.AllowEdit)
                this.View.EditCurrent();
         }

        private void deleteCurrentRow()
        {
            this.deleteGridViewRow(this.GridView.CurrentRow);
        }

        protected virtual void deleteGridViewRow(DataGridViewRow row)
        {
            if (this.View != null && !this.View.AllowRemove)
                return;

            if (!FormManager.Confirm("Czy napewno chcesz usunąć rekord?"))
                return;

            object data = row.DataBoundItem;
            if (this.View != null)
                this.View.Remove(data);
        }

        #endregion

        #region Events
        #endregion

        #region Events handlers

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.DataContext != null)
            {
                IEnumerable selectedRows = null;

                foreach (var action in this.actions)
                {
                    if (action.PropertySelectedRows != null)
                    {
                        if (selectedRows == null)
                            selectedRows = this.getSelectedRows();
                        action.PropertySelectedRows.SetValue(action.Action, selectedRows, null);
                    }

                    if (action.PropertyCurrentRow != null)
                        action.PropertyCurrentRow.SetValue(action.Action, this.GridView.CurrentRow != null ? this.GridView.CurrentRow.DataBoundItem : null, null);

                    if (action.MethodOnSelectionChanged != null)
                        action.MethodOnSelectionChanged.Invoke(action.Action, null);
                }

                if (cleanFindText)
                {
                    findTextBox.Text = "";
                    //findTextBox.Visible = false;
                    //if (this.topPanelCollapsed != null)
                    //    this.TopPanel.Panel1Collapsed = this.topPanelCollapsed.Value;
                    cleanFindText = false;
                }
            }

        }

        private void loadRowsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.IsLoadingRows)
                return;

            try
            {
                lock (loadingLock)
                    this.isLoadingRows = true;
                loadRows();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lock (loadingLock)
                    this.isLoadingRows = false;
            }

        }

        private void ParamControl_ValueChanged(object sender, EventArgs e)
        {
            if (this.DataContext != null)
            {
                Control control = (Control)sender;
                if (this.dataContextParams != null && this.dataContextParams.ContainsKey(control.Name))
                {
                    var info = this.dataContextParams[control.Name];
                    this.DataContext.FireParamValueChanged(info.Param, control);
                }
            }
        }

        private void findTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = false;
                this.findTextBox.Visible = false;
                int idx = this.View.Find(this.View.SortProperty, this.findTextBox.Text);
                if (idx >= 0 && idx < this.GridView.Rows.Count)
                {
                    this.View.Position = idx;
                }
                this.GridView.Select();
                if (this.topPanelCollapsed != null)
                    this.TopPanel.Panel1Collapsed = this.topPanelCollapsed.Value;
                this.fireKeyPress = true;
            }
             */
        }

        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(findTextBox.Text))
            {
                cleanFindText = false;
                if (this.topPanelCollapsed == null)
                    this.topPanelCollapsed = this.topSplitContainer.Panel1Collapsed;
                if (this.topSplitContainer.Panel1Collapsed)
                    this.topSplitContainer.Panel1Collapsed = false;

                if (!findTextBox.Visible)
                    findTextBox.Visible = true;

                int idx = this.View.Find(this.View.SortProperty, this.findTextBox.Text);
                if (idx >= 0 && idx < this.GridView.Rows.Count)
                {
                    this.View.Position = idx;
                }
                this.GridView.Select();

                cleanFindText = true;
            }
            else
            {
                findTextBox.Visible = false;
                if (this.topPanelCollapsed != null)
                {
                    this.topSplitContainer.Panel1Collapsed = this.topPanelCollapsed.Value;
                    this.topPanelCollapsed = null;
                }
            }
        }

        private void bindingNavigatorRefreshItem_Click(object sender, EventArgs e)
        {
            this.Reload();
        }

        private void bindingNavigatorGridConfig_Click(object sender, EventArgs e)
        {
            var form = new GridConfigForm(this);
            form.StartPosition = FormStartPosition.Manual;
            Rectangle rec = new Rectangle(this.GridView.Location.X + this.GridView.Size.Width - form.Size.Width, this.GridView.Location.Y, form.Width, this.GridView.Size.Height);
            rec = this.GridView.RectangleToScreen(rec);
            form.Location = new Point(rec.X, rec.Y);
            form.Size = new System.Drawing.Size(rec.Width, rec.Height);
            form.View = (BAL.Business.View)this.DataContext;
            form.Show();
            this.Select();
        }

        private void DataContext_ColumnChanged(object sender, Types.ColumnChangedEventArgs e)
        {
            DataGridViewColumn column = this.GridView.Columns[e.Column.Name];

            switch (e.PropertyName)
            {
                case "Visible":
                    if (e.Column.Visible)
                    {
                        if (column == null)
                        {
                            column = this.CreateGridColumn(e.Column);
                            this.GridView.Columns.Add(column);
                        }
                    }
                    else
                    {
                        if (column != null)
                            GridView.Columns.Remove(column);
                    }
                    break;
                case "Order":
                    this.reorderColumns();
                    break;
                case "HeaderText":
                    if (column != null)
                        column.HeaderText = e.Column.HeaderText;
                    break;
                case "HeaderTextAlign":
                    if (column != null)
                    {
                        switch (e.Column.HeaderTextAlign)
                        {
                            case TextAlign.None:
                            case TextAlign.Left:
                                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                break;
                            case TextAlign.Right:
                                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                break;
                            case TextAlign.Center:
                                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                        }
                    }
                    break;
                case "TextAlign":
                    if (column != null)
                        switch (e.Column.TextAlign)
                        {
                            case Types.TextAlign.None:
                            case Types.TextAlign.Left:
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                break;
                            case Types.TextAlign.Right:
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                break;
                            case Types.TextAlign.Center:
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                        }
                    break;
                case "Format":
                    if (column != null)
                    {
                        column.DefaultCellStyle.Format = e.Column.Format;
                    }
                    break;
                case "ReadOnly":
                    if (column != null)
                    {
                        column.ReadOnly = e.Column.ReadOnly;
                    }
                    break;
            }
        }

        private void DataContext_ColumnAdded(object sender, Types.ColumnEventArgs e)
        {
            DataGridViewColumn column = this.CreateGridColumn(e.Column);
            this.GridView.Columns.Add(column);
        }

        private void topSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!disableFirePanels_SplitterMoved && this.DataContext != null)
                this.DataContext.SetStoredProperty("TopPanelSpliterDistance", topSplitContainer.SplitterDistance);
        }

        private void GridView_ColumnHeadersHeightChanged(object sender, EventArgs e)
        {
            if (this.DataContext != null)
            {
                this.DataContext.SetStoredProperty("columnheadersheight", this.GridView.ColumnHeadersHeight);
            }
        }

        private void GridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = this.GridView.Rows[e.RowIndex];
            this.gridTemplate.SetRowStyle(row);
        }

        private void verticalSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!disableFirePanels_SplitterMoved && this.DataContext != null)
            {
                this.DataContext.SetStoredProperty("LeftPanelSpliterDistance", verticalSplitContainer.SplitterDistance);
            }

        }

        private void GridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (this.DataContext != null)
            {
                var view = (BAL.Business.View)this.DataContext;
                BAL.Types.Column column = null;
                foreach (BAL.Types.Column col in view.VisibleColumns.GetVisible())
                    if (col.Name == e.Column.Name)
                    {
                        column = col;
                        break;
                    }
                if (column != null)
                    column.Width = e.Column.Width;
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.addNewRow();
        }

        private void bindingNavigatorEditItem_Click(object sender, EventArgs e)
        {
            editCurrentRow();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            deleteCurrentRow();
        }

        #endregion

        #region Nested Types

        internal class ParamInfo
        {
            public DataContextParam Param { get; set; }
            public Control Control { get; set; }
        }

        public class ActionInfo : IComparable
        {

            #region Fields

            private object action;
            private int priority;
            private string text;
            private string description;
            private ActionTarget target;
            internal Control control;
            private MethodInfo methodOnAction;
            private MethodInfo methodOnSelectionChanged;
            private PropertyInfo propertySelectedRows;
            private PropertyInfo propertyCurrentRow;
            private PropertyInfo propertyFireReload;
            private PropertyInfo propertyFireSelectionChanged;
            private PropertyInfo propertyFireRefresh;

            #endregion

            public object Action
            {
                get { return this.action; }
            }

            public int Priority
            {
                get { return this.priority; }
            }

            public string Text
            {
                get { return this.text; }
            }

            public string Description
            {
                get { return this.description; }
            }

            public ActionTarget Target
            {
                get { return this.target; }
            }

            public Control Control
            {
                get { return this.control; }
            }

            public MethodInfo MethodOnAction
            {
                get { return this.methodOnAction; }
            }

            public MethodInfo MethodOnSelectionChanged
            {
                get { return this.methodOnSelectionChanged; }
            }

            public PropertyInfo PropertySelectedRows
            {
                get { return this.propertySelectedRows; }
            }

            public PropertyInfo PropertyCurrentRow
            {
                get { return this.propertyCurrentRow; }
            }

            public PropertyInfo PropertyFireReload
            {
                get { return this.propertyFireReload; }
            }

            public PropertyInfo PropertyFireSelectionChanged
            {
                get { return this.propertyFireSelectionChanged; }
            }

            public PropertyInfo PropertyFireRefresh
            {
                get { return this.propertyFireRefresh; }
            }

            public int? MarginLeft;
            public int? MarginRight;
            public int? MarginTop;
            public int? MarginBottom;


            public ActionInfo(Type type)
            {
                this.action = type.GetConstructor(new Type[0]).Invoke(new object[0]);
                this.priority = PriorityAttribute.GetPriority(this.action, 1000);
                this.text = CaptionAttribute.GetCaption(this.action, type.Name);
                this.description = BAL.Types.AttributesExtensions.GetDescription(null, this.action);
                var pinfo = type.GetProperty("Target", BindingFlags.Instance | BindingFlags.Public);
                if (pinfo != null)
                    this.target = (ActionTarget)pinfo.GetValue(this.action, null);
                else
                    this.target = ActionTarget.None;

                pinfo = type.GetProperty("MarginLeft", BindingFlags.Instance | BindingFlags.Public);
                if (pinfo != null)
                    this.MarginLeft = (int)pinfo.GetValue(this.action, null);

                pinfo = type.GetProperty("MarginRight", BindingFlags.Instance | BindingFlags.Public);
                if (pinfo != null)
                    this.MarginRight = (int)pinfo.GetValue(this.action, null);

                pinfo = type.GetProperty("MarginTop", BindingFlags.Instance | BindingFlags.Public);
                if (pinfo != null)
                    this.MarginTop = (int)pinfo.GetValue(this.action, null);

                pinfo = type.GetProperty("MarginBottom", BindingFlags.Instance | BindingFlags.Public);
                if (pinfo != null)
                    this.MarginBottom = (int)pinfo.GetValue(this.action, null);


                this.propertySelectedRows = type.GetProperty("SelectedRows");
                this.propertyCurrentRow = type.GetProperty("CurrentRow");
                this.propertyFireReload = type.GetProperty("FireReload");
                this.propertyFireRefresh = type.GetProperty("FireRefresh");
                this.propertyFireSelectionChanged = type.GetProperty("FireSelectionChanged");

                this.methodOnAction = type.GetMethod("OnAction", BindingFlags.Instance | BindingFlags.Public);
                this.methodOnSelectionChanged = type.GetMethod("OnSelectionChanged", BindingFlags.Instance | BindingFlags.Public);
            }

            public int CompareTo(object obj)
            {
                var a = (ActionInfo)obj;
                int i = this.Priority.CompareTo(a.Priority);
                if (i == 0)
                    return this.GetType().FullName.CompareTo(a.GetType().FullName);
                return i;
            }
        }

        #endregion


    }
}
