using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Text;

namespace Enova.Business.Old
{
    public class Kontrahenci : Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Kontrahent>
    {
        public Kontrahenci() : base(Enova.Business.Old.Core.ContextManager.DataContext, "Kontrahenci") 
        {
            Enova.Business.Old.DB.EnovaContext dc = Enova.Business.Old.Core.ContextManager.DataContext;

           
        }

        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public override bool SupportFeatures
        {
            get
            {
                return true;
            }
        }

        public override string FeaturesTableName
        {
            get
            {
                return "Kontrahenci";
            }
        }

        public override void ApplyFeatureFilter(Enova.Business.Old.DB.FeatureDef featureDef, string value)
        {
            if (DataContext != null)
            {
                string name = featureDef.Name;
                Enova.Business.Old.DB.EnovaContext dc = DataContext as Enova.Business.Old.DB.EnovaContext;
                if (featureDef.IsTree)
                {
                    BaseQuery = (ObjectQuery<Enova.Business.Old.DB.Kontrahent>)(from k in dc.Kontrahenci
                                                                            from f in dc.Features
                                                                            where k.ID == f.Parent && f.ParentType == "Kontrahenci" && f.Name == name && f.Data.StartsWith(value)
                                                                            group k by k.ID into uk
                                                                            select uk.FirstOrDefault());
                                                                           
                }
                else
                {
                    BaseQuery = (from k in dc.Kontrahenci
                                 from f in dc.Features
                                 where k.ID == f.Parent && f.ParentType == "Kontrahenci" && f.Name == name && f.Data == value
                                 select k) as ObjectQuery<Enova.Business.Old.DB.Kontrahent>;
                }
                base.ApplyFeatureFilter(featureDef, value);
            }
        }

        public override void RemoveFeatureFilter()
        {
            if (DataContext != null)
            {
                BaseQuery = ((Enova.Business.Old.DB.EnovaContext)DataContext).Kontrahenci;
            }
            base.RemoveFeatureFilter();
        }

        public override int Find(PropertyDescriptor property, object key)
        {
            int len = ((string)key).Length;
            string s = ((string)key).ToLower();
            for(var i =0; i< Rows.Count; i++)
            {
                var row = Rows[i];
                if ((!string.IsNullOrEmpty(row.Kod) && row.Kod.Length >= len && row.Kod.Substring(0, len).ToLower() == s) ||
                    (!string.IsNullOrEmpty(row.Nazwa) && row.Nazwa.Length >= len && row.Nazwa.Substring(0, len).ToLower() == s) ||
                    (!string.IsNullOrEmpty(row.NIP) && row.NIP.Length >= len && row.NIP.Substring(0, len) == s))
                    return i;
            }
            return -1;
        }
    }
}
