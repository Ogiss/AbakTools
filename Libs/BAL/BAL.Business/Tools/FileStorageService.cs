using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BAL.Tools
{
    public class FileStorageService : IFileStorageService
    {
        #region Fields

        private string rootPath;
        private IFileStorageService parent;
        private IFileStorageService gridStorageService;
        private IFileStorageService tmp;

        #endregion

        #region Properties

        public string RootPath
        {
            get
            {
                if (parent != null)
                    return Path.Combine(parent.RootPath, this.rootPath);
                return this.rootPath;
            }
        }

        public IFileStorageService Tmp
        {
            get
            {
                if (this.parent != null)
                    return this.parent.Tmp;
                if (this.tmp == null)
                    this.tmp = this.CreateTmpStorageService();
                return this.tmp;
            }
        }

        #endregion

        #region Methods

        public FileStorageService(string rootPath, IFileStorageService parent)
        {
            this.rootPath = rootPath;
            this.parent = parent;
        }

        public FileStorageService()
        {
            this.rootPath = Directory.GetCurrentDirectory();
            this.gridStorageService = this.CreateGridStorageService();
        }

        public virtual void CheckRootPath()
        {
            if (!Directory.Exists(this.RootPath))
                Directory.CreateDirectory(this.RootPath);
        }

        public virtual bool Exist(string name)
        {
            this.CheckRootPath();
            return File.Exists(this.PrepareName(name));
        }

        public virtual IEnumerable<string> GetFolders(string path)
        {
            this.CheckRootPath();
            return Directory.GetDirectories(Path.Combine(this.RootPath, path));
        }

        public virtual IEnumerable<string> GetFiles(string path)
        {
            this.CheckRootPath();
            return Directory.GetFiles(Path.Combine(this.RootPath, path));
        }

        public virtual System.IO.Stream GetStreamReader(string name)
        {
            if (this.Exist(name))
            {
                return new FileStream(this.PrepareName(name), FileMode.Open, FileAccess.Read);
            }
            else
                throw new FileNotFoundException(string.Format("File not found {0}", name));
        }

        public virtual System.IO.Stream GetStreamWriter(string name)
        {
            this.CheckRootPath();
            File.WriteAllText(this.PrepareName(name), string.Empty);
            return new FileStream(this.PrepareName(name), FileMode.OpenOrCreate, FileAccess.Write);
        }

        public virtual void Remove(string name)
        {
            if (this.Exist(name))
                File.Delete(this.PrepareName(name));
        }

        public virtual Business.Session Session
        {
            get { return null; }
        }

        public virtual void Dispose() { }

        public virtual string PrepareName(string name)
        {
            return Path.Combine(this.RootPath, name);
        }

        public virtual IFileStorageService CreateGridStorageService()
        {
            return new FileStorageService("Grids", this);
        }

        protected virtual IFileStorageService CreateTmpStorageService()
        {
            return new FileStorageService("Tmp", this);
        }

        public virtual IFileStorageService GetFileStorageService(string name)
        {
            if (name.EndsWith(".grid.xml"))
                return this.gridStorageService;
            else if (name.EndsWith(".tmp"))
                return this.Tmp;
            return this;
        }

        #endregion
    }
}
