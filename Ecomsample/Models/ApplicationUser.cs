using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomsample.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }

        public IEnumerable<Order> Order { get; set; }


    }
}
