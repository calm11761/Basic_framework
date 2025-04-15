import request from "../../utils/request";

// 把 enum 改为普通对象
const API = {
  Login_URL: '/Login/LoginFuor/LoginRequest'
};

// 不能加类型注解了
export const Login = (Email, Password) => {
  return request.post(API.Login_URL, { Email, Password });
};
