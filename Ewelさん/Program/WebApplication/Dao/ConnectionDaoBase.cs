using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Dao
{
    public class ConnectionDaoBase
    {
        protected String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        //protected String connectionString = @"Data Source=192.168.183.129;Initial Catalog=INTERN_TEAM1;Persist Security Info=True;User Id=sa;Password=INterN123";
         

    }
}