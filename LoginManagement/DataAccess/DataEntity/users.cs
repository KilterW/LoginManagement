using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginManagement.DataAccess.DataEntity
{
    class Users
    {
        public string User_name { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; } = 1;
        public int Is_validation { get; set; }
    }
}
