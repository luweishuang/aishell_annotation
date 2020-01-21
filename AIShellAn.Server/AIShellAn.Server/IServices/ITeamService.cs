using AIShellAn.Server.Entities;
using AIShellAn.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.IServices
{
    public interface ITeamService
    {

        TeamModel QueryById(Guid id);

        TeamModel Add(TeamModel model);

        bool AddMember(Guid TeamId, Guid UserId);

        bool RemoveMember(Guid TeamId, Guid UserId);

        bool SetManager(Guid TeamId, Guid UserId);
        bool HasTeamName(string teamName);

        bool HasMember(Guid TeamId, Guid UserId);
        ListModel<TeamModel> QueryByWhere(PredicateBuilder<Team> where, int page, int size);
        TeamModel Update(TeamModel model);
        bool Delete(Guid id);
       
        List<TeamModel> QueryByWhere(PredicateBuilder<Team> where);

        int GetTeamTaskCount(Guid teamId);


    }
}
