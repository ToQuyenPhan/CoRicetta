using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Reports;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.ReportRepo
{
    public class ReportRepo : GenericRepo<Report>, IReportRepo
    {
        public ReportRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task CreateReport(ReportFormModel model, int userId)
        {
            var report = new Report
            {
                UserId = userId,
                RecipeId = model.RecipeId,
                Description = model.Description,
                Status = (int)ReportStatus.Waiting
            };
            await CreateAsync(report);
        }
    }
}
