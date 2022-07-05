using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class Material:BaseEntity
    {
        public Guid? MaterialId { get; set; }
        [MISARequired]
        [MISAFilter(Enum.FilterOption.FilterText, "m.")]
        [MISADisplayName("Mã nguyên vật liệu")]
        public string? MaterialCode { get; set; }
        [MISARequired]
        [MISAFilter(Enum.FilterOption.FilterText, "m.")]
        [MISADisplayName("Tên nguyên vật liệu")]
        public string? MaterialName { get; set; }
        [MISARequired]
        [MISAFilter(Enum.FilterOption.SelectOption, "m.")]
        [MISADisplayName("Đơn vị tính")]
        public Guid? UnitId { get; set; }
        public string? UnitName { get; set; }
        [MISAFilter(Enum.FilterOption.SelectOption, "m.")]
        public Guid? StockId { get; set; }
        public string? StockName { get; set; }
        public DateTime? Expiry { get; set; }
        [MISAFilter(Enum.FilterOption.FilterNumber, "m.")]
        public int MinimumStock { get; set; }
        [MISAFilter(Enum.FilterOption.FilterText, "m.")]
        public string? Description { get; set; }
    }
}
