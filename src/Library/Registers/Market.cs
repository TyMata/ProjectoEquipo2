using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa el mercado con sus ofertas.
    /// </summary>
    public class Market : IJsonConvertible
    {   
        private static Market instance;

        public int count;
        /// <summary>
        /// Genera un numero mayor que el anterior para el Id.
        /// </summary>
        /// <value></value>
        public int Count 
        { 
            get
            {
                return this.count;
            }
            set
            {
                this.count = value;
            }
        }

        private Market()
        {
            Initialize();
        }

        /// <summary>
        /// Se crea un Singelton de la clase Market.
        /// </summary>
        /// <value></value>
        public static Market Instance
        {
            get{
                if (instance == null)
                {
                    instance = new Market();
                }

                return instance;
            }
        }

        public List<Offer> actualOfferList = new List<Offer>();

        /// <summary>
        /// Se crea la lista de ofertas.
        /// </summary>
        public void Initialize()
        {
            this.actualOfferList = new List<Offer>();
            this.suspendedOfferList = new List<Offer>();
        }

        /// <summary>
        /// Lista de ofertas actuales.
        /// </summary>
        /// <value></value>
        [JsonInclude]
        public List<Offer> ActualOfferList 
        {
            get
            {
                return actualOfferList;
            }
        }

        private List<Offer> suspendedOfferList;
        /// <summary>
        /// Lista de ofertas suspendidas.
        /// </summary>
        /// <value></value>
        [JsonInclude]
        public List<Offer> SuspendedOfferList 
        {
            get
            {
                return this.suspendedOfferList;
            }
        }

        /// <summary>
        /// Crea y devuelve una nueva oferta. Creamos las ofertas aca por Creator.
        /// </summary>
        /// /// <param name="material"></param>
        /// <param name="habilitation"></param>
        /// <param name="location"></param>
        /// <param name="unitOfMeasure"></param>
        /// <param name="quantityMaterial"></param>
        /// <param name="currency"></param>
        /// <param name="totalPrice"></param>
        /// <param name="company"></param>
        /// <param name="availability"></param>
        /// <param name="continuity"></param>
        /// <returns></returns>
        public Offer CreateOffer(Material material,string habilitation, LocationAdapter location, string unitOfMeasure, int quantityMaterial, string currency, int totalPrice, Company company, bool availability, string continuity)
        {
            this.Count ++;
            int id = this.Count;
            Offer nuevaOferta = new Offer(id, material, habilitation, location, unitOfMeasure, quantityMaterial, currency, totalPrice, company, availability, DateTime.Now, "continua");
            company.AddOffer(nuevaOferta);
            this.PublishOffer(nuevaOferta);
            return nuevaOferta;
        }

        /// <summary>
        /// Por la ley de demeter se crea ContainsActive.
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public bool ContainsActive(Offer offer)
        {
            if(this.actualOfferList.Contains(offer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Por la ley de demeter se crea ContainsSuspended.
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public bool ContainsSuspended(Offer offer)
        {
            if(this.SuspendedOfferList.Contains(offer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddActiveOffer(Offer offer)
        {
            if(this.ActualOfferList.Contains(offer))
            {
                throw new Exception("La oferta que se quiere crear ya existe"); //TODO CAMBIAR EXCEPTION A OBJETO YA EXISTE
            }
            this.ActualOfferList.Add(offer);
        }

        /// <summary>
        /// Añade una nueva oferta a la lista de ofertas actuales.
        /// </summary>
        /// <param name="newOffer"></param>
        public void PublishOffer(Offer newOffer)
        {
            if(this.ActualOfferList.Contains(newOffer))
            {
                throw new Exception("La oferta que se quiere crear ya existe"); //TODO CAMBIAR EXCEPTION
            }
            this.ActualOfferList.Add(newOffer);
        }

        /// <summary>
        /// Retira la oferta de la lista de ofertas actuales.
        /// </summary>
        /// <param name="id"></param>
        public void RemoveOffer(int id)
        {
            if (!this.ActualOfferList.Exists(offer => offer.Id == id))
            {
                throw new InvalidInputException($"No existe ninguna oferta con ese Id.");
            }
            Offer x = this.ActualOfferList.Find(offer => offer.Id == id);
            this.ActualOfferList.Remove(x);
        }

        /// <summary>
        /// Devuelve una lista de ofertas que cumplan con un parametro de busqueda , ARREGLAR KEYWORDS ANTES
        /// </summary>
        /// <returns></returns>
        public List<Offer> SearchOffers(string keyword)
        {
            if (!this.ActualOfferList.Exists(offer => offer.Keywords.Contains(keyword)))
            {
                throw new InvalidInputException($"No existe ninguna oferta con ese Id.");
            }
            List<Offer> x = this.ActualOfferList.FindAll(offer => offer.Keywords.Contains(keyword));
            return x;
        }      

        public Offer GetOfferById(int id)
        {
            if (!this.ActualOfferList.Exists(offer => offer.Id == id))
            {
                throw new InvalidInputException($"No existe ninguna oferta con ese Id.");
            }
            Offer x = this.ActualOfferList.Find(offer => offer.Id == id);
            return x;
        }

        public void BuyOffer(int offerId, Users user)
        {
            if (!this.ActualOfferList.Exists(offer => offer.Id == offerId))
            {
                throw new InvalidInputException($"No existe ninguna oferta con ese Id.");
            }
            Offer x = this.ActualOfferList.Find(offer => offer.Id == offerId);
            x.Company.OfferSold(x, user);
            (user.Role as EntrepreneurRole).Entrepreneur.AddBoughtOffer(x);
            x.ChangeAvailability();
            this.SuspendedOfferList.Add(x);
            this.ActualOfferList.Remove(x);
        }

        /// <summary>
        /// Suspende una oferta actual.
        /// </summary>
        /// <param name="id"></param>
        public void SuspendOffer(int id)
        {
            if (!this.ActualOfferList.Exists(offer => offer.Id == id))
            {
                throw new InvalidInputException($"No existe ninguna oferta con ese Id.");
            }
            Offer x = this.ActualOfferList.Find(offer => offer.Id == id);
            x.ChangeAvailability();
            this.SuspendedOfferList.Add(x);
            this.ActualOfferList.Remove(x);
        }

        /// <summary>
        /// A una oferta suspendida la vuelve a activar.
        /// </summary>
        /// <param name="id"></param>
        public void ResumeOffer(int id)
        {
            if (!this.SuspendedOfferList.Exists(offer => offer.Id == id))
            {
                throw new InvalidInputException($"No existe ninguna oferta con ese Id.");
            }
            Offer x = this.SuspendedOfferList.Find(offer => offer.Id == id);
            x.ChangeAvailability();
            this.ActualOfferList.Add(x);
            this.SuspendedOfferList.Remove(x);
        }

        /// <summary>
        /// Convierte un objeto a texto en formato Json. El objeto puede ser reconstruido a partir del texto en formato
        /// Json utilizando JsonSerializer.Deserialize.
        /// </summary>
        /// <returns>El objeto convertido a texto en formato Json.</returns>
        public string ConvertToJson(JsonSerializerOptions options)
        {
            // string result = "{\"Items\":[";

            // foreach (var item in this.actualOfferList)
            // {
            //     result = result + item.ConvertToJson() + ",";
            // }

            // result = result.Remove(result.Length - 1);
            // result = result + "]}";

            // string temp = JsonSerializer.Serialize(this.actualOfferList);

            return JsonSerializer.Serialize(this, options);
        } 

        public object LoadFromJson(string json, JsonSerializerOptions options)
        {
            Market temp = JsonSerializer.Deserialize<Market>(json, options);
            this.actualOfferList = temp.ActualOfferList;
            this.suspendedOfferList = temp.SuspendedOfferList;
            return this.actualOfferList;
        }
    }
}
