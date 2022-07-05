using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces
{
    public interface IUnitConversionRepository: IBaseRepository<UnitConversion>
    {
        /// <summary>
        /// Lấy các đơn vị chuyển đổi theo material
        /// </summary>
        /// <param name="materialId">id của NVL</param>
        /// <returns>Các đơn vị chuyển đôi</returns>
        /// Created By: LNTGiang(30/03/2022)
        public IEnumerable<UnitConversion> GetByMaterialId(Guid materialId);
    }
}
