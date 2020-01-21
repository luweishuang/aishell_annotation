using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server
{
    public static class LogError
    {
        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static void WriteLog(this ILogger logger, LogLevel level, string throwMsg, Exception ex)
        {
            //todo:添加用户信息,IP信息等
            string message= string.Format("【错误】：{0} \r\n 【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3} \r\n   【时间】:{4}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace,DateTime.Now.ToLongTimeString() });
            logger.Log(level, message);
        }
    }
}
