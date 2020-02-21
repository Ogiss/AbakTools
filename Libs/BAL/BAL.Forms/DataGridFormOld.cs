using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using BAL.Types;
using BAL.Business;

namespace BAL.Forms
{
    public partial class DataGridFormOld : BAL.Forms.DataForm
    {
        #region Fields

        private object loadingLock = new object();
        private bool isLoadingRows;
        private IGridTemplate gridTemplate;
        private Dictionary<string, ParamInfo> dataContextParams;
        private TableLayoutPanel headerTable;
        private FlowLayoutPanel systemPanel;
        private FlowLayoutPanel paramsPanel;
        private FlowLayoutPanel actionsPanel;
        private TextBox findTextBox;
        private bool fireKeyPress = true;
        private bool disableFirePanels_SplitterMoved = false;
        private List<MethodInvoker> onSelectionChangeActions;
        private SortedSet<ActionInfo> actions;
        private AutoResetEvent reloadResetEvent;
        private bool? topPanelCollapsed;
        private bool cleanFindText;
        private int firstRowSave = -1;

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
            get { return this.DataContext as BAL.Business.View; }
        }

        public override DataContext DataContext
        {
            get
            {
                return base.DataContext;
            }
            set
            {
                base.DataContext = value;
                if (base.DataContext != null)
                {
                    base.DataContext.PositionChanged += (sender, e) =>
                    {
                        if (this.BindingSource.Position != this.View.Position)
                            this.BindingSource.Position = this.View.Position;
                    };
                    this.BindingSource.PositionChanged += (sender, e) =>
                    {
                        if (this.View.Position != this.BindingSource.Position)
                            this.View.Position = this.BindingSource.Position;
                    };
                    this.bindingNavigatorAddNewItem.Enabled = !base.DataContext.ReadOnly;
                    this.bindingNavigatorDeleteItem.Enabled = !base.DataContext.ReadOnly;
                    var view = this.View;
                    if (view != null)
                    {
                        view.BeforeReload += (o, e) =>
                        {
                            firstRowSave = this.GridView.FirstDisplayedScrollingRowIndex;
                        };
                        view.AfterReload += (o, e) =>
                        {
                            if (firstRowSave >= 0 && firstRowSave < GridView.Rows.Count)
                            {
                                GridView.FirstDisplayedScrollingRowIndex = firstRowSave;
                                firstRowSave = -1;
                            }
                        };
                    }
                }
            }
        }

        public virtual object Current
        {
            get
            {
                if (this.View != null)
                    return this.View.Current;
                return null;
            }
        }


        [Browsable(true)]
        public bool TopPanelCollapsed
        {
            get { return this.TopPanel.Panel1Collapsed; }
            set { this.TopPanel.Panel1Collapsed = value; }
        }

        [Browsable(true)]
        public bool LeftPanelCollapsed
        {
            get { return this.LeftPanel.Panel1Collapsed; }
            set { this.LeftPanel.Panel1Collapsed = value; }
        }



        #endregion

        #region Methods

        public DataGridFormOld()
        {
            InitializeComponent();
            this.reloadResetEvent = new AutoResetEvent(true);
            this.bindingNavigatorRefreshItem.Click += new EventHandler(bindingNavigatorRefreshItem_Click);
            this.bindingNavigatorAddNewItem.Click += new EventHandler(bindingNavigatorAddNewItem_Click);
            this.bindingNavigatorEditItem.Click += new EventHandler(bindingNavigatorEditItem_Click);
            this.bindingNavigatorDeleteItem.Click += new EventHandler(bindingNavigatorDeleteItem_Click);
            this.bindingNavigatorGridConfig.Click +=new EventHandler(bindingNavigatorGridConfig_Click);
            this.bindingNavigatorExtraPanelVisible.Click+=new EventHandler(bindingNavigatorExtraPanelVisible_Click);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!this.IsLoaded && !this.DesignMode)
            {
                if (this.View != null)
                {
                    if (!this.View.IsLoaded)
                        this.View.Load();
                    bindingNavigatorAddNewItem.Enabled = this.View.AllowNew;
                    bindingNavigatorEditItem.Enabled = this.View.AllowEdit;
                    bindingNavigatorDeleteItem.Enabled = this.View.AllowRemove;
                }

                this.actions = this.GetAllActions();
                this.InitSystemPanel();
                this.InitExtraPanel();
                this.InitColumns();
                this.InitGridTemplate();
                this.InitStoredProperties();
                this.InitGridToolBar();
                this.Reload();
            }
            //this.GridView.Size = this.TopPanel.Panel2.ClientSize;
            this.ActiveControl = this.GridView;
            base.OnLoad(e);
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
                        var action = new ActionInfo(this, type);
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

