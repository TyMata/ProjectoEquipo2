using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa una ubicacion
    /// </summary>
    public class LocationAdapter 
    {   
        /// <summary>
        /// Devuelve si la ubicacion existe o no
        /// </summary>
        /// <value></value>
        public bool Found { get; set; }
        /// <summary>
        /// devuelve y guarda la latitud de la ubicacion.
        /// </summary>
        /// <value></value>
        public double Latitude { get; set; }
        /// <summary>
        /// Devuelve la longitud de la ubicacion
        /// </summary>
        /// <value></value>
        public double Longitude { get; set; }

        /// <summary>
        /// Devuelve y guarda el codigo postal de la ubicación.
        /// </summary>
        /// <value></value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Devuelve y guarda la dirección. 
        /// </summary>
        /// <value></value>
        public string Address { get; set; }

        /// <summary>
        /// Devuelve y guarda la ciudad.
        /// </summary>
        /// <value></value>
        public string City { get; set; }

        /// <summary>
        /// Devuelve y guarda el departamento.
        /// </summary>
        /// <value></value>
        public string Department { get; set; }

        private LocationApiClient client = new LocationApiClient();
        private Location location;

       /// <summary>
       /// Adaptador de la Location Api
       /// </summary>
       /// <param name="address"></param>
       /// <param name="city"></param>
       /// <param name="department"></param>
        public LocationAdapter(string address, string city, string department)
        {
            this.location =  this.client.GetLocation(address,city,department);
            if(this.location.Found)
            {
                this.Address = address;
                this.City = city;
                this.Department = department;
                this.Longitude = this.location.Longitude;
                this.Latitude = this.location.Latitude;
                this.PostalCode = this.location.PostalCode;
            }
        }

        /// <summary>
        /// Retorna la distancia entre dos locations
        /// </summary>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public double GetDistance(string address, string city, string department)
        {
           Location location2 = this.client.GetLocation(address,city,department);
            Distance distance = this.client.GetDistance(this.location,location2);

            return distance.TravelDistance;
        }

        /// <summary>
        /// Retorna la duracion entre dos locaciones
        /// </summary>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public double GetDuration(string address,string city,string department)
        {
            Location location2 = this.client.GetLocation(address,city,department);
            Distance distance = this.client.GetDistance(this.location,location2);
            return distance.TravelDuration;
        }
    }
}