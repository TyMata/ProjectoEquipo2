using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa un usuario.
    /// Decidimos crearla de esta manera porque solo tiene la responsabilidad de conocer los datos
    /// de un usuario asi cumple con el SRP, tambien decidimos que el Role sea un IRole por el LSP,
    /// asi al momento de crear un usuario se le puede otorgar cualquiera de los 3 roles (AdminRole,
    /// CompanyRole o EntrepreneurRole) ya que son subtipos de IRole.
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
        public int Id
        {
            get
            {
                return this.id;
            }
            private set
            {
                if (value > 0)
                {
                    this.Id = value;
                }

            }
        }

        /// <summary>
        /// Constructor de User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        public User(int id, IRole role)
        {
            this.Id = id;
            this.Role = role;
        }

        public bool IsCompanyUser()
        {
            return this.Role is CompanyRole;
        }
    }
}