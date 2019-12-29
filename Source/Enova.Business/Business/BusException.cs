using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Old.Types;

namespace Enova.Business.Old
{
    public class BusException : Exception, IExDescription
    {
        // Fields
        private string exDescription;

        // Methods
        public BusException()
        {
        }

        public BusException(string comment)
            : base(Format(comment))
        {
        }

        public BusException(Exception inner, string comment)
            : base(comment, inner)
        {
        }

        public BusException(string format, params object[] args)
            : base(Format(format, args))
        {
        }

        public static string Format(string format)
        {
            string str = string.Empty;
            /*
            if (LangTranslator.GetLocalizedResource(format, out str))
            {
                return str;
            }
             */
            return format;
        }

        public static string Format(string format, params object[] args)
        {
            string str = string.Empty;
            /*
            if (LangTranslator.GetLocalizedResource(format, out str))
            {
                return string.Format(str, args);
            }
             */
            return string.Format(format, args);
        }

        public static string GetMessage(Exception e)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }
            
            if (e is AccessDeniedException)
            {
                return "{brak praw}";
            }
            
            return ("{stop: " + e.Message + '}');
        }

        // Properties
        public string ExDescription
        {
            get
            {
                return this.exDescription;
            }
            set
            {
                this.exDescription = value;
            }
        }

        public virtual string Solution
        {
            get
            {
                return "";
            }
        }
    }
}
