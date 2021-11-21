using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Una interfaz que define una abstracción para el resultado de calcular distancias.
    /// </summary>
    public interface IDistanceResult
    {
        /// <summary>
        /// Obtiene un valor que indica si la dirección de origen para el cálculo de distancias existe; sólo se puede
        /// calcular la distancia entre direcciones que existen.
        /// </summary>
        bool FromExists { get; }

        /// <summary>
        /// Obtiene un valor que indica si la dirección de destino para el cálculo de distancias existe; sólo se puede
        /// calcular la distancia entre direcciones que existen.
        /// </summary>
        bool ToExists { get; }

        /// <summary>
        /// La distancia calculada.
        /// </summary>
        double Distance { get; }

        /// <summary>
        /// El tiempo en llegar del origen al destino.
        /// </summary>
        double Time { get; }
    }

    
    /// <summary>
    /// Una implementación concreta del resutlado de calcular distancias. Además de las propiedades definidas en
    /// IDistanceResult esta clase agrega propiedades para acceder a las coordenadas de las direcciones buscadas.
    /// </summary>
    public class DistanceResult : IDistanceResult
    {
        private Location from;
        private Location to;
        private double distance;
        private double time;

        /// <summary>
        /// Inicializa una nueva instancia de DistanceResult a partir de dos coordenadas, la distancia y el tiempo
        /// entre ellas.
        /// </summary>
        /// <param name="from">Las coordenadas de origen.</param>
        /// <param name="to">Las coordenadas de destino.</param>
        /// <param name="distance">La distancia entre las coordenadas.</param>
        /// <param name="time">El tiempo que se demora en llegar del origen al destino.</param>
        public DistanceResult(Location from, Location to, double distance, double time)
        {
            this.from = from;
            this.to = to;
            this.distance = distance;
            this.time = time;
        }

        public bool FromExists
        {
            get
            {
                return this.from.Found;
            }
        }

        public bool ToExists
        {
            get
            {
                return this.to.Found;
            }
        }

        public double Distance
        {
            get
            {
                return this.distance;
            }
        }

        public double Time
        {
            get
            {
                return this.time;
            }
        }

        public Location From
        {
            get
            {
                return this.from;
            }
        }

        public Location To
        {
            get
            {
                return this.to;
            }
        }
    }
}