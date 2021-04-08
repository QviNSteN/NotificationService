using System;
using System.Linq;
using AutoMapper;
using MultyNotificationService.Data;
using MultyNotificationService.Data.Dto;
using identity_connect.Models.Information;
using identity_connect.Data.Enums;

namespace MultyNotificationService.Api.Configurations.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserInfo, UserInfoModel>()
                .ForMember(x => x.Emails, s => s.MapFrom(x => x.Integrations.Where(x => x.Type == TypeIntegrationEnum.Email).Select(y => y.Value).ToList()));

           // CreateMap<ClassDTO, ClassEntity>();
        }
    }
}
