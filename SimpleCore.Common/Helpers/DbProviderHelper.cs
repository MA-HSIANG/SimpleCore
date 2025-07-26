using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Common.Helpers
{
    public class DbProviderHelper
    {
        public static Action<DbContextOptionsBuilder> GetDbContextOptions(string connectionString)
        {
            return options =>
            {
                
                var provider = DetectDbProvider(connectionString);
              
                switch (provider)
                {
                    case DbProvider.SqlServer:
                        options.UseSqlServer(connectionString);
                        break;
                    //case DbProvider.MySql:
                    //    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                    //    break;
                    //case DbProvider.Sqlite:
                    //    options.UseSqlite(connectionString);
                    //    break;
                    //case DbProvider.PostgreSql:
                    //    options.UseNpgsql(connectionString);
                    //    break;
                    default:
                        throw new NotSupportedException("不支援的資料庫類型");
                }
            };
        }

        public enum DbProvider
        {
            Unknown,
            SqlServer,
            MySql,
            Sqlite,
            PostgreSql
        }

        private static DbProvider DetectDbProvider(string connectionString)
        {
            var lower = connectionString.ToLowerInvariant();

            if (lower.Contains("server=") && lower.Contains("database=") && lower.Contains("uid=")) return DbProvider.MySql;
            if (lower.Contains("host=") && lower.Contains("port=") && lower.Contains("username=")) return DbProvider.PostgreSql;
            if (lower.Contains("data source=") && lower.Contains(".db")) return DbProvider.Sqlite;
            if ((lower.Contains("data source=") || lower.Contains("server=")) &&
        (lower.Contains("initial catalog=") || lower.Contains("database=")))
                return DbProvider.SqlServer;

            return DbProvider.Unknown;
        }
    }
}
