using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Security.Permissions;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using DBWeb = Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;



[assembly: MenuAction("Zamówienia", MenuAction = MenuActionsType.OpenView, ViewType = typeof(AbakTools.Zamowienia.Forms.ZamowieniaView), Options = ActionOptions.WithoutSession, Priority = 700)]

namespace AbakTools.Zamowienia.Forms
{
    public class ZamowieniaView : GridViewContext
    {
        #region Fields

        private const double reloadInterval = 60;
        private const int minReloadInterval = 15;

        private Enova.Business.Old.Web.ZamowieniaView table;
        private Enova.Business.Old.Controls.DateTimeSpanControl okresControl;
        //private Enova.Business.Old.Controls.KontrahentSelectControl kontrahentControl;
        private Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect kontrahentControl;
        private BAL.Forms.Controls.ComboBox sezonControl;

        //private SqlConnection connection;
        //private SqlCommand command;
        //private bool sqlDependencyEnable = true;
        private static bool disableReload = false;
        //private bool registryDependensyNeeded = false;
        //private object lockRows = new object();
        private System.Timers.Timer reloadTimer;
        private DateTime lastReload;
        private object lockReload = new object();
        private static object lockDisbaleReload = new object();
        private object lockTimerElapsed = new object();
        

        #endregion

        #region Properties

        public override string Key
        {
            get
            {
                return "ZamowieniaView";
            }
        }

        public override System.Collections.IList Rows
        {
            get
            {
                //lock (this.lockRows)
                //{
                    if (this.table == null)
                        this.initTable();
                    return this.table;
                //}
            }
        }

        public override int Count
        {
            get
            {
                return this.Rows.Count;
            }
        }

        public override bool SupportsSorting
        {
            get
            {
                return false;
            }
        }

        
        public static bool DisableReload
        {
            get
            {
                lock (lockDisbaleReload)
                    return disableReload;
            }
            set
            {
                lock (lockDisbaleReload)
                {
                    disableReload = value;
                    BAL.Forms.FormManager.MainForm.StatusLineText = "AutoReload: " + !value;
                }
              //  if (!this.disableReload && registryDependensyNeeded)
              //  {
              //      this.registryDependensyNeeded = false;
              //      this.registrySqlDependency();
              //  }
            }
        }
        

        #endregion

        #region Methods

        public ZamowieniaView()
        {
            //this.initSqlDependency();
            this.initReloadTimer();
        }


        public override Type GetDataType()
        {
            return typeof(Enova.Business.Old.DB.Web.ZamowienieView);
        }

        private void initReloadTimer()
        {
            this.lastReload = DateTime.Now;
            this.reloadTimer = new System.Timers.Timer();
            this.reloadTimer.Interval = reloadInterval * 1000;
            this.reloadTimer.Elapsed += new System.Timers.ElapsedEventHandler(reloadTimer_Elapsed);
            this.reloadTimer.Start();
        }

        private void reloadTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (this.lockTimerElapsed)
            {
                var now = DateTime.Now;
                var diff = now - lastReload;
                if (diff.TotalSeconds >= minReloadInterval)
                {
                    this.Reload();
                }
            }
        }

        private void initTable()
        {
            this.table = new Enova.Business.Old.Web.ZamowieniaView(this.getQuery());
            this.table.DataLoaded+=new EventHandler(table_DataLoaded);
            
        }


        public override int IndexOf(object obj)
        {
            if (obj is DBWeb.ZamowienieView)
                return base.IndexOf(obj);
            var zam = this.Rows.Cast<DBWeb.ZamowienieView>().Where(z => z.GUID == ((DBWeb.Zamowienie)obj).GUID).FirstOrDefault();
            return base.IndexOf(zam);
        }

        public override void Remove(object obj)
        {
            DBWeb.Zamowienie zam = ((DBWeb.ZamowienieView)obj).Zamowienie;
            zam.DeleteRecord();
            Enova.Business.Old.Core.ContextManager.WebContext.Refresh(RefreshMode.StoreWins, obj);
            this.Reload();
            base.Remove(obj);
        }

        public override int Add(object obj)
        {
            if (obj is DBWeb.Zamowienie)
            {
                var view = ((DBWeb.Zamowienie)obj).GetZamowienieView();
                //base.Add(view);
                this.Reload();
                return 0;
            }
            return base.Add(obj);
        }

