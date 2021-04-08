using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.BI.Interfaces;
using MultyNotificationService.Data.Dto;
using MultyNotificationService.BI.Options;
using System.Text.Json;
using MultyNotificationService.General.Expansions;
using identity_connect.Interfaces;
using identity_connect.Models.Information;
using AutoMapper;

namespace MultyNotificationService.BI.Services
{
    public class UserInfo : IUserInfo
    {
        private readonly UserConfig _config;
        private readonly ISenderData _sender;
        private readonly IMapper _mapper;

        public UserInfo(UserConfig config, ISenderData sender, IMapper mapper)
        {
            _config = config;
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<UserInfoModel> Get(int userId)
        {
            return await SendRequest(userId);
        }

        private async Task<UserInfoModel> SendRequest(int userId)
        {
            var user = await _sender.Get<identity_connect.Models.Information.UserInfo>(_config.AuthService.Url.FixUrl() + $"{userId}", _config.AuthService.Token);

            return _mapper.Map<UserInfoModel>(user);
        }
    }
}