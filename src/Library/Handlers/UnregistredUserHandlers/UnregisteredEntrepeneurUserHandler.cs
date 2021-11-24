using System;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de crear un usuario emprendedor.
    /// </summary>
    public class UnregisteredEntrepeneurUserHandler : AbstractHandler
    {
        private UnregisteredEntrepeneurUserState state { get; set; }

        private UnregisteredEntrepeneurUserData data{ get; set; }

        /// <summary>
        /// Constructor de objetos UnregistredEntrepreneurUserHandler.
        /// </summary>
        public UnregisteredEntrepeneurUserHandler()
        {
            this.Command = "/emprendedornoregistrado";
            this.state = UnregisteredEntrepeneurUserState.Start;
            this.data = new UnregisteredEntrepeneurUserData();
        }
        /// <summary>
        /// Pregunta por los datos del emprendedor y delega la tarea de crear un usuario emprendedor.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if ((this.state == UnregisteredEntrepeneurUserState.Start) && (CanHandle(input)))
            {
                StringBuilder datos = new StringBuilder("Asi que eres un Emprendedor!\n")
                                                .Append("Para poder registrarte vamos a necesitar algunos datos personales\n")
                                                .Append("Ingrese su nombre completo\n");
                this.state = UnregisteredEntrepeneurUserState.Name;
                response = datos.ToString();
                return true;
            }
            else if(this.state == UnregisteredEntrepeneurUserState.Name)
            {
                this.data.Name =  input.Text;
                this.state = UnregisteredEntrepeneurUserState.Address;
                response = "Ingrese su dirección:\n";
                return true;
            }
            else if (this.state == UnregisteredEntrepeneurUserState.Address)
            {
                this.data.Address =  input.Text;
                this.state = UnregisteredEntrepeneurUserState.City;
                response = "Ingrese la ciudad:\n";
                return true;
            }
            else if(this.state == UnregisteredEntrepeneurUserState.City)
            {
                this.data.City = input.Text;
                this.state = UnregisteredEntrepeneurUserState.Department;
                response = "Ingrese el departamento:\n";
                return true;
            }
            else if (this.state == UnregisteredEntrepeneurUserState.Department)
            {
                this.data.Department = input.Text;
                this.state = UnregisteredEntrepeneurUserState.Habilitations;
                this.data.LocationResult = new LocationAdapter(this.data.Address,this.data.City,this.data.Department);
                response = "Ingrese el link a sus habilitaciones\n";
                return true;
            }
            else if (this.state == UnregisteredEntrepeneurUserState.Habilitations)
            {
                string habilitaciones =  input.Text;
                this.state = UnregisteredEntrepeneurUserState.Headings;
                response = "Ingrese su rubro\n";
                return true;
            }
            else if (this.state == UnregisteredEntrepeneurUserState.Headings)
            {
                string rubro = input.Text;
                this.state = UnregisteredEntrepeneurUserState.Start;
                UserRegister.Instance.CreateEntrepreneurUser(input.Id, this.data.Name, this.data.LocationResult, this.data.Headings,this.data.Habilitations);
                response = "Gracias por sus datos, se esta creando su usuario\n";
                return true;

            }
            else
            {
                response = "";
                return false;
            } 
        }

        /// <summary>
        /// Estados para el handler de un emprendedor no registrado
        /// </summary>
        public enum UnregisteredEntrepeneurUserState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pregunta por el token de invitacion necesario para 
            /// registrar a un usuario empresa.
            /// </summary>
            Start,
            /// <summary>
            /// Estado en donde se guarda el nombre que envio el usuario y se pregunta por la direccion.
            /// </summary>
            Name,
            /// <summary>
            /// Estado en donde se guarda la direccion que envio el usuario y se pregunta por la ciudad.
            /// </summary>
            Address,
            /// <summary>
            /// Estado en donde se guarda la ciudad que envio el usuario y se pregunta por el departamento.
            /// </summary>
            City,
            /// <summary>
            /// Estado en donde se guarda el departamento que envio el usuario, se encuentra la ubicacion y se pregunta por las habilitaciones.
            /// </summary>
            Department,
            /// <summary>
            /// Estado en donde se guarda el link a las habilitaciones que envio el usuario y se pregunta por el rubro.
            /// </summary>
            Habilitations,
            /// <summary>
            /// Estado en donde se guarda el rubro que envio el usuario, se crea el usuario emprendedor y se le informa al usuario.
            /// </summary>
            Headings,
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando UnregistredEntrepeneurHandler en los diferentes estados.
        /// </summary>
        public class UnregisteredEntrepeneurUserData
        {
            /// <summary>
            /// El nombre que se ingresó en el estado UnregisteredCompanyUserState.Name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// se guarda la dirección que se ingresó en el estado UnregisteredEntrepreneurUserState.Addres .
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// Se guarda la ciudad que se ingresó en el estado UnregisteredEntrepreneurUserState.City .
            /// </summary>
            /// <value></value>
            public string City{get;set;}

            /// <summary>
            /// Se guarda eL departamento que se ingresó en el estado UnregisteredEntrepreneurUserState.Department .
            /// </summary>
            /// <value></value>
            public string Department {get;set;}

            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
            public LocationAdapter LocationResult { get; set; }
            
            /// <summary>
            /// El link a las habilitaciones que se ingresó en el estado UnregisteredCompanyUserState.Habilitations.
            /// </summary>
            public string Habilitations { get; set; }

            /// <summary>
            /// El rubro que se ingresó en el estado UnregisteredCompanyUserState.Headings.
            /// </summary>
            public string Headings { get; set; }
        }
    }
}