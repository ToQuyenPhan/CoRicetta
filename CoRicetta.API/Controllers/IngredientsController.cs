using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoRicetta.Business.Services.IngredientService;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IngredientsController : ControllerBase
    {
        private IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all active ingredients of CoRicetta")]
        public async Task<IActionResult> GetActiveIngredients()
        {
            try
            {
                var ingredients = await _ingredientService.GetActiveIngredients();
                return Ok(ingredients);
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
