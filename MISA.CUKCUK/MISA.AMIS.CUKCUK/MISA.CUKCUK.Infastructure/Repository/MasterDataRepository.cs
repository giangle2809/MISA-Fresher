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
    public class MasterDataRepository : BaseRepository<MasterData>, IMasterDataRepository
    {
        public IEnumerable<MasterData> GetByGroupId(Guid groupId)
        {
            using(var connection = new MySqlConnection(_connectionString))
            {
                var procName = "Proc_MasterDataGetByGroupId";

                var param = new DynamicParameters();

                param.Add("@ms_GroupId", groupId);

                var res = connection.Query<MasterData>(procName, param, commandType: CommandType.StoredProcedure);
                return res;
            }
        }
    }
}
