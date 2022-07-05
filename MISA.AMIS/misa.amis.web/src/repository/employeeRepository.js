import ApiUrl from '@/utils/url'
import axios from 'axios';

const EmployeeRepository = Object.freeze({
    /**
     * Lấy mã nhân viên mới
     * @returns Mã nhân viên mới
     * @author LNTGiang
     */
    getNemEmployeeCode: async function() {
        let response = await axios.get(ApiUrl.Employee.NewEmployeeCode);
        return response;
    },
    /**
     * Gọi API lấy nhân viên theo id
     * @param employeeId: Id của nhân viên  
     * @returns thông tin nhân viên
     * @author LNTGiang
     */
    getById: async function(employeeId) {
        let response = await axios.get(
            `${ApiUrl.Employee.GetEmployeeById}${employeeId}`
        );
        return response;
    },
    /**
     * Lấy toàn bộ nhân viên
     * @returns Danh sách nhân viên
     * @author LNTGiang
     */
    getAll: async function() {
        let response = await axios.get(
            `${ApiUrl.Employee.GetEmployees}`
        );
        return response;
    },
    /**
     * Phân trang và tìm kiếm nhân viên
     * @param pageSize: số record trên 1 trang  
     * @param pageNumber: trang thứ mấy
     * @param filter: thông tin cần tìm kiếm  
     * @returns Danh sách nhân viên sau khi lọc
     */
    filter: async function(pageSize, pageNumber, filter) {
        let response = await axios.get(ApiUrl.Employee.GetPaging, {
            params: {
                pageSize: pageSize,
                pageNumber: pageNumber,
                filter: filter,
            },
        });
        return response;
    },
    /**
     * Thêm mới 1 nhân viên
     * @param employee: {Thông tin nhân viên cần thêm}  
     * @author LNTGiang
     */
    insertEmployee: async function(employee) {
        let response = await axios.post(ApiUrl.Employee.Insert, employee);
        return response;
    },
    /**
     * Sửa 1 nhân viên
     * @param employeeId: id của nhân viên  
     * @param employee: thông tin nhân viên cần sửa  
     * @author LNTGiang
     */
    updateEmployee: async function(employeeId, employee) {
        let response = await axios.put(`${ApiUrl.Employee.Update}${employeeId}`, employee);
        return response;
    },
    /**
     * Xóa nhân viên
     * @param employeeIds: Danh sách id nhân viên cần xóa  
     * @author LNTGiang
     */
    deleteEmployee: async function(employeeIds) {
        let response = await axios({
            method: 'delete',
            url: ApiUrl.Employee.Delete,
            data: employeeIds
        })
        return response;
    },
})

export default EmployeeRepository;