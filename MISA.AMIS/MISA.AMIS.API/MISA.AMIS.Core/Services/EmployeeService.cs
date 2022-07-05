using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public MemoryStream Export()
        {
            // Lấy dữ liệu từ database
            var employees = _employeeRepository.Get();

            var props = typeof(Employee).GetProperties();

            MemoryStream stream;
            using (var package = new ExcelPackage())
            {
                // Tạo sheet
                var worksheet = package.Workbook.Worksheets.Add(Resources.ResourceVN.ExcelEmployeeTitle);

                // Thêm các header column
                worksheet.Cells[3, 1].Value = "STT";

                int col = 2;

                // Tạo list chứa các propName của excel
                List<string> propExcels = new List<string>();

                foreach(var prop in props)
                {
                    // Kiểm tra xem prop có được hiển thị trên excel không
                    var isExcelColumn = Attribute.IsDefined(prop, typeof(MISAExcelColumn));

                    if (isExcelColumn)
                    {
                        // Lấy ra displayName của prop
                        var displayNameAttribute  = Attribute.GetCustomAttribute(prop, typeof(MISADisplayName));

                        var propDisplayName = "";

                        if(displayNameAttribute != null)    propDisplayName = (displayNameAttribute as MISADisplayName).DisplayName;

                        // Thêm các column header
                        worksheet.Cells[3, col].Value = propDisplayName;
                        worksheet.Cells[3, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[3, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[3, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[3, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;


                        // Add propName của excel vào list
                        propExcels.Add(prop.Name);
                        col++;
                    }
                }

                // Lấy địa chỉ hàng header
                var headerRow = $"A3:{(char)((int)'A' + propExcels.Count())}3";

                // style header
                worksheet.Cells[headerRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[headerRow].Style.Font.Bold = true;
                worksheet.Cells[headerRow].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[headerRow].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);

                // Lấy các địa chỉ cột đầu và cột cuối cần merge để hiển thị title
                var titleRow = $"A1:{(char)((int)'A' + propExcels.Count())}1";
                
                // Merge các column ở dòng đầu tiên
                worksheet.Cells[titleRow].Merge = true;

                // Gán Title của Excel và style
                worksheet.Cells[titleRow].Value = Resources.ResourceVN.ExcelEmployeeTitle;
                worksheet.Cells[titleRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[titleRow].Style.Font.Bold = true;

                // Thực hiện gán dữ liệu vào từng dòng
                int row = 4;
                foreach(var employee in employees)
                {
                    col = 1;
                    worksheet.Cells[row, col].Value = (row - 3);
                    worksheet.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    foreach (var colExcel in propExcels)
                    {
                        col++;
                        var prop = employee.GetType().GetProperty(colExcel);
                        var propValue = prop.GetValue(employee);
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            worksheet.Cells[row, col].Value = ((DateTime)propValue).ToString("dd/MM/yyyy");
                            worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; 
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = propValue;
                        }

                        worksheet.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    }
                    row++;
                }

                // Set size chữ
                worksheet.Cells.Style.Font.Size = 13;

                // Set size title
                worksheet.Cells[titleRow].Style.Font.Size = 24;

                // Set độ rộng auto
                worksheet.Cells.AutoFitColumns();

                // Lưu dữ liệu
                package.Save(); 
                stream = new MemoryStream(package.GetAsByteArray());
            }
            return stream;
        }

        protected override bool ValidateCustom(Guid? entityId, Employee entity)
        {

            // Mã nhân viên không được phép trùng:
            if ( _employeeRepository.CheckEmployeeCode(entityId, entity.EmployeeCode))
            {
                throw new ValidateException(String.Format(Resources.ResourceVN.EmployeeCodeIdentical, entity.EmployeeCode), "EmployeeCode");
            }
            return true;
        }
    }
}
