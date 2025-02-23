using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.UserRequest
{
    public class GetUserRequest:PaginationInput
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? UserClaim { get; set; }
        public string? UserRole{ get; set; }
    }
}
