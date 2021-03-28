using Events.API.Models.ViewModels;
using System.Threading.Tasks;

namespace Events.API.Services.EventsServices
{
    public interface IEventServices
    {
        Task ProcessEvents(EventViewModel eventViewModel);
    }
}
