using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace ContractStatementManagementSystem
{
    public class SignGet
    {
        #region FetchSignPackage Function   
        /// <summary>  
        /// 获取签名包  
        /// </summary>  
        /// <param name="url"></param>  
        /// <returns></returns>  
        public static SignPackage FetchSignPackage(String url, JSTicket jsticket)
        {
            int unixTimestamp = SignPackageHelper.ConvertToUnixTimeStamp(DateTime.Now);
            string timestamp = Convert.ToString(unixTimestamp);
            string nonceStr = "lychgqqmyzbyxxyzhfrj";
            if (jsticket == null)
            {
                return null;
            }

            // 这里参数的顺序要按照 key 值 ASCII 码升序排序   
            string rawstring = "jsapi_ticket="+ jsticket.ticket
                             +"&noncestr=" + nonceStr
                             +"&timestamp=" + timestamp
                             +"&url=" + url;
            string signature = SignPackageHelper.Sha1Hex(rawstring).ToLower();

            var signPackage = new SignPackage()
            {
                agentId = ConfigHelper.FetchAgentID(),//取配置文件中的agentId，可依据实际配置而作调整  
                corpId = ConfigHelper.FetchCorpID(),//取配置文件中的coprId，可依据实际配置而作调整  
                timeStamp = timestamp,
                nonceStr = nonceStr,
                signature = signature,
                url = url,
                rawstring = rawstring,
                jsticket = jsticket.ticket
            };
            return signPackage;
        }

        /// <summary>  
        /// 获取签名包  
        /// </summary>  
        /// <param name="url"></param>  
        /// <returns></returns>  
        public static SignPackage FetchSignPackage(String url, AccessToken a)
        {
            int unixTimestamp = SignPackageHelper.ConvertToUnixTimeStamp(DateTime.Now);
            string timestamp = Convert.ToString(unixTimestamp);
            string nonceStr = SignPackageHelper.CreateNonceStr();
            Cache cache2 = new Cache();
            if (cache2["ticket"] == null)
            {
                TicketGet.ticketGet(a);
                cache2.Add("ticket", TicketGet.Ticket, null, DateTime.Now.AddSeconds(7200), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }

            JSTicket jsticket = (JSTicket)cache2["ticket"];



            var signPackage = FetchSignPackage(url, jsticket);
            return signPackage;
        }
        #endregion
    }
}