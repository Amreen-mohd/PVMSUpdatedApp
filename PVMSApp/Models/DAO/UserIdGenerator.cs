using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVMSApp.Models.DAO
{
    public class UserIdGenerator
    {
        private static string prefix = "USER";
        private static int id = int.Parse(new DbOperations().fetchUserId())+1;



        public static string GeneratePrimaryKey()
        {

            string generatedId = prefix + (id).ToString("D4");
            return generatedId;
        }
    }
}