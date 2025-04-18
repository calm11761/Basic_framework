using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Basis__Common
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 这个封装的没有使用到，如果你想自己重写封装也是可以的，Repository类库不要进行删除
        /// Service层进行了项目引用
        /// </summary>
        ISqlSugarClient Db { get; }
        // 查找方法
        TEntity QueryDataById(object id);
        Task<TEntity> QueryDataByIdAsync(object id);
        List<TEntity> QueryData(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> QueryDataAsync(Expression<Func<TEntity, bool>> whereExpression);

        // 添加
        int AddData(TEntity entity);
        Task<int> AddDataAsync(TEntity entity);
        int AddData(List<TEntity> listEntity);
        Task<int> AddDataAsync(List<TEntity> listEntity);

        // 更新方法
        bool UpdateData(TEntity entity);
        Task<bool> UpdateDataAsync(TEntity entity);

        // 删除方法
        bool DeleteDataById(object id);
        Task<bool> DeleteDataByIdAsync(object id);
        bool DeleteData(TEntity entity);
        Task<bool> DeleteDataAsync(TEntity entity);
    }
}
