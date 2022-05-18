using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Store.Models
{
    public class Customer : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public virtual IList<Invoice> Invoices { get; set; }
    }
}