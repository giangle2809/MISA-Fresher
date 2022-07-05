using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Phòng ban
    /// </summary>
    /// Created By: LNTGiang(09/03/2022)
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
    }
}
