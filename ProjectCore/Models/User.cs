using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCore.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public int UserRole { get; set; }
    }

    
}
