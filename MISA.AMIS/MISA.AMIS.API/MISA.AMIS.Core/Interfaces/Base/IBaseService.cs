using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Base
{
    /// <summary>
    /// Interface liệt kê các công việc thực hiện kết nối CSDL
    /// </summary>
    /// Created By: LNTGiang(11/03/2022)
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Xử lý logic khi thêm
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>
        /// Số record đã thay đổi
        /// </returns>
        public int Insert(MISAEntity entity);
        /// <summary>
        /// Xử lý logic khi sửa
        /// </summary>
        /// <param name="entityId">Id của đối tượng cần sửa</param>
        /// <param name="entity">Dữ liệu cần sửa</param>
        /// <returns>
        /// Số record đã thay đổi
        /// </returns>
        public int Update(Guid entityId, MISAEntity entity);
        /// <summary>
        /// Xử lý logic xóa nhiều nhân viên
        /// </summary>
        /// <param name="entityIds">Danh sách id đối tượng cần xóa</param>
        /// <returns>
        /// Số record bị xóa
        /// </returns>
        /// Created By: LNTGiang(12/03/2022)
        public int Delete(List<string> entityIds);
    }
}
