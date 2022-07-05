using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class PaggingResult<T>
    {
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}
