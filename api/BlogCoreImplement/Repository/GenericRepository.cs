using BlogCoreDomain;
using BlogCoreDomain.Model;
using BlogCoreInterface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreImplement.Repository
{
    /// <summary>
    /// 仓储类的实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IRepository<T> where T : BaseModel
    {
        private BlogDBContext _context;     // 数据库操作的一个上下文
        public GenericRepository(BlogDBContext context)
        {
            _context = context;
        }
        public async Task<IRepository<T>> DeleteAsync(int id)       // 根据id删除
        {
            var model = _context.Set<T>().Find(id);
            await Task.Run(()=> {
                _context.Set<T>().Remove(model);
            });
            return this;
        }
        public async Task<IRepository<T>> DeleteAsync(T model)      //  根据模型对象删除
        {
            await Task.Run(() => {
                _context.Set<T>().Remove(model);
            });
            return this;
        }

        public async Task<T> GetEntityById(int id)  // 根据id 获取模型对象
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetEntityList(Expression<Func<T,bool>> whereLambda)  // 根据lambda获取模型对象
        {
            return await Task.Run(()=>_context.Set<T>().AsNoTracking().Where(whereLambda).ToList());
        }

        public async Task<IRepository<T>> InsertAsync(T model)  // 增加
        {
            await _context.Set<T>().AddAsync(model);
            return this;
        }

        public async Task<IRepository<T>> InsertAsync(List<T> listModel)    // 增加多个
        {
            await _context.Set<T>().AddRangeAsync(listModel);
            return this;
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IRepository<T>> UpdateAsync(T model, params string[] fields)  // 传入模型 更新数据
        {
            await Task.Run(()=> {
                _context.Set<T>().Update(model);
            });
            return this;
        }
    }
}
