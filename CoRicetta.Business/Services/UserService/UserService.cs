using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.Repositories.UserRepo;
using CoRicetta.Data.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.UserService
{
    public class UserService : IUserService
    {
        private IGenericRepo<User> _genericRepo;
        private IUserRepo _userRepo; 
        public UserService(IGenericRepo<User> genericRepo, IUserRepo userRepo) {
            _genericRepo = genericRepo;
            _userRepo = userRepo;
        }
        public IEnumerable<User> GetUsers()
        {
            return _genericRepo.GetAll();
        }

        public Task<string> Login(UserLoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                throw new ArgumentException("Email Null || Password Nulll");
            try
            {
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
