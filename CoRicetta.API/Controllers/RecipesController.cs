using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoRicetta.Business.Services.RecipeService;
using Swashbuckle.AspNetCore.Annotations;
using CoRicetta.Data.ViewModels.Recipes;
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

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all recipes of CoRicetta")]
        public async Task<IActionResult> GetAllRecipes([FromQuery] RecipeFilterRequestModel request)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var users = await _recipeService.GetRecipes(token, request);
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

        [HttpGet("{recipeId}")]
        [SwaggerOperation(Summary = "Get recipe by recipeId of CoRicetta")]
        public async Task<ActionResult> GetRecipeById(int recipeId)
        {
            try
            {
                var recipe = await _recipeService.getById(recipeId);
                return Ok(recipe);
            }
            catch (ArgumentException ex)
            {
                return Ok(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create a recipe in CoRicetta")]
        public async Task<ActionResult> CreateRecipe([FromBody]RecipeFormViewModel model)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _recipeService.CreateRecipe(model, token);
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
    }
}
