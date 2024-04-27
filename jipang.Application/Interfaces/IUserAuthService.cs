using jipang.Application.DTOs.OUT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jipang.Application.Interfaces
{
    public interface IUserAuthService
    {
        Task<UserDtoOut> LoginUser(string username, string password);
        Task<UserDtoOut> LogoutUser(string username);
    }
}
