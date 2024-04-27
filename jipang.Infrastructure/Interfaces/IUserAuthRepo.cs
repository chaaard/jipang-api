using jipang.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jipang.Infrastructure.Interfaces
{
    public interface IUserAuthRepo
    {
        Task<Users?> GetByUsername(string username);
    }
}
