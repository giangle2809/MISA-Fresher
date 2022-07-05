using Dapper;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.AMIS.Infastructure.Repository
{
    /// <summary>
    /// Thực hiện làm việc với dữ liệu nhân viên
    /// </summary>
    /// Created By: LNTGiang(09/03/2022)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public bool CheckEmployeeCode(Guid? employeeId, string employeeCode)
        {
            int result = 0;
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                // Khai báo thông tin kết nối:
                string procName = "Proc_CheckEmployeeCode";

                // Khai báo các giá trị truyền vào proc
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ms_EmployeeId", employeeId == null ? "" : employeeId);
                parameters.Add("@ms_EmployeeCode", employeeCode);

                // Lấy dữ liệu:
                result = sqlConnection.QueryFirstOrDefault<int>(sql: procName, parameters, commandType: CommandType.StoredProcedure);
            }
            // Trả kết quả:
            return result > 0;
        }

        public string GetNewEmployeeCode()
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                // Khai báo thông tin kết nối:
                string procName = "Proc_NewEmployeeCode";

                // Lấy dữ liệu:
                var result = sqlConnection.QueryFirstOrDefault<string>(sql: procName, commandType: CommandType.StoredProcedure);

                // Trả kết quả:
                return result; 
            }
        }
    }
}
