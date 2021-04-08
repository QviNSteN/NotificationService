using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.BI.Interfaces;
using MultyNotificationService.BI.Options;
using MultyNotificationService.Data.Dto;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MultyNotificationService.General.Expansions;

namespace MultyNotificationService.BI.Services
{
    public class Email : ISendDataEmail
    {
        private readonly IUserInfo _userInfo;
        private readonly EmailConfig _config;

        public Email(IUserInfo userInfo, EmailConfig config)
        {
            _userInfo = userInfo;
            _config = config;
        }

        public async Task SendData(object data) => await SendData((EmailData)data);

        private async Task SendData(EmailData data)
        {
            var emailMessages = await GetMessages(data);

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_config.Host, _config.Port, _config.IsSSL);
                await client.AuthenticateAsync(_config.EmailFrom, _config.PasswordFrom);
                foreach(var email in emailMessages)
                    await client.SendAsync(email);

                await client.DisconnectAsync(true);
            }
        }

        private static string GetBody(string body, IList<File> files) =>
            $@"Поступило новое уведомление:
{body}{(files != null && files.Count > 0 ? $@"{'\n'}

--------------------------
Прикреплённы{(files.Count == 1 ? "й файл" : "е файлы")}:
{String.Join('\n', files.Select(file => String.Format($"{file.Filename}: {file.Link}")))}" : "")}";

        private async Task<IList<MimeMessage>> GetMessages(EmailData data)
        {
            var userInfo = data.UserInfo ?? await _userInfo.Get(data.UserId);

            var messages = new List<MimeMessage>();
            var text = GetBody(data.Body, data.Files);

            foreach (var email in userInfo.Emails)
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_config.NameService, _config.EmailFrom));
                emailMessage.To.Add(new MailboxAddress(userInfo.SimpleFio, email));
                emailMessage.Subject = String.IsNullOrEmpty(data.Header) ? "Новое уведомление" : data.Header;
                emailMessage.Body = new TextPart(TextFormat.Text)
                {
                    Text = text
                };

                messages.Add(emailMessage);
            }

            return messages;
        }
    }
}
