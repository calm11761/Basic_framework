using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Basis__Common
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public ISqlSugarClient Db { get; }

        public BaseRepository(ISqlSugarClient db)
        {
            Db = db;
        }

        // Query Methods
        public TEntity QueryDataById(object id)
        {
            return Db.Queryable<TEntity>().InSingle(id);
        }

        public async Task<TEntity> QueryDataByIdAsync(object id)
        {
            return await Db.Queryable<TEntity>().InSingleAsync(id);
        }

        public List<TEntity> QueryData(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Db.Queryable<TEntity>().Where(whereExpression).ToList();
        }

        public async Task<List<TEntity>> QueryDataAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Db.Queryable<TEntity>().Where(whereExpression).ToListAsync();
        }

        // Add Methods
        public int AddData(TEntity entity)
        {
            return Db.Insertable(entity).ExecuteCommand();
        }

        public async Task<int> AddDataAsync(TEntity entity)
        {
            return await Db.Insertable(entity).ExecuteCommandAsync();
        }

        public int AddData(List<TEntity> listEntity)
        {
            return Db.Insertable(listEntity).ExecuteCommand();
        }

        public async Task<int> AddDataAsync(List<TEntity> listEntity)
        {
            return await Db.Insertable(listEntity).ExecuteCommandAsync();
        }

        // Update Methods
        public bool UpdateData(TEntity entity)
        {
            return Db.Updateable(entity).ExecuteCommand() > 0;
        }

        public async Task<bool> UpdateDataAsync(TEntity entity)
        {
            return await Db.Updateable(entity).ExecuteCommandAsync() > 0;
        }

        // Delete Methods
        public bool DeleteDataById(object id)
        {
            return Db.Deleteable<TEntity>().In(id).ExecuteCommand() > 0;
        }

        public async Task<bool> DeleteDataByIdAsync(object id)
        {
            return await Db.Deleteable<TEntity>().In(id).ExecuteCommandAsync() > 0;
        }

        public bool DeleteData(TEntity entity)
        {
            return Db.Deleteable(entity).ExecuteCommand() > 0;
        }

        public async Task<bool> DeleteDataAsync(TEntity entity)
        {
            return await Db.Deleteable(entity).ExecuteCommandAsync() > 0;
        }
    }
    }
