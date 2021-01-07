using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreDomain.Model
{
    /// <summary>
    /// 用户关注模型
    /// </summary>
    public class Attention:BaseModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 朋友Id
        /// </summary>
        public int FriendId { get; set; }
        /// <summary>
        /// 用户，导航
        /// </summary>
        public User User { get; set; }
    }
}
