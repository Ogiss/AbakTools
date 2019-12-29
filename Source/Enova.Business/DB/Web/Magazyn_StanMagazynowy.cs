using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Enova.Business.Old.DB.Web
{
    public partial class Magazyn_StanMagazynowy : Enova.Business.Old.Core.IContextSaveChanges, Enova.Business.Old.IDbContext
    {
        #region Properties

        public System.Data.Objects.ObjectContext DbContext { get; set; }

        public double StanRazem
        {
            get { return StanMag - Rezerwacje; }
        }


        #endregion

        #region Methods

        public bool SaveChanges(System.Data.Objects.ObjectContext dataContext)
        {
            if (dataContext != null && this.TowarGuid != Guid.Empty)
            {
                if (this.EntityState == System.Data.EntityState.Detached)
                    dataContext.AddObject("Magazyn_StanyMagazynowe", this);
                dataContext.SaveChanges();
                dataContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
                return true;
            }
            if (this.EntityState == System.Data.EntityState.Added)
                dataContext.DeleteObject(this);
            return false;
        }

        public void UstawStanMag(double zmiana, Magazyn_StanMagazynowy.RodzajZmianyStanuMag rodzajZmiany)
        {
            if (this.DbContext != null)
            {
                string cs = ((WebContext)this.DbContext).GetProviderConnectionString();
                if (!string.IsNullOrEmpty(cs))
                {
                    using (var t = new TransactionScope())
                    {
                        int num = 0;
                        using (var con = new SqlConnection(cs))
                        {
                            con.Open();
                            var command = new SqlCommand("UstawStanMag")
                            {
                                CommandType = CommandType.StoredProcedure,
                                Connection = con
                            };

                            command.Parameters.Add("@TowarGuid", SqlDbType.UniqueIdentifier);
                            command.Parameters["@TowarGuid"].Direction = ParameterDirection.Input;
                            command.Parameters["@TowarGuid"].Value = this.TowarGuid;

                            command.Parameters.Add("@Zmiana", SqlDbType.Float);
                            command.Parameters["@Zmiana"].Direction = ParameterDirection.Input;
                            command.Parameters["@Zmiana"].Value = (float)zmiana;

                            command.Parameters.Add("@RodzajZmiany", SqlDbType.Int);
                            command.Parameters["@RodzajZmiany"].Direction = ParameterDirection.Input;
                            command.Parameters["@RodzajZmiany"].Value = (int)rodzajZmiany;

                            num = command.ExecuteNonQuery();
                        }
                        t.Complete();
                    }
                    this.DbContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, this);
                }
            }
        }

        #endregion

        #region Nested Types

        public enum RodzajZmianyStanuMag { Ustaw = 0, Dodaj = 1, Odejmij = 2 };

        #endregion

    }
}
