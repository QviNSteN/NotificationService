using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.Data.Dto;
using Newtonsoft.Json.Linq;

namespace MultyNotificationService.BI.Interfaces
{
    public interface ISwitch
    {
        Task SwitchData(JObject data);
    }
}
