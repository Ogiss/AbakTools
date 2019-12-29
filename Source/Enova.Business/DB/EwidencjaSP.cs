using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace Enova.Business.Old.DB
{
    public partial class EwidencjaSP
    {
        public decimal Saldo
        {
            get
            {
                decimal? wp = this.RaportyESP.CreateSourceQuery().Sum(r => r.WplatyValue);
                decimal? wy = this.RaportyESP.CreateSourceQuery().Sum(r => r.WyplatyValue);
                return (wp == null ? 0 : wp.Value) - (wy == null ? 0 : wy.Value) + (this.SaldoBOValue == null ? 0 : this.SaldoBOValue.Value); 
            }
        }
    }
}
