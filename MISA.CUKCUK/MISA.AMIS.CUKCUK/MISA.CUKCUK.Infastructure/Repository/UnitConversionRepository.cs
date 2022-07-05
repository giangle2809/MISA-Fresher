using Dapper;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Infastructure.Repository
{
    public class UnitConversionRepository : BaseRepository<UnitConversion>, IUnitConversionRepository
    {
        public IEnumerable<UnitConversion> GetByMaterialId(Guid materialId)
        {
            using(var connection = new MySqlConnection(_connectionString))
            {
                var procName = "Proc_GetUnitConversionByMaterialId";

                var param = new DynamicParameters();

                param.Add("@ms_MaterialId", materialId);

                var res = connection.Query<UnitConversion>(procName, param, commandType: CommandType.StoredProcedure);
                return res.ToList();
            }
        }
    }
}
