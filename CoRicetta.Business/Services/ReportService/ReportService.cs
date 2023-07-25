using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.ReportRepo;
using System.Threading.Tasks;
using System;
using CoRicetta.Data.ViewModels.Reports;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.Repositories.ActionRepo;
using CoRicetta.Data.Repositories.CategoryDetailRepo;
using CoRicetta.Data.Repositories.MenuDetailRepo;
using CoRicetta.Data.Repositories.RecipeDetailRepo;
using CoRicetta.Data.Repositories.RecipeRepo;
using CoRicetta.Data.Repositories.StepRepo;
using CoRicetta.Data.Models;

namespace CoRicetta.Business.Services.ReportService
{
    public class ReportService : IReportService
    {
        private IReportRepo _reportRepo;
        private IRecipeRepo _recipeRepo;
        private IRecipeDetailRepo _recipeDetailRepo;
        private IStepRepo _stepRepo;
        private ICategoryDetailRepo _categoryDetailRepo;
        private IActionRepo _actionRepo;
        private IMenuDetailRepo _menuDetailRepo;
        private DecodeToken _decodeToken;

        public ReportService(IRecipeRepo recipeRepo, IRecipeDetailRepo recipeDetailRepo, IStepRepo stepRepo,
            ICategoryDetailRepo categoryDetailRepo, IActionRepo actionRepo, IReportRepo reportRepo,
            IMenuDetailRepo menuDetailRepo) {
            _recipeRepo = recipeRepo;
            _recipeDetailRepo = recipeDetailRepo;
            _stepRepo = stepRepo;
            _categoryDetailRepo = categoryDetailRepo;
            _actionRepo = actionRepo;
            _reportRepo = reportRepo;
            _menuDetailRepo = menuDetailRepo;
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
            switch (report.Status)
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
            var recipe = await _recipeRepo.GetRecipeById(model.RecipeId);
            if (recipe == null) throw new NullReferenceException("Not found any reicpes!");
            await _categoryDetailRepo.DeleteCategoryDetailByRecipeId(model.RecipeId);
            await _recipeDetailRepo.DeleteRecipeDetailByRecipeId(model.RecipeId);
            await _stepRepo.DeleteStepsByRecipeId(model.RecipeId);
            await _actionRepo.DeleteActionsByRecipeId(model.RecipeId);
            await _menuDetailRepo.DeleteMenuDetailsByRecipeId(model.RecipeId);
            await _reportRepo.DeleteReportsByRecipeId(model.RecipeId);
            await _recipeRepo.DeleteRecipe(model.RecipeId);
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
