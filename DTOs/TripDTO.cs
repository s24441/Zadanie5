namespace Zadanie7.DTOs
{
    public class TripDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly DateFrom { get; set; }

        public DateOnly DateTo { get; set; }

        public int MaxPeople { get; set; }
        public IEnumerable<CountryDTO> Countries { get; set; }
        public IEnumerable<ClientDTO> Clients { get; set; }
    }
}
