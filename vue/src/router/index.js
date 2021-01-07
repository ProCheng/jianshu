import Vue from 'vue'
import VueRouter from 'vue-router'

// 主页
import home_Index from '@/views/home/index.vue'

// 用户登录注册
import user_In_Up from '@/views/user/user_in_up'
// 用户登录
import sign_In from '@/components/user/sign_in_up/sign_in'
// 用户注册
import sign_Up from '@/components/user/sign_in_up/sign_up'

// 用户主页
import personal_Homepage from '@/views/user/personal_homepage'

// 文章详情
import article_Info from '@/views/article/article_info'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    component: home_Index
  },
  {
    path: '/user_In_Up',
    component: user_In_Up,
    children: [
      { path: 'sign_in', component: sign_In },
      { path: 'sign_up', component: sign_Up }
    ]
  },
  {
    path: '/personal_Homepage',
    component: personal_Homepage
  },
  {
    path: '/article_Info',
    component: article_Info
  }
]

const router = new VueRouter({
  routes
})
// // 挂载路由导航守卫
// router.beforeEach((to, from, next) => {
//   // to 将要访问的路径
//   // from 代表从哪个路径跳转而来
//   // next 是一个函数，表示放行
//   // next() 放行    next('/Login')  强制跳转
//   if (to.path === '/login') return next()
//   // 获取token
//   const tokenStr = window.sessionStorage.getItem('token')
//   if (!tokenStr) return next('/login')
//   next()
// })
export default router
