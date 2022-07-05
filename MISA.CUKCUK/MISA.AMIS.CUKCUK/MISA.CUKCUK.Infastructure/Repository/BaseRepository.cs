using Dapper;
using MISA.CUKCUK.Core.Common;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Infastructure.Repository
{
    public class BaseRepository<MISAEntity>:IBaseRepository<MISAEntity> where MISAEntity : class
    {
        // Chuỗi kết nối
        protected string _connectionString = Config.GetConnectionString();

        public int Delete(string entityIds)
        {
            string tableName = typeof(MISAEntity).Name;
            string procName = $"Proc_Delete{tableName}s";
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                // Khai báo các giá trị truyền vào proc
                DynamicParameters param = new DynamicParameters();
                param.Add($"@ms_{tableName}Ids", entityIds);

                // Xóa dữ liệu:
                int result = sqlConnection.Execute(sql: procName, param, commandType: CommandType.StoredProcedure);

                // Trả kết quả:
                return result;
            }
        }

        public IEnumerable<MISAEntity> Get()
        {
            string tableName = typeof(MISAEntity).Name;
            string procName = $"Proc_Get{tableName}s";
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {

                // Lấy dữ liệu
                var result = sqlConnection.Query<MISAEntity>(sql: procName, commandType: CommandType.StoredProcedure);

                // Trả kết quả:
                return result;
            }
        }
        public MISAEntity GetById(Guid entityId)
        {
            string tableName = typeof(MISAEntity).Name;
            string procName = $"Proc_{tableName}GetById";
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                // Khai báo các giá trị truyền vào proc
                DynamicParameters param = new DynamicParameters();
                param.Add($"@ms_{tableName}Id", entityId);

                // Lấy dữ liệu
                var result = sqlConnection.QueryFirstOrDefault<MISAEntity>(sql: procName, param, commandType: CommandType.StoredProcedure);

                // Trả kết quả:
                return result;
            }
        }

        public PaggingResult<MISAEntity> GetPaging(string filter, int pageSize, int pageNumber)
        {
            string tableName = typeof(MISAEntity).Name;
            string procName = $"Proc_{tableName}GetPagging";
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {

                // Khai báo các giá trị truyền vào proc
                DynamicParameters param = new DynamicParameters();

                param.Add("@ms_PageSize", pageSize, DbType.Int32);
                param.Add("@ms_Skip", (pageNumber - 1) * pageSize, DbType.Int32);
                param.Add($"@ms_{tableName}Filter", filter == null ? "" : filter, DbType.String);
                param.Add("@ms_TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);

                // Lấy dữ liệu
                var res = sqlConnection.Query<MISAEntity>(procName, param, commandType: CommandType.StoredProcedure);

                var result = new PaggingResult<MISAEntity>();

                result.TotalRecord = param.Get<int>("@ms_TotalRecord");
                result.TotalPage = (result.TotalRecord + pageSize - 1) / pageSize;
                result.Data = res;

                // Trả kết quả:
                return result;
            }
        }
    }
}
