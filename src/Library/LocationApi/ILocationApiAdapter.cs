
// namespace ClassLibrary
// {   
//     /// <summary>
//     /// Interface para el adapter de las api de ubicacion
//     /// </summary>
//     public interface ILocationApiAdapter
//     {   
//         /// <summary>
//         /// Transforma la Location de la Api a una Location del Bot
//         /// </summary>
//         /// <param name="address"></param>
//         /// <param name="ciudad"></param>
//         /// <param name="departamento"></param>
//         /// <returns></returns>
//         Location GetLocation(string address,string ciudad, string departamento);
//         /// <summary>
//         /// Devuelve la distancia entre dos Locations de la Api
//         /// </summary>
//         /// <param name="from"></param>
//         /// <param name="to"></param>
//         /// <returns></returns>
//         double GetDistance(Location from,Location to);
//         /// <summary>
//         /// Devuelve el tiempo que se tarda en llegar de una Location a otra
//         /// </summary>
//         /// <param name="from"></param>
//         /// <param name="to"></param>
//         /// <returns></returns>
//         double GetTravelTime(Location from, Location to);
//     }
// }