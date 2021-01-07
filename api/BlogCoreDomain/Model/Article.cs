using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCoreDomain.Model
{
    /// <summary>
    /// 文章类
    /// </summary>
    public class Article:BaseModel
    {
        /// <summary>
        /// 文章模型的构造函数
        /// </summary>
        public Article()
        {
            Comments = new List<Comment>();
        }
        /// <summary>
        ///  标题
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int ViewsCount { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// 评论总数
        /// </summary>
        public int CommontCount { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime PostDate { get; set; }
        /// <summary>
        /// 分类Id
        /// </summary>
        public int ClassifyId { get; set; }
        /// <summary>
        /// 分类导航，一对多
        /// </summary>
        public Classify Classify { get; set; }
        /// <summary>
        /// 发表文章的用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户导航，一对多
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 文章评论导航，多对一
        /// </summary>
        public ICollection<Comment> Comments { get; set; }  
    }
}
