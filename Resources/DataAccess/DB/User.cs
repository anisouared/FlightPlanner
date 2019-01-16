using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.DB
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Flight = new HashSet<Flight>();
        }

        public string Id { get; set; }

        public ICollection<Flight> Flight { get; set; }
    }
}
