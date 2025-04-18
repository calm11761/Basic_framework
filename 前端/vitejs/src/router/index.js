import { createRouter,createWebHistory } from "vue-router";

//暴露路由
export default createRouter({
//路由模式设计
history:createWebHistory(),
//管理路由
routes:[
  {
    path: "/Popupframe",
    component: () => import('../components/Popupframe/index.vue')
  },
   {
    path: "/",
    component: () => import('../components/Login/index.vue')
  },
 
]

})

