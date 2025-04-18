import axios from "axios";
import { ElMessage } from "element-plus";

// 创建 axios 实例
const request = axios.create({
 baseURL: '/api',  // 统一的基础路径，所有请求都会加上这个前缀
  timeout: 5000,  // 请求超时设置
  withCredentials: true // 让浏览器请求时自动携带 Cookie（包括 .NET Core 的 ASP.NET_SessionId），让 Session 成功绑定。
});

// 请求拦截器
request.interceptors.request.use((config) => {
  // 请求头中添加公共参数
  config.headers['Cache-Control'] = 'no-cache';  // 禁止缓存
  config.headers['Pragma'] = 'no-cache';  // HTTP/1.0 兼容
  return config;
});

// 响应拦截器
request.interceptors.response.use(
  (response) => {
    return response.data;  // 简化返回数据，直接返回数据内容
  },
  (error) => {
    let status = error.response?.status;
    switch (status) {
      case 404:
        ElMessage({ type: 'error', message: '请求失败路径出现问题' });
        break;
      case 500:
      case 501:
      case 502:
      case 503:
      case 504:
        ElMessage({ type: 'error', message: '服务器挂了' });
        break;
      case 401:
        ElMessage({ type: 'error', message: '参数有误' });
        break;
      default:
        ElMessage({ type: 'error', message: '请求失败' });
    }
    return Promise.reject(new Error(error.message));
  }
);

export default request;
