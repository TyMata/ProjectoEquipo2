using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class Register
    {
        
        public List<User> UserList{get;set;}
        public List<Company> CompanyList{get;set;}
        public void AddUser(User user)
        {
            if(UserList.Contains(user)==false)
            {
                this.UserList.Add(user);
            }

        }

        public void AddCompany(Company company)
        
        {
            if(CompanyList.Contains(company)==false)
            {
                this.CompanyList.Add(company);
            }
            
        }

        public void RemoveUser(int Id)
        {
            foreach (User x in this.UserList)
            {
                if(x.Id==Id)
                {
                    this.UserList.Remove(x);
                }
            }
        }
       

        public void RemoveCompany(int Id)
        {
            foreach (User x in this.UserList)
            {
                if(x.Id==Id)
                {
                    this.UserList.Remove(x);
                }
            }
        }
    }
}