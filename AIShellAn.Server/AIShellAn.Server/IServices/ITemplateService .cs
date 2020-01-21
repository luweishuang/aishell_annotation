using AIShellAn.Server.Entities;
using AIShellAn.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.IServices
{
    public interface ITemplateService
    {
        TemplateModel QueryById(Guid id);

        TemplateModel Add(TemplateModel model);

        ListModel<TemplateModel> QueryByWhere(PredicateBuilder<AnnotationTemplate> where, int page, int size);

        bool Update(TemplateModel model);
        int Delete(Guid id);


    }
}
