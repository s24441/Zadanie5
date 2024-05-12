using Microsoft.AspNetCore.Mvc;
using Zadanie7.Interfaces;

namespace Zadanie7.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : Controller
    {
        private readonly IClientRepository _repository;
        public ClientsController(IClientRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int idClient)
        {
            await _repository.DeleteClientAsync(idClient);
            return Ok();
        }
    }
}
