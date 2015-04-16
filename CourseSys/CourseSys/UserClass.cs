using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSys
{
    public class UserClass
    {
        public string UserAccount;
        public string UserPassword;
        public int UserType;
        public string UserName;
        public UserClass(string userAct, string userPsd, int userTyp,string userNam)
        {
            UserAccount = userAct;
            UserPassword = userPsd;
            UserType = userTyp;
            UserName = userNam;
        }
        public UserClass()
        {

        }
    }
}
