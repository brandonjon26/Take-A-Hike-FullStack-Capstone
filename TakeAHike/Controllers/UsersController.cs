using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TakeAHike.Models;
using TakeAHike.Repositories;

namespace TakeAHike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetByFirebaseUserId(string firebaseUserId)
        {
            return Ok(_usersRepository.GetByFirebaseUserId(firebaseUserId));
        }

        [HttpGet("DoesUserExist/{firebaseUserId}")]
        public IActionResult DoesUserExist(string firebaseUserId)
        {
            var userProfile = _usersRepository.GetByFirebaseUserId(firebaseUserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Users users)
        {
            users.userTypeId = 2;
            _usersRepository.Add(users);
            return CreatedAtAction(
                nameof(GetByFirebaseUserId), new { firebaseUserId = users.FireBaseUserId }, users);
            //return CreatedAtAction("Get", new { id = users.Id }, users);
            //nameof(GetUser),
            //new { firebaseUserId = users.FireBaseUserId },
            //users);
        }

        private int GetCurrentUserProfileId()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var users= _usersRepository.GetByFirebaseUserId(firebaseUserId);
            return users.Id;
        }

        private string GetCurrentFirebaseUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return id;
        }

        [HttpGet("GetCurrentUser")]
        public IActionResult GetCurrentLoggedInUser()
        {
            var user = GetCurrentUserProfile();
            return Ok(user);
        }
        private Users GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _usersRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}