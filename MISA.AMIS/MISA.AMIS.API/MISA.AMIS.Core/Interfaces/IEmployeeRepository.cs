using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.Core.Interfaces
{
    /// <summary>
    /// Interface liệt kê các công việc thực hiện kết nối CSDL
    /// </summary>
    /// Created By: LNTGiang(09/03/2022)
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        /// <summary>
        /// Kiểm tra mã nhân viên có bị trùng không
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <param name="EmployeeCode">Mã nhân viên</param>
        /// <returns>
        /// true - trùng
        /// false - không trùng
        /// </returns>
        /// Created By: LNTGiang(09/03/2022)
        public bool CheckEmployeeCode(Guid? employeeId, string employeeCode);
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// Mã nhân viên mới
        /// </returns>
        /// Created By: LNTGiang(12/03/2022)
        public string GetNewEmployeeCode();
    }
}
