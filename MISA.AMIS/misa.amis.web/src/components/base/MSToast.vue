<template>
  <div class="ms-toast-box" :style="[isShow ? 'display: flex' : 'display: none']">
      <div class="ms-toast-item ms ms-icon-24" :class="status"></div>
      <div class="ms-toast-mess">{{mess}}</div>
      <div class="ms-toast-item ms ms-icon-16 ms-toast-close pointer" @click="close"></div>
  </div>
</template>

<script>
export default {
    name: 'MSToast',
    props: ['mess', 'isShow', 'status'],
    emits: ['close'],
    methods: {
        /**
         * đóng toast
         * @author LNTGiang
         */
        close(){
            this.$emit('close');
        }
    },
}
</script>

<style scoped>
@import url('@/style/components/toast.css');
</style>