using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace SqlSugarDemo.DAL
{
    public partial class Sdemo_classEO
    {
        #region tinyfx
        public static implicit operator Demo_classEO(Sdemo_classEO value)
        {
            return new Demo_classEO
            {
		        ClassID = value.ClassID,
		        Name = value.Name,
		        Sort1 = value.Sort1,
		        Sort2 = value.Sort2,
            };
        }
        public static implicit operator Sdemo_classEO(Demo_classEO value)
        {
            return new Sdemo_classEO
            {
		        ClassID = value.ClassID,
		        Name = value.Name,
		        Sort1 = value.Sort1,
		        Sort2 = value.Sort2,
            };
        }
        #endregion
    }
}