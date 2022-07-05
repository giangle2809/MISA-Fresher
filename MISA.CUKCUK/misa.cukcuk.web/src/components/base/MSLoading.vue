<template>
  <div class="m-loading" :style="[isShow ? 'display: block' : 'display:none']">
    <div class="m-loading-icon"></div>
  </div>
</template>

<script>
export default {
    name: 'MSLoading',
    props: ['isShow']
};
</script>

<style scoped>
@import url('../../style/base/loading.css');
</style>
