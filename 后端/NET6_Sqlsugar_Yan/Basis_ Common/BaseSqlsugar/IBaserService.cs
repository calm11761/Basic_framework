using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basis__Common;


namespace Basis__Common
{
    public interface IBaseService<T> where T : class, new()
    {
        WebResponseContent AddData(T entity);
        WebResponseContent UpdateData(T entity);
        WebResponseContent DeleteData(object id);
        T GetById(object id);
        List<T> GetAll();
    }
}
