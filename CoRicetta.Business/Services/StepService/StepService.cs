using CoRicetta.Data.Repositories.StepRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.StepService
{
    public class StepService : IStepService
    {
        private IStepRepo _stepRepo;

        public StepService(IStepRepo stepRepo)
        {
            _stepRepo = stepRepo;
        }
    }
}
