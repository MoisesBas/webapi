using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Infrastructure.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace webapi.UI.JWTApi.Controllers
{
    [Authorize]
    [Route("api/values")]
    public class ValuesController : Controller
    {
        public readonly IUserService _userService;
        public readonly IConfiguration _config;

        public ValuesController(IUserService userService, IConfiguration config)
        { 
            _userService = userService;
            _config = config;

        }
      
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            if (users == null) return NotFound();
            return Ok(new
            {
                users = users
            });
        }
    }
}
