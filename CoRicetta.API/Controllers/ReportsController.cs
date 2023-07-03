using CoRicetta.Business.Services.ReportService;
using CoRicetta.Data.ViewModels.Menus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using CoRicetta.Data.ViewModels.Reports;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create a report in CoRicetta")]
        public async Task<ActionResult> CreateMenu([FromBody] ReportFormModel model)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _reportService.CreateReport(model, token);
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
