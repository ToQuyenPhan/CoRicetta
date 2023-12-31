﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoRicetta.Business.Services.ActionService;
using Swashbuckle.AspNetCore.Annotations;
using CoRicetta.Data.ViewModels.Actions;
using Microsoft.AspNetCore.Authorization;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionsController : ControllerBase
    {
        private IActionService _actionService;

        public ActionsController(IActionService actionService)
        {
            _actionService = actionService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all actions of CoRicetta")]
        public async Task<IActionResult> GetAllAction([FromQuery] ActionRequestModel request)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var actions = await _actionService.GetActions(token, request);
                return Ok(actions);
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
        [SwaggerOperation(Summary = "Delete an action by id of CoRicetta")]
        public async Task<IActionResult> DeleteAction(int actionId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                _actionService.DeleteAction(token, actionId);
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

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create a comment in CoRicetta")]
        public async Task<IActionResult> CreateComment([FromBody] ActionFormModel model)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _actionService.CreateAction(model, token);
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

        [HttpGet("getAction")]
        [SwaggerOperation(Summary = "Get an action in CoRicetta")]
        public async Task<IActionResult> GetAction([FromQuery] ActionRequestModel request)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var action = await _actionService.GetAction(token, request);
                return Ok(action);
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

        [HttpPut("update")]
        [SwaggerOperation(Summary = "Update a comment in CoRicetta")]
        public async Task<IActionResult> UpdateComment([FromBody] ActionFormModel model, [FromQuery] int actionId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _actionService.UpdateComment(model, token, actionId);
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
    }
}
