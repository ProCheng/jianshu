using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCoreDomain.Model
{
    /// <summary>
    /// 文章分类
    /// </summary>
    public class Classify:BaseModel
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        public string  CName { get; set; }
        /// <summary>
        /// 分类描述
        /// </summary>
        public string  Description { get; set; }
    }
}
