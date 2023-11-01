using PVMSApp.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVMSApp.Models.BL
{
    public class Validation
    {
        DbOperations D = new DbOperations();
        public List<State> validateState()
        {
            return D.GetStates();
        }
        public List<city> validateCity(string sId)
        {
            return D.Getcity(sId);
        }
        public string validatePassport(passport P)
        {
            return D.insertPassport(P);

        }
        public passport getuserById(string id)
        {
            return D.getuserById(id);
        }
        public string passportrenewal(passport P)
        {
            return D.PassportRenewal(P);
        }
        public List<Visa_form> getvisa() 
        {
            return D.GetVisa_Forms();
        }
        public List<Visa_type> getvisaType1() 
        {
            return D.GetVisa_Types();
        }
        public string insertvisa(visa v)
        {
            return D.InsertVisa(v);
        }
        public visa getbyvisaid(string id)
        {
            return D.getuserbyvisaid(id);
        }
        public string visacancellation(visa v)
        {
            return D.CancellationCost(v);
        }
        public List<string> ActiveVisaCountries(string userId)
        {
            return D.fetchActiveVisaCountires(userId);
        }
        public string GetVisaIdByCountry(string country)
        {
            return D.GetVisaIdByCountry(country);
        }
        public string CountryVisaValidation(visa v)
        {
            return D.CountryVisaValidation(v);
        }
        public string verifyphone(string phone)
        {
            return D.verifyphone(phone);
        }
        public string verifyemail(string email)
        {
            return D.verifyemail(email);
        }
    }
}