using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class UserRegister
    {
        private List<User> DataUsers { get; set; }

        public void AddUser(int Id, IRole role)
        {
            this.DataUsers.Add(new User(Id, role));
        }
        public void RemoveUser(int Id)
        {
            if (this.DataUsers != null)
            {
                foreach (User x in DataUsers)
                {
                   if (x.Id.Equals(Id)) 
                   {
                       this.DataUsers.Remove(x);
                   }
                }
            }
        }
    }
}