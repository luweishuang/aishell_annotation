using AIShellAn.Server.Entities;
using AIShellAn.Server.IServices;
using AIShellAn.Server.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly AIShellAnContext _db;
        private readonly IMapper _mapper;
        public TemplateService(AIShellAnContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public bool HasTemplate(string name)
        {
            var template= _db.AnnotationTemplate.Where(x => x.Name == name).FirstOrDefault();
            if(template!=null)
            {
                return true;
            }
            return false;
        }


        public TemplateModel Add(TemplateModel model)
        {
            if(HasTemplate(model.Name))
            {
                return null;
            }
            AnnotationTemplate template = _mapper.Map<AnnotationTemplate>(model);
            List<AnnotationTemplateItem> items= _mapper.Map<List<AnnotationTemplateItem>>(model.Items);
            var newTemplate = _db.AnnotationTemplate.Add(template);
            _db.AnnotationTemplateItem.AddRange(items);
            if (_db.SaveChanges() > 0)
            {
                var newModel = _mapper.Map<TemplateModel>(newTemplate);
                return newModel;
            }
            else
            {
                return null;
            }
        }


        public bool Update(TemplateModel model)
        {
            AnnotationTemplate template = _mapper.Map<AnnotationTemplate>(model);
            List<AnnotationTemplateItem> items = _mapper.Map<List<AnnotationTemplateItem>>(model.Items);
            var newTemplate = _db.AnnotationTemplate.Update(template);
            var oldTemplateItems = _db.AnnotationTemplateItem.Where(x => x.AnnotationTemplateId == model.Id);
            _db.AnnotationTemplateItem.RemoveRange(oldTemplateItems);
            _db.AnnotationTemplateItem.AddRange(items);
            if(_db.SaveChanges()>0)
            {
                return true;
            }
            return false;
        }


        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ListModel<TemplateModel> QueryByWhere(PredicateBuilder<AnnotationTemplate> where, int page, int size)
        {
            ListModel<TemplateModel> listModel = new ListModel<TemplateModel>();
            var query = _db.AnnotationTemplate.Where(where.Expression);
            var list = query.AsNoTracking().ToPagedList(page, size);
            listModel.list = _mapper.Map<List<TemplateModel>>(list.Items);
            listModel.count = list.TotalItemCount;
            listModel.page = page;
            listModel.size = size;
            return listModel;
        }

        public TemplateModel QueryById(Guid id)
        {
            var template = _db.AnnotationTemplate.Where(x => x.Id == id).FirstOrDefault();
            if (template == null)
            {
                return null;
            }
            TemplateModel tModel = _mapper.Map<TemplateModel>(template);

            var templateItems = _db.AnnotationTemplateItem.Where(x => x.AnnotationTemplateId == id).ToList();
            if(templateItems.Count>0)
            {
                List<TemplateItemModel> itemModelList = _mapper.Map<List<TemplateItemModel>>(templateItems);
                tModel.Items = itemModelList;
            }
          
            return tModel;

        }

     
    }
}
