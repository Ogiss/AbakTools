using System.Collections.Generic;
using System.Net.Mail;

namespace AbakTools.Web
{
    public class MailData
    {
        public string MailTo { get; set; }
        public string MailToName { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public List<AttachmentData> Attachments { get; set; } = new List<AttachmentData>();
    }
}
