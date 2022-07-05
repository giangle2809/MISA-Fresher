<template>
  <div
    class="ms-popup-container"
    :style="[isShow ? 'display: flex' : 'display: none']"
  >
    <div class="ms-popup-message">
      <div class="ms-mess-content">
        <div class="ms ms-icon-48" :class="status" style="flex-shrink: 0;"></div>
        <div class="ms-mess-text">{{ message }}</div>
      </div>
      <div class="ms-mess-line"></div>
      <div class="ms-mess-footer">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script>
import Constant from "@/utils/constant";

export default {
  name: "MSPopupMessage",
  data() {
    return { messageStatus: Constant.MessageBoxStatus };
  },
  props: ["message", "isShow", "status"],
};
</script>

<style scoped>
@import url('@/style/components/messagebox.css');
</style>
