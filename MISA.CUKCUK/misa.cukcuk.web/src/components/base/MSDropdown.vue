<template>
  <div class="m-dropdown-container" ref="dropdown" @click="dropdownOnClick">
      {{selectedName}}
      <div class="m-dropdown-expand" ref="dropdown-expand" :style="[isExpand ? 'display: block' : 'display: none']">
          <div 
            class="m-dropdown-item" 
            v-for="(option, index) in options" :key="index" 
            :class="{'ms-dropdown-item-active': selectedValue == option.Value }"
            @click="optionOnClick(option)"
            >
              {{option.Name}}
          </div>
      </div>
  </div>
</template>

<script>
export default {
    name: 'MSDropdown',
    props: ['options', 'selectedValue'],
    emits: ['selected'],
    data(){
        return {
            isExpand: false
        }
    },
    created() {
        window.addEventListener("click", this.windownOnClick);
    },
    beforeUnmount() {
        window.removeEventListener("click", this.windownOnClick);
    },
    computed:{
        /**
        * Hiển thị tên option được chọn
        * @author: LNTGiang
        * Created date: 16:14 05/04/2022
        */
        selectedName: function(){
            var result = ""
            this.options.forEach(element => {
                if(element.Value == this.selectedValue){
                    result = element.Name
                }
            });
            return result
        }
    },
    methods:{
        /**
        * Click ra ngoài đóng expand
        * @author: LNTGiang
        * Created date: 16:20 05/04/2022
        */
        windownOnClick(event) {
        if (
            !this.$refs["dropdown"].contains(event.target) ||
            this.$refs["dropdown-expand"].contains(event.target)
        ) {
            this.isExpand = false;
        }
        },
        /**
        * Khi chọn 1 option trong 
        * @param option được chọn
        * @author: LNTGiang
        * Created date: 16:05 05/04/2022
        */
        optionOnClick(option){
            this.$emit('selected', option);
        },
        /**
        * Khi click vào dropdown
        * @author: LNTGiang
        * Created date: 16:24 05/04/2022
        */
        dropdownOnClick(){
            this.isExpand = !this.isExpand;
        }
        
    }
}
</script>

<style>
@import url('../../style/base/dropdown.css');
</style>