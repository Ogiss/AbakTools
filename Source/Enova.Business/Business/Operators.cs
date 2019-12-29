using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.Core;
using Enova.Business.Old.DB;

namespace Enova.Business.Old
{
    public partial class Operators : EnovaGuidedTable<Operator>
    {
        #region Fields

        public static Guid Administrator;
        public static Guid eaNetADMOperator;
        public static Guid eaNetCRMOperator;
        public static Guid eaNetPLNDyrektor;
        public static Guid eaNetPLNKierownik;
        public static Guid eaNetPLNOperator;
        public static Guid eaNetZAMOperator;
        public static Guid enovaNetOperator;

        private ByNameKey keyByName;

        #endregion

        #region Properties

        new public BusinessModule Module
        {
            get { return (BusinessModule)base.Module; }
        }

        new public EnovaContext DataContext
        {
            get { return (EnovaContext)base.DataContext; }
        }

        public ByNameKey ByName
        {
            get
            {
                return this.keyByName;
            }
        }

        #endregion

        #region Methods

        static Operators()
        {
            Administrator = new Guid("00000000-0015-0001-0001-000000000000");
            enovaNetOperator = new Guid("00000000-0015-0001-0100-000000000000");
            eaNetCRMOperator = new Guid("00000000-0015-0001-0100-000000000001");
            eaNetPLNOperator = new Guid("00000000-0015-0001-0100-000000000002");
            eaNetPLNKierownik = new Guid("00000000-0015-0001-0100-000000000003");
            eaNetPLNDyrektor = new Guid("00000000-0015-0001-0100-000000000004");
            eaNetZAMOperator = new Guid("00000000-0015-0001-0100-000000000005");
            eaNetADMOperator = new Guid("00000000-0015-0001-0100-000000000006");
        }

        public override void Adding(Module module)
        {
            base.Adding(module);
            this.keyByName = new ByNameKey(this);
        }

        protected override System.Data.Objects.ObjectQuery<Operator> CreateQuery()
        {
            return DataContext.Operators;
        }

        #endregion

        #region Nested Types

        public class ByNameKey : Key<Operator>
        {
            public ByNameKey(TableBase<Operator> table) : base(table) { }

            public Operator this[string name]
            {
                get
                {
                    return Table.BaseQuery.Where(o => o.Name == name).FirstOrDefault();
                }
            }
        }


        #endregion
    }
}
