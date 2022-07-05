import { createRouter, createWebHistory } from 'vue-router'
import EmployeeList from '@/view/employee/EmployeeList.vue'
import InProgress from '@/view/inprogress/InProgress.vue'

const routes = [
    { path: '/dashboard', component: InProgress },
    { path: '/employee', component: EmployeeList },
    { path: '/bank', component: InProgress },
    { path: "/pu", component: InProgress },
    { path: "/sale", component: InProgress },
    { path: "/invoice", component: InProgress },
    { path: "/stock", component: InProgress },
    { path: "/tools", component: InProgress },
    { path: "/assets", component: InProgress },
    { path: "/tax", component: InProgress },
    { path: "/price", component: InProgress },
    { path: "/general", component: InProgress },
    { path: "/budget", component: InProgress },
    { path: "/report", component: InProgress },
    { path: "/finance", component: InProgress },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router;