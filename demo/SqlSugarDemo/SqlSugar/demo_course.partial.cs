using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace SqlSugarDemo.DAL
{
    public partial class Sdemo_courseEO
    {
        #region tinyfx
        public static implicit operator Demo_courseEO(Sdemo_courseEO value)
        {
            return new Demo_courseEO
            {
		        Year = value.Year,
		        CourseID = value.CourseID,
		        Name = value.Name,
		        OrderNum = value.OrderNum,
            };
        }
        public static implicit operator Sdemo_courseEO(Demo_courseEO value)
        {
            return new Sdemo_courseEO
            {
		        Year = value.Year,
		        CourseID = value.CourseID,
		        Name = value.Name,
		        OrderNum = value.OrderNum,
            };
        }
        #endregion
    }
}