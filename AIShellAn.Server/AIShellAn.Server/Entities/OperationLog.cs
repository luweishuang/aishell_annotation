using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.Entities
{
    public enum OperationLogContent
    {
        登录,
        注销,
        修改密码,

    }
    /// <summary>
    /// 用户操作日志
    /// </summary>
    public class OperationLog
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public OperationLogContent Content { get; set; }

        public DateTime Time { get; set; }

        public string IP { get; set; }
    }
}
