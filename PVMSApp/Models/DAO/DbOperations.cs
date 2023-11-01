using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PVMSApp.Models.DAO
{
    public class DbOperations
    {
        test3Entities t = new test3Entities();

        public string fetchPassportId(string bookletType)
        {
            bookletType = "-" + bookletType;
            var lastInsertedUser = t.passports.Where(x => x.passportId.Contains(bookletType)).OrderByDescending(x => x.passportId).FirstOrDefault();

            //var lastInsertedUser = from s in t.passports
            //                       where s.passportId.Contains("-" + bookletType)

            //                       .OrderByDescending(u => u.passportId)
            //                       .FirstOrDefault();

            //var lastInsertedUser = t.passports
            //.OrderByDescending(u => u.passportId)
            //.FirstOrDefault();

            if (lastInsertedUser != null)
            {
                return lastInsertedUser.passportId.Substring(6);
            }

            return "0000";
        }


        public string fetchUserId()
        {
            var lastInsertedUser = t.user_registration2
            .OrderByDescending(u => u.userId)
            .FirstOrDefault();

            if (lastInsertedUser != null)
            {
                return lastInsertedUser.userId.Substring(4);
            }

            return "0000";
        }
        public user_registration2 insert(user_registration2 U)
        {
            U.userId = UserIdGenerator.GeneratePrimaryKey();
            U.citizenType = CitizenTypeGenerator.citizenTypeGeneration(U.dob);
            U.password = CitizenTypeGenerator.gen_password();

            t.user_registration2.Add(U);
            t.SaveChanges();

            return U;
        }
        public bool checkUser(string userId, string contactNo, String password)
        {
            var eu = t.user_registration2.FirstOrDefault(a => a.userId == userId && a.password == password && a.contactNo == contactNo);
            if (eu != null)
            {
                return true;
            }
            return false;
        }

        public user_registration2 fetch(string userId)
        {
            return t.user_registration2.FirstOrDefault(x => x.userId == userId);
        }

        public user_registration2 fetchPassword(string email)
        {
            return t.user_registration2.FirstOrDefault(x => x.email == email);
        }
        public void update(user_registration2 U)
        {
            user_registration2 user1 = t.user_registration2.FirstOrDefault(x => x.userId == U.userId);

            if (user1 != null)
            {
                //t.user_registration2.Attach(user1);
                user1.firstName = U.firstName;
                user1.lastName = U.lastName;
                user1.email = U.email;
                user1.contactNo = U.contactNo;
                user1.address = U.address;
                user1.qualification = U.qualification;
                //t.Entry(U).State = EntityState.Modified;
                t.SaveChanges();
            }


        }

        public string fetchVisaId()
        {
            var lastInsertedUser = t.visas
            .OrderByDescending(u => u.visaId)
            .FirstOrDefault();

            if (lastInsertedUser != null)
            {
                return lastInsertedUser.visaId.Substring(5);
            }
            return "0000";
        }
        public List<State> GetStates()
        {
            t.Configuration.ProxyCreationEnabled = false;
            return t.States.ToList();
        }
        public List<city> Getcity(string sId)
        {
            List<city> city = new List<city>();
            t.Configuration.ProxyCreationEnabled = false;
            city = (from C in t.cities
                    join State in t.States on C.state_id equals State.state_id
                    where State.state_name == sId
                    select C).ToList();
            return city;

        }
        public string insertPassport(passport P)
        {
            t.Configuration.ProxyCreationEnabled = false;
            try
            {
                DateTime applicationDate = DateTime.Now;
                DateTime IssueDate = applicationDate.AddDays(5);
                DateTime ExpiryDate = IssueDate.AddYears(10);
                P.issueDate = IssueDate;
                P.expiryDate = ExpiryDate;
                P.status = "Active";
                P.passportId = PassportIdGenerator.GeneratePrimaryKey(P.bookletType);
                if (P.typeOfService.Equals("Normal"))
                {
                    P.amount = 2500;
                }
                else
                {
                    P.amount = 5000;
                }
                t.passports.Add(P);
                t.SaveChanges();
                return "success";
            }

            catch (DbUpdateException e)
            {
                SqlException S = e.GetBaseException() as SqlException;

                if (S.Message.Contains("FK_passport_user_registration"))
                {
                    return "user Id not Found";
                }
                else
                {
                    return S.Message;
                }

            }
        }

        public string PassportRenewal(passport P)
        {
            t.Configuration.ProxyCreationEnabled = false;
            try
            {
                DateTime applicationDate = DateTime.Now;
                DateTime IssueDate = applicationDate.AddDays(5);
                DateTime ExpiryDate = IssueDate.AddYears(10);
                P.issueDate = IssueDate;
                P.expiryDate = ExpiryDate;
                P.status = "Active";
                P.passportId = PassportIdGenerator.GeneratePrimaryKey(P.bookletType);
                if (P.typeOfService.Equals("Normal"))
                {
                    P.amount = 2500;
                }
                else
                {
                    P.amount = 5000;
                }
                passport L = t.passports.FirstOrDefault(a => a.userId == P.userId & a.status == "Active");
                if (L != null)
                {
                    L.status = "InActive";
                    //t.Entry(L).State = EntityState.Modified;
                    //t.SaveChanges();
                    t.passports.Add(P);
                    t.SaveChanges();
                }
                return "success";


            }
            catch (DbUpdateException e)
            {
                SqlException S = e.GetBaseException() as SqlException;
                if (S.Message.Contains("FK_passport_user_registration"))
                {
                    return "user Id not Found";
                }
                else
                {
                    return S.Message;
                }

            }
        }
        public string InsertVisa(visa v)
        {
            t.Configuration.ProxyCreationEnabled = false;
            v.visaId = null;

            try
            {
                DateTime applicationdate = (DateTime)v.dateOfApplication;
                DateTime issuedate = applicationdate.AddDays(10);
                v.dateOfIssue = issuedate;
                List<DateTime> passportexpiry = (from P in t.passports
                                                 where P.passportId == v.passportId
                                                 select P.expiryDate).ToList();
                DateTime pexpiry = passportexpiry[0];
                VisaIdgenerator V = new VisaIdgenerator();
                v.visaId = V.GeneratePrimaryKey();
                List<double> amount = (from q in t.Visa_form
                                       where q.Country == v.country & q.Occupation == v.occupation
                                       select q.Amount).ToList();
                v.cost = amount[0];

                List<double> validity = (from q in t.Visa_form
                                         where q.Occupation == v.occupation & q.Country == v.country
                                         select q.Validity).ToList();
                var months = validity[0] * 12;
                DateTime dateOfExpiry = issuedate.AddMonths((int)months);
                if (dateOfExpiry.CompareTo(pexpiry) == 1)
                {
                    v.dateOfExpiry = (DateTime)pexpiry;
                }
                else
                {
                    v.dateOfExpiry = dateOfExpiry;

                }
                v.status = "Active";
                t.visas.Add(v);
                t.SaveChanges();
                return v.visaId;
            }
            catch (DbUpdateException e)
            {
                SqlException S = e.GetBaseException() as SqlException;
                if (S.Message.Contains("FK_visa_user_registration"))
                {
                    return "user Id not Found";
                }
                else
                {
                    return S.Message;
                }
            }
        }
        public passport getuserById(string id)
        {
            t.Configuration.ProxyCreationEnabled = false;

            passport p = t.passports.FirstOrDefault(m => m.userId == id && m.status == "Active");
            return p;
        }
        public List<passport> getuserById1(string id)
        {
            t.Configuration.ProxyCreationEnabled = false;

            List<passport> p = (from q in t.passports where q.userId == id select q).ToList();
            return p;
        }
        public List<Visa_form> GetVisa_Forms()
        {
            return t.Visa_form.ToList();
        }
        public List<Visa_type> GetVisa_Types()
        {
            return t.Visa_type.ToList();
        }
        public visa getuserbyvisaid(string id)
        {
            t.Configuration.ProxyCreationEnabled = false;
            visa result = new visa();
            result = t.visas.FirstOrDefault(m => m.visaId == id);
            return result;
        }
        public List<visa> getuserbyvisaid1(string id)
        {
            t.Configuration.ProxyCreationEnabled = false;
            List<visa> result = new List<visa>();
            result = (from E in t.visas
                      where E.userId == id && E.status == "Active"
                      select E).ToList();
            return result;
        }
        public string CancellationCost(visa v)
        {
            t.Configuration.ProxyCreationEnabled = false;
            string s = null;
            try
            {
                var vw = (from E in t.visas
                          where E.userId==v.userId && E.country == v.country && E.status=="Active"
                          select E).FirstOrDefault();
                DateTime expirydate = vw.dateOfExpiry;
                DateTime applydate = DateTime.Now;
                int diffinmonths = (expirydate.Year - applydate.Year) * 12 + (expirydate.Month - applydate.Month);



                if (vw.occupation == "Student")
                {
                    if (diffinmonths < 6)
                    {
                        s = "cancellation charges are 500";
                    }
                    else
                    {
                        s = "cancellation charges are 100";
                    }



                }
                else if (vw.occupation == "Private Employee")
                {
                    if (diffinmonths < 6)
                    {
                        s = "cancellation charges are 1000";
                    }
                    else if (diffinmonths >= 6 && diffinmonths < 12)
                    {
                        s = "cancellation charges are 1500";
                    }
                    else
                    {
                        s = "Cancellation Charges are 1800";
                    }



                }
                else if (vw.occupation == "Government Employee")
                {
                    if (diffinmonths < 6)
                    {
                        s = "cancellation charges are 1500";
                    }
                    else if (diffinmonths >= 6 && diffinmonths < 12)
                    {
                        s = "cancellation charges are 2500";
                    }
                    else
                    {
                        s = "Cancellation Charges are 2800";
                    }
                }
                else if (vw.occupation == "Retired Employee")
                {
                    if (diffinmonths < 6)
                    {
                        s = "cancellation charges are 1500";
                    }
                    else
                    {
                        s = "cancellation charges are 2000";
                    }
                }
                else if (vw.occupation == "Self Employeed")
                {
                    if (diffinmonths < 6)
                    {
                        s = "cancellation charges are 1500";
                    }
                    else
                    {
                        s = "cancellation charges are 1000";
                    }
                }



                vw.status = "Cancelled";
                t.visas.AddOrUpdate(vw);
                t.SaveChanges();
                return s;
            }
            catch (DbUpdateException e)
            {
                SqlException S = e.GetBaseException() as SqlException;
                if (S.Message.Contains("FK_visa_user_registration"))
                {
                    return "user Id not Found";
                }
                else
                {
                    return S.Message;
                }
            }





        }
        public void updstatus()
        {
            foreach (var i in t.passports)
            {
                if (i.expiryDate <= DateTime.Now && i.status == "Active")
                {
                    i.status = "InActive";
                }
            }
            t.SaveChanges();
            foreach (var i in t.visas)
            {
                if (i.dateOfExpiry <= DateTime.Now && i.status == "Active")
                {
                    i.status = "InActive";
                }
            }
            t.SaveChanges();
        }

        // api db methods
        public string insertUser(user_registration2 U)
        {
            try
            {
                U.userId = UserIdGenerator.GeneratePrimaryKey();
                U.citizenType = CitizenTypeGenerator.citizenTypeGeneration(U.dob);
                U.password = CitizenTypeGenerator.gen_password();
                t.user_registration2.Add(U);
                t.SaveChanges();
                return "1";
            }
            catch (DbUpdateException ex)
            {
                return "0";
            }
        }

        public userProfile fetchUserProfile(string userId)
        {
            userProfile user = new userProfile();
            user_registration2 user1 = t.user_registration2.FirstOrDefault(x => x.userId == userId);

            user.firstName = user1.firstName;
            user.lastName = user1.lastName;
            user.address = user1.address;
            user.email = user1.email;
            user.qualification = user1.qualification;
            user.contactNo = user1.contactNo;

            return user;
        }

        public void updateUserProfile(userProfile user1)
        {
            user_registration2 user = t.user_registration2.FirstOrDefault(y => y.userId == user1.userId);

            user.firstName = user1.firstName;
            user.lastName = user1.lastName;
            user.address = user1.address;
            user.email = user1.email;
            user.qualification = user1.qualification;
            user.contactNo = user1.contactNo;

            t.SaveChanges();

        }

        public string fetchCurrentPassportId(string userId)
        {
            passport passport = t.passports.FirstOrDefault(x => x.userId == userId && x.status == "Active");
            if (passport != null)
            {
                return passport.passportId + " expires on " + passport.expiryDate.Date;
            }
            return null;
        }

        public List<string> fetchCurrentVisaId(string userId)
        {
            List<visa> visa = (from E in t.visas
                               where E.userId == userId && E.status == "Active"
                               select E).ToList();
            List<string> visalist = new List<string>();
            if (visa != null)
            {
                for (int i = 0; i < visa.Count; i++)
                {
                    visalist.Add(visa[i].visaId + " for " + visa[i].country + " expires on " + visa[i].dateOfExpiry.Date);
                }
                return visalist;
            }
            return null;
        }
        public List<string> fetchActiveVisaCountires(string userId)
        {
            List<string> ActiveVisaCounies = (from E in t.visas
                                              where E.userId == userId && E.status == "Active"
                                              select E.country).ToList();
            return ActiveVisaCounies;
        }
        public string GetVisaIdByCountry(string country)
        {
            string vid = (from E in t.visas where E.country == country select E.visaId).FirstOrDefault();
            return vid;
        }
        public string CountryVisaValidation(visa v)
        {
            visa visa = t.visas.FirstOrDefault(x => x.userId == v.userId && x.status == "Active" && x.country == v.country);
            if (visa != null)
            {
                return "failure";
            }
            else
            {
                return "success";
            }
        }
        public string verifyphone(string phone)
        {
            user_registration2 u = t.user_registration2.FirstOrDefault(x => x.contactNo == phone);
            if (u != null)
            {
                return "exists";
            }
            return null;



        }
        public string verifyemail(string email)
        {
            user_registration2 u = t.user_registration2.FirstOrDefault(x => x.email == email);
            if (u != null)
            {
                return "exists";
            }
            return null;



        }
        public PassportId GetPassportId(string userId)
        {

            var passportId = from user in t.user_registration2
                             join passport in t.passports
                             on user.userId equals passport.userId
                             where user.userId == userId && passport.status == "Active"
                             select new PassportId
                             {
                                 passportNo = passport.passportId,
                                 surname = user.firstName,
                                 givenName = user.lastName,
                                 gender = user.gender,
                                 dob = user.dob.ToString(),
                                 placeOfBirth = user.address,
                                 doi = passport.issueDate.ToString(),
                                 doe = passport.expiryDate.ToString()
                             };
            
            
            return passportId.FirstOrDefault();
        }
    } 
}
