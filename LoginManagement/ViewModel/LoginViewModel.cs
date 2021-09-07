using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LoginManagement.Common;
using LoginManagement.DataAccess;
using LoginManagement.Model;

namespace LoginManagement.ViewModel
{
    class LoginViewModel
    {
        public LoginModel LoginModel { get; set; } = new LoginModel();

        public CommandBase CloseWindowCommand { get; set; } 
        public CommandBase LoginCommand { get; set; }
        public CommandBase ChangeValidationCodeCommand { get; set; }
        public CommandBase RegisterCommand { get; set; }

        public LoginViewModel()
        {
            InitialCommand();

            LoginModel.OriginalValidationCode = GenerateChineseWords();

        }

        private void InitialCommand()
        {
            CloseWindowCommand = new CommandBase
            {
                DoExecute = new Action<object>((o) =>
                {
                    (o as Window).Close();
                }),
                DoCanExecute = new Func<object, bool>((o) => { return true; })
            };

            LoginCommand = new CommandBase
            {
                DoExecute = new Action<object>(DoLogin),
                DoCanExecute = new Func<object, bool>((o) => { return true; })
            };

            ChangeValidationCodeCommand = new CommandBase
            {
                DoExecute = new Action<object>((o) => { LoginModel.OriginalValidationCode = GenerateChineseWords(); }),
                DoCanExecute = new Func<object, bool>((o) => { return true; })
            };

            RegisterCommand = new CommandBase
            {
                DoExecute=new Action<object>(DoRegister),
                DoCanExecute=new Func<object, bool>((o)=> { return true; })
            };
        }
        
        private void DoLogin(object o)
        {
            try
            {
                LogHelper.TraceIn(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, string.Format("object o:{0}", o));
                if (string.IsNullOrEmpty(LoginModel.UserName))
                {
                    LoginModel.ErrorMessage = "用户名不能为空";
                    LogHelper.LogError(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "登录失败，用户名不能为空");
                    return;
                }

                if (string.IsNullOrEmpty(LoginModel.Password))
                {
                    LoginModel.ErrorMessage = "密码不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(LoginModel.ValidationCode))
                {
                    LoginModel.ErrorMessage = "验证码不能为空";
                    return;
                }
                if (LoginModel.ValidationCode != LoginModel.OriginalValidationCode)
                {
                    LoginModel.ErrorMessage = "验证码错误";
                    return;
                }
                LoginModel.ErrorMessage = "";
                Task.Run(new Action(() =>
                {
                    try
                    {
                        bool isExist = MysqlDataAccess.Instance.CheckUserInfo(LoginModel.UserName, LoginModel.Password);
                        if (!isExist)
                        {
                            LoginModel.ErrorMessage = "用户名或者密码错误";
                            return;
                        }
                        bool is_validation = MysqlDataAccess.Instance.CheckUserValidation(LoginModel.UserName);
                        if (!is_validation)
                        {
                            LoginModel.ErrorMessage = "用户名已失效";
                            return;
                        }

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            (o as Window).DialogResult = true;
                        }));

                    }
                    catch (Exception ex)
                    {
                        LoginModel.ErrorMessage = ex.Message;
                        LogHelper.LogErrorException(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "数据库核对用户信息异常", ex);
                    }
                }));
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorException(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "登录异常" , ex);
            }
            
            
        }

        //随机生成常用的汉字
        private string GenerateChineseWords()
        {
            string chineseWords = "";
            Random rm = new Random();
            Encoding gb = Encoding.GetEncoding("gb2312");

            for (int i = 0; i < 4; i++)
            {
                // 获取区码(常用汉字的区码范围为16-55)   
                int regionCode = rm.Next(16, 55);
                // 获取位码(位码范围为1-94 由于55区的90,91,92,93,94为空,故将其排除)   
                int positionCode;
                if (regionCode == 55)
                {
                    // 55区排除90,91,92,93,94   
                    positionCode = rm.Next(1, 90);
                }
                else
                {
                    positionCode = rm.Next(1, 95);
                }

                // 转换区位码为机内码   
                int regionCode_Machine = regionCode + 160;// 160即为十六进制的20H+80H=A0H   
                int positionCode_Machine = positionCode + 160;// 160即为十六进制的20H+80H=A0H   

                // 转换为汉字   
                byte[] bytes = new byte[] { (byte)regionCode_Machine, (byte)positionCode_Machine };
                chineseWords += gb.GetString(bytes);
            }

            return chineseWords;
        }

        private void DoRegister(object o)
        {
            if (string.IsNullOrEmpty(LoginModel.UserName))
            {
                LoginModel.ErrorMessage = "注册时，用户名不能为空";
                return;
            }

            if (string.IsNullOrEmpty(LoginModel.Password))
            {
                LoginModel.ErrorMessage = "注册时，密码不能为空";
                return;
            }

            Task.Run(new Action(()=> 
            {
                try
                {
                    var isExist = MysqlDataAccess.Instance.CheckUserExist(LoginModel.UserName);
                    if (isExist)
                    {
                        LoginModel.ErrorMessage = "用户名已被注册";
                        return;
                    }
                    var rtn= MysqlDataAccess.Instance.AddUser(LoginModel.UserName,LoginModel.Password);
                    if (rtn)
                    {
                        LoginModel.ErrorMessage = "注册成功";
                        return;
                    }
                    LoginModel.ErrorMessage = "注册异常，请重试";
                }
                catch (Exception ex)
                {
                    LoginModel.ErrorMessage = ex.Message;
                    return;
                }
                
                    
            }));


        }

    }

    
    
}
