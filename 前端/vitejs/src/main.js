import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
//引入路由
import router from './router'
//引入eml
//引入国际化
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import zhCn from 'element-plus/es/locale/lang/zh-cn'

//引用pinia
import { createPinia } from 'pinia'

const app=createApp(App);

//使用路由
app.use(router)


//使用elm
app.use(ElementPlus)

//国际化
app.use(ElementPlus,{
    locale:zhCn
})

//使用pinia
app.use(createPinia())

app.mount('#app')