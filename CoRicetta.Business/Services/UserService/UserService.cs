using CoRicetta.Business.Utils;
using CoRicetta.Data.Enum;
using CoRicetta.Data.JWT;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.Repositories.UserRepo;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Users;
using System;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.UserService
{
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;
        private DecodeToken _decodeToken;
        private IGenericRepo<User> _genericRepo;

        public UserService(IGenericRepo<User> genericRepo, IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _genericRepo = genericRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task<string> Login(UserLoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                throw new ArgumentException("Email Null || Password Nulll");
            try
            {
                bool check = await _userRepo.CheckEmailAndPassword(model.Email, model.Password);
                if (check)
                {
                    UserTokenViewModel user = await _userRepo.GetByEmailAndPassword(model.Email, model.Password);
                    if (user.Status == UserStatus.InActive)
                    {
                        throw new ArgumentException("Your account is inactive!");
                    }
                    else
                    {
                        return JWTUserToken.GenerateJWTTokenUser(user);
                    }
                }
                else
                {
                    throw new ArgumentException("Incorrect email or password!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagingResultViewModel<ViewUser>> GetUsers(string token, PagingRequestViewModel request)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            PagingResultViewModel<ViewUser> users = await _userRepo.GetUsers(request);
            if (users.Items == null) throw new NullReferenceException("Not found any users");
            return users;
        }

        public async Task<string> SignUpAsync(UserRegisterViewModel model)
        {
            try
            {
                var user = await _genericRepo.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null)
                {
                    throw new ArgumentException("Email already exists, please sign in instead.");
                }
                var newUser = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Role = UserRole.USER.ToString(),
                    Status = (int)UserStatus.Active
                };
                await _genericRepo.CreateAsync(newUser);
                return newUser.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Something went wrong, please try again later!");
            }
        }

        public async Task<ViewUser> GetUserById(string token, int userId)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("ADMIN"))
                {
                    throw new UnauthorizedAccessException("You do not have permission to access this resource!");
                }
                var recipe = await _userRepo.GetUserById(userId);
                if (recipe == null)
                {
                    throw new ArgumentException("The user does not exist.");
                }
                return recipe;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Something went wrong, please try again later!");
            }
        }
    }
}

