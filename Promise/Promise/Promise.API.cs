using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promise
{
    public partial struct Promise
    {
        public static Promise Delay(float delay)
        {
            return new Promise(DelayAction.Create(delay));
        }

        public static Promise WaitUtil(Func<bool> predicate)
        {
            return new Promise(WaitUtilAction.Create(predicate));
        }
    }
}
