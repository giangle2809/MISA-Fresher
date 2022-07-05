export const ToastTime = 3000;
/**
 * Các trạng thái (thêm, sửa, xóa , nhân bản)
 * @author: LNTGiang
 * Created date: 16:03 04/04/2022
 */
export const DataStatus = Object.freeze({
    Edit: 1,
    Add: 2,
    Delete: 3,
    Clone: 4,
});
/**
 * Các phép toán
 * @author: LNTGiang
 * Created date: 16:02 04/04/2022
 */
export const Calculation = Object.freeze({
    Add: 1,
    Sub: 2,
    Mul: 3,
    Div: 4
});
/**
 * Edit Mode
 * @author: LNTGiang
 * Created date: 20:58 06/04/2022
 */
export const Mode = Object.freeze({
    None: 0,
    Add: 1,
    Edit: 2,
    Delete: 3
});
/**
 * Bộ lọc 
 * @author: LNTGiang
 * Created date: 09:22 07/04/2022
 */
export const Filter = Object.freeze({
    EQual: {
        display: 'Bằng',
        value: 0,
        compare: ['text', 'number']
    },
    Contain: {
        display: 'Chứa',
        value: 1,
        compare: ['text']
    },
    Greater: {
        display: 'Lớn hơn',
        value: 2,
        compare: ['number']
    },
    GreaterOrEqual: {
        display: 'Lớn hơn hoặc bằng',
        value: 3,
        compare: ['number']
    },
    Less: {
        display: 'Nhỏ hớn',
        value: 4,
        compare: ['number']
    },
    LessOrEqual: {
        display: 'Nhỏ hơn hoặc bằng',
        value: 5,
        compare: ['number']
    },
    NotEqual: {
        display: "Khác",
        value: 6,
        compare: ['text', 'number']
    }
})