using CivilRegistryService.Models;
using CivilRegistryService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CivilRegistryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CivilRegistryController : ControllerBase
    {
        private readonly MainService _mainService;
        public CivilRegistryController(MainService mainService)
        {
            _mainService = mainService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetCivilRegistry(string id)
        {
            var person = await _mainService.GetPersonAsync(id.ToString());
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }



    }
}
