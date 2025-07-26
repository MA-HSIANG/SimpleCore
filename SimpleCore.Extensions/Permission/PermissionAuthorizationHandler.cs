using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Serilog;
using SimpleCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleCore.Extensions.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>, IAuthorizationRequirement
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IAuthService _authService;
        public IAuthenticationSchemeProvider Schemes { get; set; }

        public PermissionAuthorizationHandler(IHttpContextAccessor accessor, IAuthService authService, IAuthenticationSchemeProvider schemes)
        {
            _accessor = accessor;
            _authService = authService;
            Schemes = schemes;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var httpContext = _accessor.HttpContext;

            if (requirement.Permissions.Count <= 0)
            {
                var data = await _authService.RoleMenuModuleMap();
                var list = new List<PermissionItem>();

                list = data
                    .OrderBy(x => x.Id)
                    .Where(x => x.Status)
                    .Select(item => new PermissionItem
                    {
                        Url = item.Module?.LinkUrl,
                        RoleName = item.Role?.Name
                    })
                    .ToList();
                requirement.Permissions = list;
            }

            if (httpContext != null)
            {
                var questUrl = httpContext.Request.Path.Value;
                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null) 
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    if (result?.Principal != null)
                    {
                        //令牌時效驗證
                        bool isExp = false;

                        var expClaim = httpContext.User.Claims.FirstOrDefault(s => s.Type == "exp")?.Value;
                        var currentTime = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;


                        if (!string.IsNullOrEmpty(expClaim))
                        {
                            isExp = long.Parse(expClaim) >= currentTime;
                        }

                        if (isExp)
                        {
                            context.Fail(new AuthorizationFailureReason(this, "授權過期，請重新登入"));
                            return;
                        }
                        //獲取當前使用者訊息
                        var currentUserRoles = new List<string>();
                        currentUserRoles = (from item in httpContext.User.Claims
                                            where item.Type == ClaimTypes.Role
                                            select item.Value).ToList();
                        //其他驗證
                        if (currentUserRoles.Count > 0)
                        {
                            currentUserRoles = (from item in httpContext.User.Claims
                                                where item.Type.Contains("role")
                                                select item.Value).ToList();
                        }


                        if (currentUserRoles.All(r => r != "系統管理員"))
                        {
                            bool isMatchRole = false;
                            var permissionRole = requirement.Permissions.Where(x => currentUserRoles.Contains(x.RoleName));


                            foreach (var permission in permissionRole)
                            {
                                if (Regex.Match(questUrl, permission.Url)?.Value == questUrl)
                                {
                                    isMatchRole = true;
                                    break;
                                }
                            }
                            //驗證失敗
                            if (currentUserRoles.Count <= 0 || !isMatchRole)
                            {
                                context.Fail();
                                return;
                            }


                        }
                        context.Succeed(requirement);
                        return;
                    }
                }
                }
            }
        }
}
