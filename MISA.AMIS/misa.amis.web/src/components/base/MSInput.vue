<template>
  <div class="ms-input-box">
    <input
      class="ms-input"
      :class="{ 'ms-input-icon': isIcon }"
      type="text"
      v-model="val"
      :placeholder="placehoder"
      :maxlength="maxlength"
      :tabindex="tabindex"
      @change="onchange"
      @blur="onBlur"
      @keyup="onKeyup"
      ref="input"
      v-if="type == 'text'"
    />
    <div v-if="type == 'date'" style="max-width: 100%">
      <input
        type="text"
        class="ms-display-date"
        placeholder="DD/MM/YYYY"
        v-model="dateConverted"
        :tabindex="tabindex"
        @keypress="dateInputChange"
        ref="input-date"
        style="background-color: #fff"
      />
      <input
        class="ms-input"
        type="date"
        v-model="dateVal"
        @change="onchangeDate"
        @blur="onblur"
        :max="format.fomatDate(new Date(), 'YYYY-MM-DD')"
      />
    </div>

    <slot name="icon"> </slot>
  </div>
</template>

<script>
import Format from "@/utils/format";

export default {
  name: "MSInput",
  data() {
    return { 
      format: Format,
      dateConverted: "",
      val: "", 
      dateVal: ""
    };
  },
  props: [
    "placehoder",
    "isIcon",
    "value",
    "dateValue",
    "autoFocus",
    "type",
    "required",
    "maxlength",
    "titleError",
    "tabindex",
  ],
  watch: {
    /**
     * Thay đổi value input khi bên ngoài thay đổi
     * @author LNTGiang
     */
    value(value) {
      this.val = value;
      if(this.type == 'text'){
        this.$refs["input"].classList.remove("ms-input-error");
        this.$refs["input"].setAttribute("title", "");
      }
      
    },
    /**
     * Thay đổi value date khi bên ngoài thay đổi
     * @author LNTGiang
     */
    dateValue(value) {
      this.dateConverted = "";
      this.dateVal = "";
      if (value && isNaN(new Date(value))){
        return;
      }
      try {
        this.dateVal = Format.fomatDate(value, "YYYY-MM-DD");
        this.dateConverted = Format.fomatDate(value, "DD/MM/f");
      } catch (ex) {
        console.log();
      }
    },

    /**
     * Tự động focus
     * @author LNTGiang
     */
    autoFocus(value) {
      if (value) {
        if(this.type == 'text'){
          this.$nextTick(() => this.$refs["input"].focus());
        }
        if(this.type == 'date'){
          this.$nextTick(() => this.$refs["input-date"].focus());
        }
        
      }
    },
  },
  methods: {
    /**
     * Xử lý Khi nhập input date
     * @author LNTGiang
     */
    dateInputChange(event) {
      // Nếu ấn xóa thì tiếp tục
      if (event.key == "Backspace") return;

      // Dừng các sự kiện thêm kí tự vào input
      event.preventDefault();

      // Nếu nhập không phải là số hoặc quá 10 kí tự thì không thay đổi input
      if (
        event.keyCode < 48 ||
        event.keyCode > 57 ||
        this.dateConverted.length === 10
      ) {
        return;
      }

      // Các ngày trong từng tháng
      let dateArr = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

      // Thêm kí tự vừa nhập vào chuỗi
      this.dateConverted = this.dateConverted.concat(event.key);

      // Kiểm tra ngày ngày không được vượt quá 31 và tháng k vượt quá 12

      // Vị trí và giá trị tối đa của kí tự đầu trong ngày và tháng
      let arr = [
        { pos: 0, maxVal: 3 },
        { pos: 3, maxVal: 1 },
      ];

      // Khi nhập xong ngày hoặc tháng tự động thêm dấu '/'
      if (this.dateConverted.length === 2 || this.dateConverted.length === 5) {
        this.dateConverted = this.dateConverted.concat("/");
      }

      arr.forEach((element) => {
        // kiểm tra kí tự đầu trong ngày không vượt quá 3 và tháng không vượt quá 1
        let num = parseInt(this.dateConverted[element.pos]);
        if (
          isNaN(num) ||
          parseInt(this.dateConverted[element.pos]) > element.maxVal
        ) {
          this.dateConverted = this.dateConverted.substring(
            0,
            Math.max(element.pos, 0)
          );
        }

        // Kiểm tra ngày không quá 31 và tháng không quá 12
        let val = this.dateConverted.substring(element.pos, element.pos + 2);
        if (
          isNaN(val) ||
          (element.pos === 0 && val > 31) ||
          (element.pos === 3 && val > 12)
        ) {
          this.dateConverted = this.dateConverted.substring(
            0,
            Math.max(element.pos + 1, 0)
          );
        }
      });

      // Nếu nhập đủ ngày tháng năm
      if (this.dateConverted.length === 10) {
        // Lấy ra từng giá trị ngày tháng năm
        let day = this.dateConverted.substring(0, 2);
        let month = this.dateConverted.substring(3, 5);
        let year = this.dateConverted.substring(6, 10);

        let date = `${year}-${month}-${day}`;

        // Nếu là tháng 2 thì phải kiểm tra năm đó phải năm nhuận không
        if (month == "02") {
          let tmp = parseInt(year);
          if ((tmp % 4 === 0 && tmp % 100 != 0) || tmp % 400 == 0) {
            dateArr[1] += 1;
          }
        }

        // kiểm tra ngày nhập có vượt quá số ngày trong tháng đó không
        if (day > dateArr[parseInt(month - 1)]) {
          this.dateConverted = this.dateConverted.substring(0, 9);
          return;
        }

        // Check ngày nhập không được lớn hơn ngày hiện tại
        if(new Date(date) > new Date()){
          this.dateConverted = this.dateConverted.substring(0, 9);
          return;
        }

        // Gán lại giá trị về input date và trả ra cho bên ngoài
        this.dateVal = date;
        this.$emit("onChange", new Date(this.dateVal));
        return;
      }
    },
    /**
     * Khi value date thay đổi
     * @author LNTGiang
     */
    onchangeDate() {
      if (isNaN(new Date(this.dateVal))) return;
      this.dateConverted = Format.fomatDate(this.dateVal, "DD/MM/YYYY");
      this.$emit("onChange", new Date(this.dateVal));
    },
    /**
     * Sự kiện thay đổi value
     * @author LNTGiang
     */
    onKeyup() {
      this.$emit("onKeyup", this.val);
    },
    /**
     * Sự kiện blur
     * @author LNTGiang
     */
    onBlur() {
      if (this.required) {
        if (!this.val) {
          this.$refs["input"].classList.add("ms-input-error");
          this.$refs["input"].setAttribute("title", this.titleError);
        } else {
          this.$refs["input"].classList.remove("ms-input-error");
          this.$refs["input"].setAttribute("title", "");
        }
      }
    },
  },
};
</script>

<style scoped>
@import url("@/style/components/input.css");
</style>
