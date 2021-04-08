using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultyNotificationService.BI.Options
{
    public class Config
    {
        public TelegramConfig TelegramConfig { get; set; }

        public EmailConfig EmailConfig { get; set; }

        public UserConfig UserConfig { get; set; }

        public AmoConfig AmoConfig { get; set; }
    }
}
