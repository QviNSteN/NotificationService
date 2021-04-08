using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.BI.Interfaces;
using MultyNotificationService.Data.Dto;
using MultyNotificationService.General.Expansions;
using MultyNotificationService.Data.Enums;
using Autofac.Features.AttributeFilters;
using MultyNotificationService.Data.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultyNotificationService.BI.Services
{
    public class Switch : ISwitch
    {
        private readonly ISendData _email;
        private readonly ISendData _telegram;
        private readonly ISendDataAmo _amo;

        public Switch(ISendDataEmail email, ISendDataTelegram telegram, ISendDataAmo amo)
        {
            _email = email;
            _telegram = telegram;
            _amo = amo;
        }

        private (ISendData, Type) GetService(TypeEnum type) =>
            type switch
            {
                TypeEnum.Email => (_email, type.GetAttributeValue<ModelTypeAttribute, Type>(x => x.Type)),
                TypeEnum.Telegram => (_telegram, type.GetAttributeValue<ModelTypeAttribute, Type>(x => x.Type)),
                TypeEnum.Amo => (_amo, type.GetAttributeValue<ModelTypeAttribute, Type>(x => x.Type)),
                _ => throw new Exception()
            };

        public async Task SwitchData(JObject data)
        {
            var (service, dataType) = GetService(Enum.Parse<TypeEnum>(String.IsNullOrEmpty(data.Value<string>("Type")) ? data.Value<string>("type") : data.Value<string>("Type")));
            var convertData = data.ToObject(dataType);

           await service.SendData(convertData);
        }
    }
}
