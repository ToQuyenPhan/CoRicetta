using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoRicetta.Data.Models;
using CoRicetta.Business.Services.UserService;
using Swashbuckle.AspNetCore.Annotations;
using CoRicetta.Data.ViewModels.Users;
using System;
using Microsoft.AspNetCore.Authorization;
using CoRicetta.Data.ViewModels.Paging;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Log in for CoRicetta")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel model)
        {
            try
            {
                var token = await _userService.Login(model);
                return Ok(token);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all users of CoRicetta")]
        public async Task<IActionResult> GetAllUsers([FromQuery]PagingRequestViewModel request)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var users = await _userService.GetUsers(token, request);
                return Ok(users);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NullReferenceException)
            {
                return Ok(new List<object>());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
