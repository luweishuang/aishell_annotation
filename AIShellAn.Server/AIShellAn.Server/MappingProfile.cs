using AIShellAn.Server.Entities;
using AIShellAn.Server.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<AnnotationTask, AnnotationTaskModel>().ReverseMap();
            CreateMap<Team, TeamModel>().ReverseMap();
            CreateMap<TeamUser, TeamUserModel>().ReverseMap();
            CreateMap<AnnotationTemplate, TemplateModel>().ReverseMap();
            CreateMap<AnnotationTemplateItem, TemplateItemModel>().ReverseMap();
            CreateMap<DataPackage, DataPackageModel>().ReverseMap();

        }
    }
}
