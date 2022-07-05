<template>
  <div class="m-messagebox-container" :style="[isShow ? 'display:flex' : 'display:none']">
    <div class="m-messagebox">
      <div class="m-messagebox-header">
        <div>CUKCUK - Phần mềm quản lý nhà hàng</div>
        <div class="m-space"></div>
        <div class="m-detail-header-item" @click="closeMessageBox">
          <i class="fas fa-times"></i>
        </div>
      </div>
      <div class="m-messagebox-body">
        <div class="m-messagebox-content">{{message}}</div>
        <div class="m-messagebox-footer">
          <div class="m-space"></div>
          <ms-button
            :classBtn="'m-btn-secondary'"
            :icon="'mi mi-16 mi-accept'"
            style="margin-right: 6px"
            @click="btnOkOnClick"
            >Đồng ý</ms-button
          >
          <ms-button
            :classBtn="'m-btn-secondary'"
            :icon="'mi mi-16 mi-cancel-arrow'"
            @click="closeMessageBox"
            >Hủy bỏ</ms-button
          >
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MSMessageBox",
  emits: ["close", "ok"],
  props: ["isShow", 'message'],
  methods: {
    /**
     * Đóng messagebox
     * @author: LNTGiang
     * Created date: 14:33 01/04/2022
     */
    closeMessageBox() {
      this.$emit("close");
    },
    /**
     * Click vào button đồng ý
     * @author: LNTGiang
     * Created date: 14:34 01/04/2022
     */
    btnOkOnClick(){
        this.$emit('ok');
    },
  },
};
</script>

<style scoped>
@import url("../../style/base/messagebox.css");
</style>
