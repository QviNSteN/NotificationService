using System;
using Autofac;
using AutoMapper;
using MultyNotificationService.Api.Configurations.AutoMapper;
using MultyNotificationService.BI.Options;
using MultyNotificationService.BI.Interfaces;
using MultyNotificationService.BI.Services;
using Divergic.Configuration.Autofac;
using Autofac.Core;
using System.Collections;
using Autofac.Features.Metadata;
using identity_connect.Interfaces;
using identity_connect.Http;

namespace MultyNotificationService.Api.Configurations.Autofac
{
    public class ServiceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Telegram>()
                .As<ISendDataTelegram>()
                .WithMetadata("sendTo", "Telegram");

            builder.RegisterType<Email>()
                .As<ISendDataEmail>()
                .WithMetadata("sendTo", "Email");

            builder.RegisterType<Amo>()
                .As<ISendDataAmo>()
                .WithMetadata("sendTo", "Email");

            builder.RegisterType<Switch>()
                .As<ISwitch>();
            builder.RegisterType<UserInfo>()
                .As<IUserInfo>();
            builder.RegisterType<Request>()
                .As<ISenderData>();
                

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var resolver = new EnvironmentJsonResolver<Config>("appsettings.json", $"appsettings.{env}.json");
            var module = new ConfigurationModule(resolver);

            builder.RegisterModule(module);
        }
    }
}
