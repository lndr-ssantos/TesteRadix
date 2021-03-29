﻿using Events.API.Models.Entities;
using Events.API.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.API.Services.EventsServices
{
    public interface IEventServices
    {
        Task ProcessEvents(EventViewModel eventViewModel);
        Task<List<Event>> List();

    }
}
