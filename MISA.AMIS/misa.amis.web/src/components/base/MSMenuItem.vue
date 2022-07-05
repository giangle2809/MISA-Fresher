<template>
  <router-link class="ms-menu-item" :to="item.url">
    <div :class="['ms-menu-icon', 'ms', item.icon]"></div>
    <div class="ms-menu-title">{{ item.text }}</div>
  </router-link>
</template>

<script>
export default {
  name: "MSMenuItem",
  props: ["item"],
};
</script>

<style scpoed>
.ms-menu-item {
  display: flex;
  height: 42px;
  padding: 0 14px 0 8px;
  border-left: 6px solid transparent;
  align-items: center;
  cursor: pointer;
}

.ms-menu-item:hover {
  background-color: #ebedf02f;
}

.ms-menu-item .ms-menu-icon {
  width: 24px;
  height: 24px;
  margin-right: 8px;
}

.ms-menu-item .ms-menu-title{
  color: #fff ;
}

.router-link-exact-active{
  border-left-color: #2ca01c;
  font-weight: 700;
  background-color: #ebedf02f;
}
</style>
