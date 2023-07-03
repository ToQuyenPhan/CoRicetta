using CoRicetta.Data.ViewModels.Reports;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.ReportRepo
{
    public interface IReportRepo
    {
        Task CreateReport(ReportFormModel model, int userId);
    }
}