        private ObjectQuery<DBWeb.ZamowienieView> getQuery()
        {
            DateTime from = okresControl.DateFrom.Date;
            DateTime to = okresControl.DateTo.Date.AddDays(1);
            string trasa = kontrahentControl.Trasa;
            string przedstawiciel = kontrahentControl.Przedstawiciel;
            string sezon = sezonControl.SelectedIndex == 0 ? null : (string)sezonControl.Value;
            Enova.Business.Old.DB.Web.Kontrahent kontrahent = null;
            Guid[] guids = null;

            if (kontrahentControl.Kontrahent != null)
            {
                //kontrahent = kontrahentControl.Kontrahent.GetWebKontrahent();
                kontrahent = Enova.Business.Old.DB.Web.Kontrahent.GetWebKontrahent(kontrahentControl.Kontrahent);
            }
            else if (!string.IsNullOrEmpty(przedstawiciel) && !string.IsNullOrEmpty(trasa))
            {
                trasa = @"\" + przedstawiciel + @"\" + trasa + @"\";
                var ec = Enova.Business.Old.Core.ContextManager.DataContext;
                guids = (from k in ec.Kontrahenci
                         join f in ec.Features on
                         new { ParentType = "Kontrahenci", Parent = k.ID, Name = "TRASY" } equals
                         new { ParentType = f.ParentType, Parent = f.Parent, Name = f.Name }
                         where f.Data == trasa
                         select k.Guid).ToArray();
            }


            string kontrahentKod = kontrahentControl.Kontrahent != null ? kontrahentControl.Kontrahent.Kod : null;
            string przedtawiciel = string.IsNullOrEmpty(kontrahentControl.Przedstawiciel) ? null : kontrahentControl.Przedstawiciel;

            var dc = Enova.Business.Old.Core.ContextManager.WebContext;

            if(guids != null)
                return (ObjectQuery<DBWeb.ZamowienieView>)dc.ZamowieniaView.Where(z =>
                (z.DataDodania >= from && z.DataDodania <= to
                || (z.StatusTyp != null && z.StatusTyp != (int)DBWeb.TypStatusuZamowienia.Anulowane && z.StatusTyp != (int)DBWeb.TypStatusuZamowienia.Wyslane))
                && z.KontrahentGuid!=null && guids.Contains(z.KontrahentGuid.Value)
                && (sezon == null || (z.Sezon == sezon || z.SezonDodatkowy == sezon))
                && z.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete
                && z.Synchronizacja != (int)RowSynchronizeOld.Synchronizing
                && z.Synchronizacja != (int)RowSynchronizeOld.Notsaved);
           
            return (ObjectQuery<DBWeb.ZamowienieView>)dc.ZamowieniaView.Where(z =>
                (z.DataDodania >= from && z.DataDodania <= to
                || (z.StatusTyp != null && z.StatusTyp != (int)DBWeb.TypStatusuZamowienia.Anulowane && z.StatusTyp != (int)DBWeb.TypStatusuZamowienia.Wyslane))
                && (kontrahentKod == null || z.KontrahentKod == kontrahentKod)
                && (przedtawiciel == null || z.PrzedstawicielKod == przedtawiciel)
                && (sezon == null || (z.Sezon == sezon || z.SezonDodatkowy == sezon))
                && z.Synchronizacja != (int)RowSynchronizeOld.NotsynchronizedDelete
                && z.Synchronizacja != (int)RowSynchronizeOld.Synchronizing
                && z.Synchronizacja != (int)RowSynchronizeOld.Notsaved);
        }

        public override string GetTitle()
        {
            return "Zamówienia";
        }

        public override IEnumerable<DataContextParam> GetParams()
        {
            return new DataContextParam[]{
                new DataContextParam("DataParam", "Data:", new PropertyPath(this.GetDataType(),"DataDodania")){ ControlType = typeof(Enova.Business.Old.Controls.DateTimeSpanControl)},
                new DataContextParam("KontrahentParam", null, new PropertyPath(this.GetDataType(),"KontrahentID")){ControlType = typeof(Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect)},
                new DataContextParam("SezonParam", "Sezon:", new PropertyPath(this.GetDataType(), "Sezon")){ControlType = typeof(BAL.Forms.Controls.ComboBox)}
            };
        }

        /*
        public override DataContext CreateContext(object data)
        {
            return new ZamowienieViewContext(data);
        }
         */

        public override object CreateData()
        {
            var data = new DBWeb.Zamowienie();
            data.GUID = Guid.NewGuid();
            data.DataDodania = DateTime.Now;
            data.DataAtualizacji = DateTime.Now;
            data.PSID = 0;
            data.Synchronizacja = (int)RowSynchronizeOld.NotsynchronizedNew;
            data.Stamp = DateTime.Now;
            data.RodzajTransportu = RodzajTransportu.NieWybrano;
            return data;
        }

        protected override void OnInitParam(DataContextParamEventArgs e)
        {
            switch (e.Param.Name)
            {
                case "DataParam":
                    this.okresControl = e.Control as Enova.Business.Old.Controls.DateTimeSpanControl;
                    break;
                case "KontrahentParam":
                    //this.kontrahentControl = e.Control as Enova.Business.Old.Controls.KontrahentSelectControl;
                    this.kontrahentControl = e.Control as Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect;
                    break;
                case "SezonParam":
                    this.sezonControl = e.Control as BAL.Forms.Controls.ComboBox;
                    this.sezonControl.Width = 200;
                    this.sezonControl.DataContext = new Handel.Forms.EnovaSezonyView();
                    break;
            }
            base.OnInitParam(e);
        }

