using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsbd_pozhalusta_createbd
{
    public class user
    {
        public string login { get; set; }

        public int id { get;  }
    
        public string user_status { get; set; }

        public user(string login, string user_status, int id)
        {
            this.login = login;
            this.user_status = user_status;
            this.id = id;   
        }

    }


}
