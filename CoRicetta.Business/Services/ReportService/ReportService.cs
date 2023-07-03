using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.ReportRepo;
using System.Threading.Tasks;
using System;
using CoRicetta.Data.ViewModels.Reports;

namespace CoRicetta.Business.Services.ReportService
{
    public class ReportService : IReportService
    {
        private IReportRepo _reportRepo;
        private DecodeToken _decodeToken;

        public ReportService(IReportRepo reportRepo) { 
            _reportRepo = reportRepo; 
            _decodeToken = new DecodeToken();
        }

        public async Task CreateReport(ReportFormModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            int userId = _decodeToken.Decode(token, "Id");
            await _reportRepo.CreateReport(model, userId);
        }
    }
}
