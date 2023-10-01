using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advocate.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Advocate.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        
        public void notify(string message, NotificationType notificationType, string title = "")
        {
            var msg = new
            {
                message,
                title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = "sweetAlert"
            };
            TempData["notification"] = JsonConvert.SerializeObject(msg);
        }

        public void confirmation_notify(string message, NotificationType notificationType, string title = "")
        {
            var msg = new
            {
                message,
                title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = "sweetAlert"
            };
            TempData["confirmation_notification"] = JsonConvert.SerializeObject(msg);
        }

        
    }
}
