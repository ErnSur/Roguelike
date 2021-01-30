using NUnit.Framework;

namespace LDF.EventSystem
{
    internal class TestEvent : Event
    {
    }

    internal class AnotherTestEvent : Event
    {
    }

    [TestFixture]
    public static class EventSystemTests
    {
        [Test]
        public static void Should_NotThrow_When_AddingListener()
        {
            TestDelegate d = () =>
                EventManager.AddListener<Event>((_) => { });

            Assert.DoesNotThrow(d);
        }

        [Test]
        public static void Should_NotThrow_When_AddingListenerOnce()
        {
            TestDelegate d = () =>
                EventManager.AddOneTimeListener<Event>((_) => { });

            Assert.DoesNotThrow(d);
        }

        [Test]
        public static void Should_NotThrow_WhenRemovingListener_OnEmpySystem()
        {
            TestDelegate d = () =>
                EventManager.RemoveListener<Event>((_) => { });

            Assert.DoesNotThrow(d);
        }

        [Test]
        public static void Should_NotThrow_When_RemovingAllListeners()
        {
            TestDelegate d = EventManager.RemoveAllListeners;

            Assert.DoesNotThrow(d);
        }
    }

    [TestFixture]
    public static class EventSystemIntegrationTests
    {
        [SetUp]
        public static void SetUp()
        {
            EventManager.RemoveAllListeners();
        }

        [Test]
        public static void Should_Not_TriggerEvent_When_AddedOnceAndRemoved()
        {
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { };

            EventManager.AddOneTimeListener(firedEventAction);
            TestDelegate d = () =>
                EventManager.RemoveListener(firedEventAction);

            Assert.DoesNotThrow(d);
        }

        [Test]
        public static void Should_NotThrow_WhenRemoving_ExistingListener()
        {
            EventManager.AddListener<TestEvent>((_) => { });

            TestDelegate d = () =>
                EventManager.RemoveListener<TestEvent>((_) => { });

            Assert.DoesNotThrow(d);
        }

        [Test]
        public static void Should_NotTrigger_After_RemovingAllListenersOfType()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 0;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddListener(firedEventAction);
            EventManager.AddListener(firedEventAction);
            EventManager.AddListener(firedEventAction);
            EventManager.AddListener(firedEventAction);
            EventManager.RemoveAllListenersOfType<TestEvent>();
            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_RemoveListener_When_ExistingInSystem()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 0;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddListener(firedEventAction);
            EventManager.RemoveListener(firedEventAction);
            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_NotAddListener_When_ListenerAlreadyAdded()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 1;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddListener(firedEventAction);
            EventManager.AddListener(firedEventAction);
            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_NotAddListenerOnce_When_ListenerAlreadyAddedOnce()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 1;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddOneTimeListener(firedEventAction);
            EventManager.AddOneTimeListener(firedEventAction);
            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_TriggerMultipleTimes_When_ListenerAlreadyAdded()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 2;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddListener(firedEventAction);
            EventManager.AddOneTimeListener(firedEventAction);

            EventManager.TriggerEvent(new TestEvent());
            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_TriggerOnce_When_ListenerOnceAddedAlready()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 1;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddOneTimeListener(firedEventAction);
            EventManager.AddListener(firedEventAction);

            EventManager.TriggerEvent(new TestEvent());
            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_ReturnTrue_When_ListenerExistsInSystem()
        {
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { };
            EventManager.AddOneTimeListener(firedEventAction);

            var actual = EventManager.HasListener(firedEventAction);

            Assert.IsTrue(actual);
        }

        [Test]
        public static void Should_ReturnFalse_When_ListenerNotExistsInSystem()
        {
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { };

            var actual = EventManager.HasListener(firedEventAction);

            Assert.IsFalse(actual);
        }

        [Test]
        public static void Should_Trigger_CorrectEvent()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 1;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddListener(firedEventAction);
            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_TriggerMultipleTimes_CorrectEvent()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 3;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddListener(firedEventAction);
            EventManager.TriggerEvent(new TestEvent());
            EventManager.TriggerEvent(new TestEvent());
            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_NotTriggerEvent_AfterRemoval()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 1;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddListener(firedEventAction);
            EventManager.TriggerEvent(new TestEvent());
            EventManager.RemoveListener(firedEventAction);

            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_TriggerOnce_CorrectOneTimeEventListener()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 1;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };

            EventManager.AddOneTimeListener(firedEventAction);
            EventManager.TriggerEvent(new TestEvent());
            EventManager.TriggerEvent(new TestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }

        [Test]
        public static void Should_NotTrigger_IncorrectEvent_When_AskedEventIsNotAvailable()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 0;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };
            EventManager.EventDelegate<AnotherTestEvent> anotherFiredEventAction = delegate (AnotherTestEvent _) { };

            EventManager.AddListener(firedEventAction);
            EventManager.TriggerEvent(new AnotherTestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);

        }

        [Test]
        public static void Should_NotTrigger_IncorrectEvent()
        {
            int actualFiredEvents = 0;
            const int expectedFiredEvents = 0;
            EventManager.EventDelegate<TestEvent> firedEventAction = delegate (TestEvent _) { actualFiredEvents++; };
            EventManager.EventDelegate<AnotherTestEvent> anotherFiredEventAction = delegate (AnotherTestEvent _) { };

            EventManager.AddListener(firedEventAction);
            EventManager.AddListener(anotherFiredEventAction);
            EventManager.TriggerEvent(new AnotherTestEvent());

            Assert.AreEqual(expectedFiredEvents, actualFiredEvents);
        }
    }
}