using YH.EAM.Entity.CodeGenerator;

namespace YH.EAM.Facade
{
    public class LoginFacade: FacadeBase
    {
            
        public bool Login(string workId,string pwd,ref Team_User user)
        {

            DataAccess.CodeGenerator.Team_User_Da da = new DataAccess.CodeGenerator.Team_User_Da();

            if (da.GetByOne(s => s.Workid == workId) == null)
            {
                this.Message = "用户工号不存在";
                
                return false;
            }

            user = da.GetByOne(s => s.Workid == workId && s.Pwd == pwd);

            if (user==null)
            {
                this.Message = "密码错误";

                return false;
            }

            return true;

        }




    }
}
