using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class User 
    {
        /// <summary>
        /// Rol del usuario
        /// </summary>
        public IRole Role;
        private int id;
        /// <summary>
        /// Id del usuario
        /// </summary>
        /// <value></value>
        public int Id{get;private set;}
        /// <summary>
        /// Constructor de User
        /// </summary>
        /// <param name="idPar"></param>
        /// <param name="role"></param>
        public User(int idPar, IRole role)
    {
        this.Id = idPar;
        this.Role = role;
    }
  }
}



   
    
