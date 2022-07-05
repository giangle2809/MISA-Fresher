const Format = Object.freeze({
    /**
     * Hàm để chuyển định dạng ngày
     * @param date: Ngày cần format  
     * @param format: Định dạng muốn format sang (DD/MM/YYYY, MM/DD/YYYY, ...)  
     * @returns ngày tháng sau khi đã format
     * @author LNTGiang
     */
    fomatDate: function(date, format) {
        if (!date) {
            return '';
        }
        date = new Date(date);
        if (isNaN(date)) {
            return '';
        }
        let day = ("0" + date.getDate()).slice(-2);
        let month = ("0" + (date.getMonth() + 1)).slice(-2);
        let year = ("000" + date.getFullYear()).slice(-4);

        let res = format.replace('DD', day);
        res = res.replace('MM', month);
        res = res.replace('YYYY', year);

        return res;
    },
    /**
     * Hàm format string 
     * @param str: chuỗi cần format 
     * @param param: các giá trị cần fill vào chuỗi 
     * @return chuỗi sau khi đã format
     * @author LNTGiang
     */
    stringFormat: function(str, param) {
        for (let i = 0; i < param.length; i++) {
            str = str.replace(`{${i}}`, param[i]);
        }
        return str;
    }

})

export default Format;