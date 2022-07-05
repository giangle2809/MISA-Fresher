<template>
  <div class="ms-comboxbox-container" ref="combobox">
    <input
      type="text"
      v-model="val"
      :disabled="disable"
      :tabindex="tabindex"
      @focus="comboboxOnFocus"
      @blur="comboboxOnBlur"
      @keyup="onKeyup"
      ref="input"
      :style="['width:'+width]"
    />
    <div class="m-cb-close" v-if="isClear && val" @click="clearData"><i class="fas fa-times"></i></div>
    <button class="ms-combobox-icon" @click="comboboxOnClick" ref="cbIcon">
      <i class="fas fa-angle-down"></i>
    </button>
    <div
      class="ms-combobox-expand"
      ref="combobox-expand"
      :class="[showInBottom ? 'expand-in-top' : 'expand-in-bottom']"
      :style="[isExpand ? 'display: block' : 'display: none']"
    >
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "MSCombobox",
  emits: ['onKeyup', 'onBlur', 'clear'],
  data() {
    return { isExpand: false};
  },
  created() {
    window.addEventListener("click", this.windownOnClick);
  },
  beforeUnmount() {
    window.removeEventListener("click", this.windownOnClick);
  },
  props: ["value", "disable", "showInBottom", "required", "autoFocus", "tabindex", 'width', 'isClear'],
  watch: {
    /**
     * Khi thay đổi value
     * @author LNTGiang
     */
    value() {
      this.$refs["combobox"].classList.remove("ms-combobox-error");
    },
    /**
     * Tự động focus
     * @author LNTGiang
     */
    autoFocus(value) {
      if (value) {
        this.$nextTick(() => this.$refs["input"].focus());
      }
    },
  },
  computed:{
    val: {
      get(){
        return this.value;
      },
      set(value){
        this.$emit('onKeyup', value)
      }
    }
  },
  methods: {
    /**
     * Khi Click ra ngoài đóng expand
     * @author LNTGiang
     */
    windownOnClick(event) {
      if (
        !this.$refs["combobox"].contains(event.target) ||
        this.$refs["combobox-expand"].contains(event.target)
      ) {
        this.isExpand = false;
      }
    },
    /**
     * Sự kiện focus border chuyển sang màu xanh
     * @author LNTGiang
     */
    comboboxOnFocus() {
      this.isExpand = true;
      this.$refs["combobox"].classList.add("ms-border-focus");
    },
    /**
     * Sự kiện blur hết focus thì border về màu ban đầu
     * @author LNTGiang
     */
    comboboxOnBlur() {
      setTimeout(() => {
        this.isExpand = false;
      }, 300);
       
      this.$refs["combobox"].classList.remove("ms-border-focus");
      if (this.required) {
        if (!this.value) {
          this.$refs["combobox"].classList.add("ms-combobox-error");
        } else {
          this.$refs["combobox"].classList.remove("ms-combobox-error");
        }
      }
      this.$emit('onBlur')
      
    },
    /**
     * Khi click vào icon
     * @author LNTGiang
     */
    comboboxOnClick() {
      this.isExpand = !this.isExpand;
      if (this.isExpand) {
        this.$nextTick(() => this.$refs["input"].focus());
      }
    },
    /**
    * Xóa dữ liệu đã chọn
    * @author: LNTGiang
    * Created date: 09:26 14/04/2022
    */
    clearData(){
      this.$emit('clear');
    }
  },
};
</script>

<style scoped>
@import url("../../style/base/combobox.css");
</style>
