using AIShellAn.Server.Entities;
using AIShellAn.Server.IServices;
using AIShellAn.Server.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.Services
{
    public class UserService : IUserService
    {
        private readonly AIShellAnContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public UserService(AIShellAnContext db, IMapper mapper, ILogger<UserService> loger)
        {
            _db = db;
            _mapper = mapper;
            _logger = loger;
        }


        public bool HasUser(string userName)
        {
            var hasUser = _db.User.Where(u => u.UserName == userName).FirstOrDefault();
            if (hasUser != null)
            {
                return true;
            }
            return false;
        }

        public bool Add(UserModel model)
        {
            if(HasUser(model.UserName))
            {
                return false;
            }
            else
            {
                try
                {
                    User user = _mapper.Map<User>(model);
                    user.CreatedOn = DateTime.Now;
                    user.Active = true;
                    _db.User.Add(user);
                    if (_db.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    LogError.WriteLog(_logger, "添加用户异常", ex);
                    return false;
                }
               
            }
            
        }

        public bool Delete(Guid id)
        {
            var user=_db.User.Find(id);
            if(user==null)
            {
                return false;
            }
            _db.User.Remove(user);
            if(_db.SaveChanges()>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       

        public UserModel QueryById(Guid id)
        {
            var user = _db.User.Find(id);
            if(user==null)
            {
                return null;
            }
            else
            {
                var model = _mapper.Map<UserModel>(user);
                return model;
            }
        }

        public UserModel QueryByUserName(string userName)
        {
            var user = _db.User.Where(x => x.UserName == userName).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            else
            {
                var model = _mapper.Map<UserModel>(user);
                return model;
            }
        }


        public async Task<UserModel> QueryByUserNameAsyn(string userName)
        {
            var user =await _db.User.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            else
            {
                var model = _mapper.Map<UserModel>(user);
                return model;
            }
        }

        public List<UserModel> QueryByRole(string role)
        {
            var userList = _db.User.Where(x => x.Role.Contains(role)).AsNoTracking().ToList();
            if(userList.Count>0)
            {
                List<UserModel> modelList = _mapper.Map<List<UserModel>>(userList);
                return modelList;
            }
            else
            {
                return null;
            }
        }



        public List<UserModel> QueryByWhere(PredicateBuilder<User> where)
        {
            var userList = _db.User.Where(where.Expression).ToList();
            if (userList.Count > 0)
            {
                List<UserModel> modelList = _mapper.Map<List<UserModel>>(userList);
                return modelList;
            }
            else
            {
                return null;
            }
        }


        public ListModel<UserModel> QueryByWhere(PredicateBuilder<User> where, int page, int size)
        {
            ListModel<UserModel> listModel = new ListModel<UserModel>();
            var query = _db.User.Where(where.Expression);
            var list = query.AsNoTracking().ToPagedList(page, size);
            listModel.list = _mapper.Map<List<UserModel>>(list.Items);
            listModel.count = list.TotalItemCount;
            listModel.page = page;
            listModel.size = size;
            return listModel;
        }


        public bool Update(UserModel model)
        {
            User user = _mapper.Map<User>(model);
            var newUser= _db.User.Update(user);
            if( _db.SaveChanges()>0)
            {
                return true;
            }
            else
            {
                return false;
            }
         
        }


        public List<string> GetRoleList(string managerRole)
        {
            List<string> roles = new List<string>();
            if(managerRole.Contains(RoleConfig.SysteamManager))
            {
                roles.Add(RoleConfig.ProjectManager);
                roles.Add(RoleConfig.TeamManager);
            }
            else if(managerRole.Contains(RoleConfig.ProjectManager))
            {
                roles.Add(RoleConfig.TeamManager);
            }
            roles.Add(RoleConfig.AnnotationUser);
            roles.Add(RoleConfig.InspectionUser);
            return roles;
        }
    }
}
