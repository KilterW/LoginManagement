using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LoginManagement.Common
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //[CallerMemberName] 可以自动获取属性名称，可以不用传递属性名做参数
        public void DoNotify([CallerMemberName] string propName="")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propName));
        }
    }
}
