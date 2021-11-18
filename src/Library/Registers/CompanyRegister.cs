using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de empresas
    /// </summary>
    public class CompanyRegister : IJsonConvertible
    {   
        private static CompanyRegister instance;

        private CompanyRegister()
        {
            Initialize();
        }

        public static CompanyRegister Instance
        {
            get{
                if (instance == null)
                {
                    instance = new CompanyRegister ();
                }

                return instance;
            }
        }

        private List<Company> companyList;

        public void Initialize()
        {
            this.companyList = new List<Company>();
        }

        /// <summary>
        /// Lista de empresas registrados
        /// </summary>
        /// <value></value>
        [JsonInclude]
        public  List<Company> CompanyList 
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
            this.companyList.Add(company);
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
            User x = UserRegister.Instance.GetUserById(id);
            return (x.Role as CompanyRole).Company;
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
        /// <param name="ubi"></param>
        /// <param name="headings"></param>
        public Company CreateCompany(string nombre, Location ubi, string headings)
        {
            Company nuevaCompany = new Company(nombre, ubi, headings);
            CompanyRegister.Instance.Add(nuevaCompany);
            TokenRegister.Instance.TokenList.Add("nuevo token",nuevaCompany);
            return nuevaCompany;
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