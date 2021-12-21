using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IAccountService
    {
        Task<int> RegisterUser(UserRegisterRequestModel model);
        Task<UserLoginResponseModel> ValidateUser(LoginRequestModel model);
        Task<UserRegisterRequestModel> GetRegisterForm();
    }
}
