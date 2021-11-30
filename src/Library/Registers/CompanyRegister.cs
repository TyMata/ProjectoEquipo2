using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de empresas.
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
        /// Por la ley de demeter
        /// </summary>
        /// <param name="company"></param>
        public void Add(Company company)
        {
            this.CompanyList.Add(company);
        }
        
        /// <summary>
        /// Método para remover empresas de la lista de empresas. Se crea por la ley de Demeter.
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
        /// Devuelva una empresa segun el id del user.
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
        /// Por la ley de demeter
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
        /// Crea un objeto Company y lo añade a los registros. Se coloco aqui el metodo por el patron Creator.
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

        public object LoadFromJson(string json, JsonSerializerOptions options)
        {
            CompanyRegister temp = JsonSerializer.Deserialize<CompanyRegister>(json, options);
            this.companyList = temp.CompanyList;
            return this.CompanyList;
        }
    }
}