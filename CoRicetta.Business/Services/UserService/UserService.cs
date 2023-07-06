using CoRicetta.Business.Utils;
using CoRicetta.Data.Enum;
using CoRicetta.Data.JWT;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.Repositories.UserRepo;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Users;
using System;
using System.Text.RegularExpressions;
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
                if (!model.Password.Equals(model.ConfirmPassword)) throw new ArgumentException("Confirm Password and Password does not match!");
                var isExisted = await _userRepo.IsExistedEmail(model.Email);
                if (isExisted)
                {
                    throw new ArgumentException("Email already exists, please sign in instead.");
                }
                var regex = @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$";
                var match = Regex.Match(model.PhoneNumber, regex, RegexOptions.IgnoreCase);
                if (!match.Success) throw new ArgumentException("The phone number is invalid!");
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
                var userToken = new UserTokenViewModel
                {
                    Id = newUser.Id,
                    UserName = model.Username,
                    Email = model.Email,
                    Role= newUser.Role,
                    Status = UserStatus.Active
                };
                return JWTUserToken.GenerateJWTTokenUser(userToken);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ViewUser> GetUserById(int userId)
        {
            try
            {
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

        public async Task CreateUser(UserFormViewModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền thực hiện hành động này!");
            }
            var isExisted = await _userRepo.IsExistedEmail(model.Email);
            if (isExisted) throw new ArgumentException("Email đã tồn tại!");
            await _userRepo.CreateUser(model);
        }

        public async Task UpdateUser(UserFormViewModel model)
        {
            var user = await _userRepo.GetUserById((int)model.UserId);
            if (user == null) throw new NullReferenceException("Not found any users!");
            await _userRepo.UpdateUser(model);
        }

        public async Task DeleteUser(string token, int userId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            await _userRepo.DeleteUser(userId);
        }
    }
}

