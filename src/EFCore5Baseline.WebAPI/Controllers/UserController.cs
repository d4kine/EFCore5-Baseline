using System;
using System.Net.Mime;
using EFCore5Baseline.Common.Models;
using EFCore5Baseline.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EFCore5Baseline.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IGenericRepository _repo;

        public UserController(ILogger<UserController> logger, IGenericRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult AddUser([FromBody] User user)
        {
            _repo.Create(user);
            return Created("", user.ToString());
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var userList = _repo.GetAll<User>();
            return Ok(userList);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser([FromQuery] Guid id)
        {
            var userList = _repo.GetOne<User>(u => u.Id.Equals(id));
            return Ok(userList);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult UpdateUser([FromBody] User user)
        {
            _repo.Update(user);
            return Ok("User updated");
        }


        [HttpDelete]
        public IActionResult DeleteUser([FromQuery] Guid id)
        {
            _repo.Delete<User>(id);
            return NoContent();
        }
    }
}