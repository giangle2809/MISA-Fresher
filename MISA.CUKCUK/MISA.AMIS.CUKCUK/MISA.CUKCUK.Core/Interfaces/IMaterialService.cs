using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces
{
    public interface IMaterialService: IBaseService<Material>
    {
        /// <summary>
        /// Thêm mới nguyên vật liệu
        /// </summary>
        /// <param name="material">Thông tin nguyên vật liệu</param>
        /// <param name="unitConversions">Các đơn vị chuyển đổi của nguyên vật liệu</param>
        /// <returns>
        /// Số lượng bản ghi đã thay đổi
        /// </returns>
        /// Created By: LNTGiang(06/04/2022)
        public int Insert(Material material, List<UnitConversion> unitConversions);
        /// <summary>
        /// Sửa nguyên vật liệu
        /// </summary>
        /// <param name="materialId">Id của nguyên vật liệu</param>
        /// <param name="material">Thông tin nguyên vật liệu</param>
        /// <param name="unitConversions">Các đơn vị chuyển đổi của nguyên vật liệu</param>
        /// <returns>
        /// Số lượng bản ghi đã thay đổi
        /// </returns>
        /// Created By: LNTGiang(06/04/2022)
        public int Update(Guid materialId, Material material, List<UnitConversion> unitConversions);
    }
}
