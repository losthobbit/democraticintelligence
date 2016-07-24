using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostHobbit.Interfaces.Events
{
    public interface ITimedEvent
    {
        DateTime LastRun { get; set; }
        TimeSpan Interval { get; set; }
        void Action();
    }
}
