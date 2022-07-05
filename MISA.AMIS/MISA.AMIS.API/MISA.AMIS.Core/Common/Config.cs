using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MISA.AMIS.Core.Common
{
    /// <summary>
    /// Class Config
    /// </summary>
    /// Created By: LNTGiang(23/03/2022)
    public class Config
    {
        /// <summary>
        /// Lấy connection string trong appsettings.json
        /// </summary>
        /// <returns>connection string</returns>
        /// Created By: LNTGiang(23/02/2022)
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}
