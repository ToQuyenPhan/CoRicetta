using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoRicetta.Business.Services.RecipeService;
using CoRicetta.Data.ViewModels.Paging;
using Swashbuckle.AspNetCore.Annotations;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all recipes of CoRicetta")]
        public async Task<IActionResult> GetAllRecipes([FromQuery] PagingRequestViewModel request)
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
    }
}
