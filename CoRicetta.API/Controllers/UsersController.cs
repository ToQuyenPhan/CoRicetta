using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoRicetta.Data.Models;
using CoRicetta.Business.Services.UserService;
using Swashbuckle.AspNetCore.Annotations;
using CoRicetta.Data.ViewModels.Users;
using System;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Log in account for CoRicetta Web")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel model)
        {
            try
            {
                return Ok(_userService.GetUsers());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
