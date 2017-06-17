using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WeixinTookeen.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //wxRobot.Util.Utils.Utils.SetAccess("Users", Application.StartupPath);
                //设置应用程序处理异常方式：ThreadException处理  
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常  
                //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常  
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());

                /**
10              * 当前用户是管理员的时候，直接启动应用程序
11              * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
12              */
                //获得当前登录的Windows用户标示
                //System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                //System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                ////判断当前登录用户是否为管理员
                //if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                //{
                //    //如果是管理员，则直接运行
                //    Application.Run(new FormMain());
                //}
                //else
                //{
                //    //创建启动对象
                //    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //    startInfo.UseShellExecute = true;
                //    startInfo.WorkingDirectory = Environment.CurrentDirectory;
                //    startInfo.FileName = Application.ExecutablePath;
                //    //设置启动动作,确保以管理员身份运行
                //    startInfo.Verb = "runas";
                //    try
                //    {
                //        System.Diagnostics.Process.Start(startInfo);
                //    }
                //    catch
                //    {
                //        return;
                //    }
                //    //退出
                //    Application.Exit();
            //}
            }
            catch (Exception ex)
            {
                string str = GetExceptionMsg(ex, string.Empty);
                MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>  
         /// 生成自定义异常消息  
         /// </summary>  
         /// <param name="ex">异常对象</param>  
         /// <param name="backStr">备用异常消息：当ex为null时有效</param>  
         /// <returns>异常字符串文本</returns>  
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
