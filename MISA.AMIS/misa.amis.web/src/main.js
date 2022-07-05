import { createApp } from 'vue'
import App from './App.vue'
import router from './router';
import axios from 'axios'
import VueAxios from 'vue-axios'

// import base components
import MSButton from "@/components/base/MSButton";
import MSCheckBox from "@/components/base/MSCheckBox";
import MSCombobox from "@/components/base/MSCombobox";
import MSDialog from "@/components/base/MSDialog";
import MSInput from "@/components/base/MSInput";
import MSLoading from "@/components/base/MSLoading.vue";
import MSMenuItem from "@/components/base/MSMenuItem";
import MSPopupMessage from "@/components/base/MSPopupMessage";
import MSRadio from "@/components/base/MSRadio";
import MSTable from "@/components/base/MSTable.vue";
import MSToast from "@/components/base/MSToast.vue";

const app = createApp(App);

app
    .component('ms-button', MSButton)
    .component('ms-checkbox', MSCheckBox)
    .component('ms-combobox', MSCombobox)
    .component('ms-dialog', MSDialog)
    .component('ms-input', MSInput)
    .component('ms-loading', MSLoading)
    .component('ms-menu-item', MSMenuItem)
    .component('ms-popup-message', MSPopupMessage)
    .component('ms-radio', MSRadio)
    .component('ms-table', MSTable)
    .component('ms-toast', MSToast)

app.use(router)
app.use(VueAxios, axios)
app.mount('#app')