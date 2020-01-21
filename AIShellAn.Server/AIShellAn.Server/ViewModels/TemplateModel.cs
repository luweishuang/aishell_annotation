using AIShellAn.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.ViewModels
{
    public class TemplateModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模板类型
        /// </summary>
        public TemplateType Type { get; set; } //类别

        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateOn { get; set; }


        public List<TemplateItemModel> Items { get; set; }
    }


    public class TemplateItemModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 模板项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模板项类型
        /// </summary>
        public TemplateType TemplateType { get; set; }

        /// <summary>
        /// 模板项内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Necessary { get; set; } = true;

        /// <summary>
        /// 有效还是无效的选项
        /// </summary>
        public bool Effect { get; set; } = true;

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string Default { get; set; }

        public Guid AnnotationTemplateId { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateOn { get; set; }
    }

}