        protected override void OnParamValueChanged(DataContextParamEventArgs e)
        {
            this.initTable();
            this.Reset();
            base.OnParamValueChanged(e);
        }

        public override bool Load()
        {
            string name = this.Key + ".grid.xml";
            var service = AppController.Instance.FileStorageService.GetFileStorageService(name);
            if (service != null && !service.Exist(name))
            {

                string str = AbakTools.Handel.Forms.Properties.Resources.ZamowieniaView_grid;
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                using (var writer = service.GetStreamWriter(name))
                    writer.Write(buffer, 0, buffer.Length);

            }
            return base.Load();
        }

        protected override void OnParentFormChanged(EventArgs e)
        {
            base.OnParentFormChanged(e);
            this.ParentForm.Enter += (s, a) => { DisableReload = false; };
            this.ParentForm.Leave += (s, a) => { DisableReload = true; };
        }

        private delegate void reloadDelegate();
        public override void Reload()
        {
            if (!DisableReload)
            {
                var d = new reloadDelegate(this.reload);
                this.ParentForm.Invoke(d);
            }
        }

        private void reload()
        {
            lock (this.lockReload)
            {
                using (new WaitCursor(this.ParentForm))
                {
                    this.lastReload = DateTime.Now;
                    int? firstDisplayedRow = null;
                    BAL.Forms.Controls.GridView grid = null;
                    if (this.table != null)
                    {
                        var form = (BAL.Forms.DataGridFormOld)this.ParentForm;
                        if (form != null)
                        {
                            grid = form.GridView;
                            firstDisplayedRow = grid.FirstDisplayedScrollingRowIndex;
                        }

                        this.table.Reload();

                    }
                    base.Reload();
                    if (grid != null)
                    {
                        if (firstDisplayedRow != null && firstDisplayedRow.Value >= 0 && firstDisplayedRow < grid.Rows.Count)
                            grid.FirstDisplayedScrollingRowIndex = firstDisplayedRow.Value;
                    }

                }
            }
        }

        private void table_DataLoaded(object sender, EventArgs e)
        {
            //this.registrySqlDependency();
        }

        protected override void OnBeforeEdit(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBeforeEdit(e);
        }

        protected override void OnAfterEdit(EventArgs e)
        {
            base.OnAfterEdit(e);
        }

        protected override void Dispose(bool userCall)
        {
            base.Dispose(userCall);
            if (this.reloadTimer != null)
            {
                this.reloadTimer.Stop();
                this.reloadTimer.Dispose();
                this.reloadTimer = null;
            }
            this.finishSqlDependency();
            BAL.Forms.FormManager.MainForm.StatusLineText = "";
        }

        #region SqlDependency

        private bool CanRequestNotifications()
        {
            try
            {
                SqlClientPermission perm = new SqlClientPermission(PermissionState.Unrestricted);
                perm.Demand();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void initSqlDependency()
        {
            /*
            if (this.CanRequestNotifications())
            {
                string connectionString = Enova.Business.Old.Core.ContextManager.WebContext.GetProviderConnectionString();
                SqlDependency.Stop(connectionString);
                SqlDependency.Start(connectionString);

                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }

                if (command == null)
                {
                    command = new SqlCommand(this.getSqlBroker(), connection);
                }
            }
             */
        }

        private void finishSqlDependency()
        {
            /*
            SqlDependency.Stop(Enova.Business.Old.Core.ContextManager.WebContext.GetProviderConnectionString());
            if (this.command != null)
            {
                this.command.Dispose();
                this.command = null;
            }

            if (this.connection != null)
            {
                this.connection.Dispose();
                this.connection = null;
            }
             */
        }

        private string getSqlBroker()
        {
            string sql =
                "SELECT [ID], [Blokada], [DataDodania], [Pilne], [Transport], [OstatniStatusID] " +
                "FROM dbo.Zamowienia " +
                "WHERE [Synchronizacja] IN (0, 2, 3) AND [OstatniStatusID] NOT IN (3, 8)";
            return sql;
        }

        private void registrySqlDependency()
        {
            /*
            if (sqlDependencyEnable)
            {
                command.Notification = null;
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                }
            }
             */
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            /*
            if (e.Info == SqlNotificationInfo.Invalid || e.Info == SqlNotificationInfo.Error)
            {
                sqlDependencyEnable = false;
                return;
            }

            SqlDependency dependency = (SqlDependency)sender;
            dependency.OnChange -= dependency_OnChange;
            if (!this.disableReload)
            {
                this.Reload();
            }
            else
                this.registryDependensyNeeded = true;
             */
        }

        #endregion

        #endregion
    }
}
