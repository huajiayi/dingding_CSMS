using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ContractStatementManagementSystem
{
    public class TicketGet
    {
       
        public static string access_token = "access_token";
        public static string ticket;
        public static JSTicket Ticket;
        public static void ticketGet( AccessToken a)
        {
            if (Ticket == null || a.Begin.AddSeconds(7200)< DateTime.Now)//jsTicket为null表示不存在或过期,或AccessToken过期  
            {

                string TicketUrl = "https://oapi.dingtalk.com/get_jsapi_ticket";
                string apiurl = $"{TicketUrl}?access_token={a.Value}";

                WebRequest request = WebRequest.Create(@apiurl);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding encode = Encoding.UTF8;
                StreamReader reader = new StreamReader(stream, encode);
                string resultJson = reader.ReadToEnd();
                Ticket = JsonConvert.DeserializeObject<JSTicket>(resultJson);

                if (Ticket.ErrCode == ErrCodeEnum.OK)
                {
                    ticket = Ticket.ticket;
                }
            }
        }
    }
}