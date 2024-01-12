using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class SmtpSetting
    {
        public int Id { get; set; }
        public string MailDriver { get; set; }
        public string MailHost { get; set; }
        public string MailPort { get; set; }
        public string MailUsername { get; set; }
        public string MailPassword { get; set; }
        public string MailFromName { get; set; }
        public string MailFromEmail { get; set; }
        public string MailEncryption { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Verified { get; set; }
    }
}
