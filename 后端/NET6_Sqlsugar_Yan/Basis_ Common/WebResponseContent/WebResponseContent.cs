using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basis__Common
{
    public class WebResponseContent
    {
        public bool Status { get; set; }

        /// <summary>
        /// 状态吗
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        public string DevMessage { get; set; }

        public static WebResponseContent Instance => new WebResponseContent();

        public WebResponseContent()
        {
        }

        public WebResponseContent(bool status)
        {
            Status = status;
        }

        public WebResponseContent OK()
        {
            Status = true;
            return this;
        }

        public WebResponseContent OK(string message = null, object data = null)
        {
            Status = true;
            Message = message;
            Data = data;
            return this;
        }

        public WebResponseContent Error(string message = null)
        {
            Status = false;
            Message = message;
            return this;
        }
    }
}
