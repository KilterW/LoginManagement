using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using System.Configuration;
using LoginManagement.DataAccess.DataEntity;
using LoginManagement.Common;

namespace LoginManagement.DataAccess
{
    class MysqlDataAccess
    {
        public SqlSugarClient MysqlAccess { get; set; }
        private static MysqlDataAccess instance;
        
        public static MysqlDataAccess Instance
        {
            get
            {
                return instance?? (instance=new MysqlDataAccess(ConfigurationManager.ConnectionStrings["db"].ConnectionString,DbType.MySql));
            }
        }

        private MysqlDataAccess(string connStr, DbType dbType)
        {
            MysqlAccess = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = connStr,
                DbType = dbType,
                IsAutoCloseConnection = false
            });
        }

        public SqlSugarClient GetNewdb()
        {
            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlSugarClient newdb = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = connStr,
                DbType=DbType.MySql,
                IsAutoCloseConnection=true
            });
            return newdb;
        }

        public bool CheckUserInfo(string userName,string pwd)
        {
            var isExist = MysqlAccess.Queryable<Users>().Any(it => it.User_name == userName && it.Password == Md5Provider.GetMd5String($"{userName}@{pwd}"));
            return isExist;
        }

        public bool CheckUserValidation(string userName)
        {
            var info = MysqlAccess.Queryable<Users>().First(it => it.User_name == userName);
            if (info.Is_validation==1) return true;
            return false;
        }

        public bool CheckUserExist(string userName)
        {
            var isExist = MysqlAccess.Queryable<Users>().Any(it => it.User_name == userName);
            if (isExist) return true;
            return false;
        }

        public bool AddUser(string userName, string pwd)
        {
            Users newUser = new Users
            {
                User_name = userName,
                //MD5密码加密,  MD5对“用户名+@+密码”加密作为密码  
                Password = Md5Provider.GetMd5String($"{userName}@{pwd}"),  
                Is_validation=1,
            };
            var rtn= MysqlAccess.Insertable(newUser).AS("users").ExecuteCommand();
            if (rtn == 1) return true;
            return false;

        }
    }
}
