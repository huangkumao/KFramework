using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeHelper
{
    //服务器时间戳 秒
    private static long sServerTimestamp;
    private static float sLastUpdateTime;
    public static long ServerTimestamp
    {
        set
        {
            sServerTimestamp = value;
            sLastUpdateTime = Time.realtimeSinceStartup;
        }
        get { return sServerTimestamp + (int) (Time.realtimeSinceStartup - sLastUpdateTime); }
    }

    //客户端时间戳 秒
    public static long GetClientTimestamp()
    {
        return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
    }

    //获取服务器时间
    private static readonly DateTime sBaseTime = new DateTime(1970, 1, 1);
    public static DateTime GetServerTime(long pTime = 0)
    {
        if(pTime > 0)
            return sBaseTime.AddSeconds(pTime);

        return sBaseTime.AddSeconds(ServerTimestamp);
    }

    public static string TimeToString(long pTime, string pFormat = "{0}时{1}分{2}秒")
    {
        var DT = GetServerTime(pTime);
        return string.Format(pFormat, DT.Hour, DT.Minute, DT.Second);
    }

    //倒计时
    public static string CountdownToString(long pEndTime)
    {
        long countdown = pEndTime - ServerTimestamp;
        if (countdown > 0)
        {
            long day = countdown / 86400;
            countdown = countdown % 86400;
            long hour = countdown / 3600;
            countdown = countdown % 3600;
            long minute = countdown / 60;
            long second = countdown % 60;
            if (day != 0)
                return string.Format("{0}天{1}小时{2}分钟{3}秒", day, hour, minute, second);
            else
                return string.Format("{0}小时{1}分钟{2}秒", hour, minute, second);
        }
        return "已结束";
    }

    public static TimeSpan CountdownToTime(long pEndTime)
    {
        long countdown = pEndTime - ServerTimestamp;
        if (countdown > 0)
        {
            long day = countdown / 86400;
            countdown = countdown % 86400;
            long hour = countdown / 3600;
            countdown = countdown % 3600;
            long minute = countdown / 60;
            long second = countdown % 60;
            return new TimeSpan((int)day, (int)hour, (int)minute, (int)second);
        }
        return default(TimeSpan);
    }

    //星期
    public static DayOfWeek GetWeek(long pTime = 0)
    {
        return GetServerTime(pTime).DayOfWeek;
    }

    //零点
    public static DateTime GetTodayZero(long pTime = 0)
    {
        var DT = GetServerTime(pTime);
        return new DateTime(DT.Year, DT.Month, DT.Day);
    }

    public static DateTime GetTomorrowZero(long pTime = 0)
    {
        return GetTodayZero(pTime).AddDays(1);
    }

    //月初 月末
    public static DateTime GetBeginMonth(long pTime = 0)
    {
        var DT = GetServerTime(pTime);
        return new DateTime(DT.Year, DT.Month, 1);
    }

    public static DateTime GetEndMonth(long pTime = 0)
    {
        return GetBeginMonth(pTime).AddMonths(1);
    }

    //距离明天的时间 秒
    public static int ResidueTomorrow(long pTime = 0)
    {
        var _TS = GetTomorrowZero(pTime) - GetServerTime(pTime);
        return (int)_TS.TotalSeconds;
    }

    //距离月底的时间 秒
    public static int ResidueNextMonth(long pTime = 0)
    {
        var _TS = GetEndMonth(pTime) - GetServerTime(pTime);
        return (int)_TS.TotalSeconds;
    }
}
