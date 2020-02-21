namespace BAL.Forms
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public class FileWaitCursor : IDisposable
    {
        private Control control;
        private int createdCount;
        private List<FileSystemWatcher> createdWatchers = new List<FileSystemWatcher>();
        public AutoResetEvent CreateEvent = new AutoResetEvent(false);
        private bool disposed;
        private object syncRoot = new object();

        public event EventHandler Created;

        public FileWaitCursor(Control control)
        {
            this.control = control;
            if (this.control != null)
            {
                this.setEnable(false);
            }
        }

        public FileWaitCursor AddCreated(string path, string filter)
        {
            FileSystemWatcher item = new FileSystemWatcher(path, filter) {
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.DirectoryName | NotifyFilters.FileName
            };
            item.Created += delegate (object sender, FileSystemEventArgs e) {
                bool flag = false;
                lock (this.syncRoot)
                {
                    this.createdCount++;
                    flag = this.createdCount == this.createdWatchers.Count;
                }
                if (flag)
                {
                    this.CreateEvent.Set();
                    this.OnCreated(new EventArgs());
                }
            };
            item.EnableRaisingEvents = true;
            lock (this.syncRoot)
            {
                this.createdWatchers.Add(item);
            }
            return this;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool userCall)
        {
            if (!this.disposed)
            {
                this.disposed = true;
                this.CreateEvent.Dispose();
                foreach (FileSystemWatcher watcher in this.createdWatchers)
                {
                    watcher.Dispose();
                }
            }
        }

        ~FileWaitCursor()
        {
            this.Dispose(false);
        }

        protected virtual void OnCreated(EventArgs e)
        {
            if (this.Created != null)
            {
                this.Created(this, e);
            }
        }

        private void setEnable(bool enable)
        {
            if (this.control != null)
            {
                if (this.control.InvokeRequired)
                {
                    this.control.Invoke(new Action<bool>(this.setEnable), new object[] { enable });
                }
                else
                {
                    this.control.Enabled = enable;
                    this.control.Cursor = enable ? Cursors.Default : Cursors.WaitCursor;
                }
            }
        }
    }
}

