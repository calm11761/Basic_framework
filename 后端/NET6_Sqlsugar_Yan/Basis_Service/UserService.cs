using Basis__Common;
using Basis_IService;
using Basis_Model.Models;
using SqlSugar;

namespace Basis_Service
{
    public class UserService : BaseService,IUserService
    {
        private readonly IPostsService _postsService;
        public UserService(ISqlSugarClient db, IPostsService postsService) : base(db)
        {
            _postsService=postsService;//引用post服务层
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

        public WebResponseContent PostandUser(Dt_User user)
        {
            var posts = _postsService.GetAllPosts(); 

            return new WebResponseContent { Status = true, Data = posts };
        }
    }
}
