using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces;
using MISA.CUKCUK.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Services
{
    public class UnitConversionService: BaseService<UnitConversion>, IUnitConversionService
    {
        IUnitConversionRepository _unitConversionRepository;

        public UnitConversionService(IUnitConversionRepository unitConversionRepository):base(unitConversionRepository)
        {
            _unitConversionRepository = unitConversionRepository;
        }
    }
}
