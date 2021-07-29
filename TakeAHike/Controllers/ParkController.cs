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

        [HttpGet("{id}")]
        public IActionResult GetParkById(int id)
        {
            var park = _parksRepository.GetById(id);
            if (park == null)
            {
                return NotFound();
            }
            return Ok(park);
        }

        [HttpPost]
        public IActionResult Post(Park park)
        {
            _parksRepository.AddPark(park);
            return CreatedAtAction(nameof(GetAll), new { Id = park.Id }, park);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Park park)
        {
            if (id != park.Id)
            {
                return BadRequest();
            }
            _parksRepository.UpdatePark(park);
            return NoContent();
        }
    }
}
