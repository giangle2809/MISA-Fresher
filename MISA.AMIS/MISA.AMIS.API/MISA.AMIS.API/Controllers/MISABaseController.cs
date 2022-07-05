using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Base;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    
    public class MISABaseController<MISAEntity> : ControllerBase
    {
        IBaseRepository<MISAEntity> _baseRepository;
        IBaseService<MISAEntity> _baseService;

        public MISABaseController(IBaseRepository<MISAEntity> baseRepository, IBaseService<MISAEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }


        /// <summary>
        /// Lấy danh sách thông tin
        /// </summary>
        /// <returns>
        /// 200 - Danh sách dữ liệu 
        /// 204 - Không có dữ liệu
        /// 400 - Lỗi do đầu vào
        /// 500 - Lỗi do hệ thống
        /// </returns>
        /// CreatedBy: LNTGIANG(11/03/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var res = _baseRepository.Get();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="id">Id đối tuogwj</param>
        /// <returns>
        /// 200 - Dữ liệu 
        /// 204 - Không có dữ liệu
        /// 400 - Lỗi do đầu vào
        /// 500 - Lỗi do hệ thống
        /// </returns>
        /// CreatedBy: LNTGIANG(11/03/2022)
        [HttpGet("{id}")]
        public IActionResult GetByEmployeeId(Guid id)
        {
            try
            {
                var res = _baseRepository.GetById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Tìm kiếm và phân trang theo mã nhân viên hoặc họ tên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Trang thứ bao nhiêu</param>
        /// <param name="filter">Điều kiện lọc</param>
        /// <returns>
        /// 200 - Dữ liệu sau khi lọc
        /// 400 - Lỗi do đầu vào
        /// 500 - Lỗi do hệ thống
        /// </returns>
        /// CreatedBy: LNTGIANG(11/03/2022)
        [HttpGet("filter")]
        public IActionResult Filter(int pageSize = 20, int pageNumber = 1, string? filter = "")
        {
            try
            {
                var res = _baseRepository.GetPaging(filter, pageSize, pageNumber);
                return Ok(res);
            }
            catch (ValidateException ex)
            {
                return HandleValidateException(ex);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Thêm mới 
        /// </summary>
        /// <param name="entity">Thông tin cần thêm</param>
        /// <returns>
        /// 201 - Số lượng record đã được thêm
        /// 200 - Không có record nào được thêm
        /// 400 - Lỗi do đầu vào
        /// 500 - Lỗi do hệ thống
        /// </returns>
        /// CreatedBy: LNTGIANG(11/03/2022)
        [HttpPost]
        public IActionResult Post([FromBody] MISAEntity entity)
        {
            try
            {
                var res = _baseService.Insert(entity);

                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return Ok(res);
                }

            }
            catch (ValidateException ex)
            {
                return HandleValidateException(ex);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa thông tin
        /// </summary>
        /// <param name="id">Id của đối tượng cần sửa</param>
        /// <param name="employee">Thông tin cần sửa</param>
        /// <returns>
        /// 201 - Số lượng record đã được sửa
        /// 200 - Không có record nào được sửa
        /// 400 - Lỗi do đầu vào
        /// 500 - Lỗi do hệ thống
        /// </returns>
        /// CreatedBy: LNTGIANG(11/03/2022)
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] MISAEntity entity)
        {
            try
            {
                var res = _baseService.Update(id, entity);

                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return Ok(res);
                }
            }
            catch (ValidateException ex)
            {
                return HandleValidateException(ex);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="ids">Danh sách id cần xóa</param>
        /// <returns>
        /// 201 - Số lượng record đã được sửa
        /// 200 - Không có record nào được sửa
        /// 400 - Lỗi do đầu vào
        /// 500 - Lỗi do hệ thống
        /// </returns>
        /// CreatedBy: LNTGIANG(11/03/2022)
        [HttpDelete]
        public IActionResult Delete([FromBody] List<string> ids)
        {
            try
            {
                var res = _baseService.Delete(ids);

                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return Ok(res);
                }
            }
            catch (ValidateException ex)
            {
                return HandleValidateException(ex);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm xử lý exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected IActionResult HandleException(Exception ex)
        {
            //
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = Core.Resources.ResourceVN.ExceptionError;
            error.Data = ex.Data;
            return StatusCode(500, error);
        }

        /// <summary>
        /// Hàm xử lý validate exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected IActionResult HandleValidateException(ValidateException ex)
        {
            //
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = ex.Message;
            error.ErrorCode = ex.ErrorCode;
            error.Data = ex.Data;
            return StatusCode(400, error);
        }
    }
}
