using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace SqlSugarDemo.DAL
{
    public partial class Sdemo_user_courseEO
    {
        #region tinyfx
        public static implicit operator Demo_user_courseEO(Sdemo_user_courseEO value)
        {
            return new Demo_user_courseEO
            {
		        UserID = value.UserID,
		        Year = value.Year,
		        CourseID = value.CourseID,
		        Note = value.Note,
            };
        }
        public static implicit operator Sdemo_user_courseEO(Demo_user_courseEO value)
        {
            return new Sdemo_user_courseEO
            {
		        UserID = value.UserID,
		        Year = value.Year,
		        CourseID = value.CourseID,
		        Note = value.Note,
            };
        }
        #endregion
    }
}