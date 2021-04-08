using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.Data.Dto;

namespace MultyNotificationService.BI.Interfaces
{
    public interface ISendData
    {
        Task SendData(object data);
    }

    public interface ISendDataEmail : ISendData
    {
    }

    public interface ISendDataTelegram : ISendData
    {
    }

    public interface ISendDataAmo : ISendData
    {
    }
}
