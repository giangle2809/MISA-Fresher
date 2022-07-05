using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Services
{
    public class MasterDataService: BaseService<MasterData>, IMasterDataService
    {
        IMasterDataRepository _masterDataRepository;

        public MasterDataService(IMasterDataRepository masterDataRepository):base(masterDataRepository)
        {
            _masterDataRepository = masterDataRepository;
        }
    }
}
