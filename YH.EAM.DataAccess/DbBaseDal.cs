using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Victory.Core.Models;
using YH.EAM.Entity.Enums;
using YH.EAM.Entity.Model;

namespace YH.EAM.DataAccess
{
    public class DbBaseDal<T> where T:class
    {

        #region Async

        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async virtual Task<long> CountAsync()
        {
            var runsql = DBContext.Db().Select<T>();
            return await runsql.CountAsync();
        }

        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async virtual Task<long> InsertAsync(T model)
        {
            var runsql = DBContext.Db().Insert<T>(model);
            return await runsql.ExecuteIdentityAsync();
        }

        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async virtual Task<int> BatchInsertAsync(List<T> models)
        {
            var runsql = DBContext.Db().Insert<T>().AppendData(models);
            return await runsql.ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async virtual Task<bool> UpdateAsync(T model)
        {
            var runsql = DBContext.Db().Update<T>().SetSource(model);
            var rows = await runsql.ExecuteAffrowsAsync();
            return rows > 0;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> DeleteAsync(long id)
        {
            var result = await DBContext.Db().Delete<T>(id).ExecuteAffrowsAsync();
            return result > 0;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(Expression<Func<T, bool>> where)
        {
            var result =  DBContext.Db().Delete<T>().Where(where).ExecuteAffrows();
            return result > 0;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> DeleteAsync(Expression<Func<T, bool>> where)
        {
            var result = await  DBContext.Db().Delete<T>().Where(where).ExecuteAffrowsAsync();
            return result > 0;
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async virtual Task<T> GetByOneAsync(Expression<Func<T, bool>> where)
        {
            return await  DBContext.Db().Select<T>()
                .Where(where).ToOneAsync();
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public async virtual Task<(List<T> list, PageModel page)> QueryAsync(Expression<Func<T, bool>> where, PageModel p=null, List<SortInfo<T, object>> orderbys = null)
        {
            long count;
            var list = DBContext.Db().Select<T>().Where(where).Count(out count);

            if (p!=null)
            {
                list.Page(p.PageIndex, p.PageSize);
                p.TotalCount = (int)count;
                p.ToTalPage = p.TotalCount % p.PageSize > 0 ? p.TotalCount / p.PageSize + 1 : p.TotalCount / p.PageSize;
            }

            if (orderbys!=null)
            {
                foreach (var item in orderbys)
                {
                    list = item.SortMethods == SortEnum.Asc ? list.OrderBy(item.Orderby) : list.OrderByDescending(item.Orderby);
                }
            }
           
            var resultList = await list.ToListAsync();
          
            return (resultList, p);
        }


        #endregion

        #region no Async

        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual long Count()
        {
            var runsql =  DBContext.Db().Select<T>();
            return runsql.Count();
        }

        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual long Insert(T model)
        {
            var runsql =  DBContext.Db().Insert<T>(model);
            return runsql.ExecuteIdentity();
        }

        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int BatchInsert(List<T> models)
        {
            var runsql =  DBContext.Db().Insert<T>().AppendData(models);
            return runsql.ExecuteAffrows();
        }

        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Update(T model)
        {
            var runsql =  DBContext.Db().Update<T>().SetSource(model);
            var rows = runsql.ExecuteAffrows();
            return rows > 0;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(long id)
        {
            var result =  DBContext.Db().Delete<T>(id).ExecuteAffrows();
            return result > 0;
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual T GetByOne(Expression<Func<T, bool>> where)
        {
            return  DBContext.Db().Select<T>()
                .Where(where).ToOne();
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public virtual (List<T> list, PageModel page) Query(Expression<Func<T, bool>> where, PageModel p = null, List<SortInfo<T, object>> orderbys = null)
        {
            long count;
            var list = DBContext.Db().Select<T>().Where(where).Count(out count);

            if (p != null)
            {
                list.Page(p.PageIndex, p.PageSize);
                p.TotalCount = (int)count;
                p.ToTalPage = p.TotalCount % p.PageSize > 0 ? p.TotalCount / p.PageSize + 1 : p.TotalCount / p.PageSize;
            }

            if (orderbys != null)
            {
                foreach (var item in orderbys)
                {
                    list = item.SortMethods == SortEnum.Asc ? list.OrderBy(item.Orderby) : list.OrderByDescending(item.Orderby);
                }
            }

            var resultList = list.ToList();
            return (resultList, p);
        }
        #endregion

    }
}
