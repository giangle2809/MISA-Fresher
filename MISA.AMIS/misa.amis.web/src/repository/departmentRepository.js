import ApiUrl from '@/utils/url'
import axios from 'axios';

const DepartmentRepository = Object.freeze({
    /**
     * Lấy toàn bộ phòng ban
     * @returns Thông tin phòng ban
     * @author LNTGiang
     */
    getAll: async function() {
        let response = await axios.get(
            `${ApiUrl.Department.GetDepartments}`
        );
        return response;
    },

})

export default DepartmentRepository;