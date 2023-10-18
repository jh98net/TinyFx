using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace SqlSugarDemo.DAL
{
    public partial class Sdemo_courseEO
    {
        public static implicit operator Demo_courseEO(Sdemo_courseEO value)
        {
            return new Demo_courseEO
            {
                CourseID = value.CourseID,
                Name = value.Name,
                OrderNum = value.OrderNum,
                Year = value.Year,
            };
        }
        public static implicit operator Sdemo_courseEO(Demo_courseEO value)
        {
            return new Sdemo_courseEO
            {
                CourseID = value.CourseID,
                Name = value.Name,
                OrderNum = value.OrderNum,
                Year = value.Year,
            };
        }
    }
}