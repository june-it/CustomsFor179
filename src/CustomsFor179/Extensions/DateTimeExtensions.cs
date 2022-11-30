namespace CustomsFor179.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 时间转换为时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            return (long)(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc) - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
        }
    }
}
