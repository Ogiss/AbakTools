using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using Enova.Business.Old.Core;

namespace Enova.Business.Old.DB
{
    public partial class Dictionary: ISaveChanges, IDeleteRecord
    {
        public Dictionary() : this(null, string.Empty, string.Empty) { }

        public Dictionary(Dictionary parent, string category, string value)
        {
            this.Parent = parent;
            this.DataContext = null;
            this.DataContextType = null;
            this.Category = category;
            this.Lp = 0;
            this.Value = value;
            this.Guid = Guid.NewGuid();
            this.Stamp = BitConverter.GetBytes(DateTime.Now.Ticks);
        }

        public override string ToString()
        {
            return this.Value;
        }

        public ObjectQuery<Dictionary> SubDictionary
        {
            get
            {
                return (ObjectQuery<Dictionary>)Enova.Business.Old.Core.ContextManager.DataContext.DictionarySet
                    .Where(d => d.ParentID == this.ID);
            }
        }

        private Dictionary parent = null;
        public Dictionary Parent
        {
            get
            {
                if (parent == null && ParentID != null)
                {
                    if (EntityState == EntityState.Modified || EntityState == EntityState.Unchanged)
                    {
                        parent = Enova.Business.Old.Core.ContextManager.DataContext.DictionarySet
                            .Where(d => d.ID == this.ParentID).FirstOrDefault();
                    }
                }
                return parent;
            }
            set
            {
                this.parent = value;
                if (parent == null)
                {
                    this.ParentID = null;
                }
                else
                {
                    this.ParentID = parent.ID;
                }
            }
        }

        public string Path
        {
            get
            {
                return getPath("");
            }
        }

        internal string getPath(string path)
        {
            path = this.Value + @"\" + path;
            if (this.Parent == null)
            {
                return @"\" + path;
            }
            else
            {
                return Parent.getPath(path);
            }
        }

        public static ObjectQuery<Dictionary> Przedstawiciele
        {
            get
            {
                return (ObjectQuery<Dictionary>)Enova.Business.Old.Core.ContextManager.DataContext.DictionarySet
                    .Where(d => d.Category == "F.przedstawiciel");
            }
        }

        public static List<Dictionary> GetTrasy(string przedstawiciel)
        {
            Dictionary dp = Enova.Business.Old.Core.ContextManager.DataContext.DictionarySet
                .Where(d => d.Category == "F.TRASY" && d.Value == przedstawiciel).FirstOrDefault();

            if (dp != null)
            {
                return Enova.Business.Old.Core.ContextManager.DataContext.DictionarySet
                    .Where(d => d.ParentID == dp.ID).OrderBy(d => d.Value).ToList();
            }
            return null;
        }

        #region ISaveChanges

        public bool SaveChanges()
        {
            EnovaContext dc = ContextManager.DataContext;
            if (this.EntityState == EntityState.Detached || this.EntityState == EntityState.Added)
            {
                if (this.EntityState == EntityState.Detached)
                    dc.AddToDictionarySet(this);

                if (Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperator != null)
                {
                    ChangeInfo ci = new ChangeInfo()
                    {
                        Operator = Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperator,
                        SourceGuid = this.Guid,
                        SourceTable = "Dictionary",
                        Type = 1,
                        Time = DateTime.Now,
                        Info = string.Empty
                    };
                    dc.AddToChangeInfos(ci);
                }
            }

            dc.SaveChanges();

            return true;
        }

        #endregion

        private void deleteDictionary(EnovaContext dc, Dictionary dic)
        {
            var sub = dc.DictionarySet.Where(d => d.ParentID == this.ID).ToList();

            foreach (var s in sub)
                deleteDictionary(dc, s);

            dc.DeleteObject(this);

            if (Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperator != null)
            {
                ChangeInfo ci = new ChangeInfo()
                {
                    Operator = Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperator,
                    SourceGuid = this.Guid,
                    SourceTable = "Dictionary",
                    Type = 4,
                    Time = DateTime.Now,
                    Info = this.Value,
                };

                dc.AddToChangeInfos(ci);
            }
        }

        public bool DeleteRecord()
        {
            EnovaContext dc = ContextManager.DataContext;

            deleteDictionary(dc, this);

            dc.SaveChanges();

            return true;
        }
    }
}
