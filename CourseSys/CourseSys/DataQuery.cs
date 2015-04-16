using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Data.OracleClient;

namespace CourseSys
{
    class DataQuery
    {
        private static string Consgr = "Data Source=orcl;Persist Security Info=True;User ID=hr;Password=abc123;Unicode=True";
        private static OracleConnection Oac = new OracleConnection(Consgr);
        
        public static string VerificationLogin(string IptAct)
        {
            string USERPWD;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("SELECT USERPWD FROM CS_USER WHERE USERID=" + IptAct);
            USERPWD = QueryExct(CmdB);

            Oac.Close();
            return USERPWD;
        }//验证用户密码

        private static string QueryExct(StringBuilder CmdB)
        {
            string Data;//要查询的字符串型数据
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else if(Oac.State==ConnectionState.Closed)
            {
                Oac.Open();
            }
            OracleCommand Ocmd = new OracleCommand(CmdB.ToString(), Oac);
            Data = Ocmd.ExecuteScalar().ToString();
            return Data;
        }

        private static object QueryExctobj(StringBuilder CmdB)
        {
            object Data;//要查询的字符串型数据
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else if (Oac.State == ConnectionState.Closed)
            {
                Oac.Open();
            }
            OracleCommand Ocmd = new OracleCommand(CmdB.ToString(), Oac);
            Data = Ocmd.ExecuteScalar();
            return Data;
        }
                
        public static string GetCol(string colname,int rn)
        {
            string col;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("select " + colname + " from (select courseid,coursename,courseteacher,coursetime,courseloca,coursealready,courselimit,courseintro,row_number() over(order by courseid) as rn from CS_COURSE)AAA WHERE rn in " + rn);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            col = QueryExctobj(CmdB).ToString();
            Oac.Close();
            return col;
        }//获取特定列数据

        public static string GetColbyID(string colname, string cid)
        {
            string col;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("select " + colname + " from cs_course where courseid="+cid);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            col = QueryExctobj(CmdB).ToString();
            Oac.Close();
            return col;
        }//获取特定列数据

        public static int GetSlectedNum(string uid)
        {
            int col;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("select count(*) from cs_selecting where userid=" + uid);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            col = int.Parse(QueryExctobj(CmdB).ToString());
            Oac.Close();
            return col;
        }//获取该生选课数

        public static string GetSlectedCID(string uid, int rn)
        {
            string col;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("select courseid from (select courseid,row_number() over(order by courseid) as rn from (select * from cs_selecting t where t.userid=" + uid + "))AAA WHERE rn in " + rn);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            col = QueryExctobj(CmdB).ToString();
            Oac.Close();
            return col;
        }//获取选课的特定行id
                
        public static bool ChkFull(string cid)
        {
            bool isFull = true;
            int already = 0;
            int limit = 0;
            StringBuilder CmdB1 = new StringBuilder();
            CmdB1.Append("select coursealready from CS_COURSE WHERE COURSEID="+cid);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            already = int.Parse(QueryExctobj(CmdB1).ToString());
            Oac.Close();

            StringBuilder CmdB2 = new StringBuilder();
            CmdB2.Append("select courselimit from CS_COURSE WHERE COURSEID=" + cid);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            limit = int.Parse(QueryExctobj(CmdB2).ToString());
            Oac.Close();
            isFull = already == limit;
            return isFull;
        }//查看课程是否选满

        public static string GetUserName(string IptAct)
        {
            string USERNAME;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("SELECT USERNAME FROM CS_USER WHERE USERID=" + IptAct);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            USERNAME = QueryExct(CmdB);
            Oac.Close();
            return USERNAME;
        }//获取用户名称
                
        public static int GetUserType(string IptAct)
        {
            int USERTYPE = 0;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("SELECT USERTYPE FROM CS_USER WHERE USERID=" + IptAct);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            USERTYPE = int.Parse(QueryExct(CmdB));
            Oac.Close();
            return USERTYPE;
        }//获取用户类型

        public static OracleCommand InsertInfo(string Uid, string Cid)
        {
            //if (Oac.State != ConnectionState.Open)
            //{
            //    Oac.Open();
            //}
            StringBuilder Ocmd = new StringBuilder();
            //OracleTransaction tran = Oac.BeginTransaction();
            Ocmd.Append("INSERT INTO CS_SELECTING VALUES( :Useid, :Csid, :Cked);");
            //Ocmd.Append("VALUES( :Userid, :Courseid, :Chked)");
            OracleCommand CMD = new OracleCommand(Ocmd.ToString());
            CMD.Parameters.Add("Userid", OracleType.VarChar).Value = Uid;
            CMD.Parameters.Add("Csid", OracleType.Char).Value = Cid;
            CMD.Parameters.Add("Cked", OracleType.Int16).Value = 0;
            //CMD.Parameters.AddWithValue("Userid",Uid);
            //CMD.Parameters.AddWithValue("Csid",Cid);
            //CMD.Parameters.AddWithValue("Cked",0);
           
            
            //tran.Commit();
            //Oac.Close();
            return CMD;
        }//INSERT THE DATA
        public static OracleCommand Select(string Cid)
        {
            StringBuilder Ocmd = new StringBuilder();
            Ocmd.Append("SELECT COURSEALREADY FROM CS_COURSE WHERE COURSEID=" + Cid);
            OracleCommand CMD = new OracleCommand(Ocmd.ToString());
            return CMD;
        }//GOT THE VALUE OF COURSE ALREADY FROM THE CS_COURSE
        //YOU NEED A "STRING" TYPE TO SAVE THE RESULT
        public static OracleCommand Update(string Cid, int Already)
        {
            StringBuilder Ocmd = new StringBuilder();
            Ocmd.Append("UPDATE CS_COURSE SET COURSEALREADY= :alrdy Where COURSEID=" + Cid+";");
            //Ocmd.Append("SET COURSEALREADY=:ardy Where COURSEID=" + Cid);
            OracleCommand CMD = new OracleCommand(Ocmd.ToString());
            CMD.Parameters.Add("ardy", OracleType.Int16).Value = Already;
            //CMD.Parameters.AddWithValue("ardy",Already);
            return CMD;
        }//UPDATE THE VALUE OF ALREADY
        public static void QueryExctWithouValue(OracleCommand CMD)
        {
            //if (Oac.State == ConnectionState.Open)
            //{
            //    Oac.Close();
            //    Oac.Open();
            //}
            //else if (Oac.State == ConnectionState.Closed)
            //{
            //    Oac.Open();
            //}
            string rowId = string.Empty;
            if (Oac.State != ConnectionState.Open)
            {
                Oac.Open();
            }
            CMD.Connection = Oac;
             OracleString oracleRowId;
            int p = CMD.ExecuteOracleNonQuery(out oracleRowId);
            rowId = oracleRowId.Value;
            Oac.Close();
        }//EXECUTE THE QUERY WITHOUT ANY RETURN VALUE

