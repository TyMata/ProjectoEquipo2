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

        public CompanyState State {get;set;}
        
        public CompanyData Data {get;set;}

        /// <summary>
        /// Constructor de los objetos AddCompanyHandler
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
        /// Pide algunos datos de la empresa que se quiere registrar la crea
        /// </summary>
        /// <param name="input"></param>
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
                this.Data.Location = new LocationAdapter(this.Data.Address, this.Data.City,this.Data.Estate);
                this.Data.company = CompanyRegister.Instance.CreateCompany(nombre, this.Data.Location,this.Data.Headings);
                response = "Ya se creo la empresa.";
                return true;
               
            }
            response = string.Empty;
            return false;
        }  

        // private string AskForCompanyName()
        // {
        //     this.messageChannel.SendMessage("Ingrese el nombre de la empresa\n");
        //     string nombre;
        //     nombre = this.messageChannel.ReceiveMessage().Text;
        // }
    }

    public enum CompanyState
    {
        Start,
        Name,
        Country,
        Estate,
        City,
        Address,
        Headings,
        Location,
    }

    public class CompanyData
        {
            /// <summary>
            /// La dirección que se ingresó en el estado AddressState.AddressPrompt.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
            public string Country { get; set; }

            public string Estate { get; set; }
            public string City { get; set; }

            public string Address { get; set; }
            
            public string Headings { get; set; }

             public LocationAdapter Location { get; set; }
            public Company company {get;set;}
          


        }
}
