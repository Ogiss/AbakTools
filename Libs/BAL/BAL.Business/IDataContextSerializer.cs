using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BAL.Business
{
    public interface IDataContextSerializer
    {
        void Serialize(Stream stream, DataContext context);
        void Deserialize(Stream stream, DataContext context);
    }
}
