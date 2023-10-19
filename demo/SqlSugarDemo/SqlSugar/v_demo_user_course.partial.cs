using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace SqlSugarDemo.DAL
{
    public partial class Sv_demo_user_courseEO
    {
        #region tinyfx
        public static implicit operator V_demo_user_courseEO(Sv_demo_user_courseEO value)
        {
            return new V_demo_user_courseEO
            {
		        UserID = value.UserID,
		        ClassID = value.ClassID,
		        CourseID = value.CourseID,
		        Name = value.Name,
		        Note = value.Note,
		        TestColumn = value.TestColumn,
            };
        }
        public static implicit operator Sv_demo_user_courseEO(V_demo_user_courseEO value)
        {
            return new Sv_demo_user_courseEO
            {
		        UserID = value.UserID,
		        ClassID = value.ClassID,
		        CourseID = value.CourseID,
		        Name = value.Name,
		        Note = value.Note,
		        TestColumn = value.TestColumn,
            };
        }
        #endregion
    }
}