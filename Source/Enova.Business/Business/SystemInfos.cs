using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.DB;
using Enova.Business.Old.Core;

namespace Enova.Business.Old
{
    public partial class SystemInfos : EnovaTable<SystemInfo>
    {
        #region Fields
        #endregion

        #region Properties

        new public BusinessModule Module
        {
            get { return (BusinessModule)base.Module; }
        }

        public string this[SysInfoIdentifier ident]
        {
            get
            {
                return this.BaseQuery.Where(r => r.Ident == (int)ident).Select(r => r.Value).FirstOrDefault();
            }
        }

        #endregion

        #region Methods

        protected override System.Data.Objects.ObjectQuery<SystemInfo> CreateQuery()
        {
            return this.Module.DataContext.SystemInfos;
        }

        #endregion

        #region Nested Types
        #endregion
    }
}
