using System;

namespace ClassLibrary
{
    public interface ILocationApiAdapter
    {
        Location GetLocation(string addres,string ciudad, string departamento);
        int GetDistance(Location ToLocation,Location FromLocation);
    }
}