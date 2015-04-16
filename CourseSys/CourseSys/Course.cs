using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSys
{
    class Course
    {
        private string CID;
        public string cID
        {
            get { return CID; }
            set { CID = value; }
        }

        private string Cname;
        public string cname
        {
            get {return Cname;}
            set {Cname = value;}
        }

        private string Cteacher;
        public string cteacher
        {
            get { return Cteacher; }
            set { Cteacher = value; }
        }

        private string Ctime;
        public string ctime
        {
            get { return Ctime; }
            set { Ctime = value; }
        }

        private string Clocation;
        public string clocation
        {
            get { return Clocation; }
            set { Clocation = value; }
        }

        private string Cintro;
        public string cintro
        {
            get { return Cintro; }
            set { Cintro = value; }
        }

        private string Climit;
        public string climit
        {
            get { return Climit; }
            set { Climit = value; }
        }

        private string Calready;
        public string calready
        {
            get { return Calready; }
            set { Calready = value; }
        }

        public Course(string cID,string cname,string cteacher,string ctime,string cloca)
        {
            this.CID = cID;
            this.Cname = cname;
            this.Cteacher = cteacher;
            this.Ctime = ctime;
            this.Clocation = cloca;
        }
        public Course(string cID, string cname, string cteacher, string ctime, string cloca,string cintro)
        {
            this.CID = cID;
            this.Cname = cname;
            this.Cteacher = cteacher;
            this.Ctime = ctime;
            this.Clocation = cloca;
            this.Cintro = cintro;
        }

        public Course()
        { 

        }
    }
}
