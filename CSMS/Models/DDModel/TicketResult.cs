using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractStatementManagementSystem
{
    public class TicketResult
    {
        public errCodeEnum errcode { get; set; } = errCodeEnum.Unknown;

        /// <summary>  
        /// 错误消息  
        /// </summary>  
        public string ErrMsg { get; set; }

        public string ticket { get; set; }
        public string expires_in { get; set; }
       
    } public enum errCodeEnum
        {
            OK = 0,

            VoildAccessToken = 45009,

            /// <summary>  
            /// 未知  
            /// </summary>  
            Unknown = int.MaxValue
        }
}