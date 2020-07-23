using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Victory.Core.Controller;
using YH.EAM.DataAccess.CodeGenerator;
using YH.EAM.Entity.CodeGenerator;
using YH.EAM.WebApp.Attribute;

namespace YH.EAM.WebApp.Controllers
{
    [Authorize]
    public class UserRoleController : TopControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        [Right(PowerName = "角色管理")]
        public IActionResult Role()
        {
            return View();
        }




        [Right(PowerName = "页面权限")]
        [HttpPost]
        public IActionResult GetAllRole()
        {
            Tright_Role_Da rolemanger = new Tright_Role_Da();
            var list = rolemanger.Select.OrderBy(s => s.Id).ToList();

            return SuccessResultList(list);
        }



        [Right(PowerName = "添加角色")]
        [HttpPost]
        public IActionResult AddRole(Tright_Role model)
        {
            if (string.IsNullOrEmpty(model.Rolename))
            {
                return FailMessage("角色名不能为空！");
            }


            Tright_Role_Da da = new Tright_Role_Da();
            da.Insert(model);
            return SuccessMessage("成功！");

        }


        [Right(PowerName = "修改角色")]
        [HttpPost]
        public IActionResult UpdateRole(Tright_Role model)
        {

            if (string.IsNullOrEmpty(model.Rolename))
            {
                return FailMessage("角色名不能为空！");
            }

            Tright_Role_Da da = new Tright_Role_Da();
            da.Update(model);
            return SuccessMessage("成功！");
        }


        [Right(PowerName = "角色查询")]
        [HttpPost]
        public IActionResult ListRole()
        {
            Tright_Role_Da da = new Tright_Role_Da();

            return SuccessResultList(da.Select.ToList());
        }

        [Right(PowerName = "删除角色")]
        [HttpPost]
        public IActionResult DelRole(int id)
        {
            Tright_Role_Da da = new Tright_Role_Da();

            if (da.Delete(s => s.Id == id) > 0)
            {
                return SuccessMessage("成功！");

            }
            return FailMessage();

        }



        /// <summary>
        /// 获取用户与角色的中间 表 信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// 
        [Right(PowerName = "分配权限")]
        [HttpPost]
        public IActionResult GetUserRoleMebmer(int userid)
        {
            Tright_User_Role_Da userroleManage = new Tright_User_Role_Da();
            var list = userroleManage.Select.Where(s => s.Userid == userid).ToList();

            return SuccessResultList(list);


        }




        /// <summary>
        /// 获取角色 与 权限 的中间表信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [Right(PowerName = "角色功能配置")]
        [HttpPost]
        public IActionResult GetRolePowerMebmer(int roleid)
        {
            Tright_Role_Power_Da userroleManage = new Tright_Role_Power_Da();
            var list = userroleManage.Select.Where(s => s.Roleid == roleid).ToList();

            return SuccessResultList(list);



        }


        [Right(PowerName = "用户关联角色")]
        [HttpPost]
        public IActionResult AddUserRoleMebmer(int userid, int roleid)
        {
            Tright_User_Role_Da userroleManage = new Tright_User_Role_Da();

            if (userroleManage.Select.Where(s => s.Roleid == roleid && s.Userid == userid).Count() > 0)
            {
                return SuccessMessage("请不要反复添加！");
            }



            Tright_User_Role model = new Tright_User_Role
            {
                Roleid = roleid,
                Userid = userid
            };
            userroleManage.Insert(model);

            return SuccessMessage("添加成功！");
        }


        [Right(PowerName = "用户退出角色")]
        [HttpPost]
        public IActionResult DeleteUserRoleMebmer(int id)
        {
            Tright_User_Role_Da userroleManage = new Tright_User_Role_Da();
            var model = userroleManage.Select.Where(s => s.Id == id);

            if (model == null)
            {
                return SuccessMessage("请不要反复取消！"); ;
            }



            if (userroleManage.Delete(s => s.Id == id) > 0)
            {
                return SuccessMessage("成功！");

            }
            return FailMessage();

        }



        [Right(PowerName = "角色关联功能")]
        [HttpPost]
        public IActionResult AddRolePowerMebmer(int roleid, int powerid)
        {
            Tright_Role_Power_Da Manage = new Tright_Role_Power_Da();

            if (Manage.Select.Where(s => s.Roleid == roleid && s.Powerid == powerid).Count() > 0)
            {
                return SuccessMessage("请不要反复添加！");
            }



            Tright_Role_Power model = new Tright_Role_Power
            {
                Roleid = roleid,
                Powerid = powerid
            };
            Manage.Insert(model);

            return SuccessMessage("已添加！");
        }


        [Right(PowerName = "角色取消功能")]
        [HttpPost]
        public IActionResult DeletedRolePowerMebmer(int id)
        {
            Tright_Role_Power_Da userroleManage = new Tright_Role_Power_Da();
            var model = userroleManage.Select.Where(s => s.Id == id);

            if (model == null)
            {
                return SuccessMessage("请不要反复取消！"); ;
            }


            if (userroleManage.Delete(s => s.Id == id) > 0)
            {
                return SuccessMessage("成功！");

            }
            return FailMessage();


        }


    }
}
