//这个是仓库pinia位置
import { defineStore } from 'pinia';

export const useUserStore = defineStore('user', {
  state: () => ({
    token: '',
    userName: '',
  }),
  actions: {
    // 设置用户信息
    setUserInfo(token, userName) {
      this.token = token;
      this.userName = userName;
    
    },

  }
});
