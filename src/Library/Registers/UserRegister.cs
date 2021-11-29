using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de usuarios.
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
            private set{} //CARGAR DE JSON LISTA DE USUARIOS REGISTRADOS
        }

        
        private UserRegister()
        {
            Initialize();
        }

        private static UserRegister instance;

        /// <summary>
        /// Instancia de UserRegister (COMENTAR SINGLETON)
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
        /// Crea un usuario empresa.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="company"></param>
        public  void CreateCompanyUser(IMessage input,Company company)
        {
            company.AddUser(input.Id);
        }
        
        /// <summary>
        /// Crea un usuario emprendedor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="headings"></param>
        /// <param name="habilitations"></param>
        public void CreateEntrepreneurUser(int id,string name , LocationAdapter location, string headings, string habilitations)
        {
            IRole rol = new EntrepreneurRole(name , location, headings, habilitations);
            Users usuario = new Users(id, rol);
            UserRegister.Instance.Add(usuario);
        }

        /// <summary>
        /// Esto se hace por la ley de demeter.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Users item)
        {
            this.DataUsers.Add(item);
        }

        /// <summary>
        /// Remueve un user de la lista. Por la ley de demeter.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Users item)
        {
            if (!this.DataUsers.Contains(item))
            {
                throw new Exception(); //CAMBIAR
            }
            this.DataUsers.Remove(item);
        }

        /// <summary>
        /// Por la ley de demeter se crea Contains.
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
        /// Devuelve  un objeto user segun la id dada.
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
            string result = "{\"Items\":[";

            foreach (var item in this.DataUsers)
            {
                result = result + item.ConvertToJson(options) + ",";
            }

            result = result.Remove(result.Length - 1);
            result = result + "]}";

            string temp = JsonSerializer.Serialize(this.DataUsers);
            return result;
           
            JsonSerializerOptions option = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this.DataUsers, option);            
        }
        /// <summary>
        /// Convierte el texto en formato Json a obejto.
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