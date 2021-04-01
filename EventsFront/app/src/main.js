import Vue from 'vue'
import App from './App.vue'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import VueRouter from 'vue-router'

import Home from './components/Home.vue';
import EventsTable from './components/EventsTable.vue';
import Chart from './components/Chart.vue';

Vue.config.productionTip = false

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(VueRouter)

const router = new VueRouter({
  routes: [
    {
      path: '/',
      component: Home
    },
    {
      path: '/tabela',
      component: EventsTable
    },
    {
      path: '/grafico',
      component: Chart
    }
  ]
})

new Vue({
  render: h => h(App),
  router
}).$mount('#app')
