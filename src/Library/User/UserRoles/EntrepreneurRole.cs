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
        
		private Entrepreneur Entrepreneur { get; set; }
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
        /// <summary>
        /// Devuelve la data de un usuario emprendedor
        /// </summary>
        /// <returns></returns>
        public string Data()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Nombre: {Entrepreneur.Name}\n")
                .Append($"Ubicacion: {Entrepreneur.Location}\n")
                .Append($"Rubro: {Entrepreneur.Heading}\n")
                .Append($"Link a habilitaciones: {Entrepreneur.Habilitation}");
            return sb.ToString();
        }
	}
}