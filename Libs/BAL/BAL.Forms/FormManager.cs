using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Business;

namespace BAL.Forms
{
    public class FormManager
    {
        #region Fields

        private static FormManager instance;
        private static IFormService formService;
        private static IMainForm mainForm;
        private static IList menuItemCollection;
        private static IGridService defaultGridService;
        private static Dictionary<string, IGridService> gridServicesByKey;
        private DialogResult lastDialogResult;

        #endregion

        #region Properties

        public static FormManager Instance
        {
            get
            {
                if (FormManager.instance == null)
                    FormManager.instance = new FormManager();
                return instance;
            }
        }

        public static IFormService FormService
        {
            get { return FormManager.formService; }
        }

        public static IMainForm MainForm
        {
            get { return FormManager.mainForm; }
        }

        public DialogResult LastDialogResult
        {
            get
            {
                return lastDialogResult;
            }
        }
    

        #endregion

        #region Methods

        public static void StartApplication()
        {
            AppController.ExceptionProcess = (ex) =>
            {
                ExceptionForm.Show(ex);
            };

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(CurrentDomain_UnhandledException);


            FormManager.formService = (IFormService)AppController.Instance.GetService<IFormService, FormService>();
            if (FormManager.formService != null)
            {
                FormManager.FormService.Init();
                FormManager.Instance.Run();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //if (!e.IsTerminating)
            //{
                Exception ex = (Exception)e.ExceptionObject;
                AppController.ThrowException(ex);
            //}
        }

        private static void CurrentDomain_UnhandledException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = (Exception)e.Exception;
            AppController.ThrowException(ex);
        }


        public static void StopApplication()
        {
            if (FormManager.FormService != null)
                FormManager.FormService.Finish();
            AppController.Instance.Finish();
        }

        internal void initMenuItems()
        {
            menuItemCollection = mainForm.GetMenuItemCollection();

            var attributes = BAL.Business.AppController.Instance.AssemblyAttributes[typeof(MenuActionAttribute)];
            if (attributes != null)
            {
                foreach (MenuActionAttribute attribute in attributes)
                {
                    if (this.containsPath(menuItemCollection,attribute.MenuPath))
                        throw new MenuException(null, "Podwójna definicja atrybutu MenuActionAttribute dla ścieżki " + attribute.MenuPath);
                    this.addMenuItem(attribute);
                }
            }
        }

        internal void addMenuItem(MenuActionAttribute attribute)
        {
            string[] parts = attribute.MenuPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            IList coll = menuItemCollection;
            IMenuItem parent = null;
            IMenuItem item = null;

            foreach (var part in parts)
            {
                if (coll == null)
                {
                    coll = parent.SubMenu;
                }

                if (this.containsPath(coll,part))
                {
                    parent = this.getMenuItemByPath(coll, part);
                    coll = parent.SubMenu;
                    continue;
                }
                else
                {
                    item =(IMenuItem)FormService.MenuItemType.GetConstructor(new Type[0]).Invoke(new object[0]);
                    item.Click += new EventHandler(this.onMenuItemClick);
                    item.Text = part;
                    item.Parent = parent;
                    parent = item;
                    coll.Add(item);
                    coll = null;
                }
            }
            if (item != null)
            {
                item.ActionAttribute = attribute;
                item.FormType = attribute.FormType;
                item.MenuAction = attribute.MenuAction;
            }
        }

        private IMenuItem getMenuItemByPath(IList coll, string path)
        {
            string[] parts = path.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                foreach (IMenuItem item in coll)
                {
                    if (item.Text == parts[0])
                        return item;
                }
            }
            else
            {
                IMenuItem item = null;
                foreach (var part in parts)
                {
                    item = getMenuItemByPath(item == null ? coll : item.SubMenu, part);
                    if (item == null)
                        return null;

                }
                return item;
            }
            return null;
        }

        private bool containsPath(IList coll, string path)
        {
            return getMenuItemByPath(coll, path) != null;
        }

