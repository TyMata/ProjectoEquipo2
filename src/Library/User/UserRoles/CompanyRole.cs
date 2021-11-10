using System;
using System.Text;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa el Rol de CompanyUser
    /// </summary>
    public class CompanyRole : IRole
    {   
        private Company company;
        /// <summary>
        /// Devuelve la company a la que pertenece el user
        /// </summary>
        /// <value></value>
        public Company Company 
        { 
            get
            {
                return this.company;
            } 
            private set
            {
                if(value != null)
                {
                    this.Company = value;
                }
                else
                {
                    //Excepcion 
                }
            }
            
        }
        /// <summary>
        /// Constructor de CompanyRole
        /// </summary>
        /// <param name="company"></param>
        public CompanyRole(Company company)
        {
            this.Company = Company;
        }

        /// <summary>
        /// Devuelve el tipo de Rol como string
        /// </summary>
        /// <returns></returns>
        public string RoleType()
        {
            return "company";
        }
        /// <summary>
        /// Devuelve la data de un usuario empresa
        /// </summary>
        /// <returns></returns>
        public string Data()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Nombre de la empresa que representa: {Company.Name}\n")
                .Append($"Ubicacion de la empresa: {Company.Locations}\n")
                .Append($"Rubro: {Company.Headings}\n")
                .Append($"Materiales producidos por la empresa: {Company.ProducedMaterials}");
            return sb.ToString();
        }
    }
}