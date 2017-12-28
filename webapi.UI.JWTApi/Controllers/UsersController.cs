using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using webapi.Infrastructure.Services.Interface;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using webapi.core.Entities;

namespace webapi.UI.JWTApi.Controllers
{

    /// <summary>
    /// Controller for User 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Users")]
    public class UsersController : Controller
    {
        /// <summary>
        /// The user service
        /// </summary>
        public readonly IUserService _userService;
        /// <summary>
        /// The configuration
        /// </summary>
        public readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="config">The configuration.</param>
        public UsersController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;

        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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

                if (create == null) return StatusCode((int)HttpStatusCode.BadRequest,"users name and password is required.");               

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
                return StatusCode((int)HttpStatusCode.Accepted, "successfully register the user");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while registering the user");
            }
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
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

                    if (result == null) return Unauthorized();

                    var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.UniqueName, "data"),
                new Claim(JwtRegisteredClaimNames.Sub, "data"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSecurityToken:key"])); //Secret
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["JwtSecurityToken:Issuer"],
                        _config["JwtSecurityToken:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    return Ok(new JsonWebToken()
                    {
                        access_token = new JwtSecurityTokenHandler().WriteToken(token),
                        expires_in = 600000,
                        token_type = "bearer"
                    });
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