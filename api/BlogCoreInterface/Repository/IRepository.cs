using BlogCoreDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlogCoreInterface.Repository;

namespace BlogCoreInterface.Repository
{
    #region 仓储接口
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T:BaseModel
    {
        Task<int> Save();
        Task<IRepository<T>> InsertAsync(T model);          //增加单个
        Task<IRepository<T>> InsertAsync(List<T> listModel);  //增加多个
        Task<IRepository<T>> UpdateAsync(T model,params string[] fields);  //更新
        Task<IRepository<T>> DeleteAsync(int id);          // 根据id删除
        Task<IRepository<T>> DeleteAsync(T model);         // 根据model删除

        Task<T> GetEntityById(int id);     // 根据id查询
        Task<List<T>> GetEntityList(Expression<Func<T, bool>> whereLambda); // 根据条件查询，传入一个lambda
    }
    #endregion
}
