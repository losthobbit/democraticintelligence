﻿using LostHobbit.Interfaces.Events;
using LostHobbit.Services.Events;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LostHobbit.Tests.Services.Events
{
    [TestFixture]
    public class EventRunnerTests
    {
        private TimedEventRunner target;

        private Mock<ITimedEvent> evt;

        [SetUp]
        public void SetUp()
        {
            target = new TimedEventRunner(new List<ITimedEvent>());

            evt = new Mock<ITimedEvent>();
            evt.SetupGet(x => x.LastRun).Returns(DateTime.Now.AddSeconds(-10));
            evt.SetupGet(x => x.Interval).Returns(TimeSpan.FromSeconds(9));
            target.AddEvent(evt.Object);
        }

        #region RunEvents

        [Test]
        public void RunEvents_with_ready_event_runs_event()
        {
            //Act
            target.RunEvents();

            //Assert
            evt.Verify(x => x.Action(), Times.Once);
        }

        [Test]
        public void RunEvents_with_not_ready_event_runs_event()
        {
            //Arrange
            evt.SetupGet(x => x.LastRun).Returns(DateTime.Now);

            //Act
            target.RunEvents();

            //Assert
            evt.Verify(x => x.Action(), Times.Never);
        }

        [Test]
        public void RunEvents_with_ready_event_which_throws_exception_throws_AggregateException()
        {
            //Arrange
            evt.Setup(x => x.Action()).Throws<Exception>();

            //Act
            Assert.Throws<AggregateException>(() => target.RunEvents());
        }

        #endregion RunEvents

        #region RunEventsAsync

        [Test]
        public void RunEventsAsync_with_ready_event_runs_event()
        {
            // Arrange
            var pause = new ManualResetEvent(false);

            //Act
            target.RunEventsAsync(() => pause.Set());

            //Assert
            Assert.IsTrue(pause.WaitOne(2000));
            evt.Verify(x => x.Action(), Times.Once);
        }

        [Test]
        public void RunEventsAsync_with_not_ready_event_runs_event()
        {
            //Arrange
            var pause = new ManualResetEvent(false);
            evt.SetupGet(x => x.LastRun).Returns(DateTime.Now);

            //Act
            target.RunEventsAsync(() => pause.Set());

            //Assert
            pause.WaitOne(2000);
            evt.Verify(x => x.Action(), Times.Never);
        }

        #endregion RunEventsAsync

    }
}
