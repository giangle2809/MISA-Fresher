const Constant = Object.freeze({
    ToastTime: 3000,
    /**
     * Các trạng thái của data (thêm, sửa, xóa)
     * @author LNTGiang
     */
    DataStatus: Object.freeze({
        Edit: 1,
        Add: 2,
        Delete: 3,
        MultiDelete: 4,
        Replication: 5,
    }),
    /**
     * Các trạng thái của các nút ở pagination
     * @author LNTGiang
     */
    PaginationStatus: Object.freeze({
        prev: 5,
        number: 6,
        next: 7
    }),
    /**
     * Các trạng thái của MessageBox (lỗi, cảnh báo, hỏi)
     * @author LNTGiang
     */
    MessageBoxStatus: Object.freeze({
        Error: 'ms-icon-error',
        Warn: 'ms-icon-warn',
        Question: 'ms-icon-question'
    }),
    /**
     * Các trạng thái của Toast
     * @author LNTGiang
     */
    ToastStatus: Object.freeze({
        Error: 'ms-toast-error',
        Warn: 'ms-toast-warn',
        Success: 'ms-toast-success'
    }),
    /**
     * Giới tính
     * @author LNTGiang
     */
    Gender: Object.freeze({
        Male: 1,
        Female: 0,
        Other: 2,
    })
})

export default Constant;