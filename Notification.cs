using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shedding_Shield
{
    public class Notification
    {
        public delegate void NotifyEventHandler(string message);
        public event NotifyEventHandler OnNotify;

        public void Notify(string message)
        {
            OnNotify?.Invoke(message);
        }
    }
}
