using Microsoft.EntityFrameworkCore;
using PROJECT_S19.Data;
using PROJECT_S19.DTOs.Artist;
using PROJECT_S19.DTOs.Event;
using PROJECT_S19.Models;

namespace PROJECT_S19.Services
{
    public class EventService
    {
        private readonly PROJECT_S19DbContext _context;
        public EventService(PROJECT_S19DbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateEventAsync(CreateEventDto createEvent)
        {
            try
            {
                var newEvent = new Event()
                {
                    Title = createEvent.Title,
                    Date = createEvent.Date,
                    Location = createEvent.Location,
                    ArtistId = createEvent.ArtistId

                };

                _context.Events.Add(newEvent);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateEventAsync(int id, UpdateEventDto updateEvent)
        {
            try
            {
                var existingEvent = await _context.Events.FirstOrDefaultAsync(a => a.Id == id);

                if (existingEvent == null)
                {
                    return false;
                }

                existingEvent.Title = updateEvent.Title;
                existingEvent.Date = updateEvent.Date;
                existingEvent.Location = updateEvent.Location;

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            try
            {
                var existingEvent = await _context.Events.FirstOrDefaultAsync(a => a.Id == id);

                if (existingEvent == null)
                {
                    return false;
                }

                _context.Events.Remove(existingEvent);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<GetEventRequestDto>?> GetEventsAsync()
        {
            try
            {
                var getEvents = await _context.Events.ToListAsync();

                var getEventsRequest = new List<GetEventRequestDto>();

                foreach (var getEvent in getEvents)
                {
                    var request = new GetEventRequestDto()
                    {
                        Id = getEvent.Id,
                        Title = getEvent.Title,
                        Date = getEvent.Date,
                        Location = getEvent.Location,
                        ArtistId = getEvent.ArtistId
                    };

                    getEventsRequest.Add(request);
                }

                return getEventsRequest;
            }
            catch
            {
                return null;
            }
        }
        public async Task<GetEventRequestDto?> GetEventByIdAsync(int id)
        {
            try
            {
                var existingEvent = await _context.Events.FirstOrDefaultAsync(s => s.Id == id);

                if (existingEvent == null)
                {
                    return null;
                }

                var getEvent = new GetEventRequestDto()
                {
                    Id = existingEvent.Id,
                    Title = existingEvent.Title,
                    Date = existingEvent.Date,
                    Location = existingEvent.Location,
                    ArtistId = existingEvent.ArtistId
                };

                return getEvent;
            }
            catch
            {
                return null;
            }
        }
    }
}
