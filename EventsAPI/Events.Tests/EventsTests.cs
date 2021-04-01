using Events.API.Models.Entities;
using Events.API.Models.ViewModels;
using Events.API.Services.EventsServices;
using Events.API.Services.Repositories.EventsRepository;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Events.Tests
{
    public class Tests
    {
        [Fact]
        public async Task ShouldSaveEventAsProcessed()
        {
            Event savedEvent = null;
            var eventRepository = Substitute.For<IEventRepository>();
            var eventServices = new EventServices(eventRepository);
            await eventRepository.AddEventAsync(Arg.Do<Event>(e => savedEvent = e));

            var eventViewModel = new EventViewModel
            {
                Tag = "tag.teste.test01",
                TimeStamp = 15618486,
                Valor = "12345"
            };

            await eventServices.ProcessEvents(eventViewModel);

            Received.InOrder(async () =>
            {
                await eventRepository.AddEventAsync(Arg.Any<Event>());
                await eventRepository.SaveAsync();
            });
            Assert.NotNull(savedEvent);
            Assert.True(savedEvent.Processed);
        }

        [Fact]
        public async Task ShouldNotSaveEventAsProcessed()
        {
            Event savedEvent = null;
            var eventRepository = Substitute.For<IEventRepository>();
            var eventServices = new EventServices(eventRepository);

            await eventRepository.AddEventAsync(Arg.Do<Event>(e => savedEvent = e));

            var eventViewModel = new EventViewModel
            {
                Tag = "tag.teste.test01",
                TimeStamp = 15618486,
                Valor = null
            };

            await eventServices.ProcessEvents(eventViewModel);

            Received.InOrder(async () =>
            {
                await eventRepository.AddEventAsync(Arg.Any<Event>());
                await eventRepository.SaveAsync();
            });
            Assert.NotNull(savedEvent);
            Assert.False(savedEvent.Processed);
        }

        [Fact]
        public async Task ShouldThrowExceptionIfTahIsInvalid()
        {
            Event savedEvent = null;
            var eventRepository = Substitute.For<IEventRepository>();
            var eventServices = new EventServices(eventRepository);
            await eventRepository.AddEventAsync(Arg.Do<Event>(e => savedEvent = e));

            var eventViewModel = new EventViewModel
            {
                Tag = "tag",
                TimeStamp = 15618486,
                Valor = "12345"
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await eventServices.ProcessEvents(eventViewModel));
        }
    }
}