using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class PaggingResult<T>
    {
        [JsonPropertyName("totalRecord")]
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}
