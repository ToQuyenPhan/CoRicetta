﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoRicetta.Business.Services.MenuService;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using CoRicetta.Data.ViewModels.Menus;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create a menu in CoRicetta")]
        public async Task<ActionResult> CreateMenu([FromBody] MenuFormViewModel model)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _menuService.CreateMenu(model, token);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all menus of CoRicetta")]
        public async Task<IActionResult> GetWithFilters([FromQuery] MenuFilterRequestModel request)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var menus = await _menuService.GetWithFilters(token, request);
                return Ok(menus);
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

        [HttpGet("{menuId}")]
        [SwaggerOperation(Summary = "Get a menu by id in CoRicetta")]
        public async Task<IActionResult> GetMenuById(int menuId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var menu = await _menuService.GetMenuById(token, menuId);
                return Ok(menu);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NullReferenceException)
            {
                return Ok(new object());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
