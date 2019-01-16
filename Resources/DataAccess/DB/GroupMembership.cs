using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.DB
{
    public partial class GroupMembership : IdentityRole 
    {
        public string Id { get; set; }
    }
}
