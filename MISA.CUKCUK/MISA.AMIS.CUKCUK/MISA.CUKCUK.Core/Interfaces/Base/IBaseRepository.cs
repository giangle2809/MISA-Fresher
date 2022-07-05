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
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy danh sách đối tượng
        /// </summary>
        /// <returns>Danh sách cần lấy</returns>
        /// Created By: LNTGiang(11/03/2022)
        public IEnumerable<MISAEntity> Get();
        /// <summary>
        /// Lấy thông tin 1 đối tượng
        /// </summary>
        /// <param name="entityId">Id của đối tượng cần truy xuất</param>
        /// <returns>Thòn tin cần lấy</returns>
        /// Created By: LNTGiang(11/03/2022)
        public MISAEntity GetById(Guid entityId);
        /// <summary>
        /// Xóa 
        /// </summary>
        /// <param name="entityIds">Các Id đối tượng cần xóa phân cách nhau bởi dấu phẩy("id1,id2,id3")</param>
        /// <returns>
        /// Số record đã xóa
        /// </returns>
        /// Created By: LNTGiang(12/03/2022)
        public int Delete(string entityIds);
        /// <summary>
        /// Phân trang và tìm kiếm
        /// </summary>
        /// <param name="filter">Từ khóa cần tìm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="PageNumber">Trang số mấy</param>
        /// <returns>Dữ liệu sau khi lọc</returns>
        /// Created By: LNTGiang(11/03/2022)
        public PaggingResult<MISAEntity> GetPaging(string filter, int pageSize, int pageNumber);
    }
}