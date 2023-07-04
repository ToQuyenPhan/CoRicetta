using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Reports;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PagingResultViewModel<ViewReport>> GetAllReports(PagingRequestViewModel request)
        {
            var query = from rp in context.Reports
                        join u in context.Users on rp.UserId equals u.Id
                        join rc in context.Recipes on rp.RecipeId equals rc.Id
                        where rp.Status.Equals((int)ReportStatus.Waiting)
                        select new { rp, u, rc };
            int totalCount = query.Count();
            List<ViewReport> items = await query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                                         .Select(selector => new ViewReport()
                                         {
                                             UserId = selector.rp.UserId,
                                             UserName = selector.u.UserName,
                                             RecipeId = selector.rp.RecipeId,
                                             RecipeName = selector.rc.RecipeName,
                                             Description = selector.rp.Description,
                                             Status = ((ReportStatus)selector.rp.Status).ToString(),
                                         }).ToListAsync();
            return (items.Count() > 0) ? new PagingResultViewModel<ViewReport>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }

        public async Task ApproveReport(ReportRequestFormModel model)
        {
            var report = await GetReport(model);
            report.Status = (int)ReportStatus.Approved;
            await UpdateAsync(report);
        }

        public async Task RejectReport(ReportRequestFormModel model)
        {
            var report = await GetReport(model);
            report.Status = (int)ReportStatus.Rejected;
            await UpdateAsync(report);
        }

        public async Task<ViewReport> FindReport(ReportRequestFormModel model)
        {
            var query = from rp in context.Reports
                        join u in context.Users on rp.UserId equals u.Id
                        join rc in context.Recipes on rp.RecipeId equals rc.Id
                        where rp.UserId.Equals(model.UserId) && rp.RecipeId.Equals(model.RecipeId)
                        select new { rp, u, rc };
            ViewReport item = await query.Select(selector => new ViewReport()
                                         {
                                             UserId = selector.rp.UserId,
                                             UserName = selector.u.UserName,
                                             RecipeId = selector.rp.RecipeId,
                                             RecipeName = selector.rc.RecipeName,
                                             Description = selector.rp.Description,
                                             Status = ((ReportStatus)selector.rp.Status).ToString(),
                                         }).FirstOrDefaultAsync();
            return (item != null) ? item : null;
        }

        private async Task<Report> GetReport(ReportRequestFormModel model)
        {
            var query = from r in context.Reports
                        where r.UserId.Equals(model.UserId) && r.RecipeId.Equals(model.RecipeId)
                        select r;
            return await query.Select(selector => new Report()
            {
                UserId = selector.UserId,
                RecipeId = selector.RecipeId,
                Description = selector.Description,
                Status = selector.Status
            }).FirstOrDefaultAsync();
        }
    }
}
