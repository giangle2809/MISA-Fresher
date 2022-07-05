<template>
  <div
    class="ms-loading-box"
    :style="[isShow ? 'display: flex' : 'display: none']"
  >
    <div class="ms-loading-item"></div>
  </div>
</template>

<script>
export default {
  name: "MSLoading",
  props: ["isShow"],
};
</script>

<style scoped>
@import url("@/style/components/loading.css");
</style>
