<template>
  <div
    class="ms-dialog"
    :style="[!isShow ? 'display: none' : 'display: flex']"
  >
    <div class="ms-dialog-content">
      <div class="ms-dialog-items flex">
        <div
          class="ms ms-icon-help ms-icon-24"
        ></div>
        <div
          class="ms ms-icon-close ms-icon-24"
          @click="closeDialog"
          title="Đóng(ESC)"
        ></div>
        
      </div>
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "MSDialog",
  props: ["isShow"],
  methods: {
    /**
     * Sự kiện đóng dialog
     * @author LNTGiang
     */
    closeDialog() {
      this.$emit("closeDialog");
    },
  },
};
</script>

<style scoped>
@import url("@/style/components/dialog.css");
</style>
