<template>
  <label class="ms-checkbox-container">
    <slot></slot>
    <input
      type="checkbox"
      class="ms-checkbox"
      ref="input"
      :checked="checked"
      @change="onChange"
    />
    <span class="ms-checkbox-custom"></span>
  </label>
</template>

<script>
export default {
  name: "MSCheckBox",
  props: ["checked"],
  methods: {
    onChange() {
      this.$emit("onChange", this.$refs["input"].checked);
    },
  },
};
</script>

<style scoped>
@import url("@/style/components/checkbox.css");
</style>
