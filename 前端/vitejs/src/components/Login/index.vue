<template>
  <div class="box">
    <div class="forum-title" style="color: #4caf50"></div>
    <div class="login-container">
      <h2>欢迎您的登录</h2>
      <form id="login-form" @submit.prevent="handleSubmit">
        <div class="input-group">
          <label for="email" style="color: white">账号</label>
          <input type="text" id="email" v-model="Email" required />
        </div>
        <div class="input-group">
          <label for="password" style="color: white">密码</label>
          <input type="password" id="password" v-model="PassWord" required />
        </div>

        <!-- ✅ 验证码输入区域 -->
        <div class="input-group captcha-group">
          <label for="captcha" style="color: white">验证码</label>
          <div class="captcha-wrapper">
            <input type="text" id="captcha" v-model="captchaInput" required />
            <img :src="captchaSrc" alt="验证码" @click="refreshCaptcha" />
          </div>
        </div>

        <div class="input-group">
          <input
            type="submit"
            value="登录"
            style="width: 350px; margin-left: 5px"
          />
        </div>
      </form>
    </div>
  </div>
</template>

  <script setup>
  import axios from 'axios';
import { ref, onMounted } from "vue";
import { Login } from "../../api";
import router from "../../router";
import { useUserStore } from "../../stores";
import { ElMessage, resultProps } from "element-plus";

const Email = ref("");
const PassWord = ref("");
const captchaInput = ref("");
const loading = ref(false);

//存储用户信息
const stores = useUserStore();

// captchaSrc 初始为空
const captchaSrc = ref("");

// 页面加载时生成验证码
onMounted(() => {
  refreshCaptcha(); // 一进来就生成验证码
});


const refreshCaptcha = async () => {
  const response = await axios.get(`api/User/GenerateCaptcha?time=${Date.now()}`, {
    responseType: "blob"
  });

  captchaSrc.value = URL.createObjectURL(response.data); // 或 base64 替代
};
//登入
async function handleSubmit() {
  loading.value = true;
  try {
    const result = await Login(PassWord.value, Email.value, captchaInput.value);
    console.log("result:", result);

    if (result.status === true) {
      stores.setUserInfo(result.data.token, result.data.userName);
      router.push("/Popupframe");
    } else {
      ElMessage.error(result.message || "账号或密码错误");
    }
  } catch (error) {
    console.error("登入错误:", error);
    ElMessage.error("登录失败，请检查网络或后端服务");
  } finally {
    loading.value = false;
  }
}


</script>
  

<style lang="scss" scoped>
.box {
  font-family: Arial, sans-serif;
  margin: 0;
  padding: 0;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100vh;
  width: 100%;
  background-image: url("../../assets/image/wucat.jpg");
  background-size: cover;
  animation: fadeInBackground 3s ease-in-out; /* 背景图片淡入 */
}

@keyframes fadeInBackground {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

.forum-title {
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  width: 100%;
  font-size: 24px;
  margin-bottom: 20px;
}

.login-container {
  width: 400px;
  padding: 16px;
  background-color: #12494f;
  border: 1px solid #12494f;
  border-radius: 4px;
  display: flex;
  flex-direction: column;
  align-items: center;
  animation: slideIn 0.5s ease-out; /* 登录表单弹入 */
}

@keyframes slideIn {
  from {
    transform: translateY(-50px);
    opacity: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
}

.login-container h2 {
  text-align: center;
  margin-bottom: 24px;
  color: orange;
}

.login-container form .input-group {
  margin-bottom: 20px;
  padding: 0 20px;
}

.login-container form .input-group input {
  width: 100%;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
  background-color: #256a71;
  color: white;
  transition: box-shadow 0.3s; /* 聚焦效果 */
}

.login-container form .input-group input:focus {
  box-shadow: 0 0 10px rgba(255, 255, 255, 0.7);
  border-color: orange;
  outline: none;
}

.login-container form .input-group input[type="submit"] {
  background-color: orange;
  color: white;
  cursor: pointer;
  transition: transform 0.3s; /* 悬停效果 */
}

.login-container form .input-group input[type="submit"]:hover {
  transform: scale(1.05);
  background-color: #45a049;
}

.login-container .links {
  width: 100%;
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
}

.login-container .links a {
  color: white;
  text-decoration: none;
  position: relative;
  font-weight: bold;
  transition: color 0.3s ease; /* 文本颜色变化 */
}

.login-container .links a:hover {
  color: orange; /* 悬停时文本颜色变化 */
}

.login-container .links a::after {
  content: "";
  position: absolute;
  width: 0%;
  height: 2px;
  bottom: -4px;
  left: 0;
  background-color: orange;
  transition: width 0.3s ease; /* 下划线动画 */
}

.login-container .links a:hover::after {
  width: 100%;
}

.login-container select {
  background-color: #0c5671;
  color: white;
}

img {
  width: 25px;
}

.captcha-group {
  .captcha-wrapper {
    display: flex;
    align-items: center;
    gap: 10px;

    input {
      flex: 1;
      padding: 8px;
      border: 1px solid #ddd;
      border-radius: 4px;
      background-color: #256a71;
      color: white;
    }

    img {
      width: 100px;
      height: 36px;
      cursor: pointer;
      border: 1px solid #ccc;
      border-radius: 4px;
    }
  }
}
</style>