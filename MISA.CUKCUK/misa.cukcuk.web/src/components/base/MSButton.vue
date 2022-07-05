<template>
  <div class="m-button" :class="[classBtn, isDisable? 'm-btn-disabled': '']">
      <div class="m-btn-icon" :class="icon"></div>
      <div class="m-btn-text"><slot></slot></div>
  </div>
</template>

<script>
export default {
    name: 'MS_Button',
    props: ['classBtn', 'icon', 'isDisable']
}
</script>

<style>
@import url('../../style/base/button.css');
</style>