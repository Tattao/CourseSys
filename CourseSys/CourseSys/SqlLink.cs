using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSys
{
    class SqlLink
    {
        public static string SqlAddress = "Data Source=orcl;Persist Security Info=True;User ID=hr;Password=abc123;";
        public static SqlConnection SqlCon = new SqlConnection(SqlAddress);  
    }
}
