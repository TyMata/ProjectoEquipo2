namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa el Rol de CompanyUser
    /// </summary>
    public class CompanyRole : IRole
    {   
        /// <summary>
        /// Devuelve la company a la que pertenece el user
        /// </summary>
        /// <value></value>
        public Company Company { get; set; }
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
        public string TipoRol()
        {
            return "company";
        }
    }
}