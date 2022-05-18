using System;
using Microsoft.AspNetCore.Identity;

namespace Store.Models
{
    public class Employee : IdentityUser
    {
        public DateTime EmploymentDate { get; set; }
        public string Position { get; set; }
    }
}