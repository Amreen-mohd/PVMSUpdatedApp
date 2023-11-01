using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVMSApp.Models.DAO
{
    public class DbOperations
    {
        test3Entities1 t=new test3Entities1();
        public String insert(user_registration U)
        {
            t.user_registration.Add(U);
            t.SaveChanges();
            return "Your User Id and your password is";
        }
    }
}