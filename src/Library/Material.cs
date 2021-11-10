using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa un material
    /// </summary>
    public class Material
    {   
        private string name;
        /// <summary>
        /// Devuelve el tipo de objeto
        /// </summary>
        /// <value></value>
        public string Name{
            get
            {
                return this.name;
            }
            private set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.name = value;
                }
                else
                {
                    //EXCEPCION DE NOMBRE VACIO O NULO
                }
            }
        }
        private string type;
        /// <summary>
        /// Devuelve el tipo de material
        /// </summary>
        /// <value></value>
        public string Type{
            get
            {
                return this.type;
            }
            private set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.type = value;
                }
                else
                {
                    //EXCEPCION DE NOMBRE VACIO O NULO
                }
            }
        }
        private string classification;
        /// <summary>
        /// Devuelve la clasificacion del material
        /// </summary>
        /// <value></value>
        public string Classification{
            get
            {
                return this.classification;
            }
            private set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.classification = value;
                }
                else
                {
                    //EXCEPCION DE NOMBRE VACIO O NULO
                }
            }
        }

        /// <summary>
        /// Constructor de objetos Material
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="classification"></param>
        public Material (string name, string type ,string classification )
        {
            this.Name = name;
            this.Type=type;
            this.Classification=classification;
        }
    }
}