using CQRSlite.Bus;
using CQRSlite.Messages;
using CQRSlite.Tests.Substitutes;
using NUnit.Framework;

namespace CQRSlite.Tests.Bus
{
    [TestFixture]
    public class When_publishing_events
    {
        private InProcessBus _bus;
        private HandlerRegistrar _registrar;
        private MessageRouter _router;

        [SetUp]
        public void Setup()
        {
            _registrar = new HandlerRegistrar();
            _router = new MessageRouter(_registrar);
            _bus = new InProcessBus(_router);
        }

        [Test]
        public void Should_publish_to_all_handlers()
        {
            var handler = new TestAggregateDidSomethingHandler();
            _registrar.RegisterHandler<TestAggregateDidSomething>(handler.Handle);
            _registrar.RegisterHandler<TestAggregateDidSomething>(handler.Handle);
            _bus.Publish(new TestAggregateDidSomething());
            Assert.AreEqual(2, handler.TimesRun);
        }

        [Test]
        public void Should_work_with_no_handlers()
        {
            _bus.Publish(new TestAggregateDidSomething());
        }
    }
}