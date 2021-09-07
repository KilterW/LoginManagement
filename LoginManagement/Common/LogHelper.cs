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

//使用配置文件log4net.config,    Watch=true : 监视配置文件,当配置文件发生变化的时候，就会重新加载。
[assembly:log4net.Config.XmlConfigurator(ConfigFile ="log4net.config",Watch =true)]
namespace LoginManagement.Common
{
    public class LogHelper
    {
        //获得配置文件中相应的Logger,其中"TraceLog/ErrorLog"便是我们自定义的日志对象<logger>的name的值。
        private static ILog m_TraceLog = LogManager.GetLogger("TraceLog");
        private static ILog m_ErrorLog = LogManager.GetLogger("ErrorLog");

        // ************************************************************************************************************
        // 函数名：TraceIn
        // 输入参数：
        // className: 类名称
        // func: 函数名
        // info: 参数信息
        // 输出参数：无
        // 返回值：无
        // 说明：输出函数输入信息
        // ************************************************************************************************************
        public static void TraceIn(string className, string func, string info)
        {
            string msg = string.Format("{0}.{1} <- {2}",className,func,info);
            if (m_TraceLog.IsInfoEnabled)
            {
                m_TraceLog?.Info(msg);
            }
        }

        // ************************************************************************************************************
        // 函数名：TraceOut
        // 输入参数：
        // className: 类名称
        // func: 函数名
        // info: 返回值和输出参数
        // 输出参数：无
        // 返回值：无
        // 说明：输出函数返回信息
        // ************************************************************************************************************
        public static void TraceOut(string className, string func, string info)
        {
            string msg = string.Format("{0}.{1} -> {2}", className, func, info);
            if (m_TraceLog.IsInfoEnabled)
            {
                m_TraceLog?.Info(msg);
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
        // 说明：输出错误信息
        // ************************************************************************************************************
        public static void LogError(string className, string func, string info)
        {
            string msg = string.Format("{0}.{1} -> {2}", className, func, info);
            if (m_ErrorLog.IsErrorEnabled)
            {
                m_ErrorLog?.Error(msg);
            }
        }

        // ************************************************************************************************************
        // 函数名：LogErrorException
        // 输入参数：
        // className: 类名称
        // func: 函数名
        // info: 报错信息
        // 输出参数：无
        // 返回值：字节数组
        // 说明：输出错误信息&异常
        // ************************************************************************************************************
        public static void LogErrorException(string className, string func, string info, Exception ex)
        {
            string msg = string.Format("{0}.{1} -> {2}", className, func, info);
            if (m_ErrorLog.IsErrorEnabled)
            {
                m_ErrorLog?.Error(msg,ex);
            }
        }
    }
}
