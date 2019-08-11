using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amaury.Abstractions;
using Amaury.Abstractions.Bus;
using Amaury.Test.Fixtures;
using Amaury.Tests.Fixtures;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Amaury.Tests
{
    public class EventSourcedAggregateTests
    {
        private readonly ICelebrityEventsBus _eventBus;

        public EventSourcedAggregateTests()
        {
            _eventBus = Substitute.For<ICelebrityEventsBus>();
        }

        [Fact(DisplayName = "Deve reduzir os eventos para a entidade")]
        public async Task ShouldReduceEventsToEntity()
        {
            var expectedAggregatedId = Guid.NewGuid().ToString();
            var events = new Queue<ICelebrityEvent>();
            var fisrtEvent = new FakeCelebrityEvent(expectedAggregatedId, new { Foo = "Bar", Bar = "Foo" });
            var secondEvent = new FakeCelebrityEvent(expectedAggregatedId, new { Foo = "Foo", Bar = "Bar" });
            events.Enqueue(fisrtEvent);
            events.Enqueue(secondEvent);

            _eventBus.Get(Arg.Any<string>()).ReturnsForAnyArgs(events);

            var fooBar = new FooBar(_eventBus);
            var reduced = await fooBar.Reduce(fooBar, expectedAggregatedId);

            reduced.Should().NotBeEquivalentTo(fisrtEvent.Data);
            reduced.Should().BeEquivalentTo(secondEvent.Data);
        }
    }
}