        private void onMenuItemClick(object sender, EventArgs e)
        {
            IMenuItem item = sender as IMenuItem;
            if (item != null && item.MenuAction != MenuActionsType.None)
            {
                if (!FormService.CheckMenuActionRights(item))
                    return;

                switch (item.MenuAction)
                {
                    case MenuActionsType.OpenForm:
                        if (item.FormType != null)
                        {
                            using (new WaitCursor())
                            {
                                IControlContainer cc = mainForm as IControlContainer;
                                var form = item.FormType.GetConstructor(new Type[0]).Invoke(new object[0]);
                                if (typeof(Form).IsAssignableFrom(form.GetType()))
                                {
                                    ((Form)form).TopLevel = false;
                                    ((Form)form).FormBorderStyle = FormBorderStyle.None;
                                    //((Form)form).WindowState = FormWindowState.Maximized;
                                    var rec = cc.GetClientRectagle();
                                    ((Form)form).Location = new System.Drawing.Point(0, 0);
                                    ((Form)form).Size = new System.Drawing.Size(rec.Width, rec.Height);
                                    ((Form)form).Dock = DockStyle.Fill;
                                }


                                if (cc != null)
                                {
                                    var col = cc.GetControlCollection();
                                    col.Add(form);
                                    ((Control)form).Show();
                                }
                            }
                        }
                        break;
                    case MenuActionsType.OpenFormModal:
                        if (item.FormType != null)
                        {
                            var form = item.FormType.GetConstructor(new Type[0]).Invoke(new object[0]);
                            if (typeof(Form).IsAssignableFrom(form.GetType()))
                            {
                                ((Form)form).FormBorderStyle = FormBorderStyle.FixedDialog;
                                ((Form)form).StartPosition = FormStartPosition.CenterParent;
                                ((Form)form).ShowDialog();
                            }
                        }
                        break;
                    case MenuActionsType.OpenViewModal:
                    case MenuActionsType.OpenView:
                        Session session = null;
                        if ((item.ActionAttribute.Options & ActionOptions.WithoutSession) == ActionOptions.None)
                            session = AppController.Instance.CurrentLogin.CreateSession(false, false, "");
                        BAL.Business.View view = null;
                        if (item.ActionAttribute.ViewType != null)
                        {
                            if (item.ActionAttribute.Data != null)
                            {
                                try
                                {
                                    if (session != null)
                                        view = (BAL.Business.View)item.ActionAttribute.ViewType.GetConstructor(new Type[] { typeof(Session), typeof(IMenuItem) }).Invoke(new object[] { session, item });
                                    else
                                        view = (BAL.Business.View)item.ActionAttribute.ViewType.GetConstructor(new Type[] { typeof(IMenuItem) }).Invoke(new object[] { item });
                                }
                                catch { }
                            }
                            if (view == null)
                            {
                                if (session != null)
                                    view = (BAL.Business.View)item.ActionAttribute.ViewType.GetConstructor(new Type[] { typeof(Session) }).Invoke(new object[] { session });
                                else
                                    view = (BAL.Business.View)item.ActionAttribute.ViewType.GetConstructor(new Type[0]).Invoke(new object[0]);
                            }
                        }
                        else if(item.ActionAttribute.DataType !=null && session != null)
                        {
                            var tableName = this.getTableName(item.ActionAttribute.DataType);
                            if (!string.IsNullOrEmpty(tableName))
                            {
                                var table = session.Tables[tableName];
                                view = table.CreateView();
                            }
                        }
                        if (view != null)
                        {
                            view.Load();
                            var gridForm = this.GetDefaultGridForm(view);
                            gridForm.FormClosed += (s, a) =>
                            { 
                                view.Dispose();
                                if (session != null)
                                    session.Dispose();
                            };
                            this.openForm((Control)gridForm, item.MenuAction == MenuActionsType.OpenViewModal);
                        }
                        break;
                        
                }
            }
        }

