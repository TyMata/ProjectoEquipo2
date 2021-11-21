using System;
using System.Text;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de crear y publicar una oferta en el registro
    /// </summary>
    public class PublishOfferHandler : AbstractHandler
    {
        public OfferData Data {get;set;}

        public OfferState State {get; set;}

        private Company company;
        /// <summary>
        /// Constructor de objetos PublishOfferHandler
        /// </summary>
        /// <param name="channel"></param>
        public PublishOfferHandler(IMessageChannel channel)
        {
            this.Command = "/publicaroferta";
            this.messageChannel = channel;
            this.State = OfferState.Start;
            this.Data = new OfferData();
            this.company = null;
        }
        /// <summary>
        /// Pregunta por los datos de la oferta a crear y delega la accion de crearla y publicarla
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if (this.State == OfferState.Start && CanHandle(input))
            {
                this.State = OfferState.Material;
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder materials = new StringBuilder("Estos son los materiales de tu empresa");
                foreach (Material item in this.company.ProducedMaterials)
                {
                    materials.Append($"Nombre del Material: {item.Name}\n")
                            .Append($"Tipo: {item.Type}\n")
                            .Append($"Clasificacion: {item.Classification}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                }
                response = "Que material desea vender?:\nIngrese el nombre\n";
                return true;
            }
            else if(this.State == OfferState.Material)
            {
                this.Data.Material = this.company.GetMaterial(input.Text); 
                this.State = OfferState.Quantity;
                response = "Cantidad de material:\n";
                return true;
            }
            else if(this.State == OfferState.Quantity)
            {
                this.Data.Quantity = Convert.ToInt32(input.Text);
                this.State = OfferState.Price;
                response = "¿Cuál va a ser el precio total?\n";
                return true;
            }
            else if (this.State == OfferState.Price)
            {
                this.Data.Price = Convert.ToDouble(input.Text);
                this.State = OfferState.Location;
                StringBuilder location = new StringBuilder("Estas son las locaciones de tu empresa:\n");
                foreach (LocationAdapter item in this.company.Locations) 
                {
                    location.Append($"Ubicacion:\n")   
                            .Append($"Direccion: {item.Address}\n")   
                            .Append($"\n-----------------------------------------------\n\n");
                }
                response = location.ToString();
                return true;
            }
            else if(this.State == OfferState.Location)
            {
                string address = input.Text;
                this.Data.Location = this.company.GetLocation(address);
                this.State = OfferState.Habilitations;
                response = "¿Que habilitaciones son necesarias para poder manipular este material?:\n";
                return true;
            }
            else if(this.State == OfferState.Habilitations)
            {
                this.Data.Habilitations = input.Text;
                this.State = OfferState.Start;
                Market.Instance.CreateOffer(this.Data.Material,this.Data.Habilitations,this.Data.Location,this.Data.Quantity,this.Data.Price,this.company,true);
                response = "La oferta a sido creada y publicada en el mercado.\n";
                return true;
            }
            response = string.Empty;    
            return false;
        
    
        }
        public enum OfferState
        {
            Start,
            Material,
            Quantity,
            Price,
            Location,
            Habilitations,
            Offer,

        }

        public class OfferData
        {
            public Material Material {get;set;}

            public int Quantity {get;set;}

            public double Price {get;set;}

            public string Habilitations {get;set;}

            public Offer Offer {get;set;}
            public LocationAdapter Location {get;set;}
        }
    }
}