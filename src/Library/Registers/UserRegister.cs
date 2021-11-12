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
        /// ESto se hace por la ley de demeter
        /// </summary>
        /// <param name="item"></param>
        public void Add(User item)
        {
            this.DataUsers.Add(item);
        }


        /// <summary>
        /// Remueve un user de la lista. Por la ley de demeter
        /// </summary>
        /// <param name="item"></param>
        public void Remove(User item)
        {
            if (!this.DataUsers.Contains(item))
            {
                throw new Exception(); //CAMBIAR
            }
            this.DataUsers.Remove(item);
        }

        /// <summary>
        /// Devuelve  un objeto user segun la id dada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            User result = null;
            int index = 0;
            while (result == null && index < this.DataUsers.Count)
            {
                if (this.DataUsers[index].Id == id)
                {
                    result = this.DataUsers[index];
                }
            }

            return result;
        }
    }
}