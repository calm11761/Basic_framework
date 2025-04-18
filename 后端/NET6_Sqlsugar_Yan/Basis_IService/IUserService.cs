using Basis__Common;
using Basis_DTO;
using Basis_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basis_IService
{
    public interface IUserService
    {
        WebResponseContent AddUser(Dt_User user);

        Task<WebResponseContent> Login(LoginDto loginDto);


        /// <summary>
        /// 生成验证码图片字节流
        /// </summary>
        /// <param name="codeLength">验证码长度</param>
        /// <returns>字节数组和验证码字符串</returns>
        (byte[] ImageBytes, string Code) GenerateCaptchaImage(int codeLength = 4);

    }
}
