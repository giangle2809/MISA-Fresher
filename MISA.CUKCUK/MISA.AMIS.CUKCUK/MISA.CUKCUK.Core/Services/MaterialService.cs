using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Exceptions;
using MISA.CUKCUK.Core.Interfaces;
using MISA.CUKCUK.Core.Interfaces.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Services
{
    public class MaterialService : BaseService<Material>, IMaterialService
    {
        IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository):base(materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public int Insert(Material material, List<UnitConversion> unitConversions)
        {
            if(Validate(null, material))
            {
                string jsonMaterial = JsonConvert.SerializeObject(material);
                string jsonUnitConversion = JsonConvert.SerializeObject(unitConversions);
                int res = _materialRepository.Insert(jsonMaterial, jsonUnitConversion);

                return res;
            }
            return 0;   
        }

        public int Update(Guid materialId, Material material, List<UnitConversion> unitConversions)
        {
            if (Validate(materialId, material))
            {
                string jsonMaterial = JsonConvert.SerializeObject(material);
                string jsonUnitConversion = JsonConvert.SerializeObject(unitConversions);
                int res = _materialRepository.Update(materialId, jsonMaterial, jsonUnitConversion);

                return res;
            }
            return 0;
        }

        protected override bool ValidateCustom(Guid? entityId, Material entity)
        {
            int res = _materialRepository.CheckMaterialCode(entityId, entity.MaterialCode);
            if(res > 0)
            {
                throw new ValidateException(String.Format(Resources.ResourceVN.MaterialCodeIdentical, entity.MaterialCode), "MaterialCode");
            }
            return true;
        }
    }
}
