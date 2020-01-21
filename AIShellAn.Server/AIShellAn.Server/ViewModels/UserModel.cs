using AIShellAn.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.ViewModels
{
    public class UserModel
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }


        public string Password { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorName { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? Birthday { get; set; }

        public string NativePlace { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

        public string Role { get; set; }

        public bool Active { get; set; }
    }

    public class PasswordModel
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
