using Basis_IService;
using Basis_Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NET6_Sqlsugar基础框架.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //这里没有使用BaseController类
        private readonly IPostsService _postsService;
        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        public List<Dt_Posts> GetAllPosts()
        {
            return _postsService.GetAllPosts();
        }
    }
}
