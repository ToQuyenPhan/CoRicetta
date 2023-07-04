using Microsoft.AspNetCore.Mvc;
using CoRicetta.Business.Services.StepService;
using Microsoft.AspNetCore.Authorization;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StepsController : ControllerBase
    {
        private IStepService _stepService;

        public StepsController(IStepService stepService)
        {
            _stepService = stepService;
        }
    }
}
