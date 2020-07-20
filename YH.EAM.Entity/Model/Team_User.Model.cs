using System;
using System.Linq;
using System.Text;
namespace YH.EAM.Entity.Model
{
    /// <summary>
    ///  用户表
    ///</summary>
    public class   Team_user
    {

       public Team_user()
       {
      
       }
        ///<summary>
        ///描述：用户id
        ///</summary>
        public int User_Id { get; set; }
        ///<summary>
        ///描述：用户名
        ///</summary>
        public string Name { get; set; }
        ///<summary>
        ///描述：用户密码
        ///</summary>
        public string Pwd { get; set; }
        ///<summary>
        ///描述：工号
        ///</summary>
        public string Workid { get; set; }
        ///<summary>
        ///描述：手机号
        ///</summary>
        public string Phone { get; set; }
        ///<summary>
        ///描述：用户状态
        ///</summary>
        public int Status { get; set; }
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public DateTime Createtime { get; set; }
        ///<summary>
        ///描述：备注
        ///</summary>
        public string Remarks { get; set; }

    }
 }








