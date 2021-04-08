using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultyNotificationService.BI.Options
{
    public class EmailConfig
    {
        public string EmailFrom { get; set; }

        public string PasswordFrom { get; set; }

        public string NameService { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool IsSSL { get; set; }
    }
}
