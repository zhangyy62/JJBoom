using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJBoom.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJBoom.UnitTest
{
    [TestClass]
    public class EFAndMySQLUnitTest
    {
        [TestMethod]
        public void ADDToMySQLByEF()
        {
            

            using (AccountContext accountContext = new AccountContext())
            {

                Account account = new Account()
                {
                    Name = "zhangxuan",
                    Password = "1234",
                    Role = 0
                };
                accountContext.Accounts.Add(account);
                accountContext.SaveChanges();
            }
 

        }
    }
}
