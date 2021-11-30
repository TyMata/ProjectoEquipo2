using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de añadir una empresa nueva al registro
    /// </summary>
    public class AddCompanyHandler : AbstractHandler
    {
        /// <summary>
        /// Estado para el handler de AddCompany.
        /// </summary>
        /// <value></value>
        public AddCompanyState State { get; set; }
        
        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando AddCompanyHandler.
        /// </summary>
        /// <value></value>
        public AddCompanyData Data { get; set; }

        /// <summary>
        /// Constructor de los objetos AddCompanyHandler.
        /// </summary>
        public AddCompanyHandler()
        {
            this.Command ="/registrarempresa";
            this.State = AddCompanyState.Start;
            this.Data = new AddCompanyData();
        }
        /// <summary>
        /// Pide algunos datos de la empresa que se quiere registrar la crea.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            Users user = UserRegister.Instance.GetUserById(input.Id);
            if (this.State == AddCompanyState.Start && this.CanHandle(input))  //TODO: Verificar que sea el rol correcto.
            {
                this.State = AddCompanyState.Name;
                response = "Para poder registrar una empresa vamos a necesitar algunos datos de esta.\n\nIngrese el nombre de la empresa:\n";
                return true;
            }
            else if(this.State == AddCompanyState.Name)
            {
                if (input.Text == "/menu")
                {
                    this.State = AddCompanyState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.Data.Name = input.Text;
                this.State = AddCompanyState.Country;
                response = "Ingrese el país:\n";
                return true;
            }
            else if(this.State == AddCompanyState.Country)
            {
                if (input.Text == "/menu")
                {
                    this.State = AddCompanyState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.Data.Country = input.Text;
                this.State = AddCompanyState.State;
                response = "Ingrese el departamento:\n";
                return true;
            }
            else if(this.State == AddCompanyState.State)
            {
                if (input.Text == "/menu")
                {
                    this.State = AddCompanyState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.Data.Estate = input.Text;
                this.State = AddCompanyState.City;
                response = "Ingrese la ciudad:\n";
                return true;
            } 
           else if(this.State == AddCompanyState.City)
            {   
                if (input.Text == "/menu")
                {
                    this.State = AddCompanyState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.Data.City = input.Text;
                this.State = AddCompanyState.Address;
                response = "Ingrese la dirección:\n";
                return true;
            } 
            else if(this.State == AddCompanyState.Address)
            {
                if (input.Text == "/menu")
                {
                    this.State = AddCompanyState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.Data.Address= input.Text;
                this.State = AddCompanyState.Headings;
                response = "Ingrese el rubro:\n";
                return true;
            } 
             else if(this.State == AddCompanyState.Headings)
            {
                if (input.Text == "/menu")
                {
                    this.State = AddCompanyState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.Data.Headings = input.Text;
                this.State = AddCompanyState.Email;
                response = "Ingrese el email:\n";
                return true;
            }
            else if(this.State == AddCompanyState.Email)
            {
                if (input.Text == "/menu")
                {
                    this.State = AddCompanyState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.State = AddCompanyState.PhoneNumber;
                this.Data.Email = input.Text;
                response = "Ingrese el número de teléfono:\n";
                return true;
            }
            else if(this.State == AddCompanyState.PhoneNumber)
            {
                if (input.Text == "/menu")
                {
                    this.State = AddCompanyState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.State = AddCompanyState.Start;
                this.Data.PhoneNumber = input.Text;
                this.Data.Location = new LocationAdapter(this.Data.Address, this.Data.City,this.Data.Estate);
                this.Data.Result = CompanyRegister.Instance.CreateCompany(this.Data.Name, this.Data.Location,this.Data.Headings, this.Data.Email, this.Data.PhoneNumber);
                response = $"La empresa fue creada.\n El token de invitación es: {this.Data.Result.InvitationToken}";
                return true;
            }
            response = string.Empty;
            return false;
        }  

        /// <summary>
        /// Retorna este handler al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = AddCompanyState.Start;
            this.Data = new AddCompanyData();
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando AddCompanyHandler.
        /// </summary>
        public enum AddCompanyState
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
            State,

            /// <summary>
            /// Luego de pedir la ciudad de la empresa. En este estado el comando pide la dirección de la empresa y pasa al siguiente estado.
            /// </summary>
            City,

            /// <summary>
            /// Luego de pedir la dirección de la empresa. En este estado el comando pide el rubro de la empresa y pasa al siguiente estado.
            /// </summary>
            Address,

            /// <summary>
            /// Luego de pedir el rubro de la empresa. En este estado el comando pide el e-mail de la empresa y pasa al siguiente estado.
            /// </summary>
            Headings,

            /// <summary>
            /// Luego de pedir el e-mail de la empresa. En este estado el comando pide el numero de contacto de la empresa y pasa al siguiente estado.
            /// </summary>
            Email,
            
            /// <summary>
            /// Luego de pedir el numero de contacto de la empresa. En este estado el comando crea la empresa y vuelve al estado Start.
            /// </summary>
            PhoneNumber
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando AddCompanyHandler en los diferentes estados.
        /// </summary>
        public class AddCompanyData
        {
            /// <summary>
            /// El nombre de la empresa que se ingresó en el estado CompanyState.Name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// El país de la empresa que se ingresó en el estado CompanyState.Country.
            /// </summary>
            public string Country { get; set; }

            /// <summary>
            /// El departamento de la empresa que se ingresó en el estado CompanyState.State.
            /// </summary>
            /// <value></value>
            public string Estate { get; set; }

            /// <summary>
            /// La ciudad de la empresa que se ingresó en el estado CompanyState.City.
            /// </summary>
            /// <value></value>
            public string City { get; set; }

            /// <summary>
            /// La dirección de la empresa que se ingresó en el estado CompanyState.Address.
            /// </summary>
            /// <value></value>
            public string Address { get; set; }

            /// <summary>
            /// La ubicación completa de la empresa, creada a partir de los datos de ubicación recolectados anteriormente.
            /// </summary>
            /// <value></value>
            public LocationAdapter Location { get; set; }

            /// <summary>
            /// El rubro de la empresa que se ingresó en el estado CompanyState.Headings.
            /// </summary>
            /// <value></value>
            public string Headings { get; set; }

            /// <summary>
            /// El e-mail de la empresa que se ingresó en el estado CompanyState.Email.
            /// </summary>
            /// <value></value>
            public string Email { get; set; }

            /// <summary>
            /// El numeo de contacto de la empresa que se ingresó en el estado CompanyState.PhoneNumber.
            /// </summary>
            /// <value></value>
            public string PhoneNumber { get; set; }
            
            /// <summary>
            /// La empresa creada a partir de todos los datos recolectados anteriormente.
            /// </summary>
            /// <value></value>
            public Company Result { get; set; }
        }
    }
}