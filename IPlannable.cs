using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shedding_Shield
{
    public interface IPlannable
    {
        DateTime GetBestTime();
        void Plan();
        void AddSlot(TimeSpan duration);
    }
}