        protected virtual void InitGridToolBar()
        {
            var actions = new List<ActionInfo>();
            foreach (var a in this.actions)
            {
                if ((a.Target & ActionTarget.GridToolbar) == ActionTarget.GridToolbar)
                    actions.Add(a);
            }

            if (actions.Count > 0)
            {
                GridNavigator.Items.Add(new ToolStripSeparator());
                foreach (var a in actions)
                {
                    var btn = new ToolStripButton(a.Image == null ? a.Text : "", a.Image, new EventHandler((sender, e) => {
                        var info = (ActionInfo)((ToolStripButton)sender).Tag;
                        if (info != null)
                            info.MethodOnAction.Invoke(info.Action, null);
                    }));
                    btn.Tag = a;
                    btn.ToolTipText = a.Text;
                    GridNavigator.Items.Add(btn);
                    
                }

            }
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
                for(int i = 0; i<rowsCount;i++)
                    this.headerTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                if (rowsCount > 1)
                    this.TopPanel.Panel1Collapsed = false;
                this.TopPanel.SplitterDistance = rowsCount * 34;
                this.TopPanel.Panel1.Controls.Add(this.headerTable);

                this.initSystemControls();
                if (dcParams != null && dcParams.Count() > 0) this.initParams(dcParams);
                if (actions !=null && actions.Count > 0) this.initActionsButtons(actions);
                int? height = (int?)DataContext.GetStoredProperty("TopPanelSpliterDistance");
                if (height != null && height >= 0)
                    this.TopPanel.SplitterDistance = height.Value;
                this.disableFirePanels_SplitterMoved = false;

            }
        }

        protected virtual void InitExtraPanel()
        {
            if (this.View != null)
            {
                if (typeof(GridViewContext).IsAssignableFrom(this.View.GetType()) && ((GridViewContext)this.View).ExtraPanelAvailable)
                {
                    disableFirePanels_SplitterMoved = true;
                    this.bindingNavigatorExtraPanelVisible.Visible = true;
                    this.bindingNavigatorExtraPanelVisible.Checked = ((GridViewContext)this.View).ExtraPanelVisible;
                    this.LeftPanel.Panel1Collapsed = !((GridViewContext)this.View).ExtraPanelVisible;
                    ((GridViewContext)this.View).InitExtraPanel(this.LeftPanel.Panel1);
                    int? width = null;
                    if (this.DataContext != null)
                        width = (int?)this.DataContext.GetStoredProperty("LeftPanelSpliterDistance");
                    if (width == null)
                    {
                        foreach (Control c in this.LeftPanel.Panel1.Controls)
                        {
                            if (c.Width > width)
                                width = c.Width;
                        }
                    }
                    if (width != null && width.Value > 0)
                        this.LeftPanel.SplitterDistance = width.Value;

                    

                    disableFirePanels_SplitterMoved = false;
                }
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
            this.findTextBox.KeyDown +=new KeyEventHandler(findTextBox_KeyDown);
            this.findTextBox.TextChanged+=new EventHandler(findTextBox_TextChanged);
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
                    var attrs = param.PropertyPath.Last.GetCustomAttributes(typeof(BAL.Types.ParamControlAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                        type = ((BAL.Types.ParamControlAttribute)attrs[0]).ControlType;
                    else
                        type = ParamControlAttribute.GetParamControlType(param.PropertyPath.Last.PropertyType);
                    if (type != null)
                        control = (Control)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                }

                if(control == null)
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

        protected virtual void InitColumns()
        {
            if (this.DataContext != null)
            {
                var gc = this.DataContext as GridViewContext;
                var columns = this.DataContext.VisibleColumns.GetVisible().OrderBy(c => c.Order);
                foreach (object col in columns)
                {
                    var column = this.CreateGridColumn(col);
                    this.GridView.Columns.Add(column);
                    if (gc != null)
                        gc.InitGridColumn(column);

                }
                this.DataContext.ColumnChanged +=new EventHandler<Types.ColumnChangedEventArgs>(DataContext_ColumnChanged);
                this.DataContext.ColumnAdded +=new EventHandler<Types.ColumnEventArgs>(DataContext_ColumnAdded);
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
                var properties = this.DataContext.GetStoredProperties();
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

        protected virtual IEnumerable GetRows()
        {
            if(this.View != null)
                this.View.Reload();
            return this.View;
        }

        private void loadRows()
        {
            if (this.GridView.InvokeRequired)
            {
                var d = new MethodInvoker(this.loadRows);
                this.Invoke(d);
            }
            else
            {
                using (new WaitCursor(this))
                {
                    try
                    {
                        //this.GridView.DataSource = this.GetRows();
                        this.BindingSource.DataSource = this.GetRows();
                        
                        this.GridView.Refresh();
                        if (this.DataLoaded != null)
                            this.DataLoaded(this, new EventArgs());
                    }
                    catch (Exception ex)
                    {
                        AppController.ThrowException(ex);
                    }
                }

            }
        }

        public virtual void Reload()
        {
            if (!this.loadRowsWorker.IsBusy)
                this.loadRowsWorker.RunWorkerAsync();
        }

        private void addNewRow()
        {
            if (this.View != null && this.View.AllowNew)
            {
                this.View.AddNew();
            }
        }

        protected virtual void EditCurrentRow()
        {
            this.findTextBox.Text = "";
            if (this.View != null)
            {
                if (this.View.SelectionMode)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    OnAfterSelectRow(new RowEventArgs(this.View.Current));
                }
                else if(this.View.AllowEdit)
                {
                    this.View.EditCurrent();
                    this.GridView.Refresh();
                }

            }
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
                else
                {
                    var args = new CanceledRowEventArgs(data);
                    this.OnDeletingRow(args);
                    if (!args.Cancel)
                    {
                        this.OnDeleteRow(new RowEventArgs(data));
                    }
                }
        }

        private IEnumerable getSelectedRows()
        {
            ArrayList list = new ArrayList();
            foreach (DataGridViewRow row in this.GridView.SelectedRows)
                list.Add(row.DataBoundItem);
            return list;
        }

        protected virtual Form GetEditForm()
        {
            if (this.View != null)
            {
                var type = FormManager.FormService.GetDataEditFormType(this.View.GetDataType());
                if (type != null)
                {
                    var form = (Form)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                    form.StartPosition = FormStartPosition.CenterParent;
                    return form;
                }
            }
            return null;
        }

        protected virtual Type GetEditFormType()
        {
            if (this.DataContext != null)
                return DataFormAttribute.GetFormType(this.DataContext.GetDataType());
            return null;
        }

        protected virtual void OnBeforeEditRow(CanceledRowEventArgs e)
        {
            if (this.BeforeEditRow != null)
                this.BeforeEditRow(this, e);
        }

        protected virtual void OnAfterEditRow(RowEventArgs e)
        {
            if (this.AfterEditRow != null)
                this.AfterEditRow(this, e);
        }

        protected virtual void OnEditRow(RowEventArgs e)
        {
            if (this.EditRow != null)
                this.EditRow(this, e);
        }

        protected virtual void OnAddRow(EventArgs e)
        {
            if (this.AddRow != null)
                this.AddRow(this, e);
        }

        protected virtual void OnDeletingRow(CanceledRowEventArgs e)
        {
            if (this.DeletingRow != null)
                this.DeletingRow(this, e);
        }

        protected virtual void OnDeleteRow(RowEventArgs e)
        {
            if (this.DeleteRow != null)
                this.DeleteRow(this, e);
        }

        protected virtual void OnAfterSelectRow(RowEventArgs e)
        {
            if (this.AfterSelectRow != null)
                this.AfterSelectRow(this, e);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.GridView.Select();
        }

        protected override void Select(bool directed, bool forward)
        {
            base.Select(directed, forward);
            this.GridView.Select();
        }

        #endregion

        #region Event handlers

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.addNewRow();
        }

        private void bindingNavigatorEditItem_Click(object sender, EventArgs e)
        {
            if (this.View != null)
                this.View.EditCurrent();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (this.GridView.CurrentRow != null)
            {
                this.deleteGridViewRow(this.GridView.CurrentRow);
            }
        }

        private void GridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < this.GridView.Rows.Count)
            {
                this.EditCurrentRow();
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

        private void bindingNavigatorExtraPanelVisible_Click(object sender, EventArgs e)
        {
            this.bindingNavigatorExtraPanelVisible.Checked = !this.bindingNavigatorExtraPanelVisible.Checked;
            this.LeftPanelCollapsed = !this.LeftPanelCollapsed;
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

        private void GridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (this.DataContext != null)
            {
                var view = (BAL.Business.View)this.DataContext;
                BAL.Types.Column column = null;
                foreach (BAL.Types.Column col in view.VisibleColumns.GetVisible())
                    if (col.Name  == e.Column.Name)
                    {
                        column = col;
                        break;
                    }
                if (column != null)
                    column.Width = e.Column.Width;
            }
        }

        private void GridView_ColumnHeadersHeightChanged(object sender, EventArgs e)
        {
            if (this.DataContext != null && this.IsLoaded)
            {
                this.DataContext.SetStoredProperty("columnheadersheight", this.GridView.ColumnHeadersHeight);
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

        private void GridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = this.GridView.Rows[e.RowIndex];
            this.gridTemplate.SetRowStyle(row);
        }

        private void BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void BindingSource_PositionChanged(object sender, EventArgs e)
        {

        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Alt && !e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (this.GridView.CurrentRow != null)
                        {
                            e.Handled = true;
                            this.EditCurrentRow();
                        }
                        break;
                    case Keys.Back:
                        if (!string.IsNullOrEmpty(findTextBox.Text))
                        {
                            findTextBox.Text = findTextBox.Text.Substring(0, findTextBox.Text.Length - 1);
                            findTextBox.SelectionStart = findTextBox.Text.Length;
                            /*
                            if (string.IsNullOrEmpty(findTextBox.Text))
                            {
                                findTextBox.Visible = false;
                                if (this.topPanelCollapsed != null)
                                    this.TopPanel.Panel1Collapsed = this.topPanelCollapsed.Value;
                            }
                             */
                        }
                        break;
                    case Keys.Insert:
                        e.Handled = true;
                        e.SuppressKeyPress = false;
                        this.addNewRow();
                        break;
                    default:
                        break;
                }
            }
        }

        private void DataGridForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Alt && !e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        e.Handled = true;
                        e.SuppressKeyPress = false;
                        this.addNewRow();
                        break;
                    case Keys.Escape:

                        if (this.View.SelectionMode)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                        }

                        break;
                }
            }
        }

        private void DataGridForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void DataGridForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (this.View != null && this.View.SupportsSearching)
            {
                if (CoreTools.IsPrintableChar(e.KeyChar) && fireKeyPress)
                {
                    fireKeyPress = false;
                    this.topPanelCollapsed = this.TopPanel.Panel1Collapsed;
                    if (this.TopPanel.Panel1Collapsed)
                        this.TopPanel.Panel1Collapsed = false;
                    this.findTextBox.Visible = true;
                    this.findTextBox.Select();
                    this.findTextBox.Text = e.KeyChar.ToString();
                    this.findTextBox.SelectionStart = 1;
                    this.findTextBox.SelectionLength = 0;
                }
            }
             */

            if (this.View != null && this.View.SupportsSearching)
            {
                if (CoreTools.IsPrintableChar(e.KeyChar))
                {
                    this.findTextBox.Text += e.KeyChar;
                }
            }
        }

        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(findTextBox.Text))
            {
                cleanFindText = false;
                if (this.topPanelCollapsed == null)
                    this.topPanelCollapsed = this.TopPanel.Panel1Collapsed;
                if (this.TopPanel.Panel1Collapsed)
                    this.TopPanel.Panel1Collapsed = false;

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
                    this.TopPanel.Panel1Collapsed = this.topPanelCollapsed.Value;
                    this.topPanelCollapsed = null;
                }
            }
        }

        private void GridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (this.View != null && this.View.SupportsSearching)
            {
                if (CoreTools.IsPrintableChar(e.KeyChar) && fireKeyPress)
                {
                    fireKeyPress = false;
                    this.topPanelCollapsed = this.TopPanel.Panel1Collapsed;
                    if (this.TopPanel.Panel1Collapsed)
                        this.TopPanel.Panel1Collapsed = false;
                    this.findTextBox.Visible = true;
                    this.findTextBox.Select();
                    this.findTextBox.Text = e.KeyChar.ToString();
                    this.findTextBox.SelectionStart = 1;
                    this.findTextBox.SelectionLength = 0;
                }
            }
            */
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

        private void GridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
        }

        private void GridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*
            DataGridViewColumn column = GridView.Columns[e.ColumnIndex];

            if (this.View != null)
            {
                if (this.View.IsSorted)
                {
                }
                else
                {
                    BAL.Types.PropertyPath newPropertyPath = new BAL.Types.PropertyPath(this.View.GetDataType(), column.DataPropertyName);
                }
            }
            else
            {
            }

            
            DataGridViewColumn oldColumn = GridView.SortedColumn;
            ListSortDirection direction;

            if (oldColumn != null)
            {
                if (oldColumn == newColumn &&
                    GridView.SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            
            if (this.View != null)
            {
                this.View.ApplySort(newColumn.DataPropertyName, direction);
            }
            else
                GridView.Sort(newColumn, direction);

            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;
             */
        }

        private void GridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < GridView.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < GridView.Columns.Count)
            {
                var col = this.GridView.Columns[e.ColumnIndex];
                if (!GridView.ReadOnly && !col.ReadOnly)
                    GridView.EndEdit();
            }
        }

        private void LeftPanel_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!disableFirePanels_SplitterMoved && this.DataContext != null)
            {
                this.DataContext.SetStoredProperty("LeftPanelSpliterDistance", LeftPanel.SplitterDistance);
            }
        }

        private void TopPanel_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!disableFirePanels_SplitterMoved && this.DataContext != null)
                this.DataContext.SetStoredProperty("TopPanelSpliterDistance", TopPanel.SplitterDistance);
        }

        #endregion

        #region Events
        [Browsable(true)]
        public event EventHandler<CanceledRowEventArgs> BeforeEditRow;
        [Browsable(true)]
        public event EventHandler<RowEventArgs> AfterEditRow;
        [Browsable(true)]
        public event EventHandler<RowEventArgs> EditRow;
        [Browsable(true)]
        public event EventHandler AddRow;
        [Browsable(true)]
        public event EventHandler<CanceledRowEventArgs> DeletingRow;
        [Browsable(true)]
        public event EventHandler<RowEventArgs> DeleteRow;
        [Browsable(true)]
        public event EventHandler DataLoaded;
        [Browsable(true)]
        public event EventHandler<RowEventArgs> AfterSelectRow;

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
            private Form parentForm;
            private string description;
            private ActionTarget target;
            private Image image;
            internal Control control;
            internal ToolStripItem toolStripItem;
            private MethodInfo methodOnAction;
            private MethodInfo methodOnSelectionChanged;
            private  PropertyInfo propertySelectedRows;
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

            public ToolStripItem ToolStripItem
            {
                get { return toolStripItem; }
            }

            public Image Image
            {
                get { return image; }
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


            public ActionInfo(Form parentForm, Type type)
            {
                this.parentForm = parentForm;
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

                pinfo = type.GetProperty("Image", BindingFlags.Instance | BindingFlags.Public);
                if (pinfo != null)
                    this.image = (Image)pinfo.GetValue(this.action, null);

                pinfo = type.GetProperty("ParentForm", BindingFlags.Instance | BindingFlags.Public);
                if (pinfo != null)
                    pinfo.SetValue(action, this.parentForm, null);


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
