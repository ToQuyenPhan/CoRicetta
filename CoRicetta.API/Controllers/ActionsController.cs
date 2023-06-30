using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Business.Services.ActionService;
using Swashbuckle.AspNetCore.Annotations;
using CoRicetta.Data.ViewModels.Actions;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private IActionService _actionService;

        public ActionsController(IActionService actionService)
        {
            _actionService = actionService;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Get all action of CoRicetta")]
        public async Task<IActionResult> GetAllAction([FromQuery] ActionRequestModel request)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var users = await _actionService.GetActions(token, request);
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

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete action by id of CoRicetta")]
        public async Task<IActionResult> DeleteAction(int actionId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                _actionService.DeleteAction(token, actionId);
                return Ok();
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
