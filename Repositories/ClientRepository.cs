using Microsoft.EntityFrameworkCore;
using Zadanie7.Interfaces;
using Zadanie7.Models;

namespace Zadanie7.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly S24441Context _context;
        public ClientRepository(S24441Context context) 
        { 
            _context = context;
        }

        public async Task DeleteClientAsync(int clientId)
        {
            var client = await _context.Clients.Include(client => client.ClientTrips).FirstOrDefaultAsync(client => client.IdClient == clientId);
            if (client == null) 
                throw new Exception($"The given id {clientId} doesn't corespond to any client");

            if (client.ClientTrips.Any())
                throw new Exception($"Unable to remove client with trips");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
