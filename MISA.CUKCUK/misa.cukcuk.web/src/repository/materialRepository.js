import ApiUrl from "@/utils/url";
import axios from "axios";
/**
 * Thực hiện gọi các api của NVL
 * @author: LNTGiang
 * Created date: 10:55 31/03/2022
 */
const MaterialRepository = Object.freeze({
    /**
     * Gọi api lấy tất cả NVL
     * @return Danh sách NVL
     * @author: LNTGiang
     * Created date: 10:34 31/03/2022
     */
    getAll: async function() {
        let response = await axios.get(ApiUrl.Material.GetAll);
        return response;
    },

    /**
     * Gọi api lấy thông tin NVL theo id
     * @param id id của NVL cần lấy
     * @return Thông tin NVL cần lấy
     * @author: LNTGiang
     * Created date: 10:35 31/03/2022
     */
    getById: async function(id) {
        let response = await axios.get(`${ApiUrl.Material.GetById}${id}`);
        return response;
    },

    /**
     * Phân trang và tìm kiếm 
     * @param pageSize Số lượng bản ghi trên 1 trang
     * @param pageNumber Trang thứ mấy
     * @param filter Bộ lọc
     * @return Danh sách NVL sau khi lọc
     * @author: LNTGiang
     * Created date: 10:36 31/03/2022
     */
    filter: async function(pageSize, pageNumber, filter = '[]') {
        let response = await axios.get(ApiUrl.Material.GetPaging, {
            params: {
                pageSize: pageSize,
                pageNumber: pageNumber,
                filter: filter ? JSON.stringify(filter) : '[]',
            },
        });
        return response;
    },

    /**
     * Thêm mới 1 NVL
     * @param material Thông tin NVL cần thêm
     * @param unitConversion Các đơn vị chuyển đổi
     * @return Số lượng bản ghi đã được thêm
     * @author: LNTGiang
     * Created date: 10:37 31/03/2022
     */
    insert: async function(material, unitConversions) {
        let response = await axios.post(ApiUrl.Material.Insert, {
            "Material": material,
            "UnitConversions": unitConversions
        });
        return response;
    },

    /**
     * Sửa 1 NVL
     * @param id id của NVL cần sửa 
     * @param material: Thông tin NVL cần sửa 
     * @param unitConversion Các đơn vị chuyển đổi
     * @return Số lượng bản ghi đã được sửa
     * @author: LNTGiang
     * Created date: 10:38 31/03/2022
     */
    update: async function(id, material, unitConversions) {
        let response = await axios.put(`${ApiUrl.Material.Update}${id}`, {
            "Material": material,
            "UnitConversions": unitConversions
        });
        return response;
    },

    /**
     * Xóa các NVL
     * @param ids Danh sách các id của NVL cần xóa
     * @return Số lượng bản ghi đã được xóa
     * @author: LNTGiang
     * Created date: 10:39 31/03/2022
     */
    delete: async function(ids) {
        let response = await axios({
            method: 'delete',
            url: ApiUrl.Material.Delete,
            data: ids
        })
        return response;
    },
    /**
     * Lấy mã nguyên vật liệu mới
     * @author: LNTGiang
     * Created date: 16:36 04/04/2022
     */
    getNewMaterialCode: async function() {
        let response = await axios.get(ApiUrl.Material.NewMaterialCode);
        return response;
    }
})

export default MaterialRepository;