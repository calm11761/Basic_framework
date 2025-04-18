//using Microsoft.AspNetCore.Mvc;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.PixelFormats;
//using SixLabors.ImageSharp.Processing;
//using SixLabors.ImageSharp.Drawing.Processing;
//using SixLabors.Fonts;
//using System;
//using System.IO;

//namespace NET6_Sqlsugar基础框架.Controllers
//{
//    /// <summary>
//    /// 验证码
//    /// </summary>
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class CaptchaController : ControllerBase
//    {
//        [HttpGet]
//        public IActionResult GenerateCaptcha()
//        {
//            string code = GenerateRandomCode(4);
//            HttpContext.Session.SetString("CaptchaCode", code);

//            using (var ms = new MemoryStream())
//            {
//                using (var image = GenerateCaptchaImage(code))
//                {
//                    image.SaveAsPng(ms);
//                    return File(ms.ToArray(), "image/png");
//                }
//            }
//        }

//        private string GenerateRandomCode(int length)
//        {
//            string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
//            Random random = new Random();
//            return new string(Enumerable.Repeat(chars, length)
//                .Select(s => s[random.Next(s.Length)]).ToArray());
//        }

//        private Image<Rgba32> GenerateCaptchaImage(string code)
//        {
//            int width = 120, height = 40;
//            var image = new Image<Rgba32>(width, height);

//            image.Mutate(ctx =>
//            {
//                ctx.BackgroundColor(SixLabors.ImageSharp.Color.White);  // Set background color to white

//                var font = SystemFonts.CreateFont("Arial", 20, FontStyle.Bold);
//                ctx.DrawText(code, font, SixLabors.ImageSharp.Color.Green, new PointF(10, 5));  // Draw text in green

//                // Adding random lines to make the captcha more complex
//                var rnd = new Random();
//                for (int i = 0; i < 5; i++)
//                {
//                    var p1 = new PointF(rnd.Next(width), rnd.Next(height));
//                    var p2 = new PointF(rnd.Next(width), rnd.Next(height));
//                    ctx.DrawLine(SixLabors.ImageSharp.Color.Gray, 1, new[] { p1, p2 });
//                }
//            });

//            return image;
//        }
//    }
//}
