using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCoreDomain.Model
{
    /// <summary>
    /// 发表评论模型
    /// </summary>
    public class Comment:BaseModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 文章Id
        /// </summary>
        public int ArticleId { get; set; }
        /// <summary>
        /// 文章
        /// </summary>
        public Article Article { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime CommentDate { get; set; }
        /// <summary>
        /// 父评论
        /// </summary>
        public int? ParentId { get; set; }
    }
}
