using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LoginManagement.Common;

namespace LoginManagement.Model
{
    public class LoginModel:NotifyBase
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                DoNotify();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                DoNotify();
            }
        }

        private string _validationCode;

        public string ValidationCode
        {
            get { return _validationCode; }
            set
            {
                _validationCode = value;
                DoNotify();
            }
        }

        private string _originalValidationCode;

        public string OriginalValidationCode
        {
            get { return _originalValidationCode; }
            set
            {
                _originalValidationCode = value;
                DoNotify();
            }
        }


        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                DoNotify();
            }
        }








    }
}
