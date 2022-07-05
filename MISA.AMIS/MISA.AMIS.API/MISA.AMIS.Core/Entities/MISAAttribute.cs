using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Validate propperty không được bỏ trống
    /// </summary>
    /// Created By: LNTGiang(14/03/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired: Attribute
    {
        public string? ErrorMsg { get; set; }

        public MISARequired(string? errorMsg = null)
        {
            ErrorMsg = errorMsg;
        }
    }

    /// <summary>
    /// Validate độ dài của property không vượt quá Length
    /// </summary>
    /// Created By: LNTGiang(14/03/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class MISADataLength : Attribute
    {
        public int Length { get; set; }
        
        public MISADataLength(int length)
        {
            Length = length;
        }
    }

    /// <summary>
    /// Tên của property
    /// </summary>
    /// Created By: LNTGiang(14/03/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class MISADisplayName: Attribute
    {
        public string DisplayName { get; set; }

        public MISADisplayName(string displayName)
        {
            DisplayName = displayName;
        }
    }

    /// <summary>
    /// Validate ngày không được vượt quá ngày hiện tại
    /// </summary>
    /// Created By: LNTGiang(14/03/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAValidateDate: Attribute
    {

    }

    /// <summary>
    /// Các cột hiển thị trong file excel
    /// </summary>
    /// Created By: LNTGiang(14/03/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAExcelColumn: Attribute
    {

    }

    /// <summary>
    /// Validate email đúng định dạng
    /// </summary>
    /// Created By: LNTGiang(14/02/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAEmailValidate: Attribute { 
    }
}
