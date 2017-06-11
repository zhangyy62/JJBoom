using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJBoom.DAL
{
 
    public class Account
    {
        [Key]
        public int AccountKey { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public byte Role { get; set; }
    }
}
