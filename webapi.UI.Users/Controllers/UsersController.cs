using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Infrastructure.Services.Interface;
using Newtonsoft.Json;
using webapi.core.Entities;

namespace webapi.UI.Users.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public readonly IUserService _userService;
        public UsersController(IUserService userService) => _userService = userService;
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var user = _userService.GetAll();
            if (user == null) return BadRequest();
            return CreatedAtRoute("success", new { }, user);
        }
        [HttpPost]
        public IActionResult Create(string user)
        {
            var create = JsonConvert.DeserializeAnonymousType(user, new {
                userName = string.Empty,
                password = string.Empty,
            });
            if (create == null) return BadRequest();
            var createuser = new UserEntities
            {
                userName = create.userName,
                password = create.password,
                created = DateTime.Now,
                createdby = create.userName,
                modified = DateTime.Now,
                modifiedby = create.userName
            };
            var result = _userService.CreateUser(createuser);
            
            return CreatedAtRoute("success", new { }, createuser);

        }

        [HttpGet]
        public IActionResult login(string user)
        {
            var userlogin = JsonConvert.DeserializeAnonymousType(user, new {
                userName = string.Empty,
                password = string.Empty
            });
            if (userlogin == null) return NotFound();
            var result = _userService.GeUserByUserNamePassowrdAsync(userlogin.userName, userlogin.password);

            return new ObjectResult(result);
        }

        

        
    }
}
