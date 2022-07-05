using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces;

namespace MISA.CUKCUK.API.Controllers
{
    public class UnitConversionsController : MISABaseController<UnitConversion>
    {
        IUnitConversionRepository _unitConversionRepository;
        IUnitConversionService _unitConversionService;

        public UnitConversionsController(IUnitConversionRepository unitConversionRepository, IUnitConversionService unitConversionService):base(unitConversionRepository, unitConversionService)
        {
            _unitConversionRepository = unitConversionRepository;
            _unitConversionService = unitConversionService;
        }

        /// <summary>
        /// Lấy các đơn vị tính theo id của NVL
        /// </summary>
        /// <param name="materialId">id của NVL</param>
        /// <returns>
        /// 200 - Danh sách các đơn vị tính
        /// 204 - Không có dữ liệu
        /// </returns>
        /// Created By: LNTGiang(30/3/2022)
        [HttpGet("Material/{materialId}")]
        public IActionResult GetByMaterialId(Guid materialId)
        {
            try
            {
                var res = _unitConversionRepository.GetByMaterialId(materialId);
                return Ok(res);
            }catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
