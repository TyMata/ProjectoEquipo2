using System;

namespace ClassLibrary
{
    public interface ILocationApiAdapter
    {
        Location GetLocation(string addres,string ciudad, string departamento);
        double GetDistance(Location from,Location to);
        double GetTravelTime(Location from, Location to);
    }
}