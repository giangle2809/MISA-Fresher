using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces
{
    public interface IMaterialRepository:IBaseRepository<Material>
    {
        /// <summary>
        /// Thêm mới nguyên vật liệu
        /// </summary>
        /// <param name="material">json của nguyên vật liệu</param>
        /// <param name="unitConversions">json của các đơn vị chuyển đổi</param>
        /// <returns></returns>
        /// Created By: LNTGiang(06/04/2022)
        public int Insert(string material, string unitConversions);
        /// <summary>
        /// Sửa nguyên vật liệu
        /// </summary>
        /// <param name="materialId">id của nguyên vật liệu cần sửa</param>
        /// <param name="material">json của nguyên vật liệu</param>
        /// <param name="unitConversions">json của các đơn vị chuyển đổi</param>
        /// <returns></returns>
        /// Created By: LNTGiang(06/04/2022)
        public int Update(Guid materialId, string material, string unitConversions);
        /// <summary>
        /// Lấy mã nguyên vật liệu mới
        /// </summary>
        /// <returns>Mã nguyên vật liệu mới</returns>
        /// Created By: LNTGiang(30/3/2022)
        public string GetNewMaterialCode();
        /// <summary>
        /// Kiểm tra xem mã nguyên vật liệu đã tồn tại chưa
        /// </summary>
        /// <param name="materialId">id của nguyên vật liệu - Sửa</param>
        /// <param name="materialCode">mã của nguyên vật liệu</param>
        /// <returns></returns>
        /// Created By: LNTGiang(04/04/2022)
        public int CheckMaterialCode(Guid? materialId, string materialCode);
    }
}
