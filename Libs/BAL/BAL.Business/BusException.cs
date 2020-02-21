using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class BusException : Exception
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
            return format;
        }

        public static string Format(string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string GetMessage(Exception e)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }
            /*
            if (e is AccessDeniedException)
            {
                return "{brak praw}";
            }
             */
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
