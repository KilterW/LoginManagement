using LoginManagement.Common;
using LoginManagement.DataAccess.DataEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginManagement.Model
{
    public class DataModel:NotifyBase
    {
        private ObservableCollection<Course> _courses;
        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set
            {
                if (_courses == value) return;
                _courses = value;
                DoNotify();
            }
        }

    }
}
