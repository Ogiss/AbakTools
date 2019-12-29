using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Types;
using Enova.Business.Old.Core;

namespace Enova.Business.Old.DB
{
    public partial class FeatureDef : IContextSaveChanges, IDeleteRecord
    {
        public FeatureDef() : this(string.Empty, string.Empty, FeatureTypeNumber.String) { }

        public FeatureDef(string tableName, string name, FeatureTypeNumber type)
        {
            this.TableName = tableName;
            this.Name = name;
            this.TypeNumber = (int)type;
            this.Category = "Grupowanie";
            this.Description = "Cecha wykorzystywana do grupowania danych";
            this.TypeInformation = string.Empty;
            this.ReadOnlyMode = 2;
            this.InitValueStr = "";
            this.Algorithm = 0;
            this.ValueRequired = false;
            this.Precision = 0;
            this.ValueFrom = string.Empty;
            this.ValueTo = string.Empty;
            this.TextLength = 0;
            this.ValueFilter = string.Empty;
            this.Group = true;
            this.History = false;
            this.StrictDictionary = true;
            this.Dictionary = name;
            this.Info = string.Empty;
            this.Guid = Guid.NewGuid();
            this.Stamp = BitConverter.GetBytes(DateTime.Now.Ticks);
        }

        public ObjectQuery<Dictionary> DictionarySet
        {
            get
            {
                return (ObjectQuery<Dictionary>)Enova.Business.Old.Core.ContextManager.DataContext.DictionarySet
                    .Where(d => (d.Category == "F." + this.Dictionary) && d.ParentID == null);
            }
        }

        public static List<FeatureDef> GrupyRabatowe
        {
            get
            {
                List<FeatureDef> list = new List<FeatureDef>();

                var dc = Enova.Business.Old.Core.ContextManager.DataContext;
                var guid = new Guid("00000000-0011-0002-0001-000000000000");
                var defdh = Enova.Business.Old.Core.ContextManager.DataContext.DefDokHandlowych.Where(ddh => ddh.Guid == guid).FirstOrDefault();
                if (defdh.Cena.Rabat1GrupaTowarowa != null)
                    list.Add(defdh.Cena.Rabat1GrupaTowarowa);
                if (defdh.Cena.Rabat2GrupaTowarowa != null)
                    list.Add(defdh.Cena.Rabat2GrupaTowarowa);
                if (defdh.Cena.Rabat3GrupaTowarowa != null)
                    list.Add(defdh.Cena.Rabat3GrupaTowarowa);
                if (defdh.Cena.Rabat4GrupaTowarowa != null)
                    list.Add(defdh.Cena.Rabat4GrupaTowarowa);
                if (defdh.Cena.Rabat5GrupaTowarowa != null)
                    list.Add(defdh.Cena.Rabat5GrupaTowarowa);

                return list;
            }
        }

        public FeatureTypeNumber TypCechy
        {
            get
            {
                return (FeatureTypeNumber)this.TypeNumber;
            }
        }

        public bool IsArray
        {
            get
            {
                if (this.TypCechy != FeatureTypeNumber.Array)
                {
                    return (this.TypCechy == FeatureTypeNumber.ArrayOfTrees);
                }
                return true;

            }
        }

        public bool IsTree
        {
            get
            {
                if (this.TypCechy != FeatureTypeNumber.Tree)
                {
                    return (this.TypCechy == FeatureTypeNumber.ArrayOfTrees);
                }
                return true;
            }
        }

        #region IContextSaveChanges Implementation

        public bool SaveChanges(System.Data.Objects.ObjectContext dataContext)
        {
            var dc = (EnovaContext)dataContext;
            if (this.EntityState == EntityState.Detached || this.EntityState == EntityState.Added)
            {
                if (this.EntityState == EntityState.Detached)
                    dc.AddToFeatureDefs(this);

                Operator op = null;
                if (!string.IsNullOrEmpty(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin))
                {
                    op = dc.OperatorByName(Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperatorLogin);
                }
                else
                {
                    op = dc.OperatorByName(Enova.Business.Old.DB.Web.User.LoginedUser.Login);
                }

                if (op != null)
                {
                    ChangeInfo ci = new ChangeInfo()
                    {
                        Operator=op,
                        SourceGuid = this.Guid,
                        SourceTable = "FeatureDefs",
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

        #region IDeleteRecord Implementation

        public bool DeleteRecord()
        {
            EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;
            dc.DeleteObject(this);

            if (Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperator != null)
            {
                ChangeInfo ci = new ChangeInfo()
                {
                    Operator = Enova.Business.Old.DB.Web.User.LoginedUser.EnovaOperator,
                    SourceGuid = this.Guid,
                    SourceTable = "FeatureDefs",
                    Type = 4,
                    Time = DateTime.Now,
                    Info = this.Name + " (" + this.TableName + ")"
                };
                dc.AddToChangeInfos(ci);
            }

            dc.SaveChanges();

            return true;
        }

        #endregion
    }
}
