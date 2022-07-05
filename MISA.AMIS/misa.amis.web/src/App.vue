<template>
  <div class="ms-container">
    <TheNavbar></TheNavbar>
    <div class="ms-main">
      <TheHeader></TheHeader>
      <TheContent>
        
      </TheContent>
    </div>
  </div>
</template>

<script>
import TheHeader from './components/layout/TheHeader.vue'
import TheContent from './components/layout/TheContent.vue'
import TheNavbar from './components/layout/TheNavbar.vue'

export default {
  name: 'App',
  components: {
    TheHeader,
    TheContent, 
    TheNavbar
  },

}
</script>

<style>
  @import url('./style/layout/main.css');
  @import url('./style/components/icons.css');
</style>
