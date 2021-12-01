using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de empresas.
    /// Se utiliza el patrón de diseño creacional Singleton para crear esta clase ya que mos permite asegurarnos que
    /// habrá  una solo una instancia de esta clase.
    /// </summary>
    public class CompanyRegister : IJsonConvertible
    {   
        private static CompanyRegister instance;

        private CompanyRegister()
        {
            Initialize();
        }
        /// <summary>
        /// Se crea un Singelton de la clase CompanyRegister.
        /// </summary>
        /// <value></value>
        public static CompanyRegister Instance
        {
            get{
                if (instance == null)
                {
                    instance = new CompanyRegister();
                }
                
                return instance;
            }
        }

        private List<Company> companyList;
        /// <summary>
        /// Se crea la lista de empresas.
        /// </summary>
        public void Initialize()
        {
            this.companyList = new List<Company>();
        }

        /// <summary>
        /// Lista de empresas registrados
        /// </summary>
        /// <value></value>
        [JsonInclude]
        public List<Company> CompanyList 
        {
            get
            {
                return companyList;
            }
        }
        
        /// <summary>
        /// Por la ley de demeter y para evitar el alto acoplamiento se crea el Metodo Add para añadir una empresa al registro  de empresas
        ///  y que otro objeto no deba de conocer todas la conexiones internas.
        /// </summary>
        /// <param name="company"></param>
        public void Add(Company company)
        {
            this.CompanyList.Add(company);
        }
        
        /// <summary>
        /// Método para remover empresas de la lista de empresas. 
        ///  Por la ley de demeter y para evitar el alto acoplamiento se crea el Metodo Remove para remover una empresa del registro  de empresas
        ///  y que otro objeto no deba de conocer todas la conexiones internas.
        /// </summary>
        /// <param name="company"></param>
        public void Remove(Company company)
        {
            if(!this.CompanyList.Contains(company))
            {
                throw new Exception(); //CAMBIAR EXCEPTION
            }
            this.CompanyList.Remove(company);
        }

        /// <summary>
        /// Devuelve la empresa a la que pertenece el usuario, de le pasa el id de este.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Company GetCompanyByUserId(int id)
        {
            Users x = UserRegister.Instance.GetUserById(id);
            if(x != null)
            {
                return (x.Role as CompanyRole).Company;
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Devuelve la empresa segun el nombre de esta, si esta existe en el registro.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Company GetCompanyByName(string name)
        {
            if(!this.CompanyList.Exists(company => company.Name == name))
            {
                throw new Exception("No existe esta empresa, ingrese de nuevo el nombre");
            }
            Company company = this.CompanyList.Find(company => company.Name == name);
            return company;
        } 

        /// <summary>
        /// Devuelve verdadero o falso si la empresa esta en el registro de empresas o no
        ///  Por la ley de demeter y para evitar el alto acoplamiento se crea el Metodo Contains verficar si  una empresa esta en el  registro  de empresas
        ///  y que otro objeto no deba de conocer todas la conexiones internas.
        /// </summary>
        /// <param name="company"></param>
        public bool Contains(Company company)
        {
            if(this.CompanyList.Contains(company))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Crea un objeto Company y lo añade a los registros. Se coloco aqui el metodo por el patron Creator, ya que guarda en una lista 
        /// instancias de objetos de Company
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="location"></param>
        /// <param name="headings"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        public Company CreateCompany(string nombre, LocationAdapter location, string headings, string email, string phoneNumber)
        {
            Company nuevaCompany = new Company(nombre, location, headings, email, phoneNumber);
            CompanyRegister.Instance.Add(nuevaCompany);
            TokenRegister.Instance.Add(nuevaCompany.InvitationToken, nuevaCompany ); // TODO todas las empresas tienen el mismo token
            return nuevaCompany;
        }

        /// <summary>
        /// Convierte el objeto a texto en formato Json. El objeto puede ser reconstruido a partir del texto en formato
        /// Json utilizando JsonSerializer.Deserialize.
        /// </summary>
        /// <returns>El objeto convertido a texto en formato Json.</returns>
        public string ConvertToJson(JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(this, options);
        }
        /// <summary>
        /// Convierte el texto en formato json a objeto.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public object LoadFromJson(string json, JsonSerializerOptions options)
        {
            CompanyRegister temp = JsonSerializer.Deserialize<CompanyRegister>(json, options);
            this.companyList = temp.CompanyList;
            return this.CompanyList;
        }
    }
}