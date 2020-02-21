using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BAL.Tools
{
    public interface IFileStorageService : BAL.Business.IAppService
    {
        string RootPath { get; }
        IFileStorageService Tmp { get; }
        bool Exist(string name);
        IEnumerable<string> GetFolders(string path);
        IEnumerable<string> GetFiles(string path);
        Stream GetStreamReader(string name);
        Stream GetStreamWriter(string name);
        void Remove(string name);
        IFileStorageService GetFileStorageService(string name);
    }
}
