using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MultyNotificationService.BI.Interfaces;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace MultyNotificationService.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("notification")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IMapper _mapper;

        private readonly ISwitch _switch;

        public NotificationController(ILogger<NotificationController> logger, IMapper mapper, ISwitch @switch)
        {
            _logger = logger;
            _mapper = mapper;
            _switch = @switch;
        }

        [HttpPost]
        public async Task<IActionResult> Post(JObject model)
        {
            await _switch.SwitchData(model);
            return Ok();
        }
    }
}
