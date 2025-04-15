using SqlSugar;

namespace Basis_Model.Models
{
    [SugarTable("Dt_User", "信息表")]
    public class Dt_User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "主键")]
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "账号")]
        public string Account {  get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "密码")]
        public string Password { get; set; }


        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "姓名")]
        public string Name { get; set; }

        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "年龄")]
        public int Age { get; set; }


        [SugarColumn(IsNullable = true, ColumnDescription = "创建时间")]
        public DateTime?DateTime { get; set; }
    }
}
