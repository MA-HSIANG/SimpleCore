using Microsoft.EntityFrameworkCore;
using SimpleCore.Common.DB.DbContexts;
using SimpleCore.Model;
using SimpleCore.Repository.Base;
using SimpleCore.Repository.Interfaces;

namespace SimpleCore.Repository
{
    public class AuthRepository : BaseRepository<UserIninfoModel>, IAuthRepository
    {
        protected readonly DbContext _context;
        private readonly DefaultDbContext _db;

        public AuthRepository(DbContext context, DefaultDbContext db) : base(context)
        {
            _context = context;
            _db = db;

        }

        public async Task<List<RoleMenuModuleModel>> RoleMenuModuleMap()
        {

            var permission = from rmm in _db.RoleMenuModuleEntity
                             join md in _db.ModuleEntity on rmm.MenuId equals md.Id into ModuleGroup
                             from md in ModuleGroup.DefaultIfEmpty()
                             join r in _db.RoleEntity on rmm.RoleId equals r.Id into RoleGroup
                             from r in RoleGroup.DefaultIfEmpty()
                             where rmm.Status && md.Status && r.Status
                             select new RoleMenuModuleModel
                             {
                                 Role = r,
                                 Module = md,
                                 Status = rmm.Status,
                             };
            return await permission.ToListAsync();
        }
        public async Task<string> GetUserRoleNamesById(long UserId)
        {
            string roleNames = string.Empty;
            var userLists =  _db.UserIninfoEntity;
            var user = await userLists.FirstOrDefaultAsync(x => x.Id == UserId);
            var roles = await _db.RoleEntity.Where(x => x.Status).ToListAsync();
            if (user != null)
            {
                var userRoleList = _db.UserRoleEntity;
                var userRoles = await userRoleList.Where(x => x.UserId == user.Id).ToListAsync();
                if (userRoles.Count > 0)
                {
                    var roleList = userRoles.Select(s => s.RoleId).ToList();
                    var theRoles = roles.Where(x => x != null && roleList.Contains(x.Id)).Select(s => s.Name).ToList();
                    roleNames = string.Join(",", theRoles.Select(t => t));
                }
            }
            return roleNames;
        }
    }
}
