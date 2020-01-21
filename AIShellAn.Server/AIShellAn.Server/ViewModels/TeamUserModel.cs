using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.ViewModels
{
    public class TeamUserModel
    {
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
