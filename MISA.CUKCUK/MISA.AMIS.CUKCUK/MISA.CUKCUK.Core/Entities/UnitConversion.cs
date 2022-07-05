using MISA.CUKCUK.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class UnitConversion: BaseEntity
    {
        public Guid UnitConversionId { get; set; }
        public Guid? UnitId { get; set; }
        public string? UnitName { get; set; }
        public Guid MaterialId { get; set; }
        public int ConversionRate { get; set; }
        public Calculation Calculation { get; set; }
        public string? CalculationName 
        { 
            get {
                switch (Calculation)
                {
                    case Calculation.Add:
                        return "+";
                    case Calculation.Sub: 
                        return "-";
                    case Calculation.Mul:
                        return "*";
                    case Calculation.Div:
                        return "/";
                    default:
                        return "";
                }
            } 
        }
        public Mode Mode { get; set; }

    }
}
