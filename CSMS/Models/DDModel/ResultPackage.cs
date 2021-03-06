﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractStatementManagementSystem
{
    
    public class ResultPackage
    {
        
        public ErrCodeEnum ErrCode { get; set; } = ErrCodeEnum.Unknown;

        /// <summary>  
        /// 错误消息  
        /// </summary>  
        public string ErrMsg { get; set; }

        /// <summary>  
        /// 结果的json形式  
        /// </summary>  
        public String Json { get; set; }


        #region IsOK Function                
        public bool IsOK()
        {
            return ErrCode == ErrCodeEnum.OK;
        }
        #endregion

        #region ToString  
        public override string ToString()
        {
            String info = $"{nameof(ErrCode)}:{ErrCode},{nameof(ErrMsg)}:{ErrMsg}";

            return info;
        }
        #endregion

    }
}