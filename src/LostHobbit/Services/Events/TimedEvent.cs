using LostHobbit.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostHobbit.Services.Events
{
    public abstract class TimedEvent : ITimedEvent
    {
        protected TimedEvent(TimeSpan interval)
        {
            Interval = interval;
            LastRun = DateTime.MinValue;
        }

        public DateTime LastRun { get; set; }
        public TimeSpan Interval { get; set; }

        public abstract void Action();
    }
}
