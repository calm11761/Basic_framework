using Basis__Common;
using Basis__Common.AutomaticallyCreateDatabasesAndTables;
using Basis_IService;
using Basis_Model.Models;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using Basis_DTO;



namespace Basis_Service
{
    public class UserService : BaseService,IUserService
    {
        private readonly IPostsService _postsService;
        private readonly JwtHelper _jwtHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(ISqlSugarClient db, IPostsService postsService, JwtHelper jwtHelper, IHttpContextAccessor httpContextAccessor) : base(db)
        {
            _postsService = postsService;//引用post服务层
            _jwtHelper = jwtHelper;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public  (byte[] ImageBytes, string Code) GenerateCaptchaImage(int codeLength = 4)
        {
            string code = GenerateRandomCode(codeLength);

            using var image = CreateCaptchaImage(code);
            using var ms = new MemoryStream();
            image.SaveAsPng(ms);
            return (ms.ToArray(), code);
        }

        private static string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private  Image<Rgba32> CreateCaptchaImage(string code)
        {
            int width = 120, height = 40;
            var image = new Image<Rgba32>(width, height);

            image.Mutate(ctx =>
            {
                ctx.BackgroundColor(SixLabors.ImageSharp.Color.White);

                var font = SixLabors.Fonts.SystemFonts.CreateFont("Arial", 24, SixLabors.Fonts.FontStyle.Bold);
                ctx.DrawText(code, font, SixLabors.ImageSharp.Color.DarkGreen, new SixLabors.ImageSharp.PointF(10, 5));

                var rnd = new Random();
                for (int i = 0; i < 5; i++)
                {
                    var p1 = new SixLabors.ImageSharp.PointF(rnd.Next(width), rnd.Next(height));
                    var p2 = new SixLabors.ImageSharp.PointF(rnd.Next(width), rnd.Next(height));
                    ctx.DrawLine(SixLabors.ImageSharp.Color.Gray, 1, new[] { p1, p2 });
                }
            });

            return image;
        }



        /// <summary>
        /// 登入接口，如果你要查看token，请先调用GenerateCaptchaImage接口拿到验证码再登入
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<WebResponseContent> Login(LoginDto loginDto)
        {
            var sessionCaptcha = _httpContextAccessor.HttpContext?.Session.GetString("CaptchaCode");
            Console.WriteLine("Session验证码: " + sessionCaptcha);  // 打印验证码

            if (string.IsNullOrEmpty(sessionCaptcha) || !sessionCaptcha.Equals(loginDto.InputCaptcha, StringComparison.OrdinalIgnoreCase))
            {
                return new WebResponseContent().Error("验证码错误");
            }

            var user = await Db.Queryable<Dt_User>()
                                .Where(u => u.Account == loginDto.Request.UserName && u.Password == loginDto.Request.Password)
                                .FirstAsync();

            if (user == null)
                return new WebResponseContent { Status = false, Message = "登入失败没有找到该用户" };

            var token = _jwtHelper.GenerateToken(user.Id.ToString(), user.DisplayName ?? user.Account);

            return new WebResponseContent
            {
                Status = true,
                Data = new
                {
                    Token = token,
                    UserId = user.Id,
                    UserName = user.Account, //账号}}

                }
            };
        }
            
            


        public WebResponseContent AddUser(Dt_User user)
        {
            try
            {
                var baseuser = Db.Insertable(user).ExecuteReturnEntity(); ;
                if (baseuser == null)
                {
                    return new WebResponseContent { Status = false, Message = "添加失败" };
                }
                return new WebResponseContent { Status = true, Data = baseuser };
            }
            catch (Exception ex)
            {

                return new WebResponseContent { Status = false, Message = ex.Message };
            }
        }

      


        /// <summary>
        /// 可以在这里使用其他的服务层的方法，只要注入了就可以
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public WebResponseContent PostandUser(Dt_User user)
        {
            var posts = _postsService.GetAllPosts(); 

            return new WebResponseContent { Status = true, Data = posts };
        }
    }
}
