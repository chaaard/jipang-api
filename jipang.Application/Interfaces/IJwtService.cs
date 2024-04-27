using jipang.Application.DTOs.IN;
using jipang.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jipang.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserDtoIn user);
    }
}
