using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeAHike.Models;
using TakeAHike.Repositories;

namespace TakeAHike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkController : ControllerBase
    {
        private readonly IParkRepository _parksRepository;

        public ParkController(IParkRepository parkRepository)
        {
            _parksRepository = parkRepository;
        }

        // GET: api/<ParksController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_parksRepository.GetAllParks());
        }
    }
}
