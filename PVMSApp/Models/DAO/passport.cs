//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PVMSApp.Models.DAO
{
    using System;
    using System.Collections.Generic;
    
    public partial class passport
    {
        public string userId { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public int pin { get; set; }
        public string typeOfService { get; set; }
        public string bookletType { get; set; }
        public System.DateTime issueDate { get; set; }
        public string passportId { get; set; }
        public System.DateTime expiryDate { get; set; }
        public double amount { get; set; }
        public string status { get; set; }
    
        public virtual user_registration2 user_registration2 { get; set; }
    }
}
