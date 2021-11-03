

namespace ClassLibrary
{
    public class Location 
    {
        public bool Found { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public Location(bool found, string address, double latitude, double longitude)
        {
            this.Found = found;
            this.Address = address;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}