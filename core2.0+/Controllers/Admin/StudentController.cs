using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreBussiness;
using coreEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core2._0_.Controllers.Admin
{
    /// <summary>
    /// 学生模块
    /// </summary>
    [Produces("application/json")]
    [Route("api/admin/[controller]")]
    [Authorize(Policy = "admin")]
    public class StudentController : Controller
    {
        private StudentBLL bll = new StudentBLL();
        #region base
        /// <summary>
        /// 获取学生分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        //可以不需要授权访问 [AllowAnonymous]
        public JsonResult GetStudentPageList(int pageIndex = 1, int pageSize = 10)
        {
            return Json(bll.GetPageList(pageIndex, pageSize));
        }
        /// <summary>
        /// 获取单个学生
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult GetStudentById(long id)
        {
            return Json(bll.GetById(id));
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStudent")]
        public JsonResult Add(Student entity = null)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(bll.Add(entity));
        }
        /// <summary>
        /// 编辑学生
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Student")]
        public JsonResult Update(Student entity = null)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(bll.Update(entity));
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonResult Dels(dynamic[] ids = null)
        {
            if (ids.Length == 0)
                return Json("参数为空");
            return Json(bll.Dels(ids));
        }
        #endregion
    }
}