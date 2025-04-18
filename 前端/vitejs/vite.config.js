import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path' // 需要导入 path 模块

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue()],
  //配置代理跨域
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:7094', // 请求的目标地址 //实际地址
        changeOrigin: true,               // 是否修改请求头中的 Origin 字段
        secure: false,                     // // 禁用 SSL 校验（适用于开发环境）
       // rewrite: path => path.replace(/^\/api/, '') // 去掉 /api 前缀
      }
    }
  },
  resolve: {
    alias: {
       '@': path.resolve(__dirname, './src'),

    },
  }
})