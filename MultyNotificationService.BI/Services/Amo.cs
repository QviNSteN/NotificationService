using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.BI.Interfaces;
using identity_connect.Interfaces;
using MultyNotificationService.BI.Options;
using MultyNotificationService.Data.Dto;
using MultyNotificationService.General.Expansions;

namespace MultyNotificationService.BI.Services
{
    public class Amo : ISendDataAmo
    {
        private readonly AmoConfig _config;
        private readonly ISenderData _sender;

        public Amo(AmoConfig config, ISenderData sender)
        {
            _config = config;
            _sender = sender;
        }

        public async Task SendData(object data) => await SendData((AmoData)data);

        private async Task SendData(AmoData data)
        {
            await _sender.Post(data, _config.AmoService.Url.FixUrl(), _config.AmoService.Token);
        }
    }
}
