using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase se encarga de la modificacion y busquedas en UserRegister
    /// </summary>
    public class UserRegisterServiceProvider
    {
       /// <summary>
        /// Añade un usuario a la lista de usuarios registrados
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="role"></param>
        public void AddUser(int Id, IRole role)
        {
            UserRegister.DataUsers.Add(new User(Id, role));
        }
        /// <summary>
        /// Remueve un usuario de la lista de usuarios registrados
        /// </summary>
        /// <param name="Id"></param>
        public static void RemoveUser(int Id)
        {
            if (UserRegister.DataUsers != null)
            {
                foreach (User x in UserRegister.DataUsers)
                {
                   if (x.Id.Equals(Id)) 
                   {
                        UserRegister.DataUsers.Remove(x);
                   }
                }
            }
        }
        /// <summary>
        /// Verifica si un usuario esta registrado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsRegistredUser(int id)
        {
            if (UserRegister.DataUsers != null)
            {
                foreach (User x in UserRegister.DataUsers)
                {
                   if (x.Id.Equals(id)) 
                   {
                        return true;
                   }
                }
                return false;
            }
            else return false;
        }
        /// <summary>
        /// Busca y devueve un usuario dentro de los usuarios registrados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User SearchUser(int id)
        {
            User usuario;
            if (UserRegister.DataUsers != null)
            {
                foreach (User x in UserRegister.DataUsers)
                {
                   if (x.Id.Equals(id)) 
                   {
                       usuario = x;
                       return usuario;
                   }
                }
            }
            return null;
        }
    }
}