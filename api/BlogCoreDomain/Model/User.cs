using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCoreDomain.Model
{
    /// <summary>
    /// 用户模型
    /// </summary>
    [Index("Account", IsUnique = true, Name = "User_IX")]
    public class User : BaseModel
    {
        public User()
        {
            RegDate = DateTime.Now;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telphone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
       
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Pwd { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        public string NikeName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime RegDate { get; set; }
        /// <summary>
        ///  一对多--文章模型，导航
        /// </summary>
        public ICollection<Article> Articles { get; set; } 
    }
}
