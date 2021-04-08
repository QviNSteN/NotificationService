using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultyNotificationService.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace MultyNotificationService.Data.Dto
{
    public class DataBase
    {
        public int UserId { get; set; }

        public string Body { get; set; }

        public IList<File> Files { get; set; } = new List<File>();

        [JsonConverter(typeof(StringEnumConverter))]
        public TypeEnum Type { get; set; }

        public UserInfoModel UserInfo { get; set; } = null;
    }
}
