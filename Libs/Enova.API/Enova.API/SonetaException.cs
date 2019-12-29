using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Enova.API
{
    [Serializable]
    public class SonetaException : Exception, ISerializable
    {
        string exceptionType;

        public string ExeptionType
        {
            get { return exceptionType; }
        }

        public SonetaException(string exceptionType, string message, Exception innerExeption)
            : base(message, innerExeption)
        {
            this.exceptionType = exceptionType;
        }

        public SonetaException(Exception ex) : this(ex.GetType().FullName, ex.Message, ex.InnerException == null ? null : new SonetaException(ex.InnerException)) { }

        protected SonetaException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
            exceptionType = (string)info.GetValue("exceptionType", typeof(string));
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("exceptionType", exceptionType);
        }

    }
}
