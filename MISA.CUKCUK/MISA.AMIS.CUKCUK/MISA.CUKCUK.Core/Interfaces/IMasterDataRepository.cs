using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces
{
    public interface IMasterDataRepository: IBaseRepository<MasterData>
    {
        /// <summary>
        /// Lấy data theo nhóm
        /// </summary>
        /// <param name="groupId">id của nhóm đối tượng cần lấy</param>
        /// <returns>Danh sach data cần lấy</returns>
        /// Created By: LNTGiang(04/04/2022)
        public IEnumerable<MasterData> GetByGroupId(Guid groupId);
    }
}
