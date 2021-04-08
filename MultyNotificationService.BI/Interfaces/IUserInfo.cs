using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.Data.Dto;

namespace MultyNotificationService.BI.Interfaces
{
    public interface IUserInfo
    {
        Task<UserInfoModel> Get(int userId);
    }
}
