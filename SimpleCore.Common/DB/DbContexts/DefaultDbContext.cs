
using Microsoft.EntityFrameworkCore;
using SimpleCore.Model;


namespace SimpleCore.Common.DB.DbContexts
{
    public class DefaultDbContext : DbContext
    {
        public DbSet<UserIninfoModel> UserIninfoEntity { get; set; }
        public DbSet<UserRoleModel> UserRoleEntity { get; set; }
        public DbSet<ModuleModel> ModuleEntity { get; set; }
        public DbSet<RoleModel> RoleEntity { get; set; }
        public DbSet<MenuModel> MenuEntity { get; set; }
        public DbSet<RoleMenuModuleModel> RoleMenuModuleEntity { get; set; }
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options) 
        { 
        }
    }
}
