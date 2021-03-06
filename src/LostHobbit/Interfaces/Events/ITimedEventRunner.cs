﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostHobbit.Interfaces.Events
{
    /// <summary>
    /// For triggering events periodically.
    /// </summary>
    public interface ITimedEventRunner
    {
        void RunEventsAsync(Action callback = null);
        /// <exception cref="AggregateException">Events threw exceptions.</exception>
        void RunEvents(IEnumerable<ITimedEvent> readyEvents = null, Action callback = null);

        void AddEvent(ITimedEvent evt);
        void RemoveEvent(ITimedEvent evt);
    }
}
