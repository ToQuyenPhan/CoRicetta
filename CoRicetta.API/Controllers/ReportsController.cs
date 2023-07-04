using CoRicetta.Business.Services.ReportService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using CoRicetta.Data.ViewModels.Reports;
using System.Collections.Generic;
using CoRicetta.Data.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create a report in CoRicetta")]
        public async Task<ActionResult> CreateReport([FromBody] ReportFormModel model)
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

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all reports of CoRicetta")]
        public async Task<IActionResult> GetAllReports([FromQuery] PagingRequestViewModel request)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var reports = await _reportService.GetReports(token, request);
                return Ok(reports);
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

        [HttpPut("approve")]
        [SwaggerOperation(Summary = "Approve a report in CoRicetta")]
        public async Task<ActionResult> ApproveReport([FromBody] ReportRequestFormModel model)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _reportService.ApproveReport(model, token);
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

        [HttpPut("reject")]
        [SwaggerOperation(Summary = "Reject a report in CoRicetta")]
        public async Task<ActionResult> RejectReport([FromBody] ReportRequestFormModel model)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _reportService.RejectReport(model, token);
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

        [HttpGet("find")]
        [SwaggerOperation(Summary = "Get a report by userId & recipeId in CoRicetta")]
        public async Task<IActionResult> FindReport([FromQuery] ReportRequestFormModel model)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var report = await _reportService.FindReport(token, model);
                return Ok(report);
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
