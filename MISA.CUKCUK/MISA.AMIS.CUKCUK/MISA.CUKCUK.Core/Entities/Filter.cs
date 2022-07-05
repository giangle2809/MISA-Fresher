using MISA.CUKCUK.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class Filter
    {
        public string ColumnName { get; set; }
        public FilterCalculation Calculation { get; set; }
        public string Value { get; set; }
    }
}
