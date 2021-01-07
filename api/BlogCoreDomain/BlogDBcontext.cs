using System;
using BlogCoreDomain.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogCoreDomain
{
    public class BlogDBContext : DbContext
    {
        /// <summary>
        /// 通过依赖注入，注册数据库操作对象
        /// </summary>
        /// <param name="options"></param>
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options)
        {
        
        }
        /// <summary>
        /// 用户模型
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// 分类模型
        /// </summary>
        public DbSet<Classify> Classifies { get; set; }
        /// <summary>
        /// 文章模型
        /// </summary>
        public DbSet<Article> Articles { get; set; }
        /// <summary>
        /// 评论模型
        /// </summary>
        public DbSet<Comment> Comments { get; set; }
        /// <summary>
        /// 用户关注模型
        /// </summary>
        public DbSet<Attention> Attentions { get; set; }
    }
}
