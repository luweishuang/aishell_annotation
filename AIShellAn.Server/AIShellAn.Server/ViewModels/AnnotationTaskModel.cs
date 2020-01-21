using AIShellAn.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.ViewModels
{
    public class AnnotationTaskModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 关联的项目经理,不配置外键
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 关联的标注模板
        /// </summary>
        public Guid? TemplateId { get; set; }

        /// <summary>
        /// 任务编号
        /// </summary>
       
        public string TaskCode { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
      
        public string TaskName { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
      
        public string TaskDescribe { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// 标注类型
        /// </summary>
        public AnnotationType AnnotationType { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateOn { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public Entities.TaskStatus TaskStatus { get; set; }

        /// <summary>
        /// 任务完成时间
        /// </summary>
        public DateTime? FinshedTime { get; set; }

        /// <summary>
        /// 任务紧急程度
        /// </summary>
        public Urgency Urgency { get; set; }


        /// <summary>
        /// 标注任务的模板
        /// </summary>
      

        /// <summary>
        /// 任务范围
        /// </summary>
        public TaskScope TaskScope { get; set; }

        public bool IsAnnotationTime { get; set; }
    }
}
