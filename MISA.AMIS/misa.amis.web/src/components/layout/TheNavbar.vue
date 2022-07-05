<template>
  <div class="ms-navbar">
            <div class="ms-logo-container">
                <div class="ms ms-container-option"></div>
                <div class="ms-logo"></div>
            </div>
            <div class="ms-menu-item-list">
                <ms-menu-item v-for="item in menus" v-bind:key="item.icon" v-bind:item="item" />
            </div>
        </div>
</template>

<script>
import {ref} from 'vue'

export default {
    name: 'TheNavbar',

    data() {
        const menus = ref([
            {
                icon: 'ms-sidebar-dashboard',
                text: 'Tổng quan',
                url: '/dashboard',
            },
            {
                icon: 'ms-sidebar-cash',
                text: 'Tiền mặt',
                url: '/employee',
            },
            {
                icon: 'ms-sidebar-bank',
                text: 'Tiền gửi',
                url: '/bank',
            },
            {
                icon: 'ms-sidebar-pu',
                text: 'Mua Hàng',
                url: '/pu',
            },
            {
                icon: 'ms-sidebar-sale',
                text: 'Bán hàng',
                url: '/sale',
            },
            {
                icon: 'ms-sidebar-invoice',
                text: 'Quản lý đơn hàng',
                url: '/invoice',
            },
            {
                icon: 'ms-sidebar-stock',
                text: 'Kho',
                url: '/stock',
            },
            {
                icon: 'ms-sidebar-tools',
                text: 'Công cụ dụng cụ',
                url: '/tools',
            },
            {
                icon: 'ms-sidebar-fixed-assets',
                text: 'Tài sản cố định',
                url: '/assets',
            },
            {
                icon: 'ms-sidebar-tax',
                text: 'Thuế',
                url: '/tax',
            },
            {
                icon: 'ms-sidebar-price',
                text: 'Giá thành',
                url: '/price',
            },
            {
                icon: 'ms-sidebar-general',
                text: 'Tổng hợp',
                url: '/general',
            },
            {
                icon: 'ms-sidebar-budget',
                text: 'Ngân sách',
                url: '/budget',
            },
            {
                icon: 'ms-sidebar-report',
                text: 'Báo cáo',
                url: '/report',
            },
            {
                icon: 'ms-sidebar-finance',
                text: 'Phân tích tài chính',
                url: '/finance',
            },
        ])

        return{
            menus
        }
    }

}
</script>

<style scoped>
    @import url('../../style/layout/navbar.css');
</style>