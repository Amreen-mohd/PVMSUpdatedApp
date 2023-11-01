using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVMSApp.Models.DAO
{
    public class CitizenTypeGenerator
    {
        public static string citizenTypeGeneration(DateTime dob)
        {
            int age = DateTime.Today.Year - dob.Year;
            if (age >= 0 && age <= 1)
            {
                return "Infant";
            }
            else if (age > 1 && age <= 10)
            {
                return "Children";
            }
            else if (age > 10 && age <= 20)
            {
                return "Teen";
            }
            else if (age > 20 && age <= 50)
            {
                return "Adult";
            }
            return "Senior Citizen";
        }

        private static char gen_sp()
        {
            char[] specialCharacters = new char[] { '#', '@', '$' };
            Random random = new Random();
            return specialCharacters[random.Next(0, specialCharacters.Length)];
        }
        private static int gen_num()
        {
            Random random = new Random();
            return random.Next(100, 1000);
        }
        public static string gen_password()
        {
            DateTime currentDate = DateTime.Now;
            string password = currentDate.ToString("dd").PadLeft(2, '0') + currentDate.ToString("MMM").ToLower() + gen_sp() + gen_num();
            return password;



        }
    }
}