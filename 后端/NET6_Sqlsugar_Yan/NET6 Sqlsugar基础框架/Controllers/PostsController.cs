using Basis__Common;
using Basis_IService;
using Basis_Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NET6_Sqlsugar基础框架.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController :BaseController<IPostsService>
    {
        public PostsController(IPostsService postsService):base(postsService) 
        {
           
        }

        [HttpGet]
        public List<Dt_Posts> GetAllPosts()
        {
            return Service.GetAllPosts();
        }
    }
}
