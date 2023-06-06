using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Dtos
{
    public class NotificationDto:BaseDto
    {
        public string NotificationType { get; set; }
        public string NotificationName { get; set; }
        public string Rule { get; set; }
        public string NotificationNo { get; set; }
        public string NotificationDate { get; set; }
    }
}
