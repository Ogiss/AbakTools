using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Configuration;

namespace BAL.Business.App
{
    public interface IDatabase : IDisposable
    {
        IDatabaseConfiguration Configuration { get; }
        ISet<ILogin> Logins { get; }
        ILogin Login(string domain, string user, string password);
        void Logout(ILogin login);
        DBContextBase GetDataContext(ISessionable session, Type type);
        void CloseSession(ISessionable session, bool save);
        void Save(ISessionable session);
    }
}
