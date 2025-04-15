using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basis__Common
{
    /// <summary>
    /// 封装的基础方法 可以不用每一个都写一个这种
    /// </summary>
    public class BaseService
    {
        protected readonly ISqlSugarClient Db;

        public BaseService(ISqlSugarClient db)
        {
            Db = db;
        }
    }
}
