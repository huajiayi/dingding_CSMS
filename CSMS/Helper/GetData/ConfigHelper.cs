using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ContractStatementManagementSystem
{
    public class ConfigHelper
    {
        #region FetchCorpID Function    
        /// <summary>  
        /// 获取CorpID  
        /// </summary>  
        /// <returns></returns>  
        public static String FetchCorpID()
        {
            return FetchValue("CorpID");
        }
        #endregion

        #region FetchCorpSecret Function  
        /// <summary>  
        /// 获取CorpSecret  
        /// </summary>  
        /// <returns></returns>  
        public static String FetchCorpSecret()
        {
            return FetchValue("CorpSecret");
        }
        public static String FetchAgentID()
        {
            return FetchValue("AgentID");
        }
        #endregion

        #region FetchValue Function                
        private static String FetchValue(String key)
        {
            String value = ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                throw new Exception($"{key} is null.请确认配置文件中是否已配置.");
            }
            return value;
        }
        #endregion
    }
}