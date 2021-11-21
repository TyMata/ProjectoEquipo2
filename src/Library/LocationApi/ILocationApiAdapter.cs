using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{   
    /// <summary>
    /// Interface para el adapter de las api de ubicacion
    /// </summary>
    public interface ILocationApiAdapter
    {   
        /// <summary>
        /// Transforma la Location de la Api a una Location del Bot
        /// </summary>
        /// <param name="address"></param>
        /// <param name="ciudad"></param>
        /// <param name="departamento"></param>
        /// <returns></returns>
        LocationAdapter GetLocation(string address,string ciudad, string departamento);
        /// <summary>
        /// Devuelve la distancia entre dos Locations de la Api
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <returns></returns>
        IDistanceResult GetDistance(string fromAddress, string toAddress);
    }
}