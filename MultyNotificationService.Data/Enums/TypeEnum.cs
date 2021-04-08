using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.Data.Attributes;
using MultyNotificationService.Data.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace MultyNotificationService.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TypeEnum
    {
        [ModelType(typeof(EmailData))]
        Email,
        [ModelType(typeof(TelegramData))]
        Telegram,
        [ModelType(typeof(AmoData))]
        Amo
    }
}
