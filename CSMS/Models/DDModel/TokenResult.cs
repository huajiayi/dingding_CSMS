using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractStatementManagementSystem
{
    public class TokenResult
    {
            /// <summary>  
            /// 错误码  
            /// </summary>  
            public ErrCodeEnum ErrCode { get; set; } = ErrCodeEnum.Unknown;

            /// <summary>  
            /// 错误消息  
            /// </summary>  
            public string ErrMsg { get; set; }

            public string Access_token { get; set; }
        }
    public enum ErrCodeEnum
    {
        OK = 0,

        VoildAccessToken = 40014,

        /// <summary>  
        /// 未知  
        /// </summary>  
        Unknown = int.MaxValue
    }
}