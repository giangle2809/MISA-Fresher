using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.Core.Interfaces
{
    /// <summary>
    /// Interface liệt kê các công việc xử lý dữ liệu
    /// </summary>
    /// Created By: LNTGiang(09/03/2022)
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Xử lý logic xuất file
        /// </summary>
        /// <returns>
        /// File excel
        /// </returns>
        /// Created By: LNTGiang(14/03/2022)
        public MemoryStream Export();
    }
}
