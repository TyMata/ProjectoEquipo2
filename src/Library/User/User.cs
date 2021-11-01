using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class User
    {
        private string name;
        private string id;
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        /// <value></value>
        public string Name{get;private set;}
        /// <summary>
        /// Id del usuario
        /// </summary>
        /// <value></value>
        public int Id{get;private set;}
        /// <summary>
        /// Constructor del usuario
        /// </summary>
        /// <param name="namePar"></param>
        /// <param name="idPar"></param>
        public User(string namePar, int idPar)
    {
        this.Name = namePar;
        this.Id = idPar;
    }
  }
}



   
    