        private string getTableName(Type rowType)
        {
            var attrinutes = rowType.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), true);
            if (attrinutes.Length > 0)
                return ((System.ComponentModel.DataAnnotations.Schema.TableAttribute)attrinutes[0]).Name;
            return null;
        }

        private void openForm(Control form, bool modal = false)
        {
            if (!modal)
            {
                using (new WaitCursor())
                {
                    IControlContainer cc = mainForm as IControlContainer;
                    if (typeof(Form).IsAssignableFrom(form.GetType()))
                    {
                        ((Form)form).TopLevel = false;
                        ((Form)form).FormBorderStyle = FormBorderStyle.None;
                        //((Form)form).WindowState = FormWindowState.Maximized;
                        var rec = cc.GetClientRectagle();
                        ((Form)form).Location = new System.Drawing.Point(0, 0);
                        ((Form)form).Size = new System.Drawing.Size(rec.Width, rec.Height);
                        ((Form)form).Dock = DockStyle.Fill;
                    }


                    if (cc != null)
                    {
                        var col = cc.GetControlCollection();
                        int idx = col.Add(form);
                        //((Control)form).Show();
                    }
                }
            }
            else
            {
                ((Form)form).StartPosition = FormStartPosition.CenterParent;
                lastDialogResult = ((Form)form).ShowDialog();
            }
        }

        public Form GetDefaultGridForm(BAL.Business.View view)
        {
            var gridForm = (Form)FormService.GridFormType.GetConstructor(new Type[0]).Invoke(new object[0]);
            if (gridForm is IDataContexable)
                ((IDataContexable)gridForm).DataContext = view;
            gridForm.StartPosition = FormStartPosition.CenterParent;
            return gridForm;
        }

        public DialogResult OpenView(BAL.Business.View view, bool modal)
        {
            var gridForm = this.GetDefaultGridForm(view);
            if (modal)
                return gridForm.ShowDialog();
            else
                gridForm.Show();
            return DialogResult.None;
        }

        public DialogResult ShowGridFormDialog(BAL.Business.View view)
        {
            var gridForm = this.GetDefaultGridForm(view);
            return gridForm.ShowDialog();
        }

        internal void Run()
        {
            var type = FormManager.FormService.MainFormType;
            FormManager.mainForm = type.GetConstructor(new Type[0]).Invoke(new object[0]) as IMainForm;
            if (mainForm != null)
            {
                FormManager.Instance.initMenuItems();
                mainForm.Title = FormManager.FormService.ApplicationName;
                
                Application.Run((Form)FormManager.mainForm);
            }

        }

        public static IGridService GetGridService(string key)
        {
            checkGridServices();
            if (!string.IsNullOrEmpty(key) && gridServicesByKey.ContainsKey(key))
                return gridServicesByKey[key];
            return defaultGridService;
        }

        private static void checkGridServices()
        {
            if (gridServicesByKey == null)
            {
                gridServicesByKey = new Dictionary<string, IGridService>();
                var attributes = AppController.Instance.AssemblyAttributes[typeof(AppServiceAttribute)];
                foreach (AppServiceAttribute attr in attributes)
                {
                    if (attr.ServiceInterface != typeof(IGridService))
                        continue;
                    var service = (IGridService)attr.ServiceType.GetConstructor(new Type[0]).Invoke(new object[0]);
                    var keys = service.GetAvailableKeys();
                    if (keys == null)
                        defaultGridService = service;
                    else
                    {
                        foreach (var key in keys)
                            gridServicesByKey[key] = service;
                    }
                }
                if (defaultGridService == null)
                    defaultGridService = new GridService();
            }
        }

        public static bool Confirm(string message, bool defaultYes = true)
        {
            if (MessageBox.Show(message, FormService.ApplicationName, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, defaultYes ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                return true;
            return false;
        }

        public static void Alert(string message)
        {
            MessageBox.Show(message, FormService.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        #endregion
    }
}
