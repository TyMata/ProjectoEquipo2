using System;

namespace ClassLibrary
{
    public class LocationApiAdapter : ILocationApiAdapter
    {
        //private LocationApiClient client;
        
        public Location GetLocation(string address,string ciudad, string departamento)
        {
            // Location(api) locA = client.GetLocation(address, city, department);
            // if (locA.Found)
            // {
            //     Location nuevaLocation = new Location(locA.FormattedAddress, locA.Latitude, locA.Longitude);
            //     return nuevaLocation;
            // }
            return null; //Cambiar null por excepción

            
        }
        public double GetDistance(Location from,Location to)
        {
            // Distance(api) disA = client.GetDistance(from, to);
            // if (disA.Found)
            // {
            //     return disA.TravelDistance;
            // }
            return -1; //Cambiar -1 por excepción
        }

        public double GetTravelTime(Location from, Location to)
        {
            // Distance(api) disA = client.GetDistance(from, to);
            // if (disA.Found)
            // {
            //     return disA.TravelTime;   //tiempo en minutos
            // }
            return -1; //Cambiar -1 por excepción
        }
    }
}