using Basis__Common;
using Basis_IService;
using Basis_Model.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basis_Service
{
    public class PostsService : BaseService, IPostsService
    {
        private readonly IRepository<Dt_Posts> _repository;

        // 通过构造函数注入 IRepository
        public PostsService(ISqlSugarClient db, IRepository<Dt_Posts> repository) : base(db)
        {
            _repository = repository;//这一套只封装了基础的增删改查
        }

        public List<Dt_Posts> GetAllPosts()
        {
            return Db.Queryable<Dt_Posts>().ToList();
        }
    }
}
