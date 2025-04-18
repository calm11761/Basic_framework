import request from "../utils/request";
import axios from 'axios';

// 把 enum 改为普通对象（适合大型项目）
const API = {
  //登入接口
  Login_URL: 'User/Login',
};


// 不能加类型注解了
export const Login = (passWord, userName, inputCaptcha) => {
  return request.post(API.Login_URL, {
    request: {
      userName,
      passWord
    },
    inputCaptcha
  });
};

//下面这种写法 （小项目的写法）
export const GetPageData = (data) => request.post(`User/Login`, data)
