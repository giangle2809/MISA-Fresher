using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Common;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces;

namespace MISA.CUKCUK.API.Controllers
{
    public class MasterDatasController : MISABaseController<MasterData>
    {
        IMasterDataRepository _masterDataRepository;
        IMasterDataService _masterDataService;

        public MasterDatasController(IMasterDataRepository masterDataRepository, IMasterDataService masterDataService):base(masterDataRepository, masterDataService)
        {
            _masterDataRepository = masterDataRepository;
            _masterDataService = masterDataService;
        }
        /// <summary>
        /// Lấy danh sách kho ngầm định
        /// </summary>
        /// <returns>Danh sach kho nhầm định</returns>
        /// CreatedBy: LNTGiang(04/04/2022)
        [HttpGet("Stock")]
        public IActionResult GetStock() 
        {
            try
            {
                var res = _masterDataRepository.GetByGroupId(Config.Stock);
                return Ok(res);
            }catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        
        /// <summary>
        /// Lấy danh sách đơn vị tính
        /// </summary>
        /// <returns>Danh sách đơn vị tính</returns>
        /// CreatedBy: LNTGiang(04/04/2022)
        [HttpGet("Unit")]
        public IActionResult GetUnit() 
        {
            try
            {
                var res = _masterDataRepository.GetByGroupId(Config.Unit);
                return Ok(res);
            }catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
