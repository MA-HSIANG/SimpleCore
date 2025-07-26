using SimpleCore.Model;
using SimpleCore.Model.Dtos;
using SimpleCore.Model.ViewModel;
using SimpleCore.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Service.Interfaces
{
    public interface IAuthService : IBaseService<UserIninfoModel,UserIninfoViewModel>
    {
        Task<List<RoleMenuModuleModel>> RoleMenuModuleMap();
        Task<UserIninfoViewModel> Login(LoginDTO dto);
    }
}
