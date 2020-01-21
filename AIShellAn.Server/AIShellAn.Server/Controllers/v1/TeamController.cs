using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TeamController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITeamService _teamService;
      
        public TeamController(IMapper mapper, ITeamService teamService)
        {
            _mapper = mapper;
            _teamService = teamService;
          
        }

        [HttpPost]
        public IActionResult Add([FromBody] TeamModel model)
        {
            ResponseModel<TeamModel> res = new ResponseModel<TeamModel>();
            if (_teamService.HasTeamName(model.TeamName))
            {
                res.success = false;
                res.message = "已存在该团队名!";
                return Ok(res);
            }
            TeamModel newTeamModel= _teamService.Add(model);
            if(newTeamModel!=null)
            {
                res.success = true;
                res.data = newTeamModel;
            }
            else
            {
                res.success = false;
                res.message = "添加团队失败!";
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult AddMember([FromBody]TeamUserModel model)
        {
            ResponseModel<TeamModel> res = new ResponseModel<TeamModel>();
            if (_teamService.HasMember(model.TeamId, model.UserId))
            {
                res.success = false;
                res.message = "该成员已在该团队中!";
            }
            else
            {
                if( _teamService.AddMember(model.TeamId, model.UserId))
                {
                    res.success = true;
                    res.message = "添加成功!";
                }
                else
                {
                    res.success = false;
                    res.message = "添加成员失败!";
                }
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult RemoveMember([FromBody] TeamUserModel model)
        {
            ResponseModel<TeamModel> res = new ResponseModel<TeamModel>();
            if (_teamService.RemoveMember(model.TeamId,model.UserId))
            {
                res.success = true;
                res.message = "移除成功!";
            }
            else
            {
                res.success = false;
                res.message = "移除失败!";
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Delete([FromBody]IdPayLoad payLoad)
        {
            ResponseModel<TeamModel> res = new ResponseModel<TeamModel>();
            //检查该团队是否领取有任务,如果有的话,则不允许直接删除
            if (_teamService.GetTeamTaskCount(payLoad.Id) >0)
            {
                res.success = false;
                res.message = "该团队已领取任务,无法直接删除!";
                return Ok(res);
            }

            if (_teamService.Delete(payLoad.Id))
            {
                res.success = true;
                res.message = "删除成功!";
            }
            else
            {
                res.success = false;
                res.message = "删除失败!";
            }
            return Ok(res);
        }
    }
}