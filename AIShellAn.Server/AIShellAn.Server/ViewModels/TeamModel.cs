using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.ViewModels
{
    public class TeamModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 关联项目经理ID
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 团队名称
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 团队说明介绍
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 创建该团队的项目经理
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateOn { get; set; }

        
    }
}
