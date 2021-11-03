using System;
using System.Collections.Generic;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa el Rol de EntrepreneurUser
    /// </summary>
    public class EntrepreneurRole : IRole
	{
		Entrepreneur Entrepreneur { get; set; }
        /// <summary>
        /// Es el constructor de EntrepreneurRole
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="headings"></param>
        /// <param name="habilitation"></param>
        public EntrepreneurRole(string name, Location location, string headings, string habilitation)
        {
            this.Entrepreneur = new Entrepreneur(name, location, headings, habilitation);
        }

       
        /// <summary>
        /// Devuelve el tipo de Rol como string
        /// </summary>
        /// <returns></returns>
        public string TipoRol()
        {
            return "entrepreneur";
        }
	}
}