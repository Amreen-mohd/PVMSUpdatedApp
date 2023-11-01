using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVMSApp.Models
{
    public class PassportIdGenerator
    {
        private static string prefix = "FPS-";
        

        public static string GeneratePrimaryKey(string B)
        {
            int id = int.Parse(new DbOperations().fetchPassportId(B.Split(' ')[0])) + 1;
            string providedId = B.Split(' ')[0];
            
            return prefix+providedId+id.ToString("D4");
        }
    }
}