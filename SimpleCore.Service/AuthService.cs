using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SimpleCore.Common.Helpers;
using SimpleCore.Model;
using SimpleCore.Model.Dtos;
using SimpleCore.Model.ViewModel;
using SimpleCore.Repository.Base;
using SimpleCore.Repository.Interfaces;
using SimpleCore.Service.Base;
using SimpleCore.Service.Interfaces;

namespace SimpleCore.Service
{
    public class AuthService : BaseService<UserIninfoModel, UserIninfoViewModel>, IAuthService
    {
        private readonly IAuthRepository _authRepository; 
        public AuthService(IMapper mapper, IBaseRepository<UserIninfoModel> repository, IAuthRepository authRepository) : base(mapper, repository)
        {
            _authRepository = authRepository;
        }

        public async Task<UserIninfoViewModel> Login(LoginDTO dto)
        {
            var result = new UserIninfoViewModel();
            var users = await base.GetAllAsync();
            var user = users.FirstOrDefault(x => x.LoginName == dto.LoginName && x.Password == dto.Password);

            if (user !=null)
            {
                string userRoleNames = await _authRepository.GetUserRoleNamesById(user.Id);
                var claims = JwtHelper.CreateJwtTokenClaims(user.Name, user.Id.ToString(), userRoleNames);
                string token = JwtHelper.CreateJwToken(claims.ToArray());

                if (!string.IsNullOrEmpty(token))
                {
                    result.Id = user.Id;
                    result.LoginName = user.LoginName;
                    result.Name = user.Name;
                    result.AccessToken = token;
                }
                else
                {
                    throw new ApplicationException("Token創建失敗");
                }
               
            }

            return result;

        }

        public async Task<List<RoleMenuModuleModel>> RoleMenuModuleMap()
        {
            return await _authRepository.RoleMenuModuleMap();
        }
    }
}
