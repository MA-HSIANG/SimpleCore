using SimpleCore.Model;
using SimpleCore.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Repository.Interfaces
{
    public interface IAuthRepository : IBaseRepository<UserIninfoModel>
    {
        Task<List<RoleMenuModuleModel>> RoleMenuModuleMap();
        Task<string> GetUserRoleNamesById(long UserId);
    }
}
