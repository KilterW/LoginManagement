using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows;
using LoginManagement.View;

namespace LoginManagement
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (EnsureInstance())
            {
                return;
            }
            if (new LoginView().ShowDialog() == true)
            {
                new MainView().ShowDialog();
            }

            //new MainView().ShowDialog();
            Application.Current.Shutdown();
        }

        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int SW_SHOWNOMAL = 1;
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);//显示
            SetForegroundWindow(instance.MainWindowHandle);//当到最前端
        }
        private static Process[] RuningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);
            return processes.Where(x => x.Id != currentProcess.Id).ToArray();
        }
        private static bool EnsureInstance()
        {
            var intance = RuningInstance();
            var select = false;
            if (intance != null && intance.Count() > 0)
            {
                var result = MessageBox.Show(string.Format(CultureInfo.CurrentCulture,
                       $"软件已经在后台运行{intance.Count()}个实例！\r\n" +
                       $"路径：\r\n{GetProcessPath(intance)}\r\n\n" +
                       "请选择：\n" +
                       "1.关闭后台程序重新运行\t[是]\n" +
                       "2.显示后台程序\t\t[否]\n" +
                       "3.保留后台程序并启动新程序\t[取消]\n"),
                    "LoginManagement", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        KillProcess(intance);
                        break;
                    case MessageBoxResult.No:
                    case MessageBoxResult.None:
                        Array.ForEach(intance, x => HandleRunningInstance(x));
                        select = true;
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                    default:
                        break;
                }
            }
            return select;
        }
        private static string GetProcessPath(Process[] processes)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < processes.Count(); i++)
            {
                sb.Append($"{i + 1}:{processes[i].MainModule.FileName}\n");
            }
            return sb.ToString();
        }
        private static void KillProcess(Process[] processes)
        {
            foreach (var p in processes)
            {
                p.Kill();
                if (!p.HasExited)
                {
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, "进程关闭失败！请手动确认进程已关闭"),
                    "LoginManagement", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
