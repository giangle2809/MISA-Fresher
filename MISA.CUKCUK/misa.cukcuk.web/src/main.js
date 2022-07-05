import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Toaster from '@meforma/vue-toaster'
import DatePicker from 'vue-datepicker-next';
import 'vue-datepicker-next/index.css';
import 'vue-datepicker-next/locale/vi';
// import components

import MSHeader from '@/components/layout/MSHeader'
import MSNavbar from '@/components/layout/MSNavbar'
import MSButton from '@/components/base/MSButton'
import MSGird from '@/components/base/MSGird'
import MSMessageBox from '@/components/base/MSMessageBox'
import MSCombobox from '@/components/base/MSCombobox'
import MSDropdown from '@/components/base/MSDropdown'
import MSLoading from '@/components/base/MSLoading'
import { ToastTime } from './utils/constant'

const app = createApp(App);

app
    .component('ms-header', MSHeader)
    .component('ms-navbar', MSNavbar)
    .component('ms-button', MSButton)
    .component('ms-gird', MSGird)
    .component('ms-message-box', MSMessageBox)
    .component('ms-select', MSCombobox)
    .component('ms-dropdown', MSDropdown)
    .component('ms-loading', MSLoading)
    .component('date-picker', DatePicker)

app.use(router);
app.use(VueAxios, axios);
app.use(Toaster, {
    position: 'top',
    duration: ToastTime
});
app.mount('#app');