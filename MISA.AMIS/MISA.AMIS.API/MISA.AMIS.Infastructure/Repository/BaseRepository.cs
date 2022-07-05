using Dapper;
using MISA.AMIS.Core.Common;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infastructure.Repository
{
    public class BaseRepository<MISAEntity>:IBaseRepository<MISAEntity> where MISAEntity : class
    {
        // Chuỗi kết nối
        protected string _connectionString = Config.GetConnectionString();

        public int Delete(string entityIds)
        {
            var tableName = typeof(MISAEntity).Name;
            var procName = $"Proc_Delete{tableName}s";
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                // Khai báo các giá trị truyền vào proc
                DynamicParameters param = new DynamicParameters();
                param.Add($"@ms_{tableName}Ids", entityIds);

                // Xóa dữ liệu:
                var result = sqlConnection.Execute(sql: procName, param, commandType: CommandType.StoredProcedure);

                // Trả kết quả:
                return result;
            }
        }

        public IEnumerable<MISAEntity> Get()
        {
            var tableName = typeof(MISAEntity).Name;
            var procName = $"Proc_Get{tableName}s";
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
            var tableName = typeof(MISAEntity).Name;
            var procName = $"Proc_Get{tableName}ById";
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
            var tableName = typeof(MISAEntity).Name;
            var procName = $"Proc_GetPagging{tableName}";
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {

                // Khai báo các giá trị truyền vào proc
                DynamicParameters param = new DynamicParameters();

                param.Add("@ms_PageSize", pageSize, DbType.Int32);
                param.Add("@ms_Skip", (pageNumber - 1) * pageSize, DbType.Int32);
                param.Add($"@ms_{tableName}Filter", filter == null ? "" : filter, DbType.String);
                param.Add("@ms_TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);

                // Lấy dữ liệu
                var employees = sqlConnection.Query<MISAEntity>(procName, param, commandType: CommandType.StoredProcedure);

                var result = new PaggingResult<MISAEntity>();

                result.TotalRecord = param.Get<int>("@ms_TotalRecord");
                result.TotalPage = (result.TotalRecord + pageSize - 1) / pageSize;
                result.Data = employees;

                // Trả kết quả:
                return result;
            }
        }

        public int Insert(MISAEntity entity)
        {
            var tableName = typeof(MISAEntity).Name;
            var procName = $"Proc_Insert{tableName}";

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                // Mở kết nối database
                sqlConnection.Open();

                // Đọc các tham số đầu vào từ proc
                var param = new DynamicParameters();

                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = procName;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                MySqlCommandBuilder.DeriveParameters(sqlCommand);

                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    var paramName = parameter.ParameterName;
                    var propName = paramName.Replace("@ms_", "");
                    var entityProperty = entity.GetType().GetProperty(propName);
                    if (entityProperty != null)
                    {
                        var propValue = entity.GetType().GetProperty(propName).GetValue(entity);
                        param.Add(paramName, propValue);
                    }
                    else
                    {
                        param.Add(paramName, null);
                    }
                }

                // Thêm dữ liệu
                var res = sqlConnection.Execute(procName, param, commandType: CommandType.StoredProcedure);

                // Trả dữ liệu
                return res;
            }
        }

        public int Update(Guid entityId, MISAEntity entity)
        {
            var tableName = typeof(MISAEntity).Name;
            var procName = $"Proc_Update{tableName}";

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                // Mở kết nối database
                sqlConnection.Open();

                // Đọc các tham số đầu vào từ proc
                entity.GetType().GetProperty($"{tableName}Id").SetValue(entity, entityId);

                var param = new DynamicParameters();

                var sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = procName;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                MySqlCommandBuilder.DeriveParameters(sqlCommand);

                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    var paramName = parameter.ParameterName;
                    var propName = paramName.Replace("@ms_", "");
                    var entityProperty = entity.GetType().GetProperty(propName);
                    if (entityProperty != null)
                    {
                        var propValue = entity.GetType().GetProperty(propName).GetValue(entity);
                        param.Add(paramName, propValue);
                    }
                    else
                    {
                        param.Add(paramName, null);
                    }
                }

                // Sửa dữ liệu
                var res = sqlConnection.Execute(procName, param, commandType: CommandType.StoredProcedure);

                // Trả dữ liệu
                return res;
            }
        }
    }
}
