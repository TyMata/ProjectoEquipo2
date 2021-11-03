using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa un material
    /// </summary>
    public class Material
    {   
        /// <summary>
        /// Devuelve el tipo de objeto
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// Devuelve el tipo de material
        /// </summary>
        /// <value></value>
        public string Type { get; set; }
        /// <summary>
        /// Devuelve la clasificacion del material
        /// </summary>
        /// <value></value>
        public string Classification { get; set; }

        /// <summary>
        /// Constructor de objetos Material
        /// </summary>
        /// <param name="type"></param>
        /// <param name="classification"></param>
        public Material (string type ,string classification )
        {
            this.Type=type;
            this.Classification=classification;
        }

    }
}