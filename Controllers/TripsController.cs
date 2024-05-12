using Microsoft.AspNetCore.Mvc;
using Zadanie7.DTOs;
using Zadanie7.Interfaces;

namespace Zadanie7.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private readonly ITripRepository _repository;
        public TripsController(ITripRepository repository) 
        { 
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var result = await _repository.GetTripsAsync();
            return Ok(result);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AddClientToTrip([FromRoute] int idTrip, [FromBody] AddClientToTripDTO dto)
        {

            return Ok(dto);
        }
    }
}
