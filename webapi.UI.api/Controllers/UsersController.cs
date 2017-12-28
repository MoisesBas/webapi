using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Infrastructure.Services.Interface;
using Newtonsoft.Json;
using webapi.core.Entities;
using webapi.UI.api.Helper;
using Microsoft.Extensions.Configuration;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace webapi.UI.api.Controllers
{
    //[Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {

        public readonly IUserService _userService;
        public readonly IConfiguration _config;

        public UsersController(IUserService userService, IConfiguration config)
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


        [HttpPost]
        [Route("{user}")]
        public IActionResult Create(string user)
        {
            try
            {
                var create = JsonConvert.DeserializeAnonymousType(user, new
                {
                    userName = string.Empty,
                    password = string.Empty,
                });

                if (create == null) return BadRequest();

                var token = new apiTokenBuilder()
                                          .AddSecurityKey(apiSecurityKey.Create(_config["JwtSecurityToken:key"]))
                                          .AddSubject("Example Api")
                                          .AddIssuer(_config["JwtSecurityToken:Issuer"])
                                          .AddAudience(_config["JwtSecurityToken:Audience"])
                                          .AddClaim("MembershipId", "11251981")
                                          .AddExpiry(1)
                                          .Build();

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
                return Ok(new
                {
                    token = token,                   
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult login(string user, string password)
        {
            try
            {

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password)) return NotFound();
                var result = _userService.GeUserByUserNamePassowrdAsync(user, password).Result;
                if (result != null)
                {

                }
                return Unauthorized();


            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
            }
        }


    }
}
