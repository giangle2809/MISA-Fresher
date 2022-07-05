using MISA.AMIS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// Created By: LNTGiang(09/03/2022)
    public class Employee
    {
        [MISADataLength(36)]
        public Guid? EmployeeId { get; set; }
        [MISARequired]
        [MISADisplayName("Mã nhân viên")]
        [MISADataLength(20)]
        [MISAExcelColumn]
        public string? EmployeeCode { get; set; }
        [MISARequired]
        [MISADisplayName("Tên nhân viên")]
        [MISADataLength(100)]
        [MISAExcelColumn]

        public string? FullName { get; set; }
        [MISAValidateDate]
        [MISADisplayName("Ngày sinh")]
        [MISAExcelColumn]

        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        [MISAExcelColumn]
        [MISADisplayName("Giới tính")]
        public string GenderName {
            get
            {
                switch (Gender)
                {
                    case Gender.Female:
                        return Resources.ResourceVN.Female;
                    case Gender.Male:
                        return Resources.ResourceVN.Male;
                    case Gender.Other: 
                        return Resources.ResourceVN.Other;
                    default:
                        return "";
                }
            }
        }
        [MISADataLength(25)]
        public string? IdentityNumber { get; set; }
        [MISAValidateDate]
        [MISADisplayName("Ngày cấp")]
        public DateTime? IdentityDate { get; set; }
        [MISADataLength(255)]
        public string? IdentityPlace { get; set; }
        [MISADataLength(100)]
        [MISAEmailValidate]
        public string? Email { get; set; }
        [MISADataLength(50)]
        public string? PhoneNumber { get; set; }
        [MISADataLength(255)]
        public string? Address { get; set; }
        [MISADataLength(50)]
        public string? TelephoneNumber { get; set; }
        [MISADataLength(25)]
        [MISADisplayName("Số tài khoản")]
        [MISAExcelColumn]
        public string? BankAccountNumber { get; set; }
        [MISADataLength(255)]
        [MISADisplayName("Tên ngân hàng")]
        [MISAExcelColumn]
        public string? BankName { get; set; }
        [MISADataLength(255)]
        public string? BankBranchName { get; set; }
        [MISARequired]
        [MISADisplayName("Đơn vị")]
        [MISADataLength(36)]
        public Guid? DepartmentId { get; set; }
        [MISADisplayName("Tên đơn vị")]
        [MISAExcelColumn]
        public string? DepartmentName { get; set; }
        [MISADataLength(100)]
        [MISAExcelColumn]
        [MISADisplayName("Chức danh")]
        public string? PositionName { get; set; }
    }
}
