using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities.Input
{
    public class FilterInput
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? Filter { get; set; }
    }
}
