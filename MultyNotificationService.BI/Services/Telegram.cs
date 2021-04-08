using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.BI.Interfaces;
using MultyNotificationService.BI.Options;
using MultyNotificationService.Data.Dto;
using MultyNotificationService.General.Expansions;
using identity_connect.Interfaces;

namespace MultyNotificationService.BI.Services
{
    public class Telegram : ISendDataTelegram
    {
        private readonly TelegramConfig _config;
        private readonly ISenderData _sender;

        public Telegram(TelegramConfig config, ISenderData sender)
        {
            _config = config;
            _sender = sender;
        }

        public async Task SendData(object data) => await SendData((TelegramData)data);

        private async Task SendData(TelegramData data)
        {
            await _sender.Post(data, _config.NotificationBot.Url.FixUrl(), _config.NotificationBot.Token);
        }
    }
}
