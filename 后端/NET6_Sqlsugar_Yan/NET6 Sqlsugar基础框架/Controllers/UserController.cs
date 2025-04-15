using Basis__Common;
using Basis_IService;
using Basis_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NET6_Sqlsugar基础框架.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController :BaseController<IUserService>
    {
        private readonly IPostsService _postsService;//允许额外注入多个服务层
        public UserController(IUserService userService,IPostsService postsService) :base(userService)
        {
                _postsService = postsService;
        }

        [HttpPost, Route("AddUser"), AllowAnonymous]
        public WebResponseContent AddUser([FromBody] Dt_User user)
        {
            return Service.AddUser(user);
        }


    }
}
