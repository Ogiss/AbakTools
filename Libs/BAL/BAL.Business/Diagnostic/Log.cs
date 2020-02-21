using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace BAL.Business.Diagnostic
{
    public class Log
    {
        // Fields
        private string category;
        private int indent;
        private bool isOpen;
        public static readonly string Progress = "Progress";

        // Methods
        public Log()
            : this(Progress)
        {
        }

        public Log(string category)
            : this(category, false)
        {
        }

        public Log(string category, bool open)
        {
            this.category = category;
            this.isOpen = true;
            if (open)
            {
                Trace.Write("SHOWOUTPUT", category);
            }
        }

        public Indent IncrementIndent()
        {
            return new Indent(this);
        }

        public void Warning(string message, params object[] args)
        {
            if (this.IsOpen)
            {
                Trace.WriteLine(new string('\t', this.indent) + string.Format(message, args), "W|" + this.category);
            }
        }

        public void WriteLine(object obj)
        {
            if (this.IsOpen)
            {
                Trace.WriteLine(new string('\t', this.indent) + obj.ToString(), this.category);
            }
        }

        public void WriteLine(string message, params object[] args)
        {
            if (this.IsOpen)
            {
                Trace.WriteLine(new string('\t', this.indent) + string.Format(message, args), this.category);
            }
        }

        // Properties
        public string Category
        {
            get
            {
                return this.category;
            }
        }

        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }
            set
            {
                this.isOpen = value;
            }
        }

        // Nested Types
        public class Indent : IDisposable
        {
            // Fields
            private readonly Log log;

            // Methods
            internal Indent(Log log)
            {
                this.log = log;
                this.log.indent++;
            }

            public void Dispose()
            {
                this.log.indent--;
            }
        }
    }
}
