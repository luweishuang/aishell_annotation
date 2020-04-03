using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server
{
    public class UserPayLoad:ListPayload
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public Entities.Sex? Sex { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string Role { get; set; }

    }
}
