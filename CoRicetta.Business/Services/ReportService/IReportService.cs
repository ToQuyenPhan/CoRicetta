using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Reports;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.ReportService
{
    public interface IReportService
    {
        Task CreateReport(ReportFormModel model, string token);
        Task<PagingResultViewModel<ViewReport>> GetReports(string token, PagingRequestViewModel request);
        Task ApproveReport(ReportRequestFormModel model, string token);
        Task RejectReport(ReportRequestFormModel model, string token);
        Task<ViewReport> FindReport(string token, ReportRequestFormModel model);
    }
}
