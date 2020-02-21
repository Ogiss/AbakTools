using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Business
{
    [ComplexType]
    public class Host
    {
        public int ID { get; set; }
        public string Type { get; set; }

        public Host() { }

        public Host(IRow row)
        {
            ID = row.ID;
            Type = row.Table.TableName;
        }
    }
}
