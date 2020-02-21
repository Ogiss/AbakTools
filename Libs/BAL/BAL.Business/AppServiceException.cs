using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class AppServiceException : ExceptionBase
    {
        private Type serviceInterface;

        public Type ServiceInterface
        {
            get { return this.serviceInterface; }
        }

        public AppServiceException(Type serviceInterface)
            : base(string.Format("Service {0} initialization error.", serviceInterface.Name))
        {
            this.serviceInterface = serviceInterface;
        }
    }
}
