using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Enum
{
    /// <summary>
    /// Giới tính
    /// </summary>
    /// Created By: LNTGiang(14/02/2022)
    public enum Gender
    {
        /// <summary>
        /// Nam
        /// </summary>
        /// Created By: LNTGiang(14/03/2022)
        Male = 1,
        /// <summary>
        /// Nữ
        /// </summary>
        /// Created By: LNTGiang(14/03/2022)
        Female = 0,
        /// <summary>
        /// Khác
        /// </summary>
        /// Created By: LNTGiang(14/03/2022)
        Other = 2
    }
    /// <summary>
    /// Phép toán
    /// </summary>
    /// Created By: LNTGiang(04/04/2022)
    public enum Calculation
    {
        /// <summary>
        /// Cộng
        /// </summary>
        /// Created By: LNTGiang(04/04/2022)
        Add = 1,
        /// <summary>
        /// Trừ
        /// </summary>
        /// Created By: LNTGiang(04/04/2022)
        Sub = 2,
        /// <summary>
        /// Nhân
        /// </summary>
        /// Created By: LNTGiang(04/04/2022)
        Mul = 3,
        /// <summary>
        /// Chia
        /// </summary>
        /// Created By: LNTGiang(04/04/2022)
        Div = 4,
    }
    /// <summary>
    /// Edit Mode
    /// </summary>
    /// Created By: LNTGiang(06/04/2022)
    public enum Mode
    {
        /// <summary>
        /// Không
        /// </summary>
        /// Created By: LNTGiang(06/04/2022)
        None = 0,
        /// <summary>
        /// Thêm
        /// </summary>
        /// Created By: LNTGiang(06/04/2022)
        Add = 1,
        /// <summary>
        /// Sửa
        /// </summary>
        /// Created By: LNTGiang(06/04/2022)
        Edit = 2,
        /// <summary>
        /// Xóa
        /// </summary>
        /// Created By: LNTGiang(06/04/2022)
        Delete = 3
    }
    /// <summary>
    /// Các loại filter
    /// </summary>
    /// Created By: LNTGiang(07/04/2022)
    public enum FilterOption
    {
        /// <summary>
        /// Bộ lọc text
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        FilterText,
        /// <summary>
        /// Bộ lọc số
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        FilterNumber,
        /// <summary>
        /// Bộ lọc SelectOption
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        SelectOption
    }
    /// <summary>
    /// Các phép toán trong filter
    /// </summary>
    /// Created By: LNTGiang(07/04/2022)
    public enum FilterCalculation
    {
        /// <summary>
        /// Bằng
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        EQual,
        /// <summary>
        /// Chứa
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        Contain,
        /// <summary>
        /// Lớn hơn
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        Greater,
        /// <summary>
        /// Lớn hơn hoặc bằng
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        GreaterOrEqual,
        /// <summary>
        /// Nhỏ hơn
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        Less,
        /// <summary>
        /// Nhỏ hơn hoặc bằng
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        LessOrEqual,
        /// <summary>
        /// Không bằng
        /// </summary>
        /// Created By: LNTGiang(07/04/2022)
        NotEqual
    }
}
