using System;
using System.Collections.Generic;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de usuarios
    /// </summary>
    public class UserRegister
    {   
        /// <summary>
        /// Lista de usuarios registrados
        /// </summary>
        /// <value></value>
        public List<User> DataUsers { get; private set; }
        /// <summary>
        /// Añade un usuario a la lista de usuarios registrados
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="role"></param>
        public void AddUser(int Id, IRole role)
        {
            this.DataUsers.Add(new User(Id, role));
        }
        /// <summary>
        /// Remueve un usuario de la lista de usuarios registrados
        /// </summary>
        /// <param name="Id"></param>
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