<template>
  <div class="m-navbar">
      <router-link class="m-menu-item" v-for="(item, index) in menuItem" :key="index" :to="item.href">
          <div class="menu-item-icon" :class="item.icon"></div>
          <div class="menu-item-text">{{item.name}}</div>
      </router-link>
  </div>
</template>

<script>
export default {
    name: 'MSNavbar',
    data(){
        return {
            menuItem: [
                {
                    icon: 'far fa-file',
                    href: '/material',
                    name: 'Nguyên vật liệu'
                }
            ]
        }
    }
}
</script>

<style>
@import url("../../style/layout/navbar.css");
</style>