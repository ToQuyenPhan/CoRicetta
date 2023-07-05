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
    public class ShoppingListController : ControllerBase
    {
        private IRecipeService _recipeService;
        public ShoppingListController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("{recipeId}")]
        [SwaggerOperation(Summary = "Get shopping list by recipeId of CoRicetta")]
        public async Task<ActionResult> GetShoppingListWithRecipeById(int recipeId)
        {
            try
            {
                var ingredient = await _recipeService.getShoppingListWithId(recipeId);
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
    }
}
