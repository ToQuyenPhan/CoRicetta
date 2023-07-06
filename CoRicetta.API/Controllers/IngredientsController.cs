using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoRicetta.Business.Services.IngredientService;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using CoRicetta.Data.ViewModels.Ingredients;

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

        [HttpPut("update")]
        [SwaggerOperation(Summary = "Update an ingredient in CoRicetta")]
        public async Task<ActionResult> UpdateIngredient([FromBody] IngredientFormModel model,
                [FromQuery] int ingredientId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _ingredientService.UpdateIngredient(model, token, ingredientId);
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

        [HttpGet("byId")]
        [SwaggerOperation(Summary = "Get an ingredient by Id in CoRicetta")]
        public async Task<IActionResult> GetIngredientById([FromQuery] int ingredientId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var ingredient = await _ingredientService.GetIngredientById(token, ingredientId);
                return Ok(ingredient);
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

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create an ingredient in CoRicetta")]
        public async Task<ActionResult> CreateIngredient([FromBody] IngredientFormModel model)
        {
            try
            {
                await _ingredientService.CreateIngredient(model);
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

        [HttpDelete("delete")]
        [SwaggerOperation(Summary = "Update an ingredient in CoRicetta")]
        public async Task<ActionResult> UpdateIngredient([FromQuery] int ingredientId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _ingredientService.DeleteIngredient(ingredientId, token);
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

        [HttpGet("request")]
        [SwaggerOperation(Summary = "Get all inactive ingredients of CoRicetta")]
        public async Task<IActionResult> GetInactiveIngredients()
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var ingredients = await _ingredientService.GetInactiveIngredients(token);
                return Ok(ingredients);
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
