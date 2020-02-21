using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business.App
{
    public interface ILoginService : IAppService
    {
        ILogin Login(IDatabase database, string domain, string user, string password);
        void Logout(ILogin login);
    }
}
