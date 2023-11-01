using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVMSApp.Models
{
    public class VisaIdgenerator
    {
        private static string prefix = "VISA-";
        

        public string GeneratePrimaryKey()
        {
            int id = int.Parse(new DbOperations().fetchVisaId()) + 1;
            string generatedId = prefix + (id).ToString("D4");
            return generatedId;
        }
    }
}