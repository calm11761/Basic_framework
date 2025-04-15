import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue()],
  //配置代理跨域
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:7065', // 请求的目标地址 //实际地址
        changeOrigin: true,               // 是否修改请求头中的 Origin 字段
        secure: false,                     // // 禁用 SSL 校验（适用于开发环境）
        // rewrite: (path) => path.replace(/^\/api/, '') // 替换请求路径中的 /api 前缀 那你就需要 启用 rewrite
      }
    }
  }
}
)
