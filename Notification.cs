using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shedding_Shield
{
    internal class Notification
    {
        public class NotificationItem
        {
            public string Title { get; set; }
            public string Message { get; set; }
            public DateTime Timestamp { get; set; }
            public bool IsRead { get; set; }
            public NotificationItem(string title, string message)
            {
                Title = title;
                Message = message;
                Timestamp = DateTime.Now;
                IsRead = false;
            }
            public void MarkAsRead()
            {
                IsRead = true;
            }
        }
    }
}
