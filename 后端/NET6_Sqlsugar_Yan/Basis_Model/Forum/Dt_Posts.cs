using SqlSugar;

namespace Basis_Model.Models
{
    [SugarTable("Dt_Posts", "帖子表")]
    public class Dt_Posts
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "主键")]
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "账号")]
        public string Account {  get; set; }

        /// <summary>
        /// 帖子标题
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "帖子标题")]
        public string PostTitle { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 500, ColumnDescription = "帖子内容")]
        public string PostContent { get; set; }



        [SugarColumn(IsNullable = true, ColumnDescription = "创建时间")]
        public DateTime?DateTime { get; set; }
    }
}
