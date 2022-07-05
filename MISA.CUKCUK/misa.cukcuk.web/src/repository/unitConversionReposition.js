import ApiUrl from "@/utils/url";
import axios from "axios";

/**
 * Gọi các api của đơn vị chuyển đổi
 * @author: LNTGiang
 * Created date: 15:45 31/03/2022
 */
const UnitConversionRepository = Object.freeze({
    /**
     * Gọi api lấy tất cả đơn vị chuyển đổi
     * @return Danh sách đơn vị chuyển đổi
     * @author: LNTGiang
     * Created date: 15:47 31/03/2022
     */
    getAll: async function() {
        let response = await axios.get(ApiUrl.UnitConversion.GetAll);
        return response;
    },

    /**
     * Gọi api lấy thông tin của đơn vị chuyển đổi theo id
     * @param id id của đơn vị chuyển đổi
     * @return thông tin đơn vị chuyển đổi cần lấy
     * @author: LNTGiang
     * Created date: 15:48 31/03/2022
     */
    getById: async function(id) {
        let response = await axios.get(`${ApiUrl.UnitConversion.GetById}${id}`);
        return response;
    },

    /**
     * Phân trang và tìm kiếm 
     * @param pageSize Số lượng bản ghi trên 1 trang
     * @param pageNumber Trang thứ mấy
     * @param filter Bộ lọc
     * @return Danh sách đơn vị chuyển đổi sau khi lọc
     * @author: LNTGiang
     * Created date: 15:49 31/03/2022
     */
    filter: async function(pageSize, pageNumber, filter) {
        let response = await axios.get(ApiUrl.UnitConversion.GetPaging, {
            params: {
                pageSize: pageSize,
                pageNumber: pageNumber,
                filter: filter,
            },
        });
        return response;
    },

    /**
     * Gọi api lấy danh sách đơn vị chuyển đổi theo id của NVL
     * @param materialId id của nguyên vật liệu
     * @return danh sách của đơn vị chuyển đổi cần lấy
     * @author: LNTGiang
     * Created date: 15:55 31/03/2022
     */
    getByMaterialId: async function(materialId) {
        let response = await axios.get(`${ApiUrl.UnitConversion.GetByMaterialId}${materialId}`);
        return response;
    },

    /**
     * Thêm mới 1 đơn vị chuyển đổi
     * @param unitConversion Thông tin đơn vị chuyển đổi cần thêm
     * @return Số lượng bản ghi đã được thêm
     * @author: LNTGiang
     * Created date: 15:49 31/03/2022
     */
    insert: async function(unitConversion) {
        let response = await axios.post(ApiUrl.UnitConversion.Insert, unitConversion);
        return response;
    },

    /**
     * Sửa 1 đơn vị chuyển đổi
     * @param id id của đơn vị chuyển đổi cần sửa 
     * @param unitConversion: Thông tin đơn vị chuyển đổi cần sửa 
     * @return Số lượng bản ghi đã được sửa
     * @author: LNTGiang
     * Created date: 15:50 31/03/2022
     */
    update: async function(id, unitConversion) {
        let response = await axios.put(`${ApiUrl.UnitConversion.Update}${id}`, unitConversion);
        return response;
    },

    /**
     * Xóa các đơn vị chuyển đổi
     * @param ids Danh sách các id của đơn vị chuyển đổi cần xóa
     * @return Số lượng bản ghi đã được xóa
     * @author: LNTGiang
     * Created date: 15:50 31/03/2022
     */
    delete: async function(ids) {
        let response = await axios({
            method: 'delete',
            url: ApiUrl.UnitConversion.Delete,
            data: ids
        })
        return response;
    }
})

export default UnitConversionRepository;