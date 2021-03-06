using System;
using System.Collections.Generic;

namespace Kancelaria.API.Models
{
    public class User
    {
        public int Id{get;set;}

        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Case> Cases { get; set; }
 
    }
}