using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using CoRicetta.Data.ViewModels.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.UserRepo
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(CoRicettaDBContext context) : base(context) {
        }

        public async Task<UserTokenViewModel> GetByEmailAndPassword(string email, string password)
        {
            var query = from u in context.Users where u.Email.Equals(email) && u.Password.Equals(password) select u;
            return await query.Select(selector => new UserTokenViewModel()
            {
                Id = selector.Id,
                UserName = selector.UserName,
                Email = selector.Email,
                Role= selector.Role,
                Status = (UserStatus)selector.Status,
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> CheckEmailAndPassword(string email, string password)
        {
            User user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));
            return (user != null) ? true : false;
        }

        public async Task<PagingResultViewModel<ViewUser>> GetUsers(PagingRequestViewModel request)
        {
            var query = from u in context.Users select u;
            int totalCount = query.Count();
            List<ViewUser> items = await query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                                          .Select(selector => new ViewUser()
                                          {
                                              Id = selector.Id,
                                              UserName = selector.UserName,
                                              Password = selector.Password,
                                              Email = selector.Email,
                                              PhoneNumber = (!string.IsNullOrEmpty(selector.PhoneNumber)) ? selector.PhoneNumber : "",
                                              Role = selector.Role,
                                              Status = (UserStatus)selector.Status,
                                          }
                                          ).ToListAsync();
            return (items.Count() > 0) ? new PagingResultViewModel<ViewUser>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }

        public async Task<ViewUser> GetUserById(int userId)
        {
            var query = from u in context.Users 
                        where u.Id == userId
                        select u;
            ViewUser item = await query.Select(selector => new ViewUser()
            {
                Id = selector.Id,
                UserName = selector.UserName,
                Password = selector.Password,
                Email = selector.Email,
                PhoneNumber = (!string.IsNullOrEmpty(selector.PhoneNumber)) ? selector.PhoneNumber : "",
                Role = selector.Role,
                Status = (UserStatus)selector.Status,
            }
                                          ).FirstOrDefaultAsync();
             return item;
        }
        public async Task CreateUser(UserFormViewModel model)
        {
            var _user = new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role,
                Status = (int)model.Status,
            };
            await CreateAsync(_user);
        }
        public async Task UpdateUser(UserFormViewModel model, int userId)
        {
            var _user = new User
            {
                Id = (int)model.UserId,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role,
                Status = (int)model.Status,
            };
            await UpdateAsync(_user);
        }
        public async Task DeleteUser(int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.Status = (int)UserStatus.InActive;
                await UpdateAsync(user);
            }
        }

    }
}
