using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities.Input
{
    public class MaterialInput
    {
        public Material Material { get; set; }
        public List<UnitConversion> UnitConversions { get; set; }
    }
}
