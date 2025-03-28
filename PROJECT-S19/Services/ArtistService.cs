using Microsoft.EntityFrameworkCore;
using PROJECT_S19.Data;
using PROJECT_S19.DTOs.Artist;
using PROJECT_S19.DTOs.Event;
using PROJECT_S19.Models;

namespace PROJECT_S19.Services
{
    public class ArtistService
    {
        private readonly PROJECT_S19DbContext _context;
        //INIEZIONE LOG DI SERILOG
        private readonly ILogger<ArtistService> _logger;

        public ArtistService(PROJECT_S19DbContext context, ILogger<ArtistService> logger)
        {
            _context = context;
            _logger = logger;
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

        public async Task<bool> CreateArtistAsync(CreateArtistDto createArtist)
        {
            try
            {
                var artist = new Artist()
                {
                    Name = createArtist.Name,
                    Genre = createArtist.Genre,
                    Bio = createArtist.Bio
                };

                _context.Artists.Add(artist);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                //USO DEL SERILOG PER L'ECCEZIONE
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateArtistAsync(int id, CreateArtistDto updateArtist)
        {
            try
            {
                var existingArtist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);

                if (existingArtist == null)
                {
                    return false;
                }

                existingArtist.Name = updateArtist.Name;
                existingArtist.Genre = updateArtist.Genre;
                existingArtist.Bio = updateArtist.Bio;

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            try
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);

                if (artist == null)
                {
                    return false;
                }

                _context.Artists.Remove(artist);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<GetArtistRequestDto>?> GetArtistsAsync()
        {
            try
            {
                var artists = await _context.Artists.ToListAsync();

                var artistsRequest = new List<GetArtistRequestDto>();

                foreach (var artist in artists)
                {
                    var request = new GetArtistRequestDto()
                    {
                        Id = artist.Id,
                        Name = artist.Name,
                        Genre = artist.Genre,
                        Bio = artist.Bio,
                        Events = artist.Events?.Select(e => new GetEventDto()
                        {
                            Title = e.Title,
                            Date = e.Date,
                            Location = e.Location
                        }).ToList()
                    };

                    artistsRequest.Add(request);
                }

                return artistsRequest;
            }
            catch
            {
                return null;
            }
        }
        public async Task<GetArtistRequestDto?> GetArtistByIdAsync(int id)
        {
            try
            {
                var existingArtist = await _context.Artists.FirstOrDefaultAsync(s => s.Id == id);

                if (existingArtist == null)
                {
                    return null;
                }

                var artist = new GetArtistRequestDto()
                {
                    Id = existingArtist.Id,
                    Name = existingArtist.Name,
                    Genre = existingArtist.Genre,
                    Bio = existingArtist.Bio,
                    Events = existingArtist.Events?.Select(e => new GetEventDto()
                    {
                        Title = e.Title,
                        Date = e.Date,
                        Location = e.Location
                    }).ToList()
                };

                return artist;
            }
            catch
            {
                return null;
            }
        }
    }
}
