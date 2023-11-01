using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVMSApp.Models.DAO
{
    public class PassportId
    {
        
        
        public string passportNo { get; set; }
        public string surname { get; set; }
        public string givenName { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string placeOfBirth { get; set; }
        public string doi { get; set; }
        public string doe { get; set; }
    }
}