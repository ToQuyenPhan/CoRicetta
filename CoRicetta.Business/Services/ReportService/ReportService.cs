using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.ReportRepo;
using System.Threading.Tasks;
using System;
using CoRicetta.Data.ViewModels.Reports;
using CoRicetta.Data.ViewModels.Paging;

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

        public async Task<PagingResultViewModel<ViewReport>> GetReports(string token, PagingRequestViewModel request)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            PagingResultViewModel<ViewReport> reports = await _reportRepo.GetAllReports(request);
            if (reports.Items == null) throw new NullReferenceException("Not found any reports");
            foreach (var report in reports.Items)
            {
                //switch (report.Status.ToString())
                //{
                //    case "Waiting":
                //        report.Status = "Đang đợi";
                //        break;
                //    case "Approved":
                //        report.Status = "Đã xét duyệt";
                //        break;
                //    case "Rejected":
                //        report.Status = "Đã từ chối";
                //        break;
                //    default:
                //        break;
                //}
                report.Status = "Đang đợi";
            }
            return reports;
        }

        public async Task<ViewReport> FindReport(string token, ReportRequestFormModel model)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            ViewReport report = await _reportRepo.FindReport(model);
            if (report == null) throw new NullReferenceException("Not found any reports");
            switch (report.Status.ToString())
            {
                case "Waiting":
                    report.Status = "Đang đợi";
                    break;
                case "Approved":
                    report.Status = "Đã xét duyệt";
                    break;
                case "Rejected":
                    report.Status = "Đã từ chối";
                    break;
                default:
                    break;
            }
            return report;
        }

        public async Task ApproveReport(ReportRequestFormModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            await _reportRepo.ApproveReport(model);
        }

        public async Task RejectReport(ReportRequestFormModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            await _reportRepo.RejectReport(model);
        }
    }
}
