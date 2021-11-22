using System;
using Ucu.Poo.Locations.Client;
using Nito.AsyncEx;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un adaptador de una Api de ubicaciones
    /// </summary>
    public class LocationApiAdapter : ILocationApiAdapter
    {
        private static LocationApiAdapter instance;
        public static LocationApiAdapter Instance
        {
             get{
                if (instance == null)
                {
                    instance = new LocationApiAdapter();
                }

                return instance;
            }
        }
        private LocationApiClient client = new LocationApiClient();
        
        /// <summary>
        /// Transforma la Location de la Api a una Location del Bot
        /// </summary>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public LocationAdapter GetLocation(string address,string city, string department)
        {
            Location location = AsyncContext.Run(() => client.GetLocationAsync(address, city, department));
            LocationAdapter result = new LocationAdapter(location);
            return result;
        }

        /// <summary>
        /// Devuelve las distancia entre dos direcciones
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <returns></returns>
        public IDistanceResult GetDistance(string fromAddress, string toAddress)
        {
            Location fromLocation = AsyncContext. Run(() => client.GetLocationAsync(fromAddress, "montevideo", "montevideo")); //TODO cambiar a location nuestra y conseguir de ahi los datos???
            Location toLocation = AsyncContext. Run(() => client.GetLocationAsync(toAddress, "montevideo", "montevideo"));
            Distance distance = AsyncContext. Run(() => client.GetDistanceAsync(fromLocation, toLocation));

            DistanceResult result = new DistanceResult(fromLocation, toLocation, distance.TravelDistance, distance.TravelDuration);

            return result;
        }
    }       
}