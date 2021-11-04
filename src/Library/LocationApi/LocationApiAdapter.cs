// using System;
// using Ucu.Poo.Locations.Client;

// namespace ClassLibrary
// {   
//     /// <summary>
//     /// Esta clase representa un adaptador de una Api de ubicaciones
//     /// </summary>
//     public class LocationApiAdapter : ILocationApiAdapter
//     {
//         private LocationApiClient client;
//         /// <summary>
//         /// Transforma la Location de la Api a una Location del Bot
//         /// </summary>
//         /// <param name="address"></param>
//         /// <param name="city"></param>
//         /// <param name="department"></param>
//         /// <returns></returns>
        
//         public Location GetLocation(string address,string city, string department)
//         {
//             Location locA = ;
//             if (client.GetLocationAsync(address, city, department).Found)
//             {
//                 Location nuevaLocation = new Location(locA.FormattedAddress, locA.Latitude, locA.Longitude);
//                 return nuevaLocation;
//             }
//             return null; //Cambiar null por excepción

            
//         }
//         /// <summary>
//         /// Devuelve la distancia entre dos Locations 
//         /// </summary>
//         /// <param name="from"></param>
//         /// <param name="to"></param>
//         /// <returns></returns>
//         public double GetDistance(Location from,Location to)
//         {
//             // Distance(api) disA = client.GetDistance(from, to);
//             // if (disA.Found)
//             // {
//             //     return disA.TravelDistance;
//             // }
//             return -1; //Cambiar -1 por excepción
//         }
//         /// <summary>
//         /// Devuelve el tiempo que se tarda en llegar de una Location a otra
//         /// </summary>
//         /// <param name="from"></param>
//         /// <param name="to"></param>
//         /// <returns></returns>
//         public double GetTravelTime(Location from, Location to)
//         {
//             // Distance(api) disA = client.GetDistance(from, to);
//             // if (disA.Found)
//             // {
//             //     return disA.TravelTime;   //tiempo en minutos
//             // }
//             return -1; //Cambiar -1 por excepción
//         }
//     }
// }