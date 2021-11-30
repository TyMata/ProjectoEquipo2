using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de crear y publicar una oferta en el registro
    /// </summary>
    public class PublishOfferHandler : AbstractHandler
    {
        /// <summary>
        /// Estado para el handler de PublishOfferHandler.
        /// </summary>
        /// <value></value>
        public OfferState State {get; set;}

        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando PublishOfferHandler.
        /// </summary>
        /// <value></value>
        public OfferData Data { get; set; }

        private int material = 0; //TODO: Borrar esto

        private Company company;
        /// <summary>
        /// Constructor de objetos PublishOfferHandler
        /// </summary>
        public PublishOfferHandler()
        {
            this.Command = "/publicaroferta";
            this.State = OfferState.Start;
            this.Data = new OfferData();
            this.company = null;
        }

        /// <summary>
        /// Pregunta por los datos de la oferta a crear y delega la accion de crearla y publicarla
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if (State == OfferState.Start && CanHandle(input))
            {
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder materials = new StringBuilder("Estos son los materiales de tu empresa:\n\n");
                if (this.company != null)
                {
                    if (material == 0)
                    {
                        material += 1;
                    }
                    foreach (Material item in this.company.ProducedMaterials)
                    {
                        materials.Append($"Nombre del Material: {item.Name}\n")
                                .Append($"Tipo: {item.Type}\n")
                                .Append($"Clasificación: {item.Classification}\n")
                                .Append($"\n-----------------------------------------------\n\n");
                    }
                    this.State = OfferState.Material;
                    materials.Append($"¿Qué material desea vender?\n")
                            .Append($"Ingrese el nombre.");
                    response = materials.ToString();
                    return true;
                }
                else
                {
                    materials.Append($"No se encontró ninguna empresa a la que usted pertenezca.\n")
                        .Append($"Ingrese /menu si quiere volver a ver los comandos disponibles.");
                    response = materials.ToString();
                    return true;
                }
            }
            else if(this.State == OfferState.Material)
            {
                this.Data.Material = this.company.GetMaterial(input.Text); 
                this.State = OfferState.Quantity;
                response = "Ingrese la cantidad de material.";
                return true;
            }
            else if(this.State == OfferState.Quantity)
            {
                this.Data.Quantity = Convert.ToInt32(input.Text);
                this.State = OfferState.Price;
                response = "¿Cuál va a ser el precio total?";
                return true;
            }
            else if (this.State == OfferState.Price)
            {
                this.Data.Price = Convert.ToInt32(input.Text);
                this.State = OfferState.Location;
                StringBuilder location = new StringBuilder("Estas son las locaciones de tu empresa:\n");
                if (this.company.Locations!=null)
                {
                    foreach (LocationAdapter item in this.company.Locations) 
                    {
                        location.Append($"Departamento: {item.Department}\n") //TODO: Recorrer la lista con index
                                .Append($"Ciudad: {item.City}\n")
                                .Append($"Dirección: {item.Address}\n")   
                                .Append($"-----------------------------------------------\n\n");
                    }
                location.Append("Ingresa la dirección de esta:\n");
                response = location.ToString();
                return true;
                }
                else 
                {
                    location.Append("La empresa no tiene ninguna ubicación cargada.\n")
                            .Append("Ingrese /menu si quiere volver a ver a los comandos disponibles.");
                }      
            }
            else if(this.State == OfferState.Location) //TODO: Crear handler para addmaterial y para add location.
            {
                string address = input.Text;
                this.Data.Location = this.company.GetLocation(address);
                this.State = OfferState.Habilitations;
                response = "¿Que habilitaciones son necesarias para poder manipular este material?";
                return true;
            }
            else if(this.State == OfferState.Habilitations)
            {
                this.Data.Habilitations = input.Text;
                this.State = OfferState.Continuity;
                response = $"Para determinar la continuidad de la oferta, escriba \"continua\" si es continua, o \"puntual\" si es puntual.";
                return true;
            }
            else if(this.State == OfferState.Continuity)
            {
                this.Data.Continuity = input.Text;
                this.State = OfferState.Start;
                Market.Instance.CreateOffer(this.Data.Material,this.Data.Habilitations,this.Data.Location,this.Data.Quantity,this.Data.Price,this.company,true, this.Data.Continuity);
                response = "La oferta ha sido creada y publicada en el mercado.";
                this.Data = new OfferData();
                return true;
            }
            response = string.Empty;    
            return false;
        }
        
        /// <summary>
        /// Indica los diferentes estados que tiene PublishOfferHandler.
        /// </summary>
        public enum OfferState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pregunta por el material que se quiere vender
            /// y le muestra una lista de los materiales producidos por la empresa.
            /// </summary>
            Start,
            Material,
            Quantity,
            Price,
            Location,
            Habilitations,
            Offer,
            Continuity
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando PublishOfferHandler en los diferentes estados.
        /// </summary>
        public class OfferData
        {
            /// <summary>
            /// Se guarda el material que se quiere agregar en la oferta.
            /// </summary>
            /// <value></value>
            public Material Material { get; set; }

            /// <summary>
            /// Se guarda la cantidad del material que se quiere poner en la oferta.
            /// </summary>
            /// <value></value>
            public int Quantity { get; set; }

            /// <summary>
            /// Se guarda el precio total de la oferta.
            /// </summary>
            /// <value></value>
            public int Price { get; set; }

            /// <summary>
            /// Se guardan las habilitaciones para manejar el material.
            /// </summary>
            /// <value></value>
            public string Habilitations { get; set; }

            /// <summary>
            /// Se guarda la oferta creada con la informacion guardada previamente.
            /// </summary>
            /// <value></value>
            public Offer Offer { get; set; }
            
            /// <summary>
            /// Se guarda la ubicación de la oferta.
            /// </summary>
            /// <value></value>
            public LocationAdapter Location { get; set; }
            
            /// <summary>
            /// Se guarda la continuidad de la oferta.
            /// </summary>
            /// <value></value>
            public string Continuity { get; set; }
        }
    }
}