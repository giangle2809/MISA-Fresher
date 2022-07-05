import ApiUrl from "@/utils/url";
import axios from "axios";

/**
 * Thực hiện gọi api trong bảng chỉ mục
 * @author: LNTGiang
 * Created date: 15:37 01/04/2022
 */
const MasterDataRepository = Object.freeze({
    /**
     * Lấy dữ liệu kho ngầm định
     * @return Danh sách kho ngầm định
     * @author: LNTGiang
     * Created date: 15:38 01/04/2022
     */
    async getStock() {
        let response = await axios.get(ApiUrl.MasterData.GetStock);
        return response;
    },
    /**
     * Lấy dữ liệu đơn vị tính
     * @return Danh sách đơn vị tính
     * @author: LNTGiang
     * Created date: 15:40 01/04/2022
     */
    async getUnit() {
        let response = await axios.get(ApiUrl.MasterData.GetUnit);
        return response;
    }
})

export default MasterDataRepository;