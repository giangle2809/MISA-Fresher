<template>
  <ms-header></ms-header>
  <div class="m-main">
    <ms-navbar></ms-navbar>
    <div class="m-content">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>

export default {
  name: 'App',
  components: {
  }
}
</script>

<style>
@import url("./style/layout/main.css");
@import url("./style/base/icon.css");
</style>
