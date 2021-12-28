using LoginManagement.Common;
using LoginManagement.DataAccess.DataEntity;
using LoginManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginManagement.ViewModel
{
    public class DataViewModel:NotifyBase
    {
        public DataModel DataModel { get; set; } = new DataModel();
        public DataViewModel()
        {
            DataModel.Courses = new ObservableCollection<Course>() {
                new Course()
                {
                    Name = "AA",
                    ID = 1,
                    StartDate = new DateTime(2021, 10, 11),
                    EndDate = new DateTime(2021, 10, 22)
                },
                new Course()
                {
                    Name = "BB",
                    ID = 2,
                    StartDate = new DateTime(2021, 11, 11),
                    EndDate = new DateTime(2021, 11, 22)
                },
                new Course()
                {
                    Name = "CC",
                    ID = 3,
                    StartDate = new DateTime(2021, 12, 11),
                    EndDate = new DateTime(2021, 12, 22)
                },

                };
        }
    }
}
