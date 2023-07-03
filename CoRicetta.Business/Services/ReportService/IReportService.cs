using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Reports;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.ReportService
{
    public interface IReportService
    {
        Task CreateReport(ReportFormModel model, string token);
        Task<PagingResultViewModel<ViewReport>> GetReports(string token, PagingRequestViewModel request);
    }
}
