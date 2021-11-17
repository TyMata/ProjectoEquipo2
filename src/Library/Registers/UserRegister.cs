using System;
using System.Collections.Generic;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de usuarios
    /// </summary>
    public class UserRegister
    {   
        private List<User> dataUsers = new List<User>();
        /// <summary>
        /// Lista de usuarios registrados
        /// </summary>
        /// <value></value>
        public List<User> DataUsers 
        { 
            get
            {
                return this.dataUsers;
            } 
            private set{} //CARGAR DE JSON LISTA DE USUARIOS REGISTRADOS
        }

        /// <summary>
        /// Crea un usuario empresa
        /// </summary>
        /// <param name="input"></param>
        /// <param name="company"></param>
        public  void CreateCompanyUser(IMessage input,Company company)
        {
            company.AddUser(input.Id);
        }
        /// <summary>
        /// Crea un usuario emprendedor
        /// </summary>
        /// <param name="input"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="headings"></param>
        /// <param name="habilitations"></param>
        public void CreateEntrepreneurUser(IMessage input,string name , Location location, string headings, string habilitations)
        {
            IRole rol = new EntrepreneurRole(name , location, headings, habilitations);
            User usuario = new User(input.Id, rol);
        }

        /// <summary>
        /// Esto se hace por la ley de demeter
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
        /// Por la ley de demeter se crea Contains
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ContainsUser(User user)
        {
            if(this.DataUsers.Contains(user))
            {
                return true;
            }
            else
            {
                return false;
            }
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