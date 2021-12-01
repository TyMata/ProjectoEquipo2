using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de usuarios.
    /// Se utiliza el patrón de diseño creacional Singleton para crear esta clase ya que mos permite asegurarnos que
    /// habrá  una solo una instancia de esta clase.
    /// </summary>
    public class UserRegister : IJsonConvertible
    {   
        private List<Users> dataUsers = new List<Users>();

        /// <summary>
        /// Lista de usuarios registrados.
        /// </summary>
        /// <value></value>
        public List<Users> DataUsers 
        { 
            get
            {
                return this.dataUsers;
            } 
            private set
            {
                if (value != null)
                {
                    this.dataUsers = value;
                }
                
            }
        }

        
        private UserRegister()
        {
            Initialize();
        }

        private static UserRegister instance;

        /// <summary>
        /// Instancia de UserRegister por Singleton.
        /// </summary>
        /// <value></value>
        public static UserRegister Instance
        {
            get{
                if (instance == null)
                {
                    instance = new UserRegister();
                }

                return instance;
            }
        }
        /// <summary>
        /// Se crea la lista de usuarios y se la guarda en la DataUsers.
        /// </summary>
        public void Initialize()
        {
           this.DataUsers = new List<Users>();
        }

        /// <summary>
        /// Crea un usuario empresa. Por Creator se agregó este método ya que contine una lista de instancias de objetos de Users.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="company"></param>
        public void CreateCompanyUser(int id,Company company)
        {
            company.AddUser(id);
        }
        
        /// <summary>
        /// Crea un usuario emprendedor. Por Creator se agregó este método ya que contine una lista de instancias de objetos de Users.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phone"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="headings"></param>
        /// <param name="habilitations"></param>
        public void CreateEntrepreneurUser(int id, string phone,string name , LocationAdapter location, string headings, string habilitations)
        {
            IRole rol = new EntrepreneurRole(name, phone, location, headings, habilitations);
            Users usuario = new Users(id, rol);
            UserRegister.Instance.Add(usuario);
        }

        /// <summary>
        /// Por la ley de demeter y para evitar el alto acoplamiento se crea este método para añadir usuarios a la lista
        /// de usuarios y además que otro objeto no deba de conocer todas la conexiones internas.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Users item)
        {
            this.DataUsers.Add(item);
        }

        /// <summary>
        /// Remueve un user de la lista. Por la ley de demeter y para evitar el alto acoplamiento se crea este método  para remover usuarios
        ///  de la lista usuarios y que otro objeto no deba de conocer todas la conexiones internas.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Users item)
        {
            if (!this.DataUsers.Contains(item))
            {
                throw new Exception(); //TODO CAMBIAR
            }
            this.DataUsers.Remove(item);
        }

        /// <summary>
        /// Por la ley de demeter y para evitar el alto acoplamiento se crea este método para verificar si un usarios 
        /// está en la lista de usuarios y que otro objeto no deba de conocer todas la conexiones internas.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ContainsUser(Users user)
        {
            if(this.DataUsers.Contains(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Devuelve  un objeto Users segun la id dada.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users GetUserById(int id)
        {
            Users result = null;
            if (this.DataUsers.Exists(user => user.Id == id))
            {
                result = this.DataUsers.Find(user => user.Id == id);
            }
            return result;
        }

        /// <summary>
        /// Convierte el objeto a texto en formato Json. El objeto puede ser reconstruido a partir del texto en formato
        /// Json utilizando JsonSerializer.Deserialize.
        /// </summary>
        /// <returns></returns>
        public string ConvertToJson(JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(this, options);      
        }

        /// <summary>
        /// Convierte el texto en formato Json a objeto.
        /// </summary>
        /// <param name="json"></param>
        public void LoadFromJson(string json)
        {
            this.Initialize();
            Users user = JsonSerializer.Deserialize<Users>(json);
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };

            user = JsonSerializer.Deserialize<Users>(json, options);
        }
    }
}