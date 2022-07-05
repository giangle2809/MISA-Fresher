<template>
  <div
    class="ms-btn-box"
    :class="classList"
    :title="title"
  >
    <slot name="icon"></slot>
    <div class="ms-btn">
      {{text}}
    </div>
  </div>
</template>

<script>
export default {
  name: "MSButton",
  props: [
    'classList',
    'title', 
    'text'
  ],
};
</script>

<style scoped>
@import url("@/style/components/button.css");
</style>
