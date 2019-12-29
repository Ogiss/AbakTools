using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Enova.Business.Old.DB.Web
{
    /*
    public partial class StanMagazynowy : Enova.Business.Old.IDbContext
    {
        #region Fields

        #endregion

        #region Properties

        public System.Data.Objects.ObjectContext DbContext { get; set; }

        #endregion

        public void ZmienStan(WebContext dc, double zmiana, RodzajZmianyStanu rodzajZmiany, out double stanMag)
        {
            stanMag = 0;
            using (var t = new TransactionScope())
            {
                int num = 0;
                using (var conn = new SqlConnection(dc.GetProviderConnectionString()))
                {
                    
                    conn.Open();
                    var command = new SqlCommand("ZmienStanMagazynu")
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = conn
                        
                    };

                    command.Parameters.Add("@guid", SqlDbType.UniqueIdentifier);
                    command.Parameters["@guid"].Direction = ParameterDirection.Input;
                    command.Parameters["@guid"].Value = this.EnovaTowarGuid.Value;

                    command.Parameters.Add("@zmiana", SqlDbType.Float);
                    command.Parameters["@zmiana"].Direction = ParameterDirection.Input;
                    command.Parameters["@zmiana"].Value = (float)zmiana;

                    command.Parameters.Add("@rodzajZmiany", SqlDbType.Int);
                    command.Parameters["@rodzajZmiany"].Direction = ParameterDirection.Input;
                    command.Parameters["@rodzajZmiany"].Value = (int)rodzajZmiany;

                    command.Parameters.Add("@stanMag", SqlDbType.Float);
                    command.Parameters["@stanMag"].Direction = ParameterDirection.InputOutput;
                    command.Parameters["@stanMag"].Value = stanMag;

                    num = command.ExecuteNonQuery();

                    if (!System.DBNull.Equals(command.Parameters["@stanMag"].Value, System.DBNull.Value))
                        stanMag = (double)command.Parameters["@stanMag"].Value;

                }
                t.Complete();
            }
            
        }

        public void ZmienStan(double zmiana, RodzajZmianyStanu rodzajZmiany, out double stanMag)
        {
            ZmienStan((WebContext)this.DbContext, zmiana, rodzajZmiany, out stanMag);
        }

        public Produkt Towar
        {
            get
            {
                if (DbContext != null)
                {
                    if (this.EnovaTowarGuid != null)
                        return ((WebContext)this.DbContext).Produkty
                            .Where(p =>
                                p.TowarEnova == true &&
                                (p.Usuniety == null || p.Usuniety == false) &&
                                p.Synchronizacja != (int)Types.RowSynchronizeOld.NotsynchronizedDelete &&
                                p.EnovaGuid == this.EnovaTowarGuid).FirstOrDefault();
                }
                return null;
            }
        }

        public static explicit operator StanMagazynowyView(StanMagazynowy stan)
        {
            if (stan.DbContext != null && stan.ID > 0)
                return ((WebContext)stan.DbContext).StanyMagazynoweView.Where(s => s.StanMagazynowyID == stan.ID).FirstOrDefault();
            return null;
        }

    }
     */
}
