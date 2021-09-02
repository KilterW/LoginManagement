using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Core;
using log4net.Repository.Hierarchy;

[assembly:log4net.Config.XmlConfigurator(ConfigFile ="log4net.config",Watch =true)]
namespace LoginManagement.Common
{
    public class LogHelper
    {
        private static ILog m_TraceLog = LogManager.GetLogger("TraceLog");
        private static ILog m_ErrorLog = LogManager.GetLogger("ErrorLog");
        // ************************************************************************************************************
        // 函数名：Log_Enable
        // 输入参数：
        // is_enabled: 是否使能
        // 输出参数：无
        // 返回值：无
        // 说明：
        // 使能Log和trace
        // log日志级别： OFF > FATAL > ERROR > WARN > INFO > DEBUG > ALL
        // ************************************************************************************************************
        public static void Log_Enable(bool is_enabled)
        {
            var hierarchy = LogManager.GetRepository() as Hierarchy;
            var appenders = hierarchy.Root.Repository.GetAppenders();

            foreach (var appender in appenders)
            {
                var _appender = appender as AppenderSkeleton;
                _appender.ClearFilters();
                log4net.Filter.LevelRangeFilter levelRangeFilter = new log4net.Filter.LevelRangeFilter();
                levelRangeFilter.LevelMin = Level.All;
                bool a = Level.All > Level.Off;
                levelRangeFilter.LevelMax = is_enabled == true ? Level.Off : Level.All;
                levelRangeFilter.ActivateOptions();
                _appender.AddFilter(levelRangeFilter);
            }
        }

        // ************************************************************************************************************
        // 函数名：TraceIn
        // 输入参数：
        // className: 类名称
        // func: 函数名
        // info: 参数信息
        // 输出参数：无
        // 返回值：无
        // 说明：
        // 输出函数输入信息
        // ************************************************************************************************************
        public static void TraceIn(string className, string func, string info)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string msg = string.Format("{0}.{1} <- {2}",className,func,info); 
            m_TraceLog?.Info(msg);
        }

        // ************************************************************************************************************
        // 函数名：TraceOut
        // 输入参数：
        // className: 类名称
        // func: 函数名
        // info: 返回值和输出参数
        // 输出参数：无
        // 返回值：字节数组
        // 说明：
        // 输出函数返回信息
        // ************************************************************************************************************
        public static void TraceOut(string className, string func, string info)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string msg = className + "." + func + "-> " + info;
            if (m_TraceLog != null)
            {
                m_TraceLog.Info(msg);
            }
        }

        // ************************************************************************************************************
        // 函数名：LogError
        // 输入参数：
        // className: 类名称
        // func: 函数名
        // info: 报错信息
        // 输出参数：无
        // 返回值：字节数组
        // 说明：
        // 输出错误信息
        // ************************************************************************************************************
        public static void LogError(string className, string func, string info)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string msg = className + "." + func + "-> " + info;
            if (m_ErrorLog != null)
            {
                m_ErrorLog.Error(msg);
            }
        }

        // ************************************************************************************************************
        // 函数名：Print
        // 输入参数：
        // str: 打印信息
        // 输出参数：无
        // 返回值：字节数组
        // 说明：
        // 输出打印信息
        // ************************************************************************************************************
        public static void Print(string info)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var hierarchy1 = LogManager.GetRepository() as Hierarchy;
            var appenders1 = hierarchy1.Root.Repository.GetAppenders();
            if (m_TraceLog != null)
            {
                m_TraceLog.Info(info);
            }

        }
    }
}
