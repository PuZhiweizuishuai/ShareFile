import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import store from '@/store/index.js'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    children: [
      {
        path: '/',
        name: 'Home',
        component: () => import('@/views/file-browsing.vue'),
        meta: { title: '文件分享服务 - 首页', requireAuth: true }
      },
      {
        path: '/share/link/:id',
        name: 'ShareLink',
        component: () => import('@/views/show-share.vue'),
        meta: { title: '文件分享服务' }
      },
      {
        path: '/filelist',
        name: 'FileList',
        component: () => import('@/views/file-list.vue'),
        meta: { title: '文件列表' }
      },
      {
        path: '/qrcode',
        name: 'QrCode',
        component: () => import('@/views/qr-code.vue'),
        meta: { title: '二维码分享' }
      },
      {
        path: '/setting',
        name: 'Setting',
        component: () => import('@/views/setting.vue'),
        meta: { title: '个性化', requireAuth: true }
      },
      {
        path: '/about',
        name: 'About',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
      },
      {
        path: '/login',
        name: 'Login',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../views/login.vue')
      }
    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  routes
})

router.beforeEach((to, from, next) => {
  // 获取网站基本信息
  
  if (store.state.webInfoStatus) {
    fetch(`/api/web/info`, {
      headers: {
        'Content-Type': 'application/json; charset=UTF-8'
      },
      method: 'GET'
    }).then(response => response.json())
      .then(json => {
        store.state.webInfo = json.data
        store.state.webInfoStatus = false
      })
      // 获取权限
      fetch(`/api/reader/hasAdmin`, {
        headers: {
          'Content-Type': 'application/json; charset=UTF-8'
        },
        method: 'GET'
      }).then(response => response.json())
        .then(json => {
          store.state.hasAdmin = json.data
          sessionStorage.setItem('admin', store.state.hasAdmin)
        })
  }
  if (to.meta.title) {
    document.title = to.meta.title
  }
  
  if (to.meta.requireAuth) {
    if (location.hostname == "127.0.0.1" ||
    location.hostname == "localhost" ||
    location.hostname == "[::1]") {
      return next()
    }
    
    if (sessionStorage.getItem('admin') == 'true') {
      return next()
    }
    return next({
      path: '/login',
      query: { redirect: to.fullPath }
    })
  } else {
    return next()
  }
})

export default router
