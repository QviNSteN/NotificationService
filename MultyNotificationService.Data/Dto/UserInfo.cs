using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultyNotificationService.Data.Dto
{
    public class UserInfoModel
    {
        public string[] Emails { get; set; }

        public string SimpleFio { get; set; }

        public string Fio { get; set; }

        public object Amo { get; set; }
    }
}
