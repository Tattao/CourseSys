using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBHelp
    {                       
      private static string strConn = "Data Source=orcl;Persist Security Info=True;User ID=hr;Password=abc123;Unicode=True";
      public static SqlConnection SqlCon = new SqlConnection(strConn);      
    }
}
