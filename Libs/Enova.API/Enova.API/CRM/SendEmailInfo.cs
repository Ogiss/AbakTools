using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.CRM
{
    public class SendEmailInfo
    {
        private string email;
        private string to;
        private string subject;
        private string attachName;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string To
        {
            get { return to; }
            set { to = value; }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public string AttachName
        {
            get { return attachName; }
            set { attachName = value; }
        }

        public SendEmailInfo() { }

        public SendEmailInfo(string email, string to, string attachName, string subject = "ABAK DOKUMENTY")
        {
            this.email = email;
            this.to = to;
            this.attachName = attachName;
            this.subject = subject;
        }
    }
}
