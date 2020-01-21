using AIShellAn.Server.Entities;
using AIShellAn.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.IServices
{
    public interface IAnnotationTaskService
    {
        List<AnnotationTaskModel> GetTeamTask(Guid teamId);


    }
}
