using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Business.Services.RecipeService;
using Swashbuckle.AspNetCore.Annotations;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.RequestModel;
using Microsoft.AspNetCore.Authorization;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all recipes of CoRicetta")]
        public async Task<IActionResult> Gets([FromQuery] RecipeRequestModel model)
        {
            try
            {
                //string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var recipes = await _recipeService.getWithFilters(model.UserId, model.Name, model.CategoryId, model.Level);
                return Ok(recipes);
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

        [HttpGet("{recipeId}")]
        [SwaggerOperation(Summary = "Get recipes by recipeId of CoRicetta")]
        public async Task<ActionResult> Get(int recipeId )
        {
            try
            {
                //string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var recipe = await _recipeService.getById(recipeId);
                return Ok(recipe);
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