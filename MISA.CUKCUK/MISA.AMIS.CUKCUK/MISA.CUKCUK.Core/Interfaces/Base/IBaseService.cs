using MISA.CUKCUK.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Base
{
    /// <summary>
    /// Interface liệt kê các công việc thực hiện kết nối CSDL
    /// </summary>
    /// Created By: LNTGiang(11/03/2022)
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Xử lý logic xóa nhiều nhân viên
        /// </summary>
        /// <param name="entityIds">Danh sách id đối tượng cần xóa</param>
        /// <returns>
        /// Số record bị xóa
        /// </returns>
        /// Created By: LNTGiang(12/03/2022)
        public int Delete(List<string> entityIds);
        /// <summary>
        /// Xử lý logic Filter
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Trang thứ mấy</param>
        /// <param name="filter">Bộ lọc</param>
        /// <returns></returns>
        /// Created By: LNTGiang(07/04/2022)
        public PaggingResult<MISAEntity> GetPaging(int pageSize, int pageNumber, List<Filter> filter);
    }
}
