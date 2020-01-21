using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIShellAn.Server.Entities;
using AIShellAn.Server.IServices;
using AIShellAn.Server.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIShellAn.Server.Controllers.v1
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
      
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserController( IMapper mapper,IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }


        [HttpGet("{id}")]
        public IActionResult GetByUserId([FromRoute]Guid id)
        {
            var userModel = _userService.QueryById(id);
            ResponseModel<UserModel> res = new ResponseModel<UserModel>();
            if(userModel == null)
            {
                res.success = false;
                res.message = "找不到该用户.";
            }
            else
            {
                res.success = true;
                res.data = userModel;
            }
            return Ok(res);
        }


        [HttpGet("{userName}")]
        public IActionResult GetByUserName([FromRoute]string userName)
        {
            var userModel = _userService.QueryByUserName(userName);
            ResponseModel<UserModel> res = new ResponseModel<UserModel>();
            if (userModel == null)
            {
                res.success = false;
                res.message = "找不到该用户.";
            }
            else
            {
                res.success = true;
                res.data = userModel;
            }
            return Ok(res);
        }


        [HttpGet()]
        public IActionResult GetCurrentUser()
        {
            var userModel = _userService.QueryById(this.GetCurrentUserId());
            ResponseModel<UserModel> res = new ResponseModel<UserModel>();
            if (userModel == null)
            {
                res.success = false;
                res.message = "找不到该用户.";
            }
            else
            {
                res.success = true;
                res.data = userModel;
            }
            return Ok(res);
        }


        [HttpGet]
        public IActionResult GetRoleList()
        {
            ResponseModel<object> res = new ResponseModel<object>();
            var currentUserRole = this.GetCurrentUserRole();
            var roles= _userService.GetRoleList(currentUserRole);
            res.success = true;
            res.data = roles;
            return Ok(res);
        }




        [HttpPost]
        public IActionResult Add([FromBody]UserModel model)
        {
            var success=_userService.Add(model);
            ResponseModel<UserModel> res = new ResponseModel<UserModel>();
            if (success == false)
            {
                res.success = false;
                res.message = "添加用户失败.";
            }
            else
            {
                res.success = true;
            }
            return Ok(res);
        }

        [HttpGet]
        public IActionResult List([FromQuery]UserPayLoad payload)
        {
            PredicateBuilder<User> pb =new PredicateBuilder<User>(true);
            if(!string.IsNullOrEmpty( payload.UserName))
            {
                pb.And(x => x.UserName.Contains(payload.UserName));
            }
            var userList= _userService.QueryByWhere(pb, payload.Page, payload.Size);
            ResponseModel<ListModel<UserModel>> res = new ResponseModel<ListModel<UserModel>>();
            if (userList == null)
            {
                res.success = false;
                res.message = "获取用户列表失败.";
            }
            else
            {
                res.success = true;
                res.data = userList;
            }
            return Ok(res);
        }



        [HttpPost]
        public IActionResult Update([FromBody]UserModel model)
        {
            var success = _userService.Update(model);
            ResponseModel<UserModel> res = new ResponseModel<UserModel>();
            if (!success)
            {
                res.success = false;
                res.message = "更新用户信息失败.";
            }
            else
            {
                res.success = true;
               
            }
            return Ok(res);
        }

        
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="payLoad"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            bool success= _userService.Delete(id);
            ResponseModel<UserModel> res = new ResponseModel<UserModel>();
            if (!success)
            {
                res.success = false;
                res.message = "删除用户失败.";
            }
            else
            {
                res.success = true;
            }
            return Ok(res);
        }


        [HttpGet]
        public IActionResult GetManagerList()
        {
            //判断是否管理员，才能获取这个数据
            List<UserModel> userList =_userService.QueryByRole("项目经理");
            ResponseModel<List<UserModel>> res = new ResponseModel<List<UserModel>>();
            if (userList == null)
            {
                res.success = false;
                res.message = "获取用户列表失败.";
            }
            else
            {
                res.success = true;
                res.data = userList;
            }
            return Ok(res);
        }



        [HttpPost]
        public IActionResult ChangePassword([FromBody]PasswordModel model)
        {
            ResponseModel<UserModel> res = new ResponseModel<UserModel>();
            var oldUser = _userService.QueryById(model.UserId);
            if(oldUser == null)
            {
                res.success = false;
                res.message = "找不到该用户.";
            }
            else
            {
                if(oldUser.Password==model.OldPassword.GetMD5HashCode())
                {
                    oldUser.Password = model.NewPassword.GetMD5HashCode();
                    if(_userService.Update(oldUser))
                    {
                        res.success = true;
                        res.message = "修改成功!";
                    }
                    else
                    {
                        res.success = false;
                        res.message = "修改失败!";
                    }

                    
                    
                }
                else
                {
                    res.success = false;
                    res.message = "旧密码不正确.";
                }
            }
            return Ok(res);

        }


        /// <summary>
        /// 获取某个管理员创建的质检人员,如果是团队管理员,则获取领取了某个标注任务的团队的质检员.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("{taskId}")]
        public IActionResult GetInspectionUserByManagerAndTask([FromRoute]Guid taskId)
        {
            var currentUserId = this.GetCurrentUserId();
            ResponseModel<List<UserModel>> res = new ResponseModel<List<UserModel>>();
            PredicateBuilder<User> where = new PredicateBuilder<User>(true);
            where = where.And(x => x.CreatorId == currentUserId);
            List<UserModel> userList = _userService.QueryByWhere(where);
            return Ok(res);
        }


        [HttpGet("{taskId}")]
        public IActionResult GetAnnotationUserList([FromRoute]Guid taskId)
        {
            var currentUserId = this.GetCurrentUserId();
            ResponseModel<List<UserModel>> res = new ResponseModel<List<UserModel>>();
            PredicateBuilder<User> where = new PredicateBuilder<User>(true);
            where = where.And(x => x.CreatorId == currentUserId);
            List<UserModel> userList = _userService.QueryByWhere(where);
            return Ok(res);
        }




    }
}