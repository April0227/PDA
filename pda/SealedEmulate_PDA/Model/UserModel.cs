using System;
using System.Collections.Generic;
using System.Text;

namespace SealedEmulate_PDA.Model
{
    public class UserModel : BaseModel
    {
        private string _name;
        public string UserName
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _pwd;
        public string UserPwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
    }
}
