using Dapper;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Infastructure.Repository
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        public int CheckMaterialCode(Guid? materialId, string materialCode)
        {
            using(var connection = new MySqlConnection(_connectionString))
            {
                string procName = "Proc_CheckMaterialCode";

                DynamicParameters param = new DynamicParameters();

                param.Add("@ms_MaterialId", materialId != null ? materialId : "");
                param.Add("@ms_MaterialCode", materialCode);

                int res = connection.QueryFirstOrDefault<int>(procName, param, commandType: CommandType.StoredProcedure);
                return res;
            }
        }

        public string GetNewMaterialCode()
        {
            using(var connection = new MySqlConnection(_connectionString))
            {
                string procName = "Proc_GetNewMaterialCode";

                string res = connection.QueryFirstOrDefault<string>(procName, commandType: CommandType.StoredProcedure);
                return res;
            }
        }

        public int Insert(string material, string unitConversions)
        {
            using(var connection = new MySqlConnection(_connectionString))
            {
                string procName = "Proc_MaterialInsert";

                DynamicParameters param = new DynamicParameters();

                param.Add("@ms_Material", material);
                param.Add("@ms_UnitConversion", unitConversions);

                int res = connection.Execute(procName, param, commandType: CommandType.StoredProcedure);

                return res;
            }
        }

        public int Update(Guid materialId, string material, string unitConversions)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string procName = "Proc_MaterialUpdate";

                DynamicParameters param = new DynamicParameters();

                param.Add("@ms_MaterialId", materialId);
                param.Add("@ms_Material", material);
                param.Add("@ms_UnitConversion", unitConversions);

                int res = connection.Execute(procName, param, commandType: CommandType.StoredProcedure);

                return res;
            }
        }
    }
}
