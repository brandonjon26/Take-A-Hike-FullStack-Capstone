using Microsoft.AspNetCore.Http;
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
    public class HikeController : ControllerBase
    {
        private readonly IHikeRepository _hikeRepository;

        public HikeController(IHikeRepository hikeRepository)
        {
            _hikeRepository = hikeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_hikeRepository.GetAllHikes());
        }

        [HttpPost]
        public IActionResult Post(Hike hike)
        {
            _hikeRepository.AddHike(hike);
                return CreatedAtAction(nameof(GetAll), new { Id = hike.Id }, hike);
        }
    }
}