        //获取特定行的课程
        //public static object GetACourse(int rn) {
        //    object course;
        //    StringBuilder CmdB = new StringBuilder();
        //    CmdB.Append("select * from (select courseid,coursename,courseteacher,coursetime,courseloca,coursealready,courselimit,row_number() over(order by courseid) as rn from CS_COURSE)AAA WHERE rn in "+rn);
        //    if (Oac.State == ConnectionState.Open)
        //    {
        //        Oac.Close();
        //        Oac.Open();
        //    }
        //    else
        //    {
        //        Oac.Open();
        //    }
        //    course = QueryExctobj(CmdB);
        //    Oac.Close();
        //    return course;
        //}

        public static string GetACourse(int rn)
        {
            string courseid;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("select courseid from (select courseid,row_number() over(order by courseid) as rn from CS_COURSE)AAA WHERE rn in " + rn);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            courseid = QueryExctobj(CmdB).ToString();
            Oac.Close();
            return courseid;
        }//获取特定行的课程的id
        
        public static int GetCourseNum()
        {
            int courses;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("SELECT COUNT(*) FROM CS_COURSE");
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            courses = int.Parse(QueryExct(CmdB).ToString());
            Oac.Close();
            return courses;
        }//获取课程个数
                
        public static string GetTName(string TID){
            string name;
            StringBuilder CmdB = new StringBuilder();
            CmdB.Append("SELECT USERNAME FROM CS_USER WHERE USERID="+TID);
            if (Oac.State == ConnectionState.Open)
            {
                Oac.Close();
                Oac.Open();
            }
            else
            {
                Oac.Open();
            }
            name = QueryExct(CmdB).ToString();
            Oac.Close();
            return name;
        }//获取教师名称

        public static object[] coursetbsource(int coursenum)
        {
             string[] r=new string[]{"","","","","","",""};
             object[] rows = new object[11] { r, r, r, r, r, r, r, r, r, r, r };
             int num = 0;
            for (int i = 1; i <= coursenum; i++)
            {
                if(int.Parse(GetCol("coursealready", i))<int.Parse(GetCol("courselimit", i)))
                {
                string[] ca = new string[] { GetCol("courseid", i), GetCol("coursename", i), GetTName(GetCol("courseteacher", i)), GetCol("coursetime", i), GetCol("courseloca", i), GetCol("coursealready", i), GetCol("courselimit", i) };
                //Course c = new Course();
                //c.cID = GetCol("courseid", i);
                //c.cintro = GetCol("courseintro", i);
                //c.clocation = GetCol("courseloca", i);
                //c.cname = GetCol("coursename", i);
                //c.cteacher = GetCol("courseteacher", i);
                //c.ctime = GetCol("coursetime", i);
                //c.climit = GetCol("courselimit", i);
                //c.calready = GetCol("coursealready", i);
                rows[i - 1] = ca;
                num += 1;
                }
            }
            for (int j = 1; j <= coursenum; j++)
            {
                if (int.Parse(GetCol("coursealready", j)) >= int.Parse(GetCol("courselimit", j)))
                {
                    string[] ca = new string[] { GetCol("courseid", j), GetCol("coursename", j), GetTName(GetCol("courseteacher", j)), GetCol("coursetime", j), GetCol("courseloca", j), GetCol("coursealready", j), GetCol("courselimit", j) };
                    rows[num] = ca;
                    num ++;
                }
            }
            return rows;
        }//填入可选课程信息

        public static object[] selectedcoursesrc(string uid)
        {
            string[] r = new string[] { "", "", "", "", "", "", "" };
            object[] rows = new object[11] { r, r, r, r, r, r, r, r, r, r, r };
            int cnum = GetSlectedNum(uid);
            for (int n = 1; n <= cnum; n++)
            {
                string cid=GetSlectedCID(uid, n);
                string[] sc = new string[] { cid, GetColbyID("coursename", cid), GetColbyID("courseteacher", cid), GetColbyID("coursetime", cid), GetColbyID("courseloca", cid) };
                rows[n - 1] = sc;
            }
            return rows;
        }//填入已选课程信息
    }
}
