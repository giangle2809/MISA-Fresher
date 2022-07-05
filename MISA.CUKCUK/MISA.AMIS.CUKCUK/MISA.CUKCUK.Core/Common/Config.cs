using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MISA.CUKCUK.Core.Common
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
        public static Guid Stock = Guid.Parse("6f45e010-3200-5943-27a8-6d513e380bde");
        public static Guid Unit = Guid.Parse("26fe1532-7e63-6a11-1a8f-6354f958f4c0");
    }
}
