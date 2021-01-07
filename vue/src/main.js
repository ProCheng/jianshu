import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from 'axios'
import './plugins/element.js'
// 导入全局样式表
import './assets/css/global.css'
import 'element-ui/lib/theme-chalk/display.css'
// 导入字体图标
import './assets/fonts/iconfont.css'
// 导入加载load
import nprogress from 'nprogress'
import 'nprogress/nprogress.css'

Vue.config.productionTip = false
axios.defaults.baseURL = 'https://localhost:5001'
axios.interceptors.request.use(config => {
  nprogress.start()
  config.headers.token = 'Bearer ' + localStorage.getItem('token')
  return config
})
// axios设置响应拦截器
axios.interceptors.response.use(response => {
  nprogress.done()
  return response.data
}, err => {
  nprogress.done()
  if (err.response) {
    return err.response.data
  }
  return { message: '找不到服务器', state: 404 }
})
Vue.prototype.$http = axios

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
