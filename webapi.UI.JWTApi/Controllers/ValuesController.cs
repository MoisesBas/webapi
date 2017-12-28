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
    /// <summary>
    /// Controller for Values 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    [Route("api/values")]
    public class ValuesController : Controller
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
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="config">The configuration.</param>
        public ValuesController(IUserService userService, IConfiguration config)
        { 
            _userService = userService;
            _config = config;

        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
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
