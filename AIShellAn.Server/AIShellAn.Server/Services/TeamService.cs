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
    public class TeamService:ITeamService
    {
        private readonly AIShellAnContext _db;
        private readonly IMapper _mapper;
        public TeamService(AIShellAnContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public bool HasTeamName(string  teamName)
        {
            var hasTeam = _db.Team.Where(u => u.TeamName == teamName).FirstOrDefault();
            if (hasTeam != null)
            {
                return true;
            }
            return false;
        }


        public TeamModel Add(TeamModel model)
        {
            if (HasTeamName(model.TeamName))
            {
                return null;
            }
            else
            {
                Team team = _mapper.Map<Team>(model);
                var newTeam = _db.Team.Add(team);
                if (_db.SaveChanges() > 0)
                {
                    var newModel = _mapper.Map<TeamModel>(newTeam);
                    return newModel;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool HasMember(Guid TeamId, Guid UserId)
        {
            if (_db.TeamUser.Where(t => t.TeamId == TeamId && t.UserId == UserId).Count() > 0)
            {
                return false;
            }
            return true;
        }


        public bool AddMember(Guid TeamId, Guid UserId)
        {
            if (HasMember(TeamId, UserId))
            {
                return false;
            }
            else
            {
                TeamUser tu = new TeamUser()
                {
                    UserId = UserId,
                    TeamId = TeamId,
                    CreatedOn = DateTime.Now,
                    IsManager = false
                };
                //如果加进来的是团队管理员角色,则设置一下属性
                var user = _db.User.Find(UserId);
                //如果是团队管理员
                if(user.Role.Contains(RoleConfig.TeamManager))
                {
                    tu.IsManager = true;
                }
               
                _db.TeamUser.Add(tu);
                _db.SaveChanges();
                return true;
            }
        }


        public bool RemoveMember(Guid TeamId, Guid UserId)
        {
            TeamUser tu= _db.TeamUser.Where(x => x.TeamId == TeamId && x.UserId == UserId).FirstOrDefault();
            if(tu!=null)
            {
                _db.Remove(tu);
                if( _db.SaveChanges()>0)
                {
                    return true;
                }
            }
            return false;
        }



        public bool Delete(Guid id)
        {
             var team=_db.Team.Find(id);
            if(team==null)
            {
                return false;
            }
            else
            {
                _db.Team.Remove(team);
                if(_db.SaveChanges()>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int GetTeamTaskCount(Guid teamId)
        {
            return _db.TeamTask.Where(x => x.TeamId == teamId).Count();
        }
        

        public ListModel<TeamModel> QueryByWhere(PredicateBuilder<Team> where, int page, int size)
        {
            ListModel<TeamModel> listModel = new ListModel<TeamModel>();
            var query = _db.Team.Where(where.Expression);
            var list = query.AsNoTracking().ToPagedList(page, size);
            listModel.list = _mapper.Map<List<TeamModel>>(list.Items);
            listModel.count = list.TotalItemCount;
            listModel.page = page;
            listModel.size = size;
            return listModel;
        }

        public TeamModel QueryById(Guid id)
        {
            var user = _db.Team.Find(id);
            if (user == null)
            {
                return null;
            }
            else
            {
                var model = _mapper.Map<TeamModel>(user);
                return model;
            }
        }

        public List<TeamModel> QueryByWhere(PredicateBuilder<Team> where)
        {
            var teamList = _db.Team.Where(where.Expression).ToList();
            if (teamList.Count > 0)
            {
                List<TeamModel> modelList = _mapper.Map<List<TeamModel>>(teamList);
                return modelList;
            }
            else
            {
                return null;
            }
        }


        public TeamModel Update(TeamModel model)
        {
            Team team = _mapper.Map<Team>(model);
            var newTeam = _db.Team.Update(team);
            if (_db.SaveChanges() > 0)
            {
                TeamModel newModel = _mapper.Map<TeamModel>(newTeam);
                return newModel;
            }
            else
            {
                return null;
            }
        }

        public bool SetManager(Guid TeamId,Guid UserId)
        {
            var tu= _db.TeamUser.Where(x => x.TeamId == TeamId && x.UserId == UserId).FirstOrDefault();
            if (tu == null) return false;
            tu.IsManager = true;
            _db.TeamUser.Update(tu);
            if(_db.SaveChanges()>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
