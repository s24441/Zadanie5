namespace Zadanie7.Interfaces
{
    public interface IClientRepository
    {
        Task DeleteClientAsync(int clientId);
    }
}
