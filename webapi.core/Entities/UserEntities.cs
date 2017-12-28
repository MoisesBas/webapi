using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.core.Entities
{
    public class UserEntities : BaseEntities
    {
        public string userName { get; set; }
        public string password { get; set; }      
        public DateTime created { get; set; }
        public string createdby { get; set; }
        public DateTime modified { get; set; }
        public string modifiedby { get; set; }
    }
}
