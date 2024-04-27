using jipang.Application.DTOs.IN;
using jipang.Application.DTOs.OUT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jipang.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDtoOut> CreateUser(UserDtoIn userDto);
        Task<UserDtoOut> GetUserById(int id);
        Task<IEnumerable<UserDtoOut>> GetAllUsers();
        Task<bool> UpdateUser(UserDtoIn userDto);
        Task<bool> DeleteUser(int id);
    }
}
