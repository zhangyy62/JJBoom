using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJBoom.DAL
{
    public class AccountContext : DbContext
    {
        static AccountContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<AccountContext>());
        }

        public AccountContext() : base("name=MyConnection")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("accounts", "dbo");
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
