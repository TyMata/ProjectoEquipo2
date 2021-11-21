using System;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.Locations.Client;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de añadir una empresa nueva al registro
    /// </summary>
    public class AddCompanyHandler : AbstractHandler
    {
        private string pais;
        private string nombre;

        /// <summary>
        /// Estado para el handler de AddCompany.
        /// </summary>
        /// <value></value>
        public CompanyState State { get; set; }
        
        /// <summary>
        /// Guarda la información que pasa el usuario por el chat.
        /// </summary>
        /// <value></value>
        public CompanyData Data {get;set;}

        /// <summary>
        /// Constructor de los objetos AddCompanyHandler.
        /// </summary>
        public AddCompanyHandler(IMessageChannel channel)
        {
            this.Command ="registrarempresa";
            this.messageChannel = channel;
            this.nextHandler = null;
            this.State = CompanyState.Start;
            this.Data = new CompanyData();

        }
        /// <summary>
        /// Pide algunos datos de la empresa que se quiere registrar y delega la accion de crearla.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            Users user = UserRegister.Instance.GetUserById(input.Id);
            if (this.State == CompanyState.Start && this.CanHandle(input))
            {
                this.State = CompanyState.Name;
                response = "Para poder registrar una empresa vamos a necesitar algunos datos de esta.\n\nIngrese el nombre de la empresa:\n";
                return true;
            }
            else if(this.State == CompanyState.Name)
            {
                this.Data.Name = input.Text;
                this.State = CompanyState.Country;
                response = "Ingrese el pais:\n";
                return true;
            }
             else if(this.State == CompanyState.Country)
            {
                this.Data.Country = input.Text;
                this.State = CompanyState.Estate;
                response = "Ingrese el departamento:\n";
                return true;
            }
             else if(this.State == CompanyState.Estate)
            {
                this.Data.Estate = input.Text;
                this.State = CompanyState.City;
                response = "Ingrese la ciudad:\n";
                return true;
            } 
           else if(this.State == CompanyState.City)
            {
                this.Data.City = input.Text;
                this.State = CompanyState.Address;
                response = "Ingrese la direccion:\n";
                return true;
            } 
            else if(this.State == CompanyState.City)
            {
                this.Data.Address= input.Text;
                this.State = CompanyState.Headings;
                response = "Ingrese su rubro:\n";
                return true;
            } 
             else if(this.State == CompanyState.City)
            {
                this.Data.Headings = input.Text;
                this.State = CompanyState.Start;
                this.Data.Location = LocationApiAdapter.Instance.GetLocation(this.Data.Address, this.Data.City,this.Data.Estate);
                this.Data.company = CompanyRegister.Instance.CreateCompany(nombre, this.Data.Location,this.Data.Headings);
                response = "Ya se creo la empresa.";
                return true;
               
            }
            response = string.Empty;
            return false;
        }  
    
        /// <summary>
        /// Indica los diferentes estados que puede tener el comando AddCompanyHandler.
        /// </summary>
        public enum CompanyState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pide el nombre de la empresa a registrar y pasa al siguiente estado.
            /// </summary>
            Start,

            /// <summary>
            /// Luego de pedir el nombre de la empresa. En este estado el comando pide el país de la empresa y pasa al siguiente estado.
            /// </summary>
            Name,

            /// <summary>
            /// Luego de pedir el país de la empresa. En este estado el comando pide el departamento de la empresa y pasa al siguiente estado.
            /// </summary>
            Country,

            /// <summary>
            /// Luego de pedir el departamento de la empresa. En este estado el comando pide la ciudad de la empresa y pasa al siguiente estado.
            /// </summary>
            Estate,

            /// <summary>
            /// Luego de pedir la ciudad de la empresa. En este estado el comando pide la dirección de la empresa y pasa al siguiente estado.
            /// </summary>
            City,

            /// <summary>
            /// Luego de pedir la dirección de la empresa. En este estado el comando pide el rubro de la empresa y pasa al siguiente estado.
            /// </summary>
            Address,

            /// <summary>
            /// Luego de pedir el rubro de la empresa. En este estado el comando crea la empresa y vuelve al estado Start.
            /// </summary>
            Headings
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando Distance Handler en los diferentes estados.
        /// </summary>
        public class CompanyData
        {
            /// <summary>
            /// El nombre de la companía que se ingresó en el estado CompanyState.Name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// El país de la companía que se ingresó en el estado CompanyState.Country.
            /// </summary>
            public string Country { get; set; }

            /// <summary>
            /// El departamento de la companía que se ingresó en el estado CompanyState.State.
            /// </summary>
            /// <value></value>
            public string Estate { get; set; }

            /// <summary>
            /// La ciudad de la companía que se ingresó en el estado CompanyState.City.
            /// </summary>
            /// <value></value>
            public string City { get; set; }

            /// <summary>
            /// La dirección de la companía que se ingresó en el estado CompanyState.Address.
            /// </summary>
            /// <value></value>
            public string Address { get; set; }

            /// <summary>
            /// El rubro de la companía que se ingresó en el estado CompanyState.Headings.
            /// </summary>
            /// <value></value>
            public string Headings { get; set; }

            /// <summary>
            /// La ubicación completa de la empresa, creada a partir de los datos de ubiación recolectados anteriormente.
            /// </summary>
            /// <value></value>
            public LocationAdapter Location { get; set; }
            
            /// <summary>
            /// La empresa creada a partir de todos los datos recolectados anteriormente.
            /// </summary>
            /// <value></value>
            public Company company {get;set;}
        }
    }
}
