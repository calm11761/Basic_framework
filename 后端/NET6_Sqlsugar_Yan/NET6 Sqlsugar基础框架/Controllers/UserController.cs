using Basis__Common;
using Basis_IService;
using Basis_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;
using Basis_DTO;
using Dm.filter;



namespace NET6_Sqlsugar基础框架.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController :BaseController<IUserService>
    {
        private readonly IPostsService _postsService;//允许额外注入多个服务层
        public UserController(IUserService userService,IPostsService postsService) :base(userService)
        {
                _postsService = postsService;
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GenerateCaptcha()
        {
            //var (imageBytes, code) = Service.GenerateCaptchaImage(4);
            //HttpContext.Session.SetString("CaptchaCode", code); // 存入 Session

            //return File(imageBytes, "image/png");
            var (imageBytes, code) = Service.GenerateCaptchaImage();
            HttpContext.Session.SetString("CaptchaCode", code);
            return File(imageBytes, "image/png");
        }

     

        [HttpPost]
        public async Task<WebResponseContent> Login([FromBody] LoginDto loginDto)
        {
            return await Service.Login(loginDto);
        }


        [HttpPost,Authorize]
        public WebResponseContent AddUser([FromBody] Dt_User user)
        {
            return Service.AddUser(user);
        }



    }
}
