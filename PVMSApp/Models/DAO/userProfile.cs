using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVMSApp.Models.DAO
{
    public class userProfile
    {
        public string userId { get; set; }
            
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string contactNo { get; set; }
        public string email { get; set; }
        public string qualification { get; set; }
    }
}