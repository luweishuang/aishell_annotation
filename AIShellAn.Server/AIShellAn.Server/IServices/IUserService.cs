using AIShellAn.Server.Entities;
using AIShellAn.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.IServices
{
    public interface IUserService
    {
        UserModel QueryByUserName(string userName);
        Task<UserModel> QueryByUserNameAsyn(string userName);
        UserModel QueryById(Guid id);
        bool HasUser(string userName);
        bool Add(UserModel model);
        ListModel<UserModel> QueryByWhere(PredicateBuilder<User> where, int page, int size);
        bool Update(UserModel model);
        bool Delete(Guid id);
        List<UserModel> QueryByRole(string role);
        List<UserModel> QueryByWhere(PredicateBuilder<User> where);
        List<string> GetRoleList(string managerRole);


    }
}
