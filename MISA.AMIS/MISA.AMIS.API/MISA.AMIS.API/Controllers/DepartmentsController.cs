using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : MISABaseController<Department>
    {
        IDepartmentRepository _departmentRepository;
        IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentRepository departmentRepository, IDepartmentService departmentService) : base(departmentRepository, departmentService)  
        {
            _departmentRepository = departmentRepository;
            _departmentService = departmentService;
        }
    }
}
