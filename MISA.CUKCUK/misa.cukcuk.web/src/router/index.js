import { createRouter, createWebHistory } from 'vue-router'
import MaterialList from '@/view/material/MaterialList.vue'

const routes = [
    { path: '/material', component: MaterialList }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router;