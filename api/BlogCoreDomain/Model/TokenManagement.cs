using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreDomain.Model
{
    /// <summary>
    /// jwt令牌
    /// </summary>
   public class TokenManagement
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }
        /// <summary>
        /// 发行人
        /// </summary>
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// 读者，听众
        /// </summary>
        [JsonProperty("audience")]
        public string Audience { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}
