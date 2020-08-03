//----------------
//DA  v1.1
//2020-7-31
//Near
//---------------

using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace YH.EAM.DataAccess.CodeGenerator
{
    /// <summary>
    ///  资产列表
    ///</summary>
    public class   Team_Message
    {

       public Team_Message()
       {
      
       }

        ///<summary>
        ///描述：主键
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        ///<summary>
        ///描述：设备编号
        ///</summary>
        public string Equipment_Numbers { get; set; }
        ///<summary>
        ///描述：名称
        ///</summary>
        public string Name { get; set; }
        ///<summary>
        ///描述：状态
        ///</summary>
        public string Status { get; set; }
        ///<summary>
        ///描述：规格型号
        ///</summary>
        public string Size_Model { get; set; }
        ///<summary>
        ///描述：资产类型
        ///</summary>
        public string Type { get; set; }
        ///<summary>
        ///描述：入库日期
        ///</summary>
        public DateTime Inbound_Date { get; set; }
        ///<summary>
        ///描述：财务编号
        ///</summary>
        public string Financial_Numbers { get; set; }
        ///<summary>
        ///描述：计算机名
        ///</summary>
        public string Computer_Name { get; set; }
        ///<summary>
        ///描述：物理地址
        ///</summary>
        public string Physical_Address { get; set; }
        ///<summary>
        ///描述：位置
        ///</summary>
        public string Location { get; set; }
        ///<summary>
        ///描述：是否加域
        ///</summary>
        public string Add_Domain { get; set; }
        ///<summary>
        ///描述：外网权限
        ///</summary>
        public string Outside_Network { get; set; }
        ///<summary>
        ///描述：设备详情
        ///</summary>
        public string Equipment_Detailed { get; set; }
        ///<summary>
        ///描述：备注
        ///</summary>
        public string Remarks { get; set; }
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public DateTime Createtime { get; set; }

    }
 }








