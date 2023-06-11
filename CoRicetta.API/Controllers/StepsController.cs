using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Business.Services.StepService;

namespace CoRicetta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepsController : ControllerBase
    {
        private IStepService _stepService;

        public StepsController(IStepService stepService)
        {
            _stepService = stepService;
        }
    }
}
