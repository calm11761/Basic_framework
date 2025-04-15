using Basis__Common;
using Basis_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basis_IService
{
    public interface IPostsService
    {

        List<Dt_Posts> GetAllPosts();
    }
}
