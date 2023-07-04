using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Reports;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.ReportRepo
{
    public interface IReportRepo
    {
        Task CreateReport(ReportFormModel model, int userId);
        Task<PagingResultViewModel<ViewReport>> GetAllReports(PagingRequestViewModel request);
        Task ApproveReport(ReportRequestFormModel model);
        Task RejectReport(ReportRequestFormModel model);
        Task<ViewReport> FindReport(ReportRequestFormModel model);
    }
}
