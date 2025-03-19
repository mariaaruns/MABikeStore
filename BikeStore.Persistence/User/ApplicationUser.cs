
using BikeStore.Domain.Models;
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
        public virtual Staff Staff { get; set; }
        public virtual ICollection<RepairService> RepairService {get;set;}
        public virtual ICollection<IdentityUserRole<int>> UserRoles { get; set; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    }

    public class ApplicationRole : IdentityRole<int> 
    {
        
    }

    /*public class ApplicationUserRole : IdentityUserRole<int> 
    { 
    
    public virtual ApplicationRole Roles { get; set; }
    }*/
}
