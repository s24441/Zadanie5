using Microsoft.EntityFrameworkCore;
using Zadanie7.DTOs;
using Zadanie7.Interfaces;
using Zadanie7.Models;

namespace Zadanie7.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly S24441Context _context;
        public TripRepository(S24441Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TripDTO>> GetTripsAsync()
        {
            var trips = await _context
                .Trips
                .OrderBy(trip => trip.DateFrom)
                .Select(trip => new TripDTO() { 
                    Name = trip.Name,
                    DateFrom = DateOnly.FromDateTime(trip.DateFrom),
                    DateTo = DateOnly.FromDateTime(trip.DateTo),
                    Description = trip.Description,
                    MaxPeople = trip.MaxPeople,
                    Countries = trip.IdCountries.Select(country => 
                        new CountryDTO() { 
                            Name = country.Name
                        }),
                    Clients = trip.ClientTrips.Select(clTrip => 
                        new ClientDTO() { 
                            FirstName = clTrip.IdClientNavigation.FirstName,
                            LastName = clTrip.IdClientNavigation.LastName
                        })
                }).ToListAsync();

            return trips;
        }

        public async Task<AddClientToTripDTO> AddClientToTripAsync(int idTrip, AddClientToTripDTO dto)
        {
            var tripExists = await _context.Trips.AnyAsync(trip => trip.IdTrip == idTrip);
            if (!tripExists)
                throw new Exception($"The given trip id {idTrip} doesn't corespond to any trip");

            var clientExists = await _context.Clients.AnyAsync(client => client.Pesel == dto.Pesel);
            if (!clientExists) 
            {
                var cl = new Client() {
                    IdClient = (await _context.Clients.MaxAsync(client => client.IdClient)) + 1,
                    FirstName = dto.FirstName, 
                    LastName = dto.LastName, 
                    Pesel = dto.Pesel, 
                    Telephone = dto.Telephone, 
                    Email = dto.Email
                };

                _context.Clients.Add(cl);
                await _context.SaveChangesAsync();
            }

            var client = await _context.Clients.Include(client => client.ClientTrips).FirstOrDefaultAsync(client => client.Pesel == dto.Pesel);
            if (client!.ClientTrips.Any(trip => trip.IdTrip == idTrip))
                throw new Exception($"Client already signed on trip with id {idTrip}");

            var clientTrip = new ClientTrip()
            {
                IdClient = client.IdClient,
                IdTrip = idTrip,
                PaymentDate = dto.PaymentDate,
                RegisteredAt = DateTime.Now

            };

            _context.ClientTrips.Add(clientTrip);

            await _context.SaveChangesAsync();

            return dto;
        }
    }
}
