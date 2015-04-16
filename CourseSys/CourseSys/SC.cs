using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSys
{
    class SC
    {
        private string UID;
        private string CID;
        public string cID
        {
            get { return CID; }
            set { CID = value; }
        }
        public string uID
        {
            get { return UID; }
            set { UID = value; }
        }


        public SC(string cID,string uID)
        {
            this.CID = cID;
            this.UID = uID;
        }

        public SC() { 

        }
    }
}
