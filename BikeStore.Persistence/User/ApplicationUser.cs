
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.User
{
    public class ApplicationUser:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
    }

    public class ApplicationRole : IdentityRole<int> 
    { 
    
    }
}
