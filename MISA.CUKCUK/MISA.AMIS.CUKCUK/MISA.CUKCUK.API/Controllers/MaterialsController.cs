using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Entities.Input;
using MISA.CUKCUK.Core.Exceptions;
using MISA.CUKCUK.Core.Interfaces;

namespace MISA.CUKCUK.API.Controllers
{
    public class MaterialsController : MISABaseController<Material>
    {
        IMaterialRepository _materialRepository;
        IMaterialService _materialService;

        public MaterialsController(IMaterialRepository materialRepository, IMaterialService materialService):base(materialRepository, materialService)
        {
            _materialRepository = materialRepository;
            _materialService = materialService;
        }
        /// <summary>
        /// Sinh mã nguyên vật liệu mới
        /// </summary>
        /// <returns>
        /// 200 - Mã nguyên vật liệu mới
        /// </returns>
        /// Created By: LNTGiang(11/04/2022)
        [HttpGet("NewMaterialCode")]
        public IActionResult GetNewMaterialCode()
        {
            try
            {
                var res = _materialRepository.GetNewMaterialCode();
                return Ok(res);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm mới nguyên vật liệu
        /// </summary>
        /// <param name="material">Thông tin nguyên vật liệu cần thêm</param>
        /// <param name="unitConversions">Thông tin các đơn vị chuyển đổi của nguyên vật liệu</param>
        /// <returns>
        /// 201 - Số lượng bản ghi được thêm
        /// 200 - Không thay đổi dữ liệu
        /// </returns>
        /// Created By: LNTGiang(11/04/2022)
        [HttpPost]
        public IActionResult Post([FromBody] MaterialInput materialInput)
        {
            try
            {
                var res = _materialService.Insert(materialInput.Material, materialInput.UnitConversions);

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
        /// Sửa nguyên vật liệu
        /// </summary>
        /// <param name="id">id của nguyên vật liệu</param>
        /// <param name="material">Thông tin nguyên vật liệu cần thêm</param>
        /// <param name="unitConversions">Thông tin các đơn vị chuyển đổi của nguyên vật liệu</param>
        /// 201 - Số lượng bản ghi được sửa
        /// 200 - Không thay đổi dữ liệu
        /// </returns>
        /// Created By: LNTGiang(11/04/2022)
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] MaterialInput materialInput)
        {
            try
            {
                var res = _materialService.Update(id, materialInput.Material, materialInput.UnitConversions);

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
    }
}
