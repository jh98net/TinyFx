using System;
using System.Collections.Generic;
using System.Text;
using TinyFx.ChineseCalendar;

namespace TinyFx
{
    /// <summary>
    /// 提供TinyFx辅助方法
    /// </summary>
    public static partial class TinyFxUtil
    {
        #region 日期

        /// <summary>
        /// 获取指定日期所在周的起始日期，时分秒和传入日期相同
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <param name="isMonday">是否以周一为每周第一天</param>
        /// <returns></returns>
        public static DateTime BeginDayOfWeek(this DateTime date, bool isMonday = true)
        {
            int days = 0;
            if (isMonday)
            {
                days = date.DayOfWeek - DayOfWeek.Monday;
                if (days == -1) days = 6;
            }
            else
            {
                days = date.DayOfWeek - DayOfWeek.Sunday;
            }
            return date.Subtract(new TimeSpan(days, 0, 0, 0));
        }

        /// <summary>
        /// 获取指定日期所在周的终止日期，时分秒和传入日期相同
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <param name="isMonday">是否以周一为每周第一天</param>
        /// <returns></returns>
        public static DateTime EndDayOfWeek(this DateTime date, bool isMonday = true)
            => BeginDayOfWeek(date, isMonday).AddDays(6);

        /// <summary>
        /// 获取时间段内包含的总周数（含起始周和结束周）
        /// </summary>
        /// <param name="start">起始日期</param>
        /// <param name="end">终止日期</param>
        /// <returns></returns>
        public static int WeekCount(this DateTime start, DateTime end)
        {
            DateTime min = DateTime.MinValue;
            start = BeginDayOfWeek(start);
            int days = (int)end.Subtract(min).TotalDays - (int)start.Subtract(min).TotalDays;
            return (int)days / 7 + 1;
        }

        /// <summary>
        /// 获取指定年份的总周数
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static int WeekCountOfYear(int year)
        {
            DateTime start = new DateTime(year, 1, 1);
            DateTime end = new DateTime(year + 1, 1, 1).AddDays(-1);
            return WeekCount(start, end);
        }

        /// <summary>
        /// 获取指定日期在这一年的第几周中
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime date)
        {
            DateTime start = new DateTime(date.Year, 1, 1);
            return WeekCount(start, date);
        }

        /// <summary>
        /// 获取指定日期在这一月的第几周中
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int WeekOfMonth(this DateTime date)
        {
            DateTime start = new DateTime(date.Year, date.Month, 1);
            return WeekCount(start, date);
        }

        /// <summary>
        /// 获取指定年份中指定周数（第几周）的起始日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="week">周数</param>
        /// <returns></returns>
        public static DateTime BeginDayOfWeek(int year, int week)
            => BeginDayOfWeek(new DateTime(year, 1, 1)).AddDays((week - 1) * 7);

        /// <summary>
        /// 获取某年第几周的周末日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="week">周数</param>
        /// <returns></returns>
        public static DateTime EndDayOfWeek(int year, int week)
            => BeginDayOfWeek(year, week).AddDays(6);

        /// <summary>
        /// 判断指定日期是否是当前日期所在的周
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsCurrentWeek(this DateTime date)
            => IsSameWeek(date, DateTime.Now);

        /// <summary>
        /// 判断指定的日期是否在同一周中
        /// </summary>
        /// <param name="dtA">要比较的第一个日期</param>
        /// <param name="dtB">要比较的第二个日期</param>
        /// <returns></returns>
        public static bool IsSameWeek(this DateTime dtA, DateTime dtB)
        {
            DateTime dt = BeginDayOfWeek(dtA);
            int days = (int)(dtB - dt).TotalDays;
            return (days > 0 && days < 7);
        }

        /// <summary>
        /// 获取上个月的第一天
        /// </summary>
        /// <param name="dateTime">要传入的时间</param>
        /// <returns></returns>
        public static DateTime FirstDayOfPreviousMonth(this DateTime dateTime)
            => dateTime.AddDays(1 - dateTime.Day).AddMonths(-1);

        /// <summary>
        /// 获取上个月的最后一天
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime LastDayOfPrdviousMonth(this DateTime dateTime)
            => dateTime.AddDays(1 - dateTime.Day).AddDays(-1);

        #endregion

        #region DateTime和TimeStamp (又叫Unix时间戳, Unix epoch, Unix time, Unix timestamp)

        private static DateTime _utcStartDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// 将DateTime转换为Unix时间戳timestamp
        /// </summary>
        /// <param name="date">转换的日期时间</param>
        /// <returns></returns>
        public static long DateTimeToTimestamp(this DateTime date)
        {
            var utc = date.ToUniversalTime();
            TimeSpan ts = (utc - _utcStartDate);
            return (long)ts.TotalMilliseconds;//精确到毫秒
        }
        private static DateTime _localStartDate = DateTime.MinValue;
        //private static DateTime _localStartDate = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        private static DateTime localStartDate 
        {
            get
            {
                if (_localStartDate == DateTime.MinValue) 
                {
                    TimeZoneInfo.ClearCachedData();
                    _localStartDate = new DateTime(1970, 1, 1) + TimeZoneInfo.Local.BaseUtcOffset;
                }
                return _localStartDate;
            }
        }
        /// <summary>
        /// Unix时间戳timestamp转为DateTime
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式,例如1482115779</param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(string timeStamp)
        {
            long longTimeStamp = long.Parse(timeStamp + (timeStamp.Length == 13 ? "0000" : "0000000"));
            TimeSpan ts = new TimeSpan(longTimeStamp);
            return localStartDate.Add(ts);
        }
        /// <summary>
        /// 将Unix时间戳timestamp转换为DateTime
        /// </summary>
        /// <param name="unixTime">Unix时间戳</param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(this long unixTime)
            => TimestampToDateTime(unixTime.ToString());

        /// <summary>
        /// 将DateTimeOffset转换为Unix时间戳timestamp
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeOffsetToTimestamp(DateTimeOffset dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }
        /// <summary>
        /// 将Unix时间戳timestamp转换为DateTimeOffset
        /// </summary>
        /// <param name="intDate"></param>
        /// <returns></returns>
        public static DateTimeOffset TimestampToDateTimeOffset(long intDate)
        {
            var timeInTicks = intDate * TimeSpan.TicksPerSecond;
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).AddTicks(timeInTicks);
        }
        #endregion

        #region 星座
        /// <summary>
        /// 获取星座
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="day">日期</param>
        /// <returns></returns>
        public static string GetConstellation(int month, int day)
            => new ChineseCalendarHelper(DateTime.Now.Year, month, day).ConstellationText;

        /// <summary>
        /// 获取星座
        /// </summary>
        /// <param name="birthday">出生日期</param>
        /// <returns></returns>
        public static string GetConstellation(DateTime birthday)
            => new ChineseCalendarHelper(birthday).ConstellationText;
        #endregion
    }
}
