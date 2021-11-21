using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa un usuario.
    /// Decidimos crearla de esta manera porque solo tiene la responsabilidad de conocer los datos
    /// de un usuario asi cumple con el SRP, tambien decidimos que el Role sea un IRole por el LSP,
    /// asi al momento de crear un usuario se le puede otorgar cualquiera de los 3 roles (AdminRole,
    /// CompanyRole o EntrepreneurRole) ya que son subtipos de IRole.
    /// </summary>
    public class Users : IJsonConvertible
    {
        /// <summary>
        /// Rol del usuario.
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
                    this.id = value;
                }
            }
        }

        /// <summary>
        /// Constructor de User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        public Users(int id, IRole role)
        {
            this.Id = id;
            this.Role = role;
        }

        /// <summary>
        /// Booleano para comprobar que un usuario es un usuario empresa.
        /// </summary>
        /// <returns></returns>
        public bool IsCompanyUser()
        {
            return this.Role is CompanyRole;   //TODO 
        }

        /// <summary>
        /// Booleano para comprobar que un usuario es un usuario emprendedor.
        /// </summary>
        /// <returns></returns>
        public bool IsEntrepreneurUser()
        {
            return this.Role is EntrepreneurRole;   //TODO 
        }

        /// <summary>
        /// Booleano para comprobar que un usuario es un usuario admin.
        /// </summary>
        /// <returns></returns>
        public bool IsAdminUser()
        {
            return this.Role is AdminRole;   //TODO 
        }

        /// <summary>
        /// Convierte el objeto a texto en formato Json. El objeto puede ser reconstruido a partir del texto en formato
        /// Json utilizando JsonSerializer.Deserialize.
        /// </summary>
        /// <returns>El objeto convertido a texto en formato Json.</returns>
        public string ConvertToJson()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}