using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business.App
{
    public interface ILogin
    {
        string Domain { get; }
        string User { get; }
        string Password { get; }
        IDatabase Database { get; }
        Session CreateSession(bool readOnly, bool isInternal, string name);
        void Logout();
    }
}
