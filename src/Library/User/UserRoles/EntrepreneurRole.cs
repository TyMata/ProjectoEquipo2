using System;
using System.Text;
using System.Collections.Generic;
using Ucu.Poo.Locations.Client;


namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa el Rol de EntrepreneurUser
    /// </summary>
    public class EntrepreneurRole : IRole
	{
        private Entrepreneur entrepreneur;
        /// <summary>
        /// Se hace getter y setter de Entrepeneur.
        /// </summary>
        /// <value></value>
		public Entrepreneur Entrepreneur 
        {
             get
             {
                 return this.entrepreneur;
             } 
            set 
            {
                if(value != null)
                {
                    this.entrepreneur = value;
                }

            }
        }
        
        /// <summary>
        /// Es el constructor de EntrepreneurRole
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        /// <param name="headings"></param>
        /// <param name="habilitation"></param>
        public EntrepreneurRole(string name, string phone,LocationAdapter location, string headings, string habilitation)
        {
            this.Entrepreneur = new Entrepreneur(name, phone ,location, headings, habilitation);
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
                .Append($"Habilitaciones: {Entrepreneur.Habilitation}/n")
                .Append($"Telefono de contacto: {Entrepreneur.ContactPhone}");
            return sb.ToString();
        }
	}
}