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
                var recipes = await _recipeService.GetRecipes(token, request);
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

        [HttpPut("update")]
        [SwaggerOperation(Summary = "Update a recipe in CoRicetta")]
        public async Task<ActionResult> UpdateRecipe([FromBody] RecipeFormViewModel model, [FromQuery] int recipeId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _recipeService.UpdateRecipe(model, token, recipeId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        [SwaggerOperation(Summary = "Delete a recipe in CoRicetta")]
        public async Task<ActionResult> DeleteRecipe([FromQuery] int recipeId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _recipeService.DeleteRecipe(token, recipeId);
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

        [HttpGet("shopping/{recipeId}")]
        [SwaggerOperation(Summary = "Get shopping list by recipeId in CoRicetta")]
        public async Task<ActionResult> GetShoppingListWithRecipeById(int recipeId)
        {
            try
            {
                var ingredient = await _recipeService.GetShoppingListWithId(recipeId);
                return Ok(ingredient);
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

        [HttpGet("shared")]
        [SwaggerOperation(Summary = "Get shared recipes of CoRicetta")]
        public async Task<IActionResult> GetSharedRecipes([FromQuery] RecipeFilterRequestModel request)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var recipes = await _recipeService.GetSharedRecipes(token, request);
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
    }
}
