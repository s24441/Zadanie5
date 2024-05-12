using Zadanie7.DTOs;

namespace Zadanie7.Interfaces
{
    public interface ITripRepository
    {
        Task<IEnumerable<TripDTO>> GetTripsAsync();
        Task<AddClientToTripDTO> AddClientToTripAsync(int clientId, AddClientToTripDTO dto);
    }
}
