using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultyNotificationService.Data.Dto
{
    public class EmailData : DataBase
    {
        public string Header { get; set; }

        public string Email { get; set; }
    }
}