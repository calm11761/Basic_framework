using Basis__Common;
using Basis_Model.Models;

namespace Basis_IService
{
    public interface IUserService
    {
        WebResponseContent AddUser(Dt_User user);
    }
}
