using Microsoft.EntityFrameworkCore;
using PROJECT_S19.Data;
using PROJECT_S19.DTOs.Ticket;
using PROJECT_S19.Models;

namespace PROJECT_S19.Services
{
    public class TicketService
    {
        private readonly PROJECT_S19DbContext _context;
        public TicketService(PROJECT_S19DbContext context)
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

        public async Task<bool> CreateTicketAsync(CreateTicketDto createTicket, string email)
        {
            try
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return false;
                }

                for (var i = 0; i < createTicket.Quantity; i++){
                    var newTicket = new Ticket()
                    {
                        EventId = createTicket.EventId,
                        UserId = user!.Id
                    };
                    _context.Tickets.Add(newTicket);
                }


                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTicketAsync(int id, string email)
        {
            try
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return false;
                }

                var existingTicket = await _context.Tickets.FirstOrDefaultAsync(a => a.Id == id);

                if (existingTicket == null || existingTicket.User.Id != user.Id)
                {
                    return false;
                }

                _context.Tickets.Remove(existingTicket);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<GetTicketRequestDto>?> GetTicketsAsync(string email)
        {
            try
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return null;
                }
                var getTickets = await _context.Tickets.Where(t=> t.UserId == user.Id).Include(t=> t.Event).ThenInclude(e => e.Artist).ToListAsync();

                var getTicketsRequest = new List<GetTicketRequestDto>();

                foreach (var getTicket in getTickets)
                {
                    var request = new GetTicketRequestDto()
                    {
                        Id = getTicket.Id,
                        EventId = getTicket.EventId,
                        ArtistId = getTicket.Event.ArtistId,
                        Title = getTicket.Event.Title,
                        ArtistName = getTicket.Event.Artist.Name,
                        Date = getTicket.Event.Date,
                        Location = getTicket.Event.Location,
                        PurchaseDate = getTicket.PurchaseDate
                    };

                    getTicketsRequest.Add(request);
                }

                return getTicketsRequest;
            }
            catch
            {
                return null;
            }
        }
        public async Task<GetTicketRequestDto?> GetTicketByIdAsync(int id, string email)
        {
            try
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return null;
                }
                var existingTicket = await _context.Tickets.Include(t => t.Event).ThenInclude(e => e.Artist).FirstOrDefaultAsync(s => s.Id == id);

                if (existingTicket == null || existingTicket.UserId != user.Id)
                {
                    return null;
                }

                var getTicket = new GetTicketRequestDto()
                {
                    Id = existingTicket.Id,
                    EventId = existingTicket.EventId,
                    ArtistId = existingTicket.Event.ArtistId,
                    Title = existingTicket.Event.Title,
                    ArtistName = existingTicket.Event.Artist.Name,
                    Date = existingTicket.Event.Date,
                    Location = existingTicket.Event.Location,
                    PurchaseDate = existingTicket.PurchaseDate
                };

                return getTicket;
            }
            catch
            {
                return null;
            }
        }
    }
}
