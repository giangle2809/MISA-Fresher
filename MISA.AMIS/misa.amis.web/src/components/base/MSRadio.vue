<template>
  <label class="ms-radio-container" :tabindex="tabindex">
    <slot></slot>
    <input
      type="radio"
      class="ms-radio"
      :name="radioName"
      :checked="checked"
      
      @change="onChange"
      ref="input"
    />
    <span class="ms-radio-custom"></span>
  </label>
</template>

<script>
export default {
  name: "MSRadio",
  props: ['radioName', 'checked', 'tabindex'],
  methods: {
    /**
     * Sự kiện click chọn
     * @author LNTGiang
     */
    onChange() {
      if (this.$refs["input"].checked) {
        this.$emit("onChange", this.$refs["input"].checked);
      }
    },
  },
};
</script>

<style scoped>
@import url("@/style/components/radio.css");
</style>
