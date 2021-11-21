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
        /// Devuelve la direccion completa
        /// </summary>
        /// <value></value>
        public string Address { get; set; }
        /// <summary>
        /// Devuelve la latitud de la ubicacion
        /// </summary>
        /// <value></value>
        public double Latitude { get; set; }
        /// <summary>
        /// Devuelve la longitud de la ubicacion
        /// </summary>
        /// <value></value>
        public double Longitude { get; set; }

       /// <summary>
       /// Adaptador del objeto location de la api
       /// </summary>
       /// <param name="location"></param>
        public LocationAdapter(Location location)
        {
            this.Found = location.Found;
            this.Address = location.FormattedAddress;
            this.Latitude = location.Latitude;
            this.Longitude = location.Longitude;
        }
    }
}