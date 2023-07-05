using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("signup")]
        [SwaggerOperation(Summary = "Signup for CoRicetta")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] UserRegisterViewModel model)
        {
            try
            {
                var token = await _userService.SignUpAsync(model);
                return Ok(token);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byId")]
        [SwaggerOperation(Summary = "Get user by userId of CoRicetta")]
        public async Task<ActionResult> GetUserById(int userId)
        {
            try
            {
                var users = await _userService.GetUserById(userId);
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

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create an user in CoRicetta")]
        public async Task<ActionResult> CreateUser([FromBody] UserFormViewModel model)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _userService.CreateUser(model, token);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        [SwaggerOperation(Summary = "Update an user in CoRicetta")]
        public async Task<ActionResult> UpdateUser([FromBody] UserFormViewModel model)
        {
            try
            {
                await _userService.UpdateUser(model);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete an user by id of CoRicetta")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _userService.DeleteUser(token, userId);
                return Ok("Deleted");
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
