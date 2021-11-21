using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary 
{
    /// <summary>
    /// Esta clase  representa un registro de tokens.
    /// </summary>
    public class TokenRegister : IJsonConvertible
    {   
        /// <summary>
        /// Diccionario con nombre de empresas y sus respectivos tokens habilitados.
        /// </summary>
        /// <returns></returns>
        [JsonInclude]
        public Dictionary<string , Company> TokenList;

        private static TokenRegister instance;

        private TokenRegister()
        {
            Initialize();
        }

        public static TokenRegister Instance
        {
            get{
                if (instance == null)
                {
                    instance = new TokenRegister();
                }

                return instance;
            }
        }

        public void Initialize()
        {
            this.TokenList = new Dictionary<string, Company>();
        }

        /// <summary>
        /// Devuelve la empresa a la cual le pertenece un codigo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public Company GetCompany(string codigo)
        {
            if (!TokenList.ContainsKey(codigo))
            {
                throw new Exception();
            }
            return TokenList[codigo];
        }

        /// <summary>
        /// Metodo par añadir un token al diccionario de tokens.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="company"></param>
        public void Add(string token,Company company)
        {
            this.TokenList.Add(token,company);
        }

        /// <summary>
        /// Metodo para remover un token del diccionario de tokens.
        /// </summary>
        /// <param name="token"></param>
        public void Remove(string token)
        {
            this.TokenList.Remove(token);
        }

        /// <summary>
        /// Por la ley de demeter se crea Contains.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public bool IsValid(string codigo)
        {
            if(this.TokenList.ContainsKey(codigo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ConvertToJson()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this, options);
        }

        public void LoadFromJson(string json)
        {
            this.Initialize();
            string token = JsonSerializer.Deserialize<string>(json);
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };

            token = JsonSerializer.Deserialize<string>(json, options);
        }
    }
}