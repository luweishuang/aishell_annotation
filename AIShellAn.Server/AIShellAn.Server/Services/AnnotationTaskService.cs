using AIShellAn.Server.Entities;
using AIShellAn.Server.IServices;
using AIShellAn.Server.ViewModels;
using AutoMapper;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.Services
{
    public class AnnotationTaskService:IAnnotationTaskService
    {
        private readonly AIShellAnContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public AnnotationTaskService(AIShellAnContext db, IMapper mapper, ILogger logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }


        public bool HasTaskName(string taskName)
        {
            int taskCount=_db.AnnotationTask.Where(x => x.TaskName == taskName).Count();
            if(taskCount>0)
            {
                return true;
            }
            return false;
        }

        public bool Add(AnnotationTaskModel model)
        {
            if (HasTaskName(model.TaskName))
            {
                return false;
            }

            AnnotationTask task = _mapper.Map<AnnotationTask>(model);
            task.CreateOn = DateTime.Now;
            _db.AnnotationTask.Add(task);
            _db.SaveChanges();
            return true;
        }

        public bool Update(AnnotationTaskModel model)
        {
            AnnotationTask task = _mapper.Map<AnnotationTask>(model);
            var newTask = _db.AnnotationTask.Update(task);
            if(_db.SaveChanges()>0)
            {
                return true;
            }
            return false;
        }

        #region 删除任务
        /// <summary>
        /// 删除标注任务,以及相关的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            //开启事务
            using (var tran = _db.Database.BeginTransaction())
            {
                try
                {
                    var task = _db.AnnotationTask.Find(id);
                    if (task == null)
                    {
                        return false;
                    }
                    //删除数据包
                    _db.DataPackage.Where(x => x.AnnotationTaskId == id).BatchDelete();
                    //删除任务下的数据项
                    if (task.AnnotationType == AnnotationType.短语音标注)
                    {
                        _db.ShortSpeechItem.Where(x => x.AnnotationTaskId == id).BatchDelete();
                    }
                    else if (task.AnnotationType == AnnotationType.长语音标注)
                    {
                        _db.LongSpeechItem.Where(x => x.AnnotationTaskId == id).BatchDelete();

                    }
                    else if (task.AnnotationType == AnnotationType.图像矩形标注)
                    {

                    }
                    else if (task.AnnotationType == AnnotationType.图像多边形标注)
                    {

                    }
                    //删除团队任务领取关系
                    _db.TeamTask.Where(x => x.AnnotationTaskId == id).BatchDelete();

                    //删除质检记录
                    _db.InspectionTask.Where(x => x.AnnotationTaskId == id).BatchDelete();
                    _db.InspectionPackageRecord.Where(x => x.AnnotationTaskId == id).BatchDelete();
                    _db.InspectionItemRecord.Where(x => x.AnnotationTaskId == id).BatchDelete();

                    //删除标注结果
                    _db.AnnotationResult.Where(x => x.AnnotationTaskId == id).BatchDelete();

                    //删除任务
                    _db.AnnotationTask.Remove(task);

                    _db.SaveChanges();
                    //提交事务
                    tran.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    //写入日志
                    _logger.WriteLog("删除标注任务的事务出现错误", e);
                    //回滚事务
                    tran.Rollback();
                    return false;
                }
            }
        } 
        #endregion

        public AnnotationTaskModel QueryById(Guid id)
        {
            var task = _db.AnnotationTask.Find(id);
            if (task == null)
            {
                return null;
            }
            else
            {
                var model = _mapper.Map<AnnotationTaskModel>(task);
                return model;
            }
        }


        public ListModel<AnnotationTaskModel> QueryByWhere(PredicateBuilder<AnnotationTask> where,int page,int size)
        {
            ListModel<AnnotationTaskModel> listModel = new ListModel<AnnotationTaskModel>();
            var taskList = _db.AnnotationTask.Where(where.Expression).AsNoTracking().ToPagedList(page, size);
            if (taskList.Count > 0)
            {
                listModel.list = _mapper.Map<List<AnnotationTaskModel>>(taskList.Items);
                listModel.count = taskList.TotalItemCount;
                listModel.page = page;
                listModel.size = size;
                return listModel;
            }
            else
            {
                return null;
            }
        }


        public List<AnnotationTaskModel> GetTeamTask(Guid teamId)
        {
            List<AnnotationTask> taskList = _db.TeamTask.Include(x => x.AnnotationTask).Where(x => x.TeamId == teamId).Select(x => x.AnnotationTask).ToList();

            if(taskList.Count>0)
            {
                return _mapper.Map<List<AnnotationTaskModel>>(taskList);
            }
            else
            {
                return null;
            }
        }


    }
}
