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
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
     
    }
}
