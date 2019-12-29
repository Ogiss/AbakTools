using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Types
{
    public class DataBaseSettings
    {
        public string Name { get; set; }
        public string OrginalName { get; set; }
        public string Server { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Catalog { get; set; }
        public bool Default { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderConnectionString { get; set; }
        public bool IsNew { get; set; }
    }
}
