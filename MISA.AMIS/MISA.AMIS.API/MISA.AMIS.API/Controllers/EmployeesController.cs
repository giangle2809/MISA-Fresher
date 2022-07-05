using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces;
using OfficeOpenXml;

namespace MISA.AMIS.API.Controllers
{
    public class EmployeesController : MISABaseController<Employee>
    {
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;

        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeRepository, employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// 200 - Mã nhân viên mới
        /// 204 - Không có dữ liệu
        /// 400 - Lỗi do đầu vào
        /// 500 - Lỗi do hệ thống
        /// </returns>
        /// CreatedBy: LNTGiang(12/03/2022)
        [HttpGet("newEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var res = _employeeRepository.GetNewEmployeeCode();
                
                return Ok(res);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }
        }
        
        /// <summary>
        /// Thực hiên xuất file excel
        /// </summary>
        /// <returns>
        /// 200 - File excel
        /// 500 - Lỗi do hệ thống
        /// </returns>
        /// Created By: LNTGiang(14/02/2022)
        [HttpGet("exporter")]
        public IActionResult Export()
        {
            try
            {
                var stream = _employeeService.Export();
               
                var fileName = Core.Resources.ResourceVN.EmployeeExcelFileName;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

    }
}
